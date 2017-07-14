/*
 *	Module: 		DataTrace.cs
 *	Author: 		Richard K. Chandos
 *	Date:			20030502
 *	Description:	Show the data file breakdown against
 *                  the COBOL copylib layout.
 *	Copyright:		© Richard K. Chandos 2003
 */

using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Windows.Forms;

namespace CParse
{
    public partial class DataTrace : Form
	{
		#region > Private Data <
        /// <summary>
        /// The current record index.
        /// </summary>
        private Int32 _CurrRecord;
        
        /// <summary>
        /// The display table driver.
        /// </summary>
        private DataTable _DataDisplay;
        
        /// <summary>
        /// The data file name.
        /// </summary>
        private string _DataFile;
        
        /// <summary>
        /// Our file stream object.
        /// </summary>
        private FileStream _FS;

        /// <summary>
        /// List of highlighted entries.
        /// </summary>
        private List<Hilight> _Hilights;

        /// <summary>
        /// Regular expression to determine if the input data is numeric.
        /// </summary>
        private Regex _IsNumeric;

        /// <summary>
        /// Regular expression to determine if the input data is signed numeric (coded).
        /// </summary>
        private Regex _IsSignedNumeric;
        
        /// <summary>
        /// Layout of the record.
        /// </summary>
        private DataTable _Layout;

        /// <summary>
        /// Holder for non-streamed input.
        /// </summary>
        private string[] _NonStreamInput;

        /// <summary>
        /// Total length of the record (data only, no record terminator).
        /// </summary>
        private Int32 _RecordLength;

        /// <summary>
        /// Current source record.
        /// </summary>
        private string _SourceRecord;
        
        /// <summary>
        /// Length of the record terminator.
        /// </summary>
        private Int32 _TermLength;

        /// <summary>
        /// Use line oriented input.
        /// </summary>
        private bool _UseLineInput;
		#endregion

		#region > Constructors / Destructors <
        /// <summary>
        /// Instantiate object, set events.
        /// </summary>
		public DataTrace()
		{
			InitializeComponent();
			_IsNumeric = new Regex(@"^[+-]?\d*$", RegexOptions.Compiled);
            _IsSignedNumeric = new Regex(@"^\d*[{ABCDEFGHI}JKLMNOPQR]{1}$", RegexOptions.Compiled);
			dgDataTrace.Font = new Font("System", 12);
			dgDataTrace.AutoGenerateColumns = false;
            this.Load += new EventHandler(DataTrace_Load);
            this.FormClosing += new FormClosingEventHandler(DataTrace_FormClosing);
            _UseLineInput = false;
            lblRowBuffer.Visible = _UseLineInput;
        }

        /// <summary>
        /// Instantiate object, set event and read type.
        /// </summary>
        /// <param name="UseLineOrientedRead">true for line oriented operations, false for stream oriented operations.</param>
        public DataTrace(bool UseLineOrientedRead) : this()
        {
            _UseLineInput = UseLineOrientedRead;
            lblRowBuffer.Visible = _UseLineInput;
        }
        #endregion

        #region > Event Handlers <
        /// <summary>
        /// Close the program.
        /// </summary>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Reload the specified record.
        /// </summary>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            JumpToRecord(Convert.ToInt32(udJump.Value));
        }

        /// <summary>
        /// Form closing event handler.  This is where we release the input file.
        /// </summary>
        void DataTrace_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_FS != null)
                _FS.Close();
            _FS = null;
        }

        /// <summary>
        /// Update the display if the record index has changed.
        /// </summary>
        private void udJump_ValueChanged(object sender, EventArgs e)
        {
            if (udJump.Value > udJump.Maximum)
                udJump.Value = udJump.Maximum;
            if (udJump.Value < udJump.Minimum)
                udJump.Value = udJump.Minimum;
            JumpToRecord(Convert.ToInt32(udJump.Value));
        }

        /// <summary>
        /// Form load event handler.
        /// </summary>
        /// <remarks>
        /// This event method handles setting the highlighting on the first record.
        /// </remarks>
        private void DataTrace_Load(object sender, EventArgs e)
        {
            SetHighlights();
        }
        #endregion

		#region > Public Properties <
        /// <summary>
        /// Set the Data Layout.
        /// </summary>
		public DataTable DataLayout
		{
			set 
			{ 
				_Layout = value;
				SetDataDisplay();
			}
		}

        /// <summary>
        /// Set the Data File.
        /// </summary>
		public string DataFile
		{
			set 
			{
				_DataFile = value;
                if (_UseLineInput)
                {
                    _NonStreamInput = File.ReadAllLines(_DataFile);
                    udJump.Maximum = _NonStreamInput.Length;
                }
                else
                {
                    _FS = new FileStream(_DataFile, FileMode.Open);
                    GetRecordTerminator();
                    udJump.Maximum = _FS.Length / (_RecordLength + _TermLength);
                }
                _CurrRecord = 1;
                udJump.Minimum = 1;
                JumpToRecord(_CurrRecord);
			}
		}
		#endregion

        #region > Public Methods <
        /// <summary>
        /// Set the highlighting.
        /// </summary>
        public void SetHighlights()
        {
            // Don't bother if we're not visible
            if (!this.Visible)
                return;

            foreach (Hilight h in _Hilights)
            {
                if (h.Row88Index > (-1))
                    dgDataTrace.Rows[h.Row88Index].Cells["Value"].Style.BackColor = h.Highlight;
                if (h.RowDataIndex > (-1))
                    dgDataTrace.Rows[h.RowDataIndex].Cells["Data"].Style.BackColor = h.Highlight;
            }

            // Set the current record message
            // -- We do this here so that we're accurate for the first record
            lblCurRecMsg.Text = string.Format("Record {0:N0} of {1:N0}", Convert.ToInt32(udJump.Value), Convert.ToInt32(udJump.Maximum));
            lblCurRecMsg.Left = (((btnRefresh.Left + btnRefresh.Width) + btnClose.Left) / 2) - (lblCurRecMsg.Width / 2);
        }
        #endregion

        #region > Private Methods <
        /// <summary>
        /// Get the record terminator character(s).
        /// </summary>
        private void GetRecordTerminator()
        {
            byte[] src = new byte[_RecordLength];
            byte[] term = new byte[2];
            
            // Pick up the first record
            _FS.Read(src, 0, _RecordLength);

            // Now pick up a couple characters after the record
            _FS.Read(term, 0, 2);

            // Now see if we have CR or LF characters
            _TermLength = 0;
            if (term[0] == 13 || term[0] == 10)
                _TermLength++;
            if (term[1] == 13 || term[1] == 10)
                _TermLength++;
        }

        /// <summary>
        /// Load a record into the display.
        /// </summary>
        /// <param name="Record">Record to load.</param>
        private void LoadDataDisplay(string Record)
        {
            int fldCount = 0;
            string fld = string.Empty;
            int start = 0;
            int length = 0;
            int precision = 0;
            int lastnon88row = 0;
            int level = 0;
            string lastnon88value = string.Empty;

            // New list of highlighting
            _Hilights = new List<Hilight>();

            // Spin the rows
            for(int rcnt = 0; rcnt < _Layout.Rows.Count; rcnt++ )
            {
                DataRow aRow = _Layout.Rows[rcnt];
                DataRow rRow = _DataDisplay.Rows[fldCount];
                level = Convert.ToInt32(aRow["Level"]);
                start = Convert.ToInt32(aRow["Offset"]);
                length = Convert.ToInt32(aRow["FieldLength"]);
                precision = Convert.ToInt32(aRow["FieldPrecision"]);
                if (length > 0)
                {
                    if (Record.Length < (start + length))
                        fld = string.Empty;
                    else
                        fld = Record.Substring(start, length);

                    // 88 level tracking
                    lastnon88row = rcnt;
                    lastnon88value = fld.Trim();

                    if (Convert.ToBoolean(aRow["Numeric"]))
                    {
                        if (Convert.ToBoolean(aRow["Signed"]))
                        {
                            if (!_IsSignedNumeric.IsMatch(fld))
                                _Hilights.Add(new Hilight(-1, rcnt, Color.Tomato));
                        }
                        else
                        {
                            if (!_IsNumeric.IsMatch(fld))
                                _Hilights.Add(new Hilight(-1, rcnt, Color.Tomato));
                        }
                    }
                    rRow["Data"] = fld;

                    _DataDisplay.AcceptChanges();
                }
                else
                {
                    // Check for matching 88's
                    if (level == 88)
                        if (aRow["Value"].ToString().Trim() == lastnon88value)
                            _Hilights.Add(new Hilight(rcnt, lastnon88row, Color.LawnGreen));
                }
                fldCount++;
            }
        }

        /// <summary>
        /// Jump to the specified record.
        /// </summary>
        /// <param name="RecId">Record index.</param>
        private void JumpToRecord(Int32 RecId)
        {
            if (_UseLineInput)
            {
                _SourceRecord = _NonStreamInput[RecId - 1];
            }
            else
            {
                _FS.Seek(((RecId - 1) * (_RecordLength + _TermLength)), SeekOrigin.Begin);
                byte[] src = new byte[_RecordLength];
                byte[] toss = new byte[_TermLength];
                _FS.Read(src, 0, _RecordLength);
                _FS.Read(toss, 0, _TermLength);
                _SourceRecord = Encoding.ASCII.GetString(src);
            }

            LoadDataDisplay(_SourceRecord);
            DataView dv = _DataDisplay.DefaultView;
            dv.AllowDelete = false;
            dv.AllowEdit = false;
            dv.AllowNew = false;
            dgDataTrace.DataSource = dv;

            // Set the highlights
            SetHighlights();
        }

        /// <summary>
        /// Set up the display of the data.
        /// </summary>
		private void SetDataDisplay()
		{
			// Set the display DataTable
			_DataDisplay = new DataTable("DataDisplay");
			DataColumn dc = new DataColumn("Level", typeof(string));
			_DataDisplay.Columns.Add(dc);
			dc = new DataColumn("DataName", typeof(string));
			_DataDisplay.Columns.Add(dc);
			dc = new DataColumn("Picture", typeof(string));
			_DataDisplay.Columns.Add(dc);
            dc = new DataColumn("Value", typeof(string));
            _DataDisplay.Columns.Add(dc);
			dc = new DataColumn("Data", typeof(string));
			_DataDisplay.Columns.Add(dc);

			// Set the titles
			foreach (DataRow aRow in _Layout.Rows)
			{
				DataRow bRow = _DataDisplay.NewRow();
                bRow["Level"] = string.Format("{0:00}", Convert.ToInt32(aRow["Level"]));
				bRow["DataName"] = aRow["DataName"].ToString();
				bRow["Picture"] = aRow["Picture"].ToString();
                if (aRow["Level"].ToString() == "88")
                    bRow["Value"] = aRow["Value"].ToString();
                else
                    bRow["Value"] = string.Empty;
				bRow["Data"] = string.Empty;
				_DataDisplay.Rows.Add(bRow);
				_RecordLength = Convert.ToInt32(aRow["Offset"]) + Convert.ToInt32(aRow["FieldLength"]);
			}
			_DataDisplay.AcceptChanges();
		}
		#endregion
    }

    #region >>> Internal Class <<<
    /// <summary>
    /// Keep track of hilighting.
    /// </summary>
    class Hilight
    {
        #region > Private Data <
        /// <summary>
        /// 88 row index.
        /// </summary>
        private int _row88index;

        /// <summary>
        /// Data row index.
        /// </summary>
        private int _rowdataindex;

        /// <summary>
        /// Highlighting color.
        /// </summary>
        private Color _Hilight;
        #endregion

        #region > Constructors <
        /// <summary>
        /// Instantiate object.
        /// </summary>
        public Hilight()
        {
            // Nothing to do.
        }

        /// <summary>
        /// Instantiate object, set properties.
        /// </summary>
        /// <param name="Row88Idx">88 level row index.</param>
        /// <param name="RowDataIdx">Data row index.</param>
        /// <param name="Hilight">Highlight color.</param>
        public Hilight(int Row88Idx, int RowDataIdx, Color Hilight)
        {
            this.Row88Index = Row88Idx;
            this.RowDataIndex = RowDataIdx;
            this.Highlight= Hilight;
        }
        #endregion

        #region > Public Properties <
        /// <summary>
        /// Get or Set the 88 level row index.
        /// </summary>
        public int Row88Index
        {
            get { return _row88index; }
            set { _row88index = value; }
        }

        /// <summary>
        /// Get or Set the data row index.
        /// </summary>
        public int RowDataIndex
        {
            get { return _rowdataindex; }
            set { _rowdataindex = value; }
        }

        /// <summary>
        /// Get or Set the highlighting color.
        /// </summary>
        public Color Highlight
        {
            get { return _Hilight; }
            set { _Hilight = value; }
        }
        #endregion
    }
    #endregion
}
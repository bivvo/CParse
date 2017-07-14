/*
 *	Module: 		frmRequest.cs
 *	Author: 		Richard K. Chandos
 *	Date:			20030502
 *	Description:	Startup form to display data layout
 *					and data file.
 *	Copyright:		© Richard K. Chandos 2003
 */

using System;
using System.Windows.Forms;

namespace CParse
{
    /// <summary>
    /// Capture the layout and data file that are to be displayed.
    /// </summary>
	public partial class frmCParse : Form
    {
        #region > Constructor <
        /// <summary>
        /// Instantiate object.
        /// </summary>
        public frmCParse()
		{
			InitializeComponent();
        }
        #endregion

        #region > Private Methods <
        /// <summary>
        /// Process the user selection.
        /// </summary>
        private void ProcessSelection()
		{
			COBOLParse cp = new COBOLParse();
			if (cp.ParseInclude(tbLayout.Text.Trim()))
			{
				if (cbShowData.Checked)
				{
					DataShow ds = new DataShow();
					ds.MyData = cp.Results;
					ds.ShowDialog(this);
				}

				DataTrace dt = new DataTrace(cbLineBuffer.Checked);
				dt.DataLayout = cp.Results;
				dt.DataFile = tbData.Text.Trim();
				dt.ShowDialog(this);
			}
			else
				MessageBox.Show(this, "Failed.");
		}
		#endregion

		#region > Event Handlers <
        /// <summary>
        /// Layout button click event handler.
        /// </summary>
		private void btnLayout_Click(object sender, EventArgs e)
		{
			if (ofdLayout.ShowDialog(this) == DialogResult.OK)
				tbLayout.Text = ofdLayout.FileName;
		}

        /// <summary>
        /// Go button click event handler.
        /// </summary>
		private void btnGo_Click(object sender, EventArgs e)
		{
			tbLayout.Enabled = false;
			btnLayout.Enabled = false;
			btnGo.Enabled = false;
			ProcessSelection();
			btnGo.Enabled = true;
			btnLayout.Enabled = true;
			tbLayout.Enabled = true;
		}

        /// <summary>
        /// Layout textbox value changed event handler.
        /// </summary>
		private void tbLayout_TextChanged(object sender, EventArgs e)
		{
			CheckForGo();
		}

        /// <summary>
        /// Data textbox value changed event handler.
        /// </summary>
		private void tbData_TextChanged(object sender, EventArgs e)
		{
			CheckForGo();
		}

        /// <summary>
        /// Data button click event handler.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void btnData_Click(object sender, EventArgs e)
		{
			if (ofdData.ShowDialog(this) == DialogResult.OK)
				tbData.Text = ofdData.FileName;
		}
        #endregion

        #region > Private Methods <
        /// <summary>
        /// Verify that the user is permitted to click Go.
        /// </summary>
        private void CheckForGo()
		{
			if (tbLayout.Text.Trim().Length > 0 && tbData.Text.Trim().Length > 0)
				btnGo.Enabled = true;
			else
				btnGo.Enabled = false;
		}
		#endregion
	}
}
/*
 *	Module: 		COBOLParse.cs
 *	Author: 		Richard K. Chandos
 *	Date:			20030502
 *	Description:	Parse a COBOL copycode.
 *	Copyright:		© Richard K. Chandos 2003
 */

using System;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Data;

namespace CParse
{
    /// <summary>
    /// Encapsulate the parsing of a COBOL copycode.
    /// </summary>
	public class COBOLParse
	{
		#region > Private Data <
		/// <summary>
		/// DataTable to hold the parsed structure.
		/// </summary>
		private DataTable _Parsed;

		/// <summary>
		/// Regular expression to kill tab characters.
		/// </summary>
		private Regex _KillTab;

		/// <summary>
		/// Regular expression to determine a numeric field definition.
		/// </summary>
		private Regex _IsNumeric;
		#endregion

		#region > Constructor <
		/// <summary>
		/// Constructor
		/// </summary>
		public COBOLParse()
		{
			// Build the DataTable
			InitDataTable();

			// Build a couple of Regular Expressions
			_KillTab = new Regex("\t", RegexOptions.Compiled);
			_IsNumeric = new Regex(@"^[S]?[Z9]*(\(\d*\))?[V]?9*(\(\d*\))?$");
		}
		#endregion

		#region > Public Methods <
		/// <summary>
		/// Parse the COBOL include file.
		/// </summary>
		/// <param name="FileName">File to parse.</param>
		/// <returns>true on success, false on failure.</returns>
		public bool ParseInclude(string FileName)
		{
			Int32 recId = 0;
			Int32 level;
			string dataName;
			string pictureClause;
			string valueClause;
			bool numeric;
			Int32 fieldLength;
			Int32 fieldPrecision;
			bool signed;
			Int32 offset = 0;

			// Open the data layout
			StreamReader sr = new StreamReader(FileName);
			string inBuff = sr.ReadToEnd();
			string[] lineBoundary = { "\r\n" };
			string[] lineIn = inBuff.Split(lineBoundary, StringSplitOptions.RemoveEmptyEntries);
			string[] lParms;
			for( int z = 0; z < lineIn.Length; z++ )
			{
				recId++;

                // Ignore comments
                if(lineIn[z].Substring(6, 1) != " ")
                    continue;

				// Initialize
				level = 0;
				dataName = string.Empty;
				pictureClause = string.Empty;
				valueClause = string.Empty;
				numeric = false;
				fieldLength = 0;
				fieldPrecision = 0;
				signed = false;

				// Input buffer fixups
				lineIn[z] = lineIn[z].Trim();
				lineIn[z] = _KillTab.Replace(lineIn[z], " ");
				if (lineIn[z].Substring(lineIn[z].Length - 1) == ".")
					lineIn[z] = lineIn[z].Substring(0, lineIn[z].Length - 1);
				string[] boundary = { " " };
				lParms = lineIn[z].Split(boundary, StringSplitOptions.RemoveEmptyEntries);

				// Spin the array
				for (int n = 0; n < lParms.Length; n++)
				{
					if (n == 0)
					{
						level = Convert.ToInt32(lParms[n]);
						if (level < 50)
							offset += LastFieldLength();
					}
					if( n == 1 )
						dataName = lParms[n];
					if (n > 1)
					{
						if (lParms[n].Length > 2)
							if (lParms[n].Substring(0, 3).ToUpper() == "PIC")
								pictureClause = lParms[n + 1];
						if (lParms[n].ToUpper() == "VALUE")
						{
                            StringBuilder vlu = new StringBuilder();
                            for (int ni = (n + 1); ni < lParms.Length; ni++)
                            {
                                if (vlu.Length > 0)
                                    vlu.Append(" ");
                                vlu.Append(lParms[ni]);
                            }
							valueClause = vlu.ToString().Replace("'", "");
						}
						if (lParms[n].ToUpper() == "REDEFINES")
							offset = RetrieveOffset(lParms[n + 1]);
					}
				}

				// Process the picture clause
				if (pictureClause.Length > 0)
				{
					// See if it's a numeric picture
					if (_IsNumeric.IsMatch(pictureClause))
					{
						numeric = true;
						fieldPrecision = GetPrecision(pictureClause);
					}
					fieldLength = GetFullLength(pictureClause);
					if (fieldLength < 0)
					{
						signed = true;
						fieldLength *= -1;
					}
				}

				// Add it to the DataTable
				AddRow(recId, level, dataName, pictureClause, valueClause, numeric, fieldLength, fieldPrecision, signed, offset);
			}
			return true;
		}
		#endregion

		#region > Private Methods <
		/// <summary>
		/// Initialize the DataTable structure.
		/// </summary>
		private void InitDataTable()
		{
            _Parsed = new DataTable("ParsedStructure");
            DataColumn DC = new DataColumn("RecId", typeof(Int32));
            _Parsed.Columns.Add(DC);
            DC = new DataColumn("Level", typeof(Int32));
            _Parsed.Columns.Add(DC);
            DC = new DataColumn("DataName", typeof(string));
            _Parsed.Columns.Add(DC);
            DC = new DataColumn("Picture", typeof(string));
            _Parsed.Columns.Add(DC);
            DC = new DataColumn("Value", typeof(string));
            _Parsed.Columns.Add(DC);
            DC = new DataColumn("Numeric", typeof(bool));
            _Parsed.Columns.Add(DC);
            DC = new DataColumn("FieldLength", typeof(Int32));
            _Parsed.Columns.Add(DC);
            DC = new DataColumn("FieldPrecision", typeof(Int32));
            _Parsed.Columns.Add(DC);
            DC = new DataColumn("Signed", typeof(bool));
            _Parsed.Columns.Add(DC);
            DC = new DataColumn("Offset", typeof(Int32));
            _Parsed.Columns.Add(DC);
		}

		/// <summary>
		/// Add a row to the DataTable.
		/// </summary>
        private void AddRow(Int32 Rec, Int32 Level, string DName, string Pic, string Value, bool Num, Int32 Length, Int32 Precision, bool Signed, Int32 Offset)
        {
            _Parsed.Rows.Add(new object[] { Rec, Level, DName, Pic, Value, Num, Length, Precision, Signed, Offset });
            _Parsed.AcceptChanges();
        }

		/// <summary>
		/// Compute the length of the COBOL defined data element.
		/// </summary>
		/// <param name="sourcePicture">PICTURE clause to evaluate.</param>
		/// <returns>An integer indicating the total length of the field.</returns>
		private int GetFullLength(string sourcePicture)
		{
			int part = 0;
			int ttl = 0;
			int ttlMult = 1;

			if (sourcePicture.Length > 0)
				if (sourcePicture.Substring(0, 1) == "S")
					ttlMult = -1;

			for (int n = 0; n < sourcePicture.Length; n++)
			{
				switch( sourcePicture.Substring(n,1) )
				{
					case "(":
						// Find the closing parenthesis
						int m = sourcePicture.IndexOf(")", n);

						// Compute the number between the parenthesis
						part = Convert.ToInt32(sourcePicture.Substring(n + 1, m - n - 1));

						// Add it to the total
						ttl += part;

						// Reset the partial
						part = 0;

						// Skip to the next part of the string
						n = m;
						break;
					case "S":
                        if (!sourcePicture.Contains("("))
                            ttl++;
						break;
					case "V":
						break;
                    case ".":
                        ttl++;
                        break;
					default:
						part++;
						break;
				}
			}

			// Add the last partial to the total
			ttl += part;

			// Multiply to signal signed
			ttl *= ttlMult;

			// Return the total
			return ttl;
		}

		/// <summary>
		/// Retrieve the precision of a numeric declaration.
		/// </summary>
		/// <param name="sourcePicture">PICTURE Clause to evaluate.</param>
		/// <returns>An integer indicating the level of numeric precision.</returns>
		private int GetPrecision(string sourcePicture)
		{
			int n = sourcePicture.IndexOf("V");
			if (n > 0 && n < sourcePicture.Length)
				return GetFullLength(sourcePicture.Substring(n + 1));
			return 0;
		}

		/// <summary>
		/// Retrieve the offset index of the specified data item.
		/// </summary>
		/// <param name="dataElement">The data item to retrieve.</param>
		/// <returns>The offset for the data item or zero if not found.</returns>
		private int RetrieveOffset(string dataElement)
		{
			// Find the dataElement in the DataTable
			foreach (DataRow aRow in _Parsed.Rows)
			{
				if (aRow["DataName"].ToString() == dataElement)
					return Convert.ToInt32(aRow["Offset"]);
			}

			// Only return zero if the dataElement is not found
			return 0;
		}

		/// <summary>
		/// Retrieve the length of the last field written to the DataTable.
		/// </summary>
		/// <returns>The field length or zero.</returns>
		private int LastFieldLength()
		{
			int lastRow = _Parsed.Rows.Count - 1;
			for( int n = lastRow; n >= 0; n-- )
				if( Convert.ToInt32(_Parsed.Rows[n]["Level"]) != 88 )
					return Convert.ToInt32(_Parsed.Rows[n]["FieldLength"]);
			return 0;
		}
		#endregion

		#region > Public Properties <
		/// <summary>
		/// Return the parsed DataTable.
		/// </summary>
		public DataTable Results
		{
			get { return _Parsed.Copy(); }
		}
		#endregion
	}
}

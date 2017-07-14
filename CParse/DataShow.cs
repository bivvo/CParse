/*
 *	Module: 		DataShow.cs
 *	Author: 		Richard K. Chandos
 *	Date:			20030502
 *	Description:	Display of the COBOL copycode layout information.
 *	Copyright:		© Richard K. Chandos 2003
 */

using System.Data;
using System.Windows.Forms;

namespace CParse
{
    public partial class DataShow : Form
	{
		public DataShow()
		{
			InitializeComponent();
		}

		public DataTable MyData
		{
			set { dataGridView1.DataSource = value; }
		}
	}
}
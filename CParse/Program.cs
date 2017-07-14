/*
 *	Module: 		Program.cs
 *	Author: 		Richard K. Chandos
 *	Date:			20030502
 *	Description:	Startup file for COBOL viewer.
 *	Copyright:		© Richard K. Chandos 2003
 */

using System;
using System.Windows.Forms;

namespace CParse
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new frmCParse());
		}
	}
}
/*
 *	Module: 		DataTrace.cs
 *	Author: 		Richard K. Chandos
 *	Date:			20030502
 *	Description:	Show the data file breakdown against
 *                  the COBOL copylib layout.
 *	Copyright:		© Richard K. Chandos 2003
 */

namespace CParse
{
	partial class DataTrace
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblCurRecMsg = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.udJump = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.dgDataTrace = new System.Windows.Forms.DataGridView();
            this.Level = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Picture = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Data = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblRowBuffer = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udJump)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgDataTrace)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblRowBuffer);
            this.panel1.Controls.Add(this.lblCurRecMsg);
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Controls.Add(this.btnRefresh);
            this.panel1.Controls.Add(this.udJump);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(759, 37);
            this.panel1.TabIndex = 0;
            // 
            // lblCurRecMsg
            // 
            this.lblCurRecMsg.AutoSize = true;
            this.lblCurRecMsg.Location = new System.Drawing.Point(463, 9);
            this.lblCurRecMsg.Name = "lblCurRecMsg";
            this.lblCurRecMsg.Size = new System.Drawing.Size(16, 17);
            this.lblCurRecMsg.TabIndex = 4;
            this.lblCurRecMsg.Text = "  ";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(681, 6);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.Location = new System.Drawing.Point(235, 6);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 2;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // udJump
            // 
            this.udJump.Location = new System.Drawing.Point(109, 7);
            this.udJump.Name = "udJump";
            this.udJump.Size = new System.Drawing.Size(120, 23);
            this.udJump.TabIndex = 1;
            this.udJump.ValueChanged += new System.EventHandler(this.udJump_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Jump to record:";
            // 
            // dgDataTrace
            // 
            this.dgDataTrace.AllowUserToAddRows = false;
            this.dgDataTrace.AllowUserToDeleteRows = false;
            this.dgDataTrace.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgDataTrace.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Level,
            this.DataName,
            this.Picture,
            this.Value,
            this.Data});
            this.dgDataTrace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgDataTrace.Location = new System.Drawing.Point(0, 37);
            this.dgDataTrace.Name = "dgDataTrace";
            this.dgDataTrace.ReadOnly = true;
            this.dgDataTrace.Size = new System.Drawing.Size(759, 432);
            this.dgDataTrace.TabIndex = 1;
            // 
            // Level
            // 
            this.Level.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Level.DataPropertyName = "Level";
            this.Level.FillWeight = 8F;
            this.Level.HeaderText = "Level";
            this.Level.Name = "Level";
            this.Level.ReadOnly = true;
            // 
            // DataName
            // 
            this.DataName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.DataName.DataPropertyName = "DataName";
            this.DataName.FillWeight = 50F;
            this.DataName.HeaderText = "Data Name";
            this.DataName.Name = "DataName";
            this.DataName.ReadOnly = true;
            // 
            // Picture
            // 
            this.Picture.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Picture.DataPropertyName = "Picture";
            this.Picture.FillWeight = 20F;
            this.Picture.HeaderText = "Picture";
            this.Picture.Name = "Picture";
            this.Picture.ReadOnly = true;
            // 
            // Value
            // 
            this.Value.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Value.DataPropertyName = "Value";
            this.Value.FillWeight = 20F;
            this.Value.HeaderText = "Value";
            this.Value.Name = "Value";
            this.Value.ReadOnly = true;
            // 
            // Data
            // 
            this.Data.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Data.DataPropertyName = "Data";
            this.Data.FillWeight = 90F;
            this.Data.HeaderText = "Data";
            this.Data.Name = "Data";
            this.Data.ReadOnly = true;
            // 
            // lblRowBuffer
            // 
            this.lblRowBuffer.AutoSize = true;
            this.lblRowBuffer.Location = new System.Drawing.Point(345, 9);
            this.lblRowBuffer.Name = "lblRowBuffer";
            this.lblRowBuffer.Size = new System.Drawing.Size(93, 17);
            this.lblRowBuffer.TabIndex = 5;
            this.lblRowBuffer.Text = "Row Buffered";
            this.lblRowBuffer.Visible = false;
            // 
            // DataTrace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(759, 469);
            this.ControlBox = false;
            this.Controls.Add(this.dgDataTrace);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "DataTrace";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "DataTrace";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udJump)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgDataTrace)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.NumericUpDown udJump;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.DataGridView dgDataTrace;
		private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.DataGridViewTextBoxColumn Level;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Picture;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value;
        private System.Windows.Forms.DataGridViewTextBoxColumn Data;
        private System.Windows.Forms.Label lblCurRecMsg;
        private System.Windows.Forms.Label lblRowBuffer;

	}
}
/*
 *	Module: 		frmRequest.cs
 *	Author: 		Richard K. Chandos
 *	Date:			20030502
 *	Description:	Startup form to display data layout
 *				    and data file.
 *	Copyright:		© Richard K. Chandos 2003
 */

namespace CParse
{
	partial class frmCParse
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
            this.label1 = new System.Windows.Forms.Label();
            this.tbLayout = new System.Windows.Forms.TextBox();
            this.ofdLayout = new System.Windows.Forms.OpenFileDialog();
            this.btnLayout = new System.Windows.Forms.Button();
            this.btnGo = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tbData = new System.Windows.Forms.TextBox();
            this.btnData = new System.Windows.Forms.Button();
            this.cbShowData = new System.Windows.Forms.CheckBox();
            this.ofdData = new System.Windows.Forms.OpenFileDialog();
            this.cbLineBuffer = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 11);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 17);
            this.label1.TabIndex = 7;
            this.label1.Text = "Layout";
            // 
            // tbLayout
            // 
            this.tbLayout.Location = new System.Drawing.Point(74, 8);
            this.tbLayout.Name = "tbLayout";
            this.tbLayout.Size = new System.Drawing.Size(297, 23);
            this.tbLayout.TabIndex = 0;
            this.tbLayout.TextChanged += new System.EventHandler(this.tbLayout_TextChanged);
            // 
            // ofdLayout
            // 
            this.ofdLayout.DefaultExt = "inc";
            this.ofdLayout.Filter = "INC files|*.inc|COB files|*.cob";
            this.ofdLayout.Title = "Select a file layout";
            // 
            // btnLayout
            // 
            this.btnLayout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLayout.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLayout.Location = new System.Drawing.Point(370, 8);
            this.btnLayout.Name = "btnLayout";
            this.btnLayout.Size = new System.Drawing.Size(29, 23);
            this.btnLayout.TabIndex = 1;
            this.btnLayout.Text = "...";
            this.btnLayout.UseVisualStyleBackColor = true;
            this.btnLayout.Click += new System.EventHandler(this.btnLayout_Click);
            // 
            // btnGo
            // 
            this.btnGo.Enabled = false;
            this.btnGo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGo.Location = new System.Drawing.Point(324, 76);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(75, 23);
            this.btnGo.TabIndex = 6;
            this.btnGo.Text = "Go";
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 44);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 17);
            this.label2.TabIndex = 8;
            this.label2.Text = "Data";
            // 
            // tbData
            // 
            this.tbData.Location = new System.Drawing.Point(74, 41);
            this.tbData.Name = "tbData";
            this.tbData.Size = new System.Drawing.Size(297, 23);
            this.tbData.TabIndex = 2;
            this.tbData.TextChanged += new System.EventHandler(this.tbData_TextChanged);
            // 
            // btnData
            // 
            this.btnData.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnData.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnData.Location = new System.Drawing.Point(370, 41);
            this.btnData.Name = "btnData";
            this.btnData.Size = new System.Drawing.Size(29, 23);
            this.btnData.TabIndex = 3;
            this.btnData.Text = "...";
            this.btnData.UseVisualStyleBackColor = true;
            this.btnData.Click += new System.EventHandler(this.btnData_Click);
            // 
            // cbShowData
            // 
            this.cbShowData.AutoSize = true;
            this.cbShowData.Location = new System.Drawing.Point(74, 78);
            this.cbShowData.Name = "cbShowData";
            this.cbShowData.Size = new System.Drawing.Size(130, 21);
            this.cbShowData.TabIndex = 4;
            this.cbShowData.Text = "Show layout info";
            this.cbShowData.UseVisualStyleBackColor = true;
            // 
            // ofdData
            // 
            this.ofdData.DefaultExt = "txt";
            this.ofdData.Filter = "Text files|*.txt|Data files|*.dat|All files|*.*";
            this.ofdData.Title = "Select a data file";
            // 
            // cbLineBuffer
            // 
            this.cbLineBuffer.AutoSize = true;
            this.cbLineBuffer.Location = new System.Drawing.Point(210, 78);
            this.cbLineBuffer.Name = "cbLineBuffer";
            this.cbLineBuffer.Size = new System.Drawing.Size(112, 21);
            this.cbLineBuffer.TabIndex = 5;
            this.cbLineBuffer.Text = "Row Buffered";
            this.cbLineBuffer.UseVisualStyleBackColor = true;
            // 
            // frmCParse
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(423, 111);
            this.Controls.Add(this.cbLineBuffer);
            this.Controls.Add(this.cbShowData);
            this.Controls.Add(this.btnData);
            this.Controls.Add(this.tbData);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnGo);
            this.Controls.Add(this.btnLayout);
            this.Controls.Add(this.tbLayout);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmCParse";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CParse";
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox tbLayout;
		private System.Windows.Forms.OpenFileDialog ofdLayout;
		private System.Windows.Forms.Button btnLayout;
		private System.Windows.Forms.Button btnGo;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox tbData;
		private System.Windows.Forms.Button btnData;
		private System.Windows.Forms.CheckBox cbShowData;
        private System.Windows.Forms.OpenFileDialog ofdData;
        private System.Windows.Forms.CheckBox cbLineBuffer;
	}
}


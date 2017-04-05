namespace General_Desktop_Application.Presentation
{
    partial class Form_004_003
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_004_003));
            this.rpvReport = new Microsoft.Reporting.WinForms.ReportViewer();
            this.btnClose = new System.Windows.Forms.Button();
            this.cboType = new System.Windows.Forms.ComboBox();
            this.pnlFilters = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlFilters.SuspendLayout();
            this.SuspendLayout();
            // 
            // rpvReport
            // 
            this.rpvReport.BackColor = System.Drawing.Color.RosyBrown;
            this.rpvReport.LocalReport.ReportEmbeddedResource = "General_Desktop_Application.Reports.Report_Users_001.rdlc";
            this.rpvReport.Location = new System.Drawing.Point(0, 48);
            this.rpvReport.Name = "rpvReport";
            this.rpvReport.ServerReport.BearerToken = null;
            this.rpvReport.Size = new System.Drawing.Size(992, 392);
            this.rpvReport.TabIndex = 100;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(912, 448);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 200;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // cboType
            // 
            this.cboType.BackColor = System.Drawing.Color.RosyBrown;
            this.cboType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboType.FormattingEnabled = true;
            this.cboType.Items.AddRange(new object[] {
            "Users"});
            this.cboType.Location = new System.Drawing.Point(96, 8);
            this.cboType.Name = "cboType";
            this.cboType.Size = new System.Drawing.Size(248, 21);
            this.cboType.TabIndex = 1;
            this.cboType.SelectedIndexChanged += new System.EventHandler(this.cboState_SelectedIndexChanged);
            // 
            // pnlFilters
            // 
            this.pnlFilters.BackColor = System.Drawing.Color.White;
            this.pnlFilters.Controls.Add(this.cboType);
            this.pnlFilters.Controls.Add(this.label1);
            this.pnlFilters.Location = new System.Drawing.Point(8, 8);
            this.pnlFilters.Name = "pnlFilters";
            this.pnlFilters.Size = new System.Drawing.Size(976, 40);
            this.pnlFilters.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Report type:";
            // 
            // Form_004_003
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(991, 479);
            this.Controls.Add(this.pnlFilters);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.rpvReport);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form_004_003";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reports";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_004_003_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form_004_003_FormClosed);
            this.Load += new System.EventHandler(this.Form_004_003_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Form_004_003_KeyPress);
            this.pnlFilters.ResumeLayout(false);
            this.pnlFilters.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer rpvReport;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ComboBox cboType;
        private System.Windows.Forms.Panel pnlFilters;
        private System.Windows.Forms.Label label1;
    }
}
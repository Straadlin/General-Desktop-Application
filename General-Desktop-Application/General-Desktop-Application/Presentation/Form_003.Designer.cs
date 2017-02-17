namespace General_Desktop_Application.Presentation
{
    partial class Form_003
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_003));
            this.btnBack = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.mskIP = new System.Windows.Forms.MaskedTextBox();
            this.btnTestSave = new System.Windows.Forms.Button();
            this.nupdPort = new System.Windows.Forms.NumericUpDown();
            this.lblPort = new System.Windows.Forms.Label();
            this.lblIP = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.rbtLocal = new System.Windows.Forms.RadioButton();
            this.rbtRemote = new System.Windows.Forms.RadioButton();
            this.cboInstanceConnection = new System.Windows.Forms.ComboBox();
            this.lblInstance = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnInstall = new System.Windows.Forms.Button();
            this.rbtOnly = new System.Windows.Forms.RadioButton();
            this.rbtManual = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.rbtAutomatic = new System.Windows.Forms.RadioButton();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.btnBackUp = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnRestore = new System.Windows.Forms.Button();
            this.btnCreate = new System.Windows.Forms.Button();
            this.cboInstanceMaintenance = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nupdPort)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(240, 176);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(75, 23);
            this.btnBack.TabIndex = 10003;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(8, 8);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(312, 160);
            this.tabControl1.TabIndex = 10002;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.mskIP);
            this.tabPage1.Controls.Add(this.btnTestSave);
            this.tabPage1.Controls.Add(this.nupdPort);
            this.tabPage1.Controls.Add(this.lblPort);
            this.tabPage1.Controls.Add(this.lblIP);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.rbtLocal);
            this.tabPage1.Controls.Add(this.rbtRemote);
            this.tabPage1.Controls.Add(this.cboInstanceConnection);
            this.tabPage1.Controls.Add(this.lblInstance);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(304, 134);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Connection";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // mskIP
            // 
            this.mskIP.Location = new System.Drawing.Point(32, 40);
            this.mskIP.Mask = "000.000.000.000";
            this.mskIP.Name = "mskIP";
            this.mskIP.Size = new System.Drawing.Size(136, 20);
            this.mskIP.TabIndex = 3;
            this.mskIP.Visible = false;
            // 
            // btnTestSave
            // 
            this.btnTestSave.Location = new System.Drawing.Point(187, 104);
            this.btnTestSave.Name = "btnTestSave";
            this.btnTestSave.Size = new System.Drawing.Size(104, 23);
            this.btnTestSave.TabIndex = 10000;
            this.btnTestSave.Text = "Test and save";
            this.btnTestSave.UseVisualStyleBackColor = true;
            this.btnTestSave.Click += new System.EventHandler(this.btnTestSave_Click);
            // 
            // nupdPort
            // 
            this.nupdPort.Location = new System.Drawing.Point(216, 40);
            this.nupdPort.Maximum = new decimal(new int[] {
            1410065407,
            2,
            0,
            0});
            this.nupdPort.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nupdPort.Name = "nupdPort";
            this.nupdPort.Size = new System.Drawing.Size(80, 20);
            this.nupdPort.TabIndex = 4;
            this.nupdPort.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nupdPort.Visible = false;
            // 
            // lblPort
            // 
            this.lblPort.AutoSize = true;
            this.lblPort.Location = new System.Drawing.Point(184, 40);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(29, 13);
            this.lblPort.TabIndex = 5;
            this.lblPort.Text = "Port:";
            this.lblPort.Visible = false;
            // 
            // lblIP
            // 
            this.lblIP.AutoSize = true;
            this.lblIP.Location = new System.Drawing.Point(8, 40);
            this.lblIP.Name = "lblIP";
            this.lblIP.Size = new System.Drawing.Size(20, 13);
            this.lblIP.TabIndex = 3;
            this.lblIP.Text = "IP:";
            this.lblIP.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Connection type:";
            // 
            // rbtLocal
            // 
            this.rbtLocal.AutoSize = true;
            this.rbtLocal.Checked = true;
            this.rbtLocal.Location = new System.Drawing.Point(104, 8);
            this.rbtLocal.Name = "rbtLocal";
            this.rbtLocal.Size = new System.Drawing.Size(51, 17);
            this.rbtLocal.TabIndex = 1;
            this.rbtLocal.TabStop = true;
            this.rbtLocal.Text = "Local";
            this.rbtLocal.UseVisualStyleBackColor = true;
            this.rbtLocal.CheckedChanged += new System.EventHandler(this.rbtLocal_CheckedChanged);
            // 
            // rbtRemote
            // 
            this.rbtRemote.AutoSize = true;
            this.rbtRemote.Location = new System.Drawing.Point(184, 8);
            this.rbtRemote.Name = "rbtRemote";
            this.rbtRemote.Size = new System.Drawing.Size(62, 17);
            this.rbtRemote.TabIndex = 2;
            this.rbtRemote.Text = "Remote";
            this.rbtRemote.UseVisualStyleBackColor = true;
            this.rbtRemote.CheckedChanged += new System.EventHandler(this.rbtRemote_CheckedChanged);
            // 
            // cboInstanceConnection
            // 
            this.cboInstanceConnection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboInstanceConnection.FormattingEnabled = true;
            this.cboInstanceConnection.Location = new System.Drawing.Point(64, 40);
            this.cboInstanceConnection.Name = "cboInstanceConnection";
            this.cboInstanceConnection.Size = new System.Drawing.Size(232, 21);
            this.cboInstanceConnection.TabIndex = 5;
            // 
            // lblInstance
            // 
            this.lblInstance.AutoSize = true;
            this.lblInstance.Location = new System.Drawing.Point(8, 40);
            this.lblInstance.Name = "lblInstance";
            this.lblInstance.Size = new System.Drawing.Size(51, 13);
            this.lblInstance.TabIndex = 7;
            this.lblInstance.Text = "Instance:";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnInstall);
            this.tabPage2.Controls.Add(this.rbtOnly);
            this.tabPage2.Controls.Add(this.rbtManual);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.rbtAutomatic);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(304, 134);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Installation engineer database";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnInstall
            // 
            this.btnInstall.Location = new System.Drawing.Point(216, 104);
            this.btnInstall.Name = "btnInstall";
            this.btnInstall.Size = new System.Drawing.Size(75, 23);
            this.btnInstall.TabIndex = 13;
            this.btnInstall.Text = "Install";
            this.btnInstall.UseVisualStyleBackColor = true;
            this.btnInstall.Click += new System.EventHandler(this.btnInstall_Click);
            // 
            // rbtOnly
            // 
            this.rbtOnly.AutoSize = true;
            this.rbtOnly.Location = new System.Drawing.Point(16, 80);
            this.rbtOnly.Name = "rbtOnly";
            this.rbtOnly.Size = new System.Drawing.Size(154, 17);
            this.rbtOnly.TabIndex = 12;
            this.rbtOnly.Text = "Only start wizard of MSSQL";
            this.rbtOnly.UseVisualStyleBackColor = true;
            // 
            // rbtManual
            // 
            this.rbtManual.AutoSize = true;
            this.rbtManual.Location = new System.Drawing.Point(16, 56);
            this.rbtManual.Name = "rbtManual";
            this.rbtManual.Size = new System.Drawing.Size(250, 17);
            this.rbtManual.TabIndex = 11;
            this.rbtManual.Text = "Manual installation preloading values of MSSQL";
            this.rbtManual.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Installation mode:";
            // 
            // rbtAutomatic
            // 
            this.rbtAutomatic.AutoSize = true;
            this.rbtAutomatic.Checked = true;
            this.rbtAutomatic.Location = new System.Drawing.Point(16, 32);
            this.rbtAutomatic.Name = "rbtAutomatic";
            this.rbtAutomatic.Size = new System.Drawing.Size(176, 17);
            this.rbtAutomatic.TabIndex = 10;
            this.rbtAutomatic.TabStop = true;
            this.rbtAutomatic.Text = "Automatic installation of MSSQL";
            this.rbtAutomatic.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.btnBackUp);
            this.tabPage3.Controls.Add(this.btnDelete);
            this.tabPage3.Controls.Add(this.btnRestore);
            this.tabPage3.Controls.Add(this.btnCreate);
            this.tabPage3.Controls.Add(this.cboInstanceMaintenance);
            this.tabPage3.Controls.Add(this.label2);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(304, 134);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Maintenance";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // btnBackUp
            // 
            this.btnBackUp.Enabled = false;
            this.btnBackUp.Location = new System.Drawing.Point(80, 56);
            this.btnBackUp.Name = "btnBackUp";
            this.btnBackUp.Size = new System.Drawing.Size(75, 23);
            this.btnBackUp.TabIndex = 21;
            this.btnBackUp.Text = "Back up";
            this.btnBackUp.UseVisualStyleBackColor = true;
            this.btnBackUp.Click += new System.EventHandler(this.btnBackUp_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Enabled = false;
            this.btnDelete.Location = new System.Drawing.Point(192, 56);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 22;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnRestore
            // 
            this.btnRestore.Enabled = false;
            this.btnRestore.Location = new System.Drawing.Point(192, 88);
            this.btnRestore.Name = "btnRestore";
            this.btnRestore.Size = new System.Drawing.Size(75, 23);
            this.btnRestore.TabIndex = 24;
            this.btnRestore.Text = "Restore";
            this.btnRestore.UseVisualStyleBackColor = true;
            this.btnRestore.Click += new System.EventHandler(this.btnRestore_Click);
            // 
            // btnCreate
            // 
            this.btnCreate.Enabled = false;
            this.btnCreate.Location = new System.Drawing.Point(80, 88);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(75, 23);
            this.btnCreate.TabIndex = 23;
            this.btnCreate.Text = "Create";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // cboInstanceMaintenance
            // 
            this.cboInstanceMaintenance.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboInstanceMaintenance.FormattingEnabled = true;
            this.cboInstanceMaintenance.Location = new System.Drawing.Point(64, 8);
            this.cboInstanceMaintenance.Name = "cboInstanceMaintenance";
            this.cboInstanceMaintenance.Size = new System.Drawing.Size(232, 21);
            this.cboInstanceMaintenance.TabIndex = 20;
            this.cboInstanceMaintenance.TabIndexChanged += new System.EventHandler(this.cboInstanceMaintenance_TabIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Instance:";
            // 
            // Form_003
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(328, 205);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form_003";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_003_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form_003_FormClosed);
            this.Load += new System.EventHandler(this.Form_003_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form_003_KeyDown);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nupdPort)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.MaskedTextBox mskIP;
        private System.Windows.Forms.Button btnTestSave;
        private System.Windows.Forms.NumericUpDown nupdPort;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.Label lblIP;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rbtLocal;
        private System.Windows.Forms.RadioButton rbtRemote;
        private System.Windows.Forms.ComboBox cboInstanceConnection;
        private System.Windows.Forms.Label lblInstance;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnInstall;
        private System.Windows.Forms.RadioButton rbtOnly;
        private System.Windows.Forms.RadioButton rbtManual;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton rbtAutomatic;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button btnBackUp;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnRestore;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.ComboBox cboInstanceMaintenance;
        private System.Windows.Forms.Label label2;
    }
}
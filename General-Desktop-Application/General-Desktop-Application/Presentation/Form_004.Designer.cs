namespace General_Desktop_Application.Presentation
{
    partial class Form_004
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_004));
            this.mnsMenu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeSesionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.controlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ventasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vendedoresToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cobradoresToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clientesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.inventariosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.administrationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.usersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.preferencesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutDeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manualDeAyudaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stpStatusBar = new System.Windows.Forms.StatusStrip();
            this.tsslDate = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslUser = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslCurrentSection = new System.Windows.Forms.ToolStripStatusLabel();
            this.timClock = new System.Windows.Forms.Timer(this.components);
            this.TimSession = new System.Windows.Forms.Timer(this.components);
            this.mnsMenu.SuspendLayout();
            this.stpStatusBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // mnsMenu
            // 
            this.mnsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.controlToolStripMenuItem,
            this.reportsToolStripMenuItem,
            this.administrationToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.mnsMenu.Location = new System.Drawing.Point(0, 0);
            this.mnsMenu.Name = "mnsMenu";
            this.mnsMenu.Size = new System.Drawing.Size(942, 24);
            this.mnsMenu.TabIndex = 1;
            this.mnsMenu.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeSesionToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // closeSesionToolStripMenuItem
            // 
            this.closeSesionToolStripMenuItem.Name = "closeSesionToolStripMenuItem";
            this.closeSesionToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.closeSesionToolStripMenuItem.Text = "Close session";
            this.closeSesionToolStripMenuItem.Click += new System.EventHandler(this.closeSesionToolStripMenuItem_Click);
            // 
            // controlToolStripMenuItem
            // 
            this.controlToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ventasToolStripMenuItem,
            this.vendedoresToolStripMenuItem,
            this.cobradoresToolStripMenuItem,
            this.clientesToolStripMenuItem,
            this.inventariosToolStripMenuItem});
            this.controlToolStripMenuItem.Name = "controlToolStripMenuItem";
            this.controlToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.controlToolStripMenuItem.Text = "Control";
            // 
            // ventasToolStripMenuItem
            // 
            this.ventasToolStripMenuItem.Name = "ventasToolStripMenuItem";
            this.ventasToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.ventasToolStripMenuItem.Text = "Ventas";
            // 
            // vendedoresToolStripMenuItem
            // 
            this.vendedoresToolStripMenuItem.Name = "vendedoresToolStripMenuItem";
            this.vendedoresToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.vendedoresToolStripMenuItem.Text = "Vendedores";
            // 
            // cobradoresToolStripMenuItem
            // 
            this.cobradoresToolStripMenuItem.Name = "cobradoresToolStripMenuItem";
            this.cobradoresToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.cobradoresToolStripMenuItem.Text = "Cobradores";
            // 
            // clientesToolStripMenuItem
            // 
            this.clientesToolStripMenuItem.Name = "clientesToolStripMenuItem";
            this.clientesToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.clientesToolStripMenuItem.Text = "Clientes";
            // 
            // inventariosToolStripMenuItem
            // 
            this.inventariosToolStripMenuItem.Name = "inventariosToolStripMenuItem";
            this.inventariosToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.inventariosToolStripMenuItem.Text = "Inventarios";
            // 
            // reportsToolStripMenuItem
            // 
            this.reportsToolStripMenuItem.Name = "reportsToolStripMenuItem";
            this.reportsToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.reportsToolStripMenuItem.Text = "Reports";
            // 
            // administrationToolStripMenuItem
            // 
            this.administrationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.usersToolStripMenuItem,
            this.toolStripMenuItem1,
            this.preferencesToolStripMenuItem});
            this.administrationToolStripMenuItem.Name = "administrationToolStripMenuItem";
            this.administrationToolStripMenuItem.Size = new System.Drawing.Size(98, 20);
            this.administrationToolStripMenuItem.Text = "Administration";
            // 
            // usersToolStripMenuItem
            // 
            this.usersToolStripMenuItem.Name = "usersToolStripMenuItem";
            this.usersToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.usersToolStripMenuItem.Text = "Users";
            this.usersToolStripMenuItem.Click += new System.EventHandler(this.usersToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(132, 6);
            // 
            // preferencesToolStripMenuItem
            // 
            this.preferencesToolStripMenuItem.Name = "preferencesToolStripMenuItem";
            this.preferencesToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.preferencesToolStripMenuItem.Text = "Preferences";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutDeToolStripMenuItem,
            this.manualDeAyudaToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutDeToolStripMenuItem
            // 
            this.aboutDeToolStripMenuItem.Name = "aboutDeToolStripMenuItem";
            this.aboutDeToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.aboutDeToolStripMenuItem.Text = "About...";
            // 
            // manualDeAyudaToolStripMenuItem
            // 
            this.manualDeAyudaToolStripMenuItem.Name = "manualDeAyudaToolStripMenuItem";
            this.manualDeAyudaToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.manualDeAyudaToolStripMenuItem.Text = "Ver manual";
            // 
            // stpStatusBar
            // 
            this.stpStatusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslDate,
            this.tsslUser,
            this.tsslCurrentSection});
            this.stpStatusBar.Location = new System.Drawing.Point(0, 522);
            this.stpStatusBar.Name = "stpStatusBar";
            this.stpStatusBar.Size = new System.Drawing.Size(942, 22);
            this.stpStatusBar.TabIndex = 2;
            this.stpStatusBar.Text = "statusStrip1";
            // 
            // tsslDate
            // 
            this.tsslDate.Name = "tsslDate";
            this.tsslDate.Size = new System.Drawing.Size(22, 17);
            this.tsslDate.Text = "     ";
            // 
            // tsslUser
            // 
            this.tsslUser.Name = "tsslUser";
            this.tsslUser.Size = new System.Drawing.Size(22, 17);
            this.tsslUser.Text = "     ";
            // 
            // tsslCurrentSection
            // 
            this.tsslCurrentSection.Name = "tsslCurrentSection";
            this.tsslCurrentSection.Size = new System.Drawing.Size(22, 17);
            this.tsslCurrentSection.Text = "     ";
            // 
            // timClock
            // 
            this.timClock.Enabled = true;
            this.timClock.Interval = 1000;
            this.timClock.Tick += new System.EventHandler(this.timClock_Tick);
            // 
            // TimSession
            // 
            this.TimSession.Enabled = true;
            this.TimSession.Interval = 600000;
            this.TimSession.Tick += new System.EventHandler(this.TimSession_Tick);
            // 
            // Form_004
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(942, 544);
            this.Controls.Add(this.stpStatusBar);
            this.Controls.Add(this.mnsMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.mnsMenu;
            this.Name = "Form_004";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Invalid Company";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_004_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form_004_FormClosed);
            this.Load += new System.EventHandler(this.Form_004_Load);
            this.mnsMenu.ResumeLayout(false);
            this.mnsMenu.PerformLayout();
            this.stpStatusBar.ResumeLayout(false);
            this.stpStatusBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mnsMenu;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeSesionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem controlToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ventasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem vendedoresToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cobradoresToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clientesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem inventariosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reportsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem administrationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem usersToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem preferencesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutDeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem manualDeAyudaToolStripMenuItem;
        private System.Windows.Forms.StatusStrip stpStatusBar;
        private System.Windows.Forms.ToolStripStatusLabel tsslDate;
        private System.Windows.Forms.ToolStripStatusLabel tsslUser;
        private System.Windows.Forms.Timer timClock;
        private System.Windows.Forms.ToolStripStatusLabel tsslCurrentSection;
        private System.Windows.Forms.Timer TimSession;
    }
}
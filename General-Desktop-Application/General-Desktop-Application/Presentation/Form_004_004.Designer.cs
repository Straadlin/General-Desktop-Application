namespace General_Desktop_Application.Presentation
{
    partial class Form_004_004
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_004_004));
            this.lblProduct = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.lblCompany = new System.Windows.Forms.Label();
            this.txtInfo = new System.Windows.Forms.TextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.pcbLogo = new System.Windows.Forms.PictureBox();
            this.lblDesigned = new System.Windows.Forms.Label();
            this.lblPC = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pcbLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // lblProduct
            // 
            this.lblProduct.BackColor = System.Drawing.Color.Transparent;
            this.lblProduct.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProduct.ForeColor = System.Drawing.Color.White;
            this.lblProduct.Location = new System.Drawing.Point(152, 48);
            this.lblProduct.Name = "lblProduct";
            this.lblProduct.Size = new System.Drawing.Size(360, 15);
            this.lblProduct.TabIndex = 0;
            this.lblProduct.Text = "Product Name:";
            // 
            // lblVersion
            // 
            this.lblVersion.BackColor = System.Drawing.Color.Transparent;
            this.lblVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVersion.ForeColor = System.Drawing.Color.White;
            this.lblVersion.Location = new System.Drawing.Point(152, 80);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(360, 15);
            this.lblVersion.TabIndex = 1;
            this.lblVersion.Text = "Version:";
            // 
            // lblCompany
            // 
            this.lblCompany.BackColor = System.Drawing.Color.Transparent;
            this.lblCompany.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCompany.ForeColor = System.Drawing.Color.White;
            this.lblCompany.Location = new System.Drawing.Point(152, 16);
            this.lblCompany.Name = "lblCompany";
            this.lblCompany.Size = new System.Drawing.Size(360, 15);
            this.lblCompany.TabIndex = 2;
            this.lblCompany.Text = "Company Name:";
            // 
            // txtInfo
            // 
            this.txtInfo.BackColor = System.Drawing.Color.RosyBrown;
            this.txtInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInfo.Location = new System.Drawing.Point(152, 168);
            this.txtInfo.Multiline = true;
            this.txtInfo.Name = "txtInfo";
            this.txtInfo.ReadOnly = true;
            this.txtInfo.Size = new System.Drawing.Size(368, 104);
            this.txtInfo.TabIndex = 4;
            this.txtInfo.Text = "...";
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(440, 280);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 0;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // pcbLogo
            // 
            this.pcbLogo.BackColor = System.Drawing.Color.RosyBrown;
            this.pcbLogo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pcbLogo.Location = new System.Drawing.Point(8, 56);
            this.pcbLogo.Name = "pcbLogo";
            this.pcbLogo.Size = new System.Drawing.Size(136, 104);
            this.pcbLogo.TabIndex = 5;
            this.pcbLogo.TabStop = false;
            // 
            // lblDesigned
            // 
            this.lblDesigned.BackColor = System.Drawing.Color.Transparent;
            this.lblDesigned.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDesigned.ForeColor = System.Drawing.Color.White;
            this.lblDesigned.Location = new System.Drawing.Point(152, 136);
            this.lblDesigned.Name = "lblDesigned";
            this.lblDesigned.Size = new System.Drawing.Size(360, 15);
            this.lblDesigned.TabIndex = 6;
            this.lblDesigned.Text = "Designed and Developed by:";
            // 
            // lblPC
            // 
            this.lblPC.BackColor = System.Drawing.Color.Transparent;
            this.lblPC.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPC.ForeColor = System.Drawing.Color.White;
            this.lblPC.Location = new System.Drawing.Point(152, 112);
            this.lblPC.Name = "lblPC";
            this.lblPC.Size = new System.Drawing.Size(360, 15);
            this.lblPC.TabIndex = 7;
            this.lblPC.Text = "PC:";
            // 
            // Form_004_004
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(525, 310);
            this.Controls.Add(this.lblPC);
            this.Controls.Add(this.lblDesigned);
            this.Controls.Add(this.pcbLogo);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.txtInfo);
            this.Controls.Add(this.lblCompany);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.lblProduct);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form_004_004";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "About...";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form_004_004_FormClosed);
            this.Load += new System.EventHandler(this.Form_004_004_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Form_004_004_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.pcbLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblProduct;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Label lblCompany;
        private System.Windows.Forms.TextBox txtInfo;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.PictureBox pcbLogo;
        private System.Windows.Forms.Label lblDesigned;
        private System.Windows.Forms.Label lblPC;
    }
}
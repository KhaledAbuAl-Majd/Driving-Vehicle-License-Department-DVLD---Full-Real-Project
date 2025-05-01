namespace DVLDPresentation.Licenses
{
    partial class ctrlDriverLicenses
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ctrlDriverLicenses));
            this.cmpLocalLicenseOptions = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tmsIshowLocalLicenseInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsInternationalLicenseOptions = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tmsIshowInternationalLicenseInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.gtabCDriverLicenses = new Guna.UI2.WinForms.Guna2TabControl();
            this.gtabLocal = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvLocalLicensesHistory = new System.Windows.Forms.DataGridView();
            this.lblLocalLicensesNumOfRecords = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.gtabInternational = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvInternationalLicensesHistory = new System.Windows.Forms.DataGridView();
            this.lblInternationalNumOfRecords = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmpLocalLicenseOptions.SuspendLayout();
            this.cmsInternationalLicenseOptions.SuspendLayout();
            this.gtabCDriverLicenses.SuspendLayout();
            this.gtabLocal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLocalLicensesHistory)).BeginInit();
            this.gtabInternational.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInternationalLicensesHistory)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmpLocalLicenseOptions
            // 
            this.cmpLocalLicenseOptions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tmsIshowLocalLicenseInfo});
            this.cmpLocalLicenseOptions.Name = "cmpPeopleOptoins";
            this.cmpLocalLicenseOptions.Size = new System.Drawing.Size(195, 42);
            // 
            // tmsIshowLocalLicenseInfo
            // 
            this.tmsIshowLocalLicenseInfo.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tmsIshowLocalLicenseInfo.Image = ((System.Drawing.Image)(resources.GetObject("tmsIshowLocalLicenseInfo.Image")));
            this.tmsIshowLocalLicenseInfo.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tmsIshowLocalLicenseInfo.Name = "tmsIshowLocalLicenseInfo";
            this.tmsIshowLocalLicenseInfo.Size = new System.Drawing.Size(194, 38);
            this.tmsIshowLocalLicenseInfo.Text = "Show License Info";
            this.tmsIshowLocalLicenseInfo.Click += new System.EventHandler(this.showLicenseInfoToolStripMenuItem_Click);
            // 
            // cmsInternationalLicenseOptions
            // 
            this.cmsInternationalLicenseOptions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tmsIshowInternationalLicenseInfo});
            this.cmsInternationalLicenseOptions.Name = "cmpPeopleOptoins";
            this.cmsInternationalLicenseOptions.Size = new System.Drawing.Size(195, 42);
            // 
            // tmsIshowInternationalLicenseInfo
            // 
            this.tmsIshowInternationalLicenseInfo.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tmsIshowInternationalLicenseInfo.Image = ((System.Drawing.Image)(resources.GetObject("tmsIshowInternationalLicenseInfo.Image")));
            this.tmsIshowInternationalLicenseInfo.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tmsIshowInternationalLicenseInfo.Name = "tmsIshowInternationalLicenseInfo";
            this.tmsIshowInternationalLicenseInfo.Size = new System.Drawing.Size(194, 38);
            this.tmsIshowInternationalLicenseInfo.Text = "Show License Info";
            this.tmsIshowInternationalLicenseInfo.Click += new System.EventHandler(this.InternationalLicenseHistorytoolStripMenuItem_Click);
            // 
            // gtabCDriverLicenses
            // 
            this.gtabCDriverLicenses.Controls.Add(this.gtabLocal);
            this.gtabCDriverLicenses.Controls.Add(this.gtabInternational);
            this.gtabCDriverLicenses.Cursor = System.Windows.Forms.Cursors.Hand;
            this.gtabCDriverLicenses.ItemSize = new System.Drawing.Size(140, 50);
            this.gtabCDriverLicenses.Location = new System.Drawing.Point(22, 35);
            this.gtabCDriverLicenses.Name = "gtabCDriverLicenses";
            this.gtabCDriverLicenses.SelectedIndex = 0;
            this.gtabCDriverLicenses.Size = new System.Drawing.Size(1114, 335);
            this.gtabCDriverLicenses.TabButtonHoverState.BorderColor = System.Drawing.Color.Empty;
            this.gtabCDriverLicenses.TabButtonHoverState.FillColor = System.Drawing.Color.BlueViolet;
            this.gtabCDriverLicenses.TabButtonHoverState.Font = new System.Drawing.Font("Segoe UI Semibold", 10F);
            this.gtabCDriverLicenses.TabButtonHoverState.ForeColor = System.Drawing.Color.White;
            this.gtabCDriverLicenses.TabButtonHoverState.InnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(52)))), ((int)(((byte)(70)))));
            this.gtabCDriverLicenses.TabButtonIdleState.BorderColor = System.Drawing.Color.Empty;
            this.gtabCDriverLicenses.TabButtonIdleState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(42)))), ((int)(((byte)(57)))));
            this.gtabCDriverLicenses.TabButtonIdleState.Font = new System.Drawing.Font("Segoe UI Semibold", 11F);
            this.gtabCDriverLicenses.TabButtonIdleState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(160)))), ((int)(((byte)(167)))));
            this.gtabCDriverLicenses.TabButtonIdleState.InnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(42)))), ((int)(((byte)(57)))));
            this.gtabCDriverLicenses.TabButtonSelectedState.BorderColor = System.Drawing.Color.Empty;
            this.gtabCDriverLicenses.TabButtonSelectedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(37)))), ((int)(((byte)(49)))));
            this.gtabCDriverLicenses.TabButtonSelectedState.Font = new System.Drawing.Font("Segoe UI Semibold", 10F);
            this.gtabCDriverLicenses.TabButtonSelectedState.ForeColor = System.Drawing.Color.White;
            this.gtabCDriverLicenses.TabButtonSelectedState.InnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(132)))), ((int)(((byte)(255)))));
            this.gtabCDriverLicenses.TabButtonSize = new System.Drawing.Size(140, 50);
            this.gtabCDriverLicenses.TabIndex = 30;
            this.gtabCDriverLicenses.TabMenuBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(42)))), ((int)(((byte)(57)))));
            this.gtabCDriverLicenses.TabMenuOrientation = Guna.UI2.WinForms.TabMenuOrientation.HorizontalTop;
            // 
            // gtabLocal
            // 
            this.gtabLocal.BackColor = System.Drawing.Color.White;
            this.gtabLocal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.gtabLocal.Controls.Add(this.label2);
            this.gtabLocal.Controls.Add(this.dgvLocalLicensesHistory);
            this.gtabLocal.Controls.Add(this.lblLocalLicensesNumOfRecords);
            this.gtabLocal.Controls.Add(this.label3);
            this.gtabLocal.Location = new System.Drawing.Point(4, 54);
            this.gtabLocal.Name = "gtabLocal";
            this.gtabLocal.Padding = new System.Windows.Forms.Padding(3);
            this.gtabLocal.Size = new System.Drawing.Size(1106, 277);
            this.gtabLocal.TabIndex = 0;
            this.gtabLocal.Text = "Local";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(20, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(191, 24);
            this.label2.TabIndex = 24;
            this.label2.Text = "local Licneses History";
            // 
            // dgvLocalLicensesHistory
            // 
            this.dgvLocalLicensesHistory.AllowUserToAddRows = false;
            this.dgvLocalLicensesHistory.AllowUserToDeleteRows = false;
            this.dgvLocalLicensesHistory.AllowUserToOrderColumns = true;
            this.dgvLocalLicensesHistory.AllowUserToResizeColumns = false;
            this.dgvLocalLicensesHistory.AllowUserToResizeRows = false;
            this.dgvLocalLicensesHistory.BackgroundColor = System.Drawing.Color.White;
            this.dgvLocalLicensesHistory.ColumnHeadersHeight = 35;
            this.dgvLocalLicensesHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvLocalLicensesHistory.ContextMenuStrip = this.cmpLocalLicenseOptions;
            this.dgvLocalLicensesHistory.Cursor = System.Windows.Forms.Cursors.Default;
            this.dgvLocalLicensesHistory.GridColor = System.Drawing.Color.Black;
            this.dgvLocalLicensesHistory.Location = new System.Drawing.Point(17, 48);
            this.dgvLocalLicensesHistory.MultiSelect = false;
            this.dgvLocalLicensesHistory.Name = "dgvLocalLicensesHistory";
            this.dgvLocalLicensesHistory.ReadOnly = true;
            this.dgvLocalLicensesHistory.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvLocalLicensesHistory.RowTemplate.Height = 27;
            this.dgvLocalLicensesHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLocalLicensesHistory.Size = new System.Drawing.Size(1068, 179);
            this.dgvLocalLicensesHistory.TabIndex = 25;
            // 
            // lblLocalLicensesNumOfRecords
            // 
            this.lblLocalLicensesNumOfRecords.AutoSize = true;
            this.lblLocalLicensesNumOfRecords.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLocalLicensesNumOfRecords.Location = new System.Drawing.Point(140, 239);
            this.lblLocalLicensesNumOfRecords.Name = "lblLocalLicensesNumOfRecords";
            this.lblLocalLicensesNumOfRecords.Size = new System.Drawing.Size(20, 24);
            this.lblLocalLicensesNumOfRecords.TabIndex = 27;
            this.lblLocalLicensesNumOfRecords.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(20, 237);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 24);
            this.label3.TabIndex = 26;
            this.label3.Text = "# Records";
            // 
            // gtabInternational
            // 
            this.gtabInternational.BackColor = System.Drawing.Color.White;
            this.gtabInternational.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.gtabInternational.Controls.Add(this.label1);
            this.gtabInternational.Controls.Add(this.dgvInternationalLicensesHistory);
            this.gtabInternational.Controls.Add(this.lblInternationalNumOfRecords);
            this.gtabInternational.Controls.Add(this.label5);
            this.gtabInternational.Location = new System.Drawing.Point(4, 54);
            this.gtabInternational.Name = "gtabInternational";
            this.gtabInternational.Padding = new System.Windows.Forms.Padding(3);
            this.gtabInternational.Size = new System.Drawing.Size(1106, 277);
            this.gtabInternational.TabIndex = 1;
            this.gtabInternational.Text = "International";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(21, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(253, 24);
            this.label1.TabIndex = 28;
            this.label1.Text = "International Licneses History";
            // 
            // dgvInternationalLicensesHistory
            // 
            this.dgvInternationalLicensesHistory.AllowUserToAddRows = false;
            this.dgvInternationalLicensesHistory.AllowUserToDeleteRows = false;
            this.dgvInternationalLicensesHistory.AllowUserToOrderColumns = true;
            this.dgvInternationalLicensesHistory.AllowUserToResizeColumns = false;
            this.dgvInternationalLicensesHistory.AllowUserToResizeRows = false;
            this.dgvInternationalLicensesHistory.BackgroundColor = System.Drawing.Color.White;
            this.dgvInternationalLicensesHistory.ColumnHeadersHeight = 35;
            this.dgvInternationalLicensesHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvInternationalLicensesHistory.ContextMenuStrip = this.cmsInternationalLicenseOptions;
            this.dgvInternationalLicensesHistory.Cursor = System.Windows.Forms.Cursors.Default;
            this.dgvInternationalLicensesHistory.GridColor = System.Drawing.Color.Black;
            this.dgvInternationalLicensesHistory.Location = new System.Drawing.Point(18, 48);
            this.dgvInternationalLicensesHistory.MultiSelect = false;
            this.dgvInternationalLicensesHistory.Name = "dgvInternationalLicensesHistory";
            this.dgvInternationalLicensesHistory.ReadOnly = true;
            this.dgvInternationalLicensesHistory.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvInternationalLicensesHistory.RowTemplate.Height = 27;
            this.dgvInternationalLicensesHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvInternationalLicensesHistory.Size = new System.Drawing.Size(1069, 179);
            this.dgvInternationalLicensesHistory.TabIndex = 29;
            // 
            // lblInternationalNumOfRecords
            // 
            this.lblInternationalNumOfRecords.AutoSize = true;
            this.lblInternationalNumOfRecords.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInternationalNumOfRecords.Location = new System.Drawing.Point(141, 239);
            this.lblInternationalNumOfRecords.Name = "lblInternationalNumOfRecords";
            this.lblInternationalNumOfRecords.Size = new System.Drawing.Size(20, 24);
            this.lblInternationalNumOfRecords.TabIndex = 31;
            this.lblInternationalNumOfRecords.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(21, 237);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(96, 24);
            this.label5.TabIndex = 30;
            this.label5.Text = "# Records";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.gtabCDriverLicenses);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1156, 389);
            this.groupBox1.TabIndex = 31;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Driver Licenses";
            // 
            // ctrlDriverLicenses
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.groupBox1);
            this.Name = "ctrlDriverLicenses";
            this.Size = new System.Drawing.Size(1183, 399);
            this.cmpLocalLicenseOptions.ResumeLayout(false);
            this.cmsInternationalLicenseOptions.ResumeLayout(false);
            this.gtabCDriverLicenses.ResumeLayout(false);
            this.gtabLocal.ResumeLayout(false);
            this.gtabLocal.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLocalLicensesHistory)).EndInit();
            this.gtabInternational.ResumeLayout(false);
            this.gtabInternational.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInternationalLicensesHistory)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip cmpLocalLicenseOptions;
        private System.Windows.Forms.ToolStripMenuItem tmsIshowLocalLicenseInfo;
        private System.Windows.Forms.ContextMenuStrip cmsInternationalLicenseOptions;
        private System.Windows.Forms.ToolStripMenuItem tmsIshowInternationalLicenseInfo;
        private Guna.UI2.WinForms.Guna2TabControl gtabCDriverLicenses;
        private System.Windows.Forms.TabPage gtabLocal;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgvLocalLicensesHistory;
        private System.Windows.Forms.Label lblLocalLicensesNumOfRecords;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabPage gtabInternational;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvInternationalLicensesHistory;
        private System.Windows.Forms.Label lblInternationalNumOfRecords;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}

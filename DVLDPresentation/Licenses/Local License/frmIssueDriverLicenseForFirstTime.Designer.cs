namespace DVLDPresentation.Applications.Manage_Applications.LocalDrivingLicenseApplications
{
    partial class frmIssueDriverLicenseForFirstTime
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmIssueDriverLicenseForFirstTime));
            this.gtxtNotes = new Guna.UI2.WinForms.Guna2TextBox();
            this.pictureBox7 = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.gbtnIssue = new Guna.UI2.WinForms.Guna2Button();
            this.gbtnClose = new Guna.UI2.WinForms.Guna2Button();
            this.ctrlDLApplicationInfo1 = new DVLDPresentation.Controls.ctrlDrivingLicenseApplicationInfo();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).BeginInit();
            this.SuspendLayout();
            // 
            // gtxtNotes
            // 
            this.gtxtNotes.Animated = true;
            this.gtxtNotes.BorderColor = System.Drawing.Color.Black;
            this.gtxtNotes.BorderRadius = 10;
            this.gtxtNotes.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.gtxtNotes.DefaultText = "";
            this.gtxtNotes.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.gtxtNotes.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.gtxtNotes.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.gtxtNotes.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.gtxtNotes.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.gtxtNotes.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gtxtNotes.ForeColor = System.Drawing.Color.Black;
            this.gtxtNotes.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.gtxtNotes.Location = new System.Drawing.Point(151, 483);
            this.gtxtNotes.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gtxtNotes.MaxLength = 300;
            this.gtxtNotes.Multiline = true;
            this.gtxtNotes.Name = "gtxtNotes";
            this.gtxtNotes.PlaceholderText = "";
            this.gtxtNotes.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.gtxtNotes.SelectedText = "";
            this.gtxtNotes.Size = new System.Drawing.Size(808, 120);
            this.gtxtNotes.TabIndex = 84;
            // 
            // pictureBox7
            // 
            this.pictureBox7.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox7.Image")));
            this.pictureBox7.Location = new System.Drawing.Point(110, 483);
            this.pictureBox7.Name = "pictureBox7";
            this.pictureBox7.Size = new System.Drawing.Size(25, 28);
            this.pictureBox7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox7.TabIndex = 82;
            this.pictureBox7.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(33, 485);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 20);
            this.label5.TabIndex = 83;
            this.label5.Text = "Notes:";
            // 
            // gbtnIssue
            // 
            this.gbtnIssue.Animated = true;
            this.gbtnIssue.AutoRoundedCorners = true;
            this.gbtnIssue.BackColor = System.Drawing.Color.Transparent;
            this.gbtnIssue.BorderThickness = 1;
            this.gbtnIssue.Cursor = System.Windows.Forms.Cursors.Hand;
            this.gbtnIssue.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.gbtnIssue.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.gbtnIssue.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.gbtnIssue.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.gbtnIssue.FillColor = System.Drawing.Color.White;
            this.gbtnIssue.FocusedColor = System.Drawing.SystemColors.MenuHighlight;
            this.gbtnIssue.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbtnIssue.ForeColor = System.Drawing.Color.Black;
            this.gbtnIssue.HoverState.FillColor = System.Drawing.Color.SlateBlue;
            this.gbtnIssue.HoverState.ForeColor = System.Drawing.Color.White;
            this.gbtnIssue.Image = ((System.Drawing.Image)(resources.GetObject("gbtnIssue.Image")));
            this.gbtnIssue.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.gbtnIssue.ImageSize = new System.Drawing.Size(35, 35);
            this.gbtnIssue.Location = new System.Drawing.Point(805, 623);
            this.gbtnIssue.Name = "gbtnIssue";
            this.gbtnIssue.Size = new System.Drawing.Size(154, 45);
            this.gbtnIssue.TabIndex = 80;
            this.gbtnIssue.Text = "Issue";
            this.gbtnIssue.Click += new System.EventHandler(this.gbtnIssue_Click);
            // 
            // gbtnClose
            // 
            this.gbtnClose.Animated = true;
            this.gbtnClose.AutoRoundedCorners = true;
            this.gbtnClose.BackColor = System.Drawing.Color.Transparent;
            this.gbtnClose.BorderThickness = 1;
            this.gbtnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.gbtnClose.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.gbtnClose.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.gbtnClose.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.gbtnClose.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.gbtnClose.FillColor = System.Drawing.Color.White;
            this.gbtnClose.FocusedColor = System.Drawing.SystemColors.MenuHighlight;
            this.gbtnClose.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbtnClose.ForeColor = System.Drawing.Color.Black;
            this.gbtnClose.HoverState.FillColor = System.Drawing.Color.SlateBlue;
            this.gbtnClose.HoverState.ForeColor = System.Drawing.Color.White;
            this.gbtnClose.Image = ((System.Drawing.Image)(resources.GetObject("gbtnClose.Image")));
            this.gbtnClose.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.gbtnClose.ImageSize = new System.Drawing.Size(35, 35);
            this.gbtnClose.Location = new System.Drawing.Point(645, 623);
            this.gbtnClose.Name = "gbtnClose";
            this.gbtnClose.Size = new System.Drawing.Size(154, 45);
            this.gbtnClose.TabIndex = 81;
            this.gbtnClose.Text = "Close";
            this.gbtnClose.Click += new System.EventHandler(this.gbtnClose_Click);
            // 
            // ctrlDLApplicationInfo1
            // 
            this.ctrlDLApplicationInfo1.BackColor = System.Drawing.Color.White;
            this.ctrlDLApplicationInfo1.Location = new System.Drawing.Point(12, 34);
            this.ctrlDLApplicationInfo1.Name = "ctrlDLApplicationInfo1";
            this.ctrlDLApplicationInfo1.Size = new System.Drawing.Size(960, 433);
            this.ctrlDLApplicationInfo1.TabIndex = 0;
            // 
            // frmIssueDriverLicenseForFirstTime
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(985, 680);
            this.Controls.Add(this.gtxtNotes);
            this.Controls.Add(this.pictureBox7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.gbtnIssue);
            this.Controls.Add(this.gbtnClose);
            this.Controls.Add(this.ctrlDLApplicationInfo1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmIssueDriverLicenseForFirstTime";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "IssueDriver License For The First Time";
            this.Load += new System.EventHandler(this.frmIssueDriverLicenseForFirstTime_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.ctrlDrivingLicenseApplicationInfo ctrlDLApplicationInfo1;
        private Guna.UI2.WinForms.Guna2TextBox gtxtNotes;
        private System.Windows.Forms.PictureBox pictureBox7;
        private System.Windows.Forms.Label label5;
        private Guna.UI2.WinForms.Guna2Button gbtnIssue;
        private Guna.UI2.WinForms.Guna2Button gbtnClose;
    }
}
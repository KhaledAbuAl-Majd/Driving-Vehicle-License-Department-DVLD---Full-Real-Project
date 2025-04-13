namespace DVLDPresentation.Controls
{
    partial class ctrlDriverLicenseInfoCardWithFilter
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ctrlDriverLicenseInfoCardWithFilter));
            this.gbFilterBy = new System.Windows.Forms.GroupBox();
            this.btnLicenseSearch = new System.Windows.Forms.Button();
            this.gtxtFilterValue = new Guna.UI2.WinForms.Guna2TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ctrlDriverLicenseInfo1 = new DVLDPresentation.Controls.ctrlDriverLicenseInfo();
            this.gbFilterBy.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbFilterBy
            // 
            this.gbFilterBy.BackColor = System.Drawing.Color.White;
            this.gbFilterBy.Controls.Add(this.btnLicenseSearch);
            this.gbFilterBy.Controls.Add(this.gtxtFilterValue);
            this.gbFilterBy.Controls.Add(this.label1);
            this.gbFilterBy.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbFilterBy.Location = new System.Drawing.Point(3, 5);
            this.gbFilterBy.Name = "gbFilterBy";
            this.gbFilterBy.Size = new System.Drawing.Size(581, 102);
            this.gbFilterBy.TabIndex = 31;
            this.gbFilterBy.TabStop = false;
            this.gbFilterBy.Text = "Filter";
            // 
            // btnLicenseSearch
            // 
            this.btnLicenseSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLicenseSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLicenseSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnLicenseSearch.Image")));
            this.btnLicenseSearch.Location = new System.Drawing.Point(455, 35);
            this.btnLicenseSearch.Name = "btnLicenseSearch";
            this.btnLicenseSearch.Size = new System.Drawing.Size(66, 50);
            this.btnLicenseSearch.TabIndex = 18;
            this.btnLicenseSearch.UseVisualStyleBackColor = true;
            this.btnLicenseSearch.Click += new System.EventHandler(this.btnLicenseSearch_Click);
            // 
            // gtxtFilterValue
            // 
            this.gtxtFilterValue.Animated = true;
            this.gtxtFilterValue.AutoRoundedCorners = true;
            this.gtxtFilterValue.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.gtxtFilterValue.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.gtxtFilterValue.DefaultText = "";
            this.gtxtFilterValue.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.gtxtFilterValue.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.gtxtFilterValue.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.gtxtFilterValue.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.gtxtFilterValue.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.gtxtFilterValue.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gtxtFilterValue.ForeColor = System.Drawing.Color.Black;
            this.gtxtFilterValue.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.gtxtFilterValue.Location = new System.Drawing.Point(152, 41);
            this.gtxtFilterValue.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gtxtFilterValue.MaxLength = 255;
            this.gtxtFilterValue.Name = "gtxtFilterValue";
            this.gtxtFilterValue.PlaceholderText = "";
            this.gtxtFilterValue.SelectedText = "";
            this.gtxtFilterValue.Size = new System.Drawing.Size(283, 36);
            this.gtxtFilterValue.TabIndex = 16;
            this.gtxtFilterValue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.gtxtFilterValue_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(15, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "LicneseID:";
            // 
            // ctrlDriverLicenseInfo1
            // 
            this.ctrlDriverLicenseInfo1.BackColor = System.Drawing.Color.White;
            this.ctrlDriverLicenseInfo1.Location = new System.Drawing.Point(3, 113);
            this.ctrlDriverLicenseInfo1.Name = "ctrlDriverLicenseInfo1";
            this.ctrlDriverLicenseInfo1.Size = new System.Drawing.Size(1032, 368);
            this.ctrlDriverLicenseInfo1.TabIndex = 32;
            // 
            // ctrlDriverLicenseInfoCardWithFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.ctrlDriverLicenseInfo1);
            this.Controls.Add(this.gbFilterBy);
            this.Name = "ctrlDriverLicenseInfoCardWithFilter";
            this.Size = new System.Drawing.Size(1040, 476);
            this.gbFilterBy.ResumeLayout(false);
            this.gbFilterBy.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ctrlDriverLicenseInfo ctrlDriverLicenseInfo1;
        private System.Windows.Forms.GroupBox gbFilterBy;
        private System.Windows.Forms.Button btnLicenseSearch;
        private Guna.UI2.WinForms.Guna2TextBox gtxtFilterValue;
        private System.Windows.Forms.Label label1;
    }
}

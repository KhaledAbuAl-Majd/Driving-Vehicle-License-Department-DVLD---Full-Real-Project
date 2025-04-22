namespace DVLDPresentation.Users
{
    partial class frmAddUpdateUser
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAddUpdateUser));
            this.lblHeader = new System.Windows.Forms.Label();
            this.gtcUserInfo = new Guna.UI2.WinForms.Guna2TabControl();
            this.gtpPersonInfo = new System.Windows.Forms.TabPage();
            this.ctrlPersonCardWithFilter1 = new DVLDPresentation.Controls.ctrlPersonCardWithFilter();
            this.gbtnNext = new Guna.UI2.WinForms.Guna2Button();
            this.gtpLoginInfo = new System.Windows.Forms.TabPage();
            this.pnlLoginInfo = new System.Windows.Forms.Panel();
            this.gchkIsActive = new Guna.UI2.WinForms.Guna2CheckBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.gtxtConfirmPassword = new Guna.UI2.WinForms.Guna2TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.gtxtPassword = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblUserID = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gtxtUserName = new Guna.UI2.WinForms.Guna2TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.gbtnClose = new Guna.UI2.WinForms.Guna2Button();
            this.gbtnSave = new Guna.UI2.WinForms.Guna2Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.gtcUserInfo.SuspendLayout();
            this.gtpPersonInfo.SuspendLayout();
            this.gtpLoginInfo.SuspendLayout();
            this.pnlLoginInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblHeader
            // 
            this.lblHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblHeader.Location = new System.Drawing.Point(0, 49);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(1051, 33);
            this.lblHeader.TabIndex = 24;
            this.lblHeader.Text = "Add New Person";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // gtcUserInfo
            // 
            this.gtcUserInfo.Controls.Add(this.gtpPersonInfo);
            this.gtcUserInfo.Controls.Add(this.gtpLoginInfo);
            this.gtcUserInfo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.gtcUserInfo.ItemSize = new System.Drawing.Size(140, 50);
            this.gtcUserInfo.Location = new System.Drawing.Point(27, 115);
            this.gtcUserInfo.Name = "gtcUserInfo";
            this.gtcUserInfo.SelectedIndex = 0;
            this.gtcUserInfo.Size = new System.Drawing.Size(996, 554);
            this.gtcUserInfo.TabButtonHoverState.BorderColor = System.Drawing.Color.Empty;
            this.gtcUserInfo.TabButtonHoverState.FillColor = System.Drawing.Color.BlueViolet;
            this.gtcUserInfo.TabButtonHoverState.Font = new System.Drawing.Font("Segoe UI Semibold", 10F);
            this.gtcUserInfo.TabButtonHoverState.ForeColor = System.Drawing.Color.White;
            this.gtcUserInfo.TabButtonHoverState.InnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(52)))), ((int)(((byte)(70)))));
            this.gtcUserInfo.TabButtonIdleState.BorderColor = System.Drawing.Color.Empty;
            this.gtcUserInfo.TabButtonIdleState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(42)))), ((int)(((byte)(57)))));
            this.gtcUserInfo.TabButtonIdleState.Font = new System.Drawing.Font("Segoe UI Semibold", 11F);
            this.gtcUserInfo.TabButtonIdleState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(160)))), ((int)(((byte)(167)))));
            this.gtcUserInfo.TabButtonIdleState.InnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(42)))), ((int)(((byte)(57)))));
            this.gtcUserInfo.TabButtonSelectedState.BorderColor = System.Drawing.Color.Empty;
            this.gtcUserInfo.TabButtonSelectedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(37)))), ((int)(((byte)(49)))));
            this.gtcUserInfo.TabButtonSelectedState.Font = new System.Drawing.Font("Segoe UI Semibold", 10F);
            this.gtcUserInfo.TabButtonSelectedState.ForeColor = System.Drawing.Color.White;
            this.gtcUserInfo.TabButtonSelectedState.InnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(132)))), ((int)(((byte)(255)))));
            this.gtcUserInfo.TabButtonSize = new System.Drawing.Size(140, 50);
            this.gtcUserInfo.TabIndex = 25;
            this.gtcUserInfo.TabMenuBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(42)))), ((int)(((byte)(57)))));
            this.gtcUserInfo.TabMenuOrientation = Guna.UI2.WinForms.TabMenuOrientation.HorizontalTop;
            // 
            // gtpPersonInfo
            // 
            this.gtpPersonInfo.BackColor = System.Drawing.Color.White;
            this.gtpPersonInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.gtpPersonInfo.Controls.Add(this.ctrlPersonCardWithFilter1);
            this.gtpPersonInfo.Controls.Add(this.gbtnNext);
            this.gtpPersonInfo.Location = new System.Drawing.Point(4, 54);
            this.gtpPersonInfo.Name = "gtpPersonInfo";
            this.gtpPersonInfo.Padding = new System.Windows.Forms.Padding(3);
            this.gtpPersonInfo.Size = new System.Drawing.Size(988, 496);
            this.gtpPersonInfo.TabIndex = 0;
            this.gtpPersonInfo.Text = "Person Info";
            // 
            // ctrlPersonCardWithFilter1
            // 
            this.ctrlPersonCardWithFilter1.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.ctrlPersonCardWithFilter1.BackColor = System.Drawing.Color.White;
            this.ctrlPersonCardWithFilter1.FilterEnabled = true;
            this.ctrlPersonCardWithFilter1.Location = new System.Drawing.Point(6, 11);
            this.ctrlPersonCardWithFilter1.Name = "ctrlPersonCardWithFilter1";
            this.ctrlPersonCardWithFilter1.ShowAddPerson = true;
            this.ctrlPersonCardWithFilter1.Size = new System.Drawing.Size(962, 415);
            this.ctrlPersonCardWithFilter1.TabIndex = 6;
            // 
            // gbtnNext
            // 
            this.gbtnNext.Animated = true;
            this.gbtnNext.AutoRoundedCorners = true;
            this.gbtnNext.BackColor = System.Drawing.Color.White;
            this.gbtnNext.BorderThickness = 1;
            this.gbtnNext.Cursor = System.Windows.Forms.Cursors.Hand;
            this.gbtnNext.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.gbtnNext.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.gbtnNext.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.gbtnNext.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.gbtnNext.FillColor = System.Drawing.Color.White;
            this.gbtnNext.FocusedColor = System.Drawing.SystemColors.MenuHighlight;
            this.gbtnNext.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbtnNext.ForeColor = System.Drawing.Color.Black;
            this.gbtnNext.HoverState.FillColor = System.Drawing.Color.SlateBlue;
            this.gbtnNext.HoverState.ForeColor = System.Drawing.Color.White;
            this.gbtnNext.Image = ((System.Drawing.Image)(resources.GetObject("gbtnNext.Image")));
            this.gbtnNext.ImageAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.gbtnNext.ImageSize = new System.Drawing.Size(35, 35);
            this.gbtnNext.Location = new System.Drawing.Point(806, 432);
            this.gbtnNext.Name = "gbtnNext";
            this.gbtnNext.Size = new System.Drawing.Size(154, 45);
            this.gbtnNext.TabIndex = 5;
            this.gbtnNext.Text = "Next";
            this.gbtnNext.Click += new System.EventHandler(this.gbtnNext_Click);
            // 
            // gtpLoginInfo
            // 
            this.gtpLoginInfo.BackColor = System.Drawing.Color.White;
            this.gtpLoginInfo.Controls.Add(this.pnlLoginInfo);
            this.gtpLoginInfo.Location = new System.Drawing.Point(4, 54);
            this.gtpLoginInfo.Name = "gtpLoginInfo";
            this.gtpLoginInfo.Padding = new System.Windows.Forms.Padding(3);
            this.gtpLoginInfo.Size = new System.Drawing.Size(988, 496);
            this.gtpLoginInfo.TabIndex = 1;
            this.gtpLoginInfo.Text = "LoginInfo";
            // 
            // pnlLoginInfo
            // 
            this.pnlLoginInfo.Controls.Add(this.gchkIsActive);
            this.pnlLoginInfo.Controls.Add(this.pictureBox3);
            this.pnlLoginInfo.Controls.Add(this.pictureBox5);
            this.pnlLoginInfo.Controls.Add(this.label4);
            this.pnlLoginInfo.Controls.Add(this.gtxtConfirmPassword);
            this.pnlLoginInfo.Controls.Add(this.label3);
            this.pnlLoginInfo.Controls.Add(this.gtxtPassword);
            this.pnlLoginInfo.Controls.Add(this.lblUserID);
            this.pnlLoginInfo.Controls.Add(this.pictureBox1);
            this.pnlLoginInfo.Controls.Add(this.pictureBox2);
            this.pnlLoginInfo.Controls.Add(this.label1);
            this.pnlLoginInfo.Controls.Add(this.gtxtUserName);
            this.pnlLoginInfo.Controls.Add(this.label2);
            this.pnlLoginInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLoginInfo.Location = new System.Drawing.Point(3, 3);
            this.pnlLoginInfo.Name = "pnlLoginInfo";
            this.pnlLoginInfo.Size = new System.Drawing.Size(982, 490);
            this.pnlLoginInfo.TabIndex = 33;
            // 
            // gchkIsActive
            // 
            this.gchkIsActive.Animated = true;
            this.gchkIsActive.AutoSize = true;
            this.gchkIsActive.Checked = true;
            this.gchkIsActive.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.gchkIsActive.CheckedState.BorderRadius = 2;
            this.gchkIsActive.CheckedState.BorderThickness = 1;
            this.gchkIsActive.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.gchkIsActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.gchkIsActive.Cursor = System.Windows.Forms.Cursors.Hand;
            this.gchkIsActive.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gchkIsActive.Location = new System.Drawing.Point(276, 220);
            this.gchkIsActive.Name = "gchkIsActive";
            this.gchkIsActive.Size = new System.Drawing.Size(88, 24);
            this.gchkIsActive.TabIndex = 32;
            this.gchkIsActive.Text = "Is Active";
            this.gchkIsActive.UncheckedState.BorderColor = System.Drawing.Color.Black;
            this.gchkIsActive.UncheckedState.BorderRadius = 2;
            this.gchkIsActive.UncheckedState.BorderThickness = 1;
            this.gchkIsActive.UncheckedState.FillColor = System.Drawing.Color.White;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(224, 171);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(29, 26);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 31;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox5
            // 
            this.pictureBox5.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox5.Image")));
            this.pictureBox5.Location = new System.Drawing.Point(224, 126);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(29, 26);
            this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox5.TabIndex = 30;
            this.pictureBox5.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(34, 171);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(156, 20);
            this.label4.TabIndex = 28;
            this.label4.Text = "Confirm Passord:";
            // 
            // gtxtConfirmPassword
            // 
            this.gtxtConfirmPassword.Animated = true;
            this.gtxtConfirmPassword.AutoRoundedCorners = true;
            this.gtxtConfirmPassword.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.gtxtConfirmPassword.DefaultText = "";
            this.gtxtConfirmPassword.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.gtxtConfirmPassword.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.gtxtConfirmPassword.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.gtxtConfirmPassword.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.gtxtConfirmPassword.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.gtxtConfirmPassword.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gtxtConfirmPassword.ForeColor = System.Drawing.Color.Black;
            this.gtxtConfirmPassword.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.gtxtConfirmPassword.Location = new System.Drawing.Point(273, 164);
            this.gtxtConfirmPassword.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gtxtConfirmPassword.MaxLength = 255;
            this.gtxtConfirmPassword.Name = "gtxtConfirmPassword";
            this.gtxtConfirmPassword.PasswordChar = '*';
            this.gtxtConfirmPassword.PlaceholderText = "";
            this.gtxtConfirmPassword.SelectedText = "";
            this.gtxtConfirmPassword.Size = new System.Drawing.Size(170, 36);
            this.gtxtConfirmPassword.TabIndex = 27;
            this.gtxtConfirmPassword.Validating += new System.ComponentModel.CancelEventHandler(this.gtxtConfirmPassword_Validating);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(93, 126);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 20);
            this.label3.TabIndex = 25;
            this.label3.Text = "Password:";
            // 
            // gtxtPassword
            // 
            this.gtxtPassword.Animated = true;
            this.gtxtPassword.AutoRoundedCorners = true;
            this.gtxtPassword.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.gtxtPassword.DefaultText = "";
            this.gtxtPassword.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.gtxtPassword.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.gtxtPassword.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.gtxtPassword.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.gtxtPassword.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.gtxtPassword.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gtxtPassword.ForeColor = System.Drawing.Color.Black;
            this.gtxtPassword.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.gtxtPassword.Location = new System.Drawing.Point(273, 118);
            this.gtxtPassword.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gtxtPassword.MaxLength = 255;
            this.gtxtPassword.Name = "gtxtPassword";
            this.gtxtPassword.PasswordChar = '*';
            this.gtxtPassword.PlaceholderText = "";
            this.gtxtPassword.SelectedText = "";
            this.gtxtPassword.Size = new System.Drawing.Size(170, 36);
            this.gtxtPassword.TabIndex = 24;
            this.gtxtPassword.Validating += new System.ComponentModel.CancelEventHandler(this.gtxtPassword_Validating);
            // 
            // lblUserID
            // 
            this.lblUserID.AutoSize = true;
            this.lblUserID.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserID.Location = new System.Drawing.Point(279, 38);
            this.lblUserID.Name = "lblUserID";
            this.lblUserID.Size = new System.Drawing.Size(40, 24);
            this.lblUserID.TabIndex = 23;
            this.lblUserID.Text = "???";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(224, 75);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(29, 26);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 22;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(224, 36);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(29, 26);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 21;
            this.pictureBox2.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(87, 81);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 20);
            this.label1.TabIndex = 17;
            this.label1.Text = "UserName:";
            // 
            // gtxtUserName
            // 
            this.gtxtUserName.Animated = true;
            this.gtxtUserName.AutoRoundedCorners = true;
            this.gtxtUserName.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.gtxtUserName.DefaultText = "";
            this.gtxtUserName.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.gtxtUserName.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.gtxtUserName.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.gtxtUserName.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.gtxtUserName.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.gtxtUserName.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gtxtUserName.ForeColor = System.Drawing.Color.Black;
            this.gtxtUserName.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.gtxtUserName.Location = new System.Drawing.Point(273, 72);
            this.gtxtUserName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gtxtUserName.MaxLength = 255;
            this.gtxtUserName.Name = "gtxtUserName";
            this.gtxtUserName.PlaceholderText = "";
            this.gtxtUserName.SelectedText = "";
            this.gtxtUserName.Size = new System.Drawing.Size(170, 36);
            this.gtxtUserName.TabIndex = 16;
            this.gtxtUserName.Validating += new System.ComponentModel.CancelEventHandler(this.gtxtUserName_Validating);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(116, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 20);
            this.label2.TabIndex = 13;
            this.label2.Text = "UserID:";
            // 
            // gbtnClose
            // 
            this.gbtnClose.Animated = true;
            this.gbtnClose.AutoRoundedCorners = true;
            this.gbtnClose.BackColor = System.Drawing.Color.Transparent;
            this.gbtnClose.BorderThickness = 1;
            this.gbtnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.gbtnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
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
            this.gbtnClose.Location = new System.Drawing.Point(704, 675);
            this.gbtnClose.Name = "gbtnClose";
            this.gbtnClose.Size = new System.Drawing.Size(154, 45);
            this.gbtnClose.TabIndex = 27;
            this.gbtnClose.Text = "Close";
            this.gbtnClose.Click += new System.EventHandler(this.gbtnClose_Click);
            // 
            // gbtnSave
            // 
            this.gbtnSave.Animated = true;
            this.gbtnSave.AutoRoundedCorners = true;
            this.gbtnSave.BackColor = System.Drawing.Color.Transparent;
            this.gbtnSave.BorderThickness = 1;
            this.gbtnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.gbtnSave.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.gbtnSave.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.gbtnSave.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.gbtnSave.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.gbtnSave.FillColor = System.Drawing.Color.White;
            this.gbtnSave.FocusedColor = System.Drawing.SystemColors.MenuHighlight;
            this.gbtnSave.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbtnSave.ForeColor = System.Drawing.Color.Black;
            this.gbtnSave.HoverState.FillColor = System.Drawing.Color.SlateBlue;
            this.gbtnSave.HoverState.ForeColor = System.Drawing.Color.White;
            this.gbtnSave.Image = ((System.Drawing.Image)(resources.GetObject("gbtnSave.Image")));
            this.gbtnSave.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.gbtnSave.ImageSize = new System.Drawing.Size(35, 35);
            this.gbtnSave.Location = new System.Drawing.Point(864, 675);
            this.gbtnSave.Name = "gbtnSave";
            this.gbtnSave.Size = new System.Drawing.Size(154, 45);
            this.gbtnSave.TabIndex = 26;
            this.gbtnSave.Text = "Save";
            this.gbtnSave.Click += new System.EventHandler(this.gbtnSave_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // frmAddUpdateUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.gbtnClose;
            this.ClientSize = new System.Drawing.Size(1035, 733);
            this.Controls.Add(this.gbtnClose);
            this.Controls.Add(this.gbtnSave);
            this.Controls.Add(this.gtcUserInfo);
            this.Controls.Add(this.lblHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmAddUpdateUser";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add New User";

            this.Load += new System.EventHandler(this.frmAdd_EditNewUser_Load);
            this.gtcUserInfo.ResumeLayout(false);
            this.gtpPersonInfo.ResumeLayout(false);
            this.gtpLoginInfo.ResumeLayout(false);
            this.pnlLoginInfo.ResumeLayout(false);
            this.pnlLoginInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblHeader;
        private Guna.UI2.WinForms.Guna2TabControl gtcUserInfo;
        private System.Windows.Forms.TabPage gtpPersonInfo;
        private System.Windows.Forms.TabPage gtpLoginInfo;
        private Guna.UI2.WinForms.Guna2Button gbtnNext;
        private Guna.UI2.WinForms.Guna2Button gbtnClose;
        private Guna.UI2.WinForms.Guna2Button gbtnSave;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2TextBox gtxtUserName;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblUserID;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.Label label4;
        private Guna.UI2.WinForms.Guna2TextBox gtxtConfirmPassword;
        private System.Windows.Forms.Label label3;
        private Guna.UI2.WinForms.Guna2TextBox gtxtPassword;
        private Guna.UI2.WinForms.Guna2CheckBox gchkIsActive;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private DVLDPresentation.Controls.ctrlPersonCardWithFilter ctrlPersonCardWithFilter1;
        private System.Windows.Forms.Panel pnlLoginInfo;
    }
}
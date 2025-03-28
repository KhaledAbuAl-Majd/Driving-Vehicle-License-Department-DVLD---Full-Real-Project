namespace DVLDPresentation
{
    partial class Form1
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
            this.ctrAdd_EditPerson1 = new DVLDPresentationTier.Controls.ctrAdd_EditPerson();
            this.SuspendLayout();
            // 
            // ctrAdd_EditPerson1
            // 
            this.ctrAdd_EditPerson1.BackColor = System.Drawing.Color.White;
            this.ctrAdd_EditPerson1.Location = new System.Drawing.Point(42, 44);
            this.ctrAdd_EditPerson1.Name = "ctrAdd_EditPerson1";
            this.ctrAdd_EditPerson1.Size = new System.Drawing.Size(888, 379);
            this.ctrAdd_EditPerson1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(977, 450);
            this.Controls.Add(this.ctrAdd_EditPerson1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.ResumeLayout(false);

        }

        #endregion

        private DVLDPresentationTier.Controls.ctrAdd_EditPerson ctrAdd_EditPerson1;
    }
}


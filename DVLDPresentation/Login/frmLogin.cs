using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLDBusiness;
using DVLDConstant;
using DVLDPresentation.Login_HomePage;
using DVLDPresentation.Properties;

namespace DVLDPresentation.Login_MainPage
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        void _EmptyUserNamePassword()
        {
            gtxtUserName.Text = "";
            gtxtPassword.Text = "";
        }

        private void gtxtPassword_IconRightClick(object sender, EventArgs e)
        {
            if (gtxtPassword.UseSystemPasswordChar)
            {
                gtxtPassword.UseSystemPasswordChar = false;
                gtxtPassword.IconRight = Resources.crossed_eye_icon_256370;
            }
            else
            {
                gtxtPassword.UseSystemPasswordChar = true;
                gtxtPassword.IconRight = Resources.eye_icon_256043;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gbtnLogin_Click(object sender, EventArgs e)
        {
            string EnteredUserName = gtxtUserName.Text.Trim();
            string Enteredpassword = gtxtPassword.Text.Trim();

            string Salt = clsUser.GetSaltByUserName(EnteredUserName);

            if (Salt != null)
            {
                string Hashedpassword = clsSecurity.ComputeHash(clsSecurity.GetSaltedPassword(Enteredpassword, Salt));

                clsUser User = clsUser.FindByUserNameAndPassword(EnteredUserName, Hashedpassword);

                if (User != null)
                {
                    if (!User.IsActive)
                    {
                        MessageBox.Show("Your account is not Active, Contact Admin.", "In Active Account", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        clsLogger.LogAtEventLog($"User With ID = {User.UserID} try to login But, He was refused to entry because he is not Active!");
                        gtxtUserName.Focus();
                        return;
                    }

                    if (gchkRemeberMe.Checked)
                        clsGlobal.RememberUsernameAndPassword(EnteredUserName, Enteredpassword);
                    else
                    {
                        _EmptyUserNamePassword();
                        clsGlobal.RememberUsernameAndPassword(null, null);
                    }

                    clsLogger.LogAtEventLog($"[LOGIN SUCCESS] User ID = {User.UserID}, Username = {User.UserName}, Login Time = {DateTime.Now}",EventLogEntryType.Information);

                    clsGlobal.CurrentUser = User;
                    this.Hide();
                    frmMain frm = new frmMain(this);
                    frm.ShowDialog();
                    gtxtUserName.Focus();

                    return;
                }
            }

            MessageBox.Show("Invalid Username/Password.", "Wrong Credentials", MessageBoxButtons.OK, MessageBoxIcon.Error);
            clsLogger.LogAtEventLog($"Invalid login attempt. Username: {EnteredUserName}");
            gtxtUserName.Focus();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            string UserName = "",Password = "";

            if(clsGlobal.GetStoredCredential(ref UserName,ref Password))
            {
                gtxtUserName.Text = UserName;
                gtxtPassword.Text = Password;
                gchkRemeberMe.Enabled = true;
                gtxtPassword.IconRight = null;
            }
            else
            {
                gchkRemeberMe.Checked = false;
                gtxtPassword.IconRight = Resources.eye_icon_256043;
            }
        }

        private void gtxtPassword_TextChanged(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(gtxtPassword.Text) && (gtxtPassword.IconRight == null))
            {
                gtxtPassword.UseSystemPasswordChar = true;
                gtxtPassword.IconRight = Resources.eye_icon_256043;
            }
        }
    }
}

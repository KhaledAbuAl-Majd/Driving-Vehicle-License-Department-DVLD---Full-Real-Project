using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLDBusiness;
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
            clsUser User = clsUser.FindByUserNameAndPassword(gtxtUserName.Text.Trim(), gtxtPassword.Text.Trim());

            if (User != null)
            {
                if (!User.IsActive)
                {
                    MessageBox.Show("Your accound is not Active, Contact Admin.", "In Active Account", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    gtxtUserName.Focus();
                    return;
                }

                if (gchkRemeberMe.Checked)
                    clsGlobal.RememberUsernameAndPassword(gtxtUserName.Text.Trim(), gtxtPassword.Text.Trim());
                else
                {
                    _EmptyUserNamePassword();
                    clsGlobal.RememberUsernameAndPassword("", "");
                }

                clsGlobal.CurrentUser = User;
                this.Hide();
                frmMain frm = new frmMain(this);
                frm.ShowDialog();
                gtxtUserName.Focus();
            }
            else
            {
                MessageBox.Show("Invalid Username/Password.", "Wrong Credentials", MessageBoxButtons.OK, MessageBoxIcon.Error);
                gtxtUserName.Focus();
            }

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

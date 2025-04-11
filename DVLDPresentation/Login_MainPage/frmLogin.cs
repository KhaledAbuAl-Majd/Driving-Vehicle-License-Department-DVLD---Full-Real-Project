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

        private void _EmptyUserName_Password()
        {
            gtxtUserName.Text = "";
            gtxtPassword.Text = "";
        }
        private void _RemeberMeLoadDataFromFile()
        {
            FileInfo fileInfo = new FileInfo(clsGlobalSettings.PathOfRemeberMeFile);

            if (fileInfo.Exists && fileInfo.Length != 0)
            {
                string UserName = "";
                string Password = "";
                bool RemeberMeChecked = false;

                using (StreamReader reader = new StreamReader(clsGlobalSettings.PathOfRemeberMeFile))
                {

                    UserName = reader.ReadLine();
                    Password = reader.ReadLine();
                    RemeberMeChecked = Convert.ToBoolean(reader.ReadLine());

                    if (RemeberMeChecked)
                    {
                        gtxtUserName.Text = UserName;
                        gtxtPassword.Text = Password;
                        gchkRemeberMe.Checked = RemeberMeChecked;
                        gtxtPassword.UseSystemPasswordChar = true;
                        gtxtPassword.IconRight = null;
                    }
                }
            }
            else
            {
                _EmptyUserName_Password();
                gtxtPassword.UseSystemPasswordChar = true;
                gtxtPassword.IconRight = Resources.eye_icon_256043;
            }
        }
        private void _RemeberMeAddDataToFile(bool IsRemeberMeChecked)
        {
            if (IsRemeberMeChecked)
            {
                using (StreamWriter writer = new StreamWriter(clsGlobalSettings.PathOfRemeberMeFile))
                {
                    writer.WriteLine(gtxtUserName.Text);
                    writer.WriteLine(gtxtPassword.Text);
                    writer.WriteLine(gchkRemeberMe.Checked);
                }
            }
            else
            {
                File.WriteAllText(clsGlobalSettings.PathOfRemeberMeFile, "");
            }
           
            
        }
        private void _ErrorMessage(string Message)
        {
            MessageBox.Show(Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void _CloseLoginForm()
        {
            this.Close();
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

        private void button1_Click(object sender, EventArgs e)
        {
            _CloseLoginForm();
        }

        private void gbtnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(gtxtUserName.Text) || string.IsNullOrWhiteSpace(gtxtPassword.Text))
            {
                _ErrorMessage("Invalid UserName/Passsword Enter a Correct one!");
                return;
            }
       

            clsUsers User = clsUsers.Find(gtxtUserName.Text);

            if(User == null)
            {
                _ErrorMessage("Invalid UserName/Passsword Enter a Correct one!");
                return;
            }

            if(User.Password != gtxtPassword.Text)
            {
                _ErrorMessage("Invalid UserName/Passsword Enter a Correct one!");
                return;
            }

            if (!User.IsActive)
            {
                _ErrorMessage("Your Account Is Blocked, Please Contact You Admin!");
                return;
            }

            clsGlobalSettings.LoggedInUser = User;

            _RemeberMeAddDataToFile(gchkRemeberMe.Checked);

            frmMain frm = new frmMain();
            frm.ShowDialog();
            //_CloseLoginForm();
            _RemeberMeLoadDataFromFile();

        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            _RemeberMeLoadDataFromFile();
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

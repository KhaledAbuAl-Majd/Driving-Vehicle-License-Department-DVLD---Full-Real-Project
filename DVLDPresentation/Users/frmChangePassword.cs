using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLDBusiness;
using Guna.UI2.WinForms;

namespace DVLDPresentation.Users
{
    public partial class frmChangePassword : Form
    {
        clsUser User;
        bool IsSave = false;

        public event Action OnClose;
        public frmChangePassword(int UserID)
        {
            InitializeComponent();

            User = clsUser.FindByUserID(UserID);

            if (User != null)
            {
                //ctrPersonCard1.PersonID = User.PersonID;
                _FillLoginInfomationFromUserToForm();
            }
        }

        void _GiveInitialValueForInTagPasswordsValidation()
        {
            gtxtCurrentPassword.Tag = false;
            gtxtNewPassword.Tag = false;
            gtxtConfirmPassword.Tag = false;
        }
        void _FillLoginInfomationFromUserToForm()
        {
            lblUserID.Text = User.UserID.ToString();
            lblUserName.Text = User.UserName;
            lblIsActive.Text = (User.IsActive) ? "Yes" : "No";
        }
        void _MessageIfValidatonFailedOnSave(string MessageText)
        {
            MessageBox.Show(MessageText, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private bool _CheckErrorProviderForTextBox(Guna2TextBox txt,string ErrorText)
        {
            
            if (string.IsNullOrWhiteSpace(txt.Text))
            {
                errorProvider1.SetError(txt, ErrorText);
                txt.Tag = false;
                return true;
            }
            else
            {
                errorProvider1.SetError(txt, "");
                txt.Tag = true;
                return false;
            }
        }
        private void gtxtCurrentPassword_Validating(object sender, CancelEventArgs e)
        {
            Guna2TextBox txt = (Guna2TextBox)sender;

            if(!_CheckErrorProviderForTextBox(txt, "CurrentPassword must have a value!"))
            {
                if(txt.Text != User.Password)
                {
                    errorProvider1.SetError(txt, "Please Enter The Correct Password!");
                    txt.Tag = false;
                }
                else
                {
                    errorProvider1.SetError(txt, "");
                    txt.Tag = true;
                }
            }
        }

        private void gtxtNewPassword_Validating(object sender, CancelEventArgs e)
        {
            _CheckErrorProviderForTextBox((Guna2TextBox)sender, "New Password mush have a value!");
        }

        private void gtxtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            Guna2TextBox txt = (Guna2TextBox)sender;

            if(_CheckErrorProviderForTextBox(txt,"Confirm Password must have a value!"))
            {
                if (gtxtConfirmPassword.Text != gtxtNewPassword.Text)
                {
                    errorProvider1.SetError(txt, "Password Confirmation does not match Password!");
                    txt.Tag = false;
                }
                else
                {
                    errorProvider1.SetError(txt, "");
                    txt.Tag = true;
                }
            }
        }

        private void gbtnClose_Click(object sender, EventArgs e)
        {
            if (IsSave)
            {
                if (OnClose != null)
                    OnClose();
            }

            this.Close();
        }

        private void frmChangePassword_Load(object sender, EventArgs e)
        {
            _GiveInitialValueForInTagPasswordsValidation();
        }

        private void gbtnSave_Click(object sender, EventArgs e)
        {
            bool CurrentPasswordValidatoinResult = (bool)gtxtCurrentPassword.Tag;
            bool NewPasswordValidatoinResult = (bool)gtxtNewPassword.Tag;
            bool ConfirmPasswordValidationResult = (bool)gtxtConfirmPassword.Tag;

            if (!CurrentPasswordValidatoinResult)
            {
                _MessageIfValidatonFailedOnSave("Please Enter a Correct Current Password");
                return;
            }

            if (!NewPasswordValidatoinResult)
            {
                _MessageIfValidatonFailedOnSave("Please Enter a Correct New Password!");
                return;
            }

            if (!ConfirmPasswordValidationResult)
            {
                _MessageIfValidatonFailedOnSave("Please Enter a Correct Password Confimations!");
            }

            User.Password = gtxtNewPassword.Text;

            if (User.Save())
            {
                MessageBox.Show("Password Updated Successfully!", "Succeed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                IsSave = true;
            }
            else
            {
                MessageBox.Show("Password Failed To Update", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                IsSave = false;
            }

        }
    }
}

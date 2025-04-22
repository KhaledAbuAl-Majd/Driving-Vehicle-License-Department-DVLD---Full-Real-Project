using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Lifetime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLDBusiness;
using Guna.UI2.WinForms;
using static System.Net.Mime.MediaTypeNames;

namespace DVLDPresentation.Users
{
    public partial class frmAddUpdateUser : Form
    {
        public event Action OnClose;
        public bool IsSave = false;
        public int UserID = -1;
        private void _MessageIfTxtFailedInValidating(string MessageText)
        {  
            MessageBox.Show(MessageText, "Validation Error",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        private clsUser User;

        private void _GiveTextBoxesInitialValueForValidating(bool Value)
        {
            gtxtUserName.Tag = Value;
            gtxtPassword.Tag = Value;
            gtxtConfirmPassword.Tag = Value;

        }
        void _FillDataFromFormToObject()
        {
            User.UserName = gtxtUserName.Text;
            //User.PersonID = ctrlPersonCardWithFilter1.PersonID;
            User.Password = gtxtPassword.Text;
            User.IsActive = gchkIsActive.Checked;
        }
        void _FillDataFromObjectToForm()
        {
            lblUserID.Text = User.UserID.ToString();
            gtxtUserName.Text = User.UserName;
            gtxtPassword.Text = User.Password;
            gtxtConfirmPassword.Text = User.Password;
            gchkIsActive.Checked = User.IsActive;
        }
        private  void _Save()
        {
            ////bool IsPersonSelected = (ctrlPersonCardWithFilter1.PersonID != -1);
            //bool UserNameValidatingResult = (bool)gtxtUserName.Tag;
            //bool PasswordValidatingResult = (bool)gtxtPassword.Tag;
            //bool ConfirmPassword = (bool)gtxtConfirmPassword.Tag;

            ////if (!IsPersonSelected)
            ////{
            ////    _MessageIfTxtFailedInValidating("Please Select a Person values!");
            ////    return;
            ////}

            //else if(!UserNameValidatingResult)
            //{
            //    _MessageIfTxtFailedInValidating("Please enter correct UserName!");
            //    gtxtUserName.Focus();
            //    return;
            //}

            //else if (!PasswordValidatingResult)
            //{
            //    _MessageIfTxtFailedInValidating("Please enter a correct Password!");
            //    gtxtPassword.Focus();
            //    return;
            //}

            //else if (!ConfirmPassword)
            //{
            //    _MessageIfTxtFailedInValidating("Please enter a correct Password!");
            //    gtxtConfirmPassword.Focus();
            //    return;
            //}

            _FillDataFromFormToObject();

            clsUser.enMode prevMode = User.Mode;
            
            if (User.Save())
            {
                IsSave = true;
                if (prevMode == clsUser.enMode.Update)
                    MessageBox.Show($"User Updated Successfuly", "Result"
                   , MessageBoxButtons.OK, MessageBoxIcon.Information);

                else
                {
                    _UpdateUserModeAfterAddUser();
                    MessageBox.Show($"User Addess Successfuly With ID = {User.UserID}", "Result"
                        , MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                IsSave = false;
            }
        }
        private void _ChangeHeader(string HeaderText)
        {
            lblHeader.Text = HeaderText;
        }
        void _AddNewUserMode()
        {
            User = new clsUser();
            _GiveTextBoxesInitialValueForValidating(false);
        }
        void _UpdateUserModeAfterAddUser()
        {
            _ChangeHeader("Update User");
            lblUserID.Text = User.UserID.ToString();
        }
        void _UpdateUserModeFromEditOption()
        {
            //ctrlPersonCardWithFilter1.FilterEnabled = false;
            User = clsUser.FindByUserID(UserID);

            if(User != null)
            {
                _GiveTextBoxesInitialValueForValidating(true);
                //ctrlPersonCardWithFilter1.LoadPersonInfo(clsUser.FindByUserID(UserID).PersonID);
                _ChangeHeader("Update User");
                _FillDataFromObjectToForm();
            }
        }

        public frmAddUpdateUser(int UserID)
        {
            InitializeComponent();
            this.UserID = UserID;
            if(UserID != -1)
            {
                _UpdateUserModeFromEditOption();
            }
            else
            {
                _AddNewUserMode();
            }

        }

        private void gbtnNext_Click(object sender, EventArgs e)
        {
            gtabcAddNewPerson.SelectedIndex = 1;
        }

        private void gtabcAddNewPerson_Selecting(object sender, TabControlCancelEventArgs e)
        {
            //if ((gtabcAddNewPerson.SelectedIndex == 1) && ctrlPersonCardWithFilter1.PersonID == -1)
            //{
            //    e.Cancel = true;
            //    MessageBox.Show("Please Select a Person", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            //else
            //{
            //    e.Cancel = false;
            //}
        }

        private void gtxtUserName_Validating(object sender, CancelEventArgs e)
        {
            Guna2TextBox txt = (Guna2TextBox)sender;

            if (string.IsNullOrWhiteSpace(txt.Text))
            {
                errorProvider1.SetError(txt, "UserName Must have a value!");
                txt.Tag = false;
                return;
            }

            if (clsUser.IsUserExist(txt.Text) && User.UserName != txt.Text)
            {
                errorProvider1.SetError(txt, "UserName Is Already Exist!");
                txt.Tag = false;
            }
            else
            {
                errorProvider1.SetError(txt, "");
                txt.Tag = true;
            }
        }

        bool _CheckErrorProviderForPasswords(Guna2TextBox txt,string ErrorText)
        {
            if (string.IsNullOrWhiteSpace(txt.Text))
            {
                errorProvider1.SetError(txt, "Password Must have a value!");
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
        private void gtxtPassword_Validating(object sender, CancelEventArgs e)
        {
            Guna2TextBox txt = (Guna2TextBox)sender;

            _CheckErrorProviderForPasswords(txt, "Password Must have a value!");
        }

        private void gtxtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            Guna2TextBox txt = (Guna2TextBox)sender;

            if (_CheckErrorProviderForPasswords(txt, "Confirm Password must have a value!"))
                return;

            if (gtxtConfirmPassword.Text != gtxtPassword.Text)
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

        private void gbtnSave_Click(object sender, EventArgs e)
        {
            _Save();
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

        private void frmAdd_EditNewUser_Load(object sender, EventArgs e)
        {
            if(UserID == -1)
            {
                _AddNewUserMode();
            }
            else
            {
                _UpdateUserModeFromEditOption();
            }
        }
    }
}

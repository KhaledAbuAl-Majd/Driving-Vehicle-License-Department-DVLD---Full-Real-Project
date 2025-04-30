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
        public event Action OnSave;
        public enum enMode { AddNew = 0, Update = 1 }
        enMode _Mode;
        private int _UserID = -1;
        private clsUser _User;

        public frmAddUpdateUser()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
        }
        public frmAddUpdateUser(int UserID)
        {
            InitializeComponent();
            this._UserID = UserID;
            _Mode = enMode.Update;
        }
        void _ChangeEnaplityForLoginInfoPanel(bool value)
        {
            pnlLoginInfo.Enabled = value;
        }
        void _ChageEnaplityOfSaveButton(bool value)
        {
            gbtnSave.Enabled = value;
        }
        private void _ChangeHeader(string HeaderText)
        {
            lblHeader.Text = HeaderText;
        }
        void _ChangeFormText(string Text)
        {
            this.Text = Text;
        }
        void _AddNewUserMode()
        {
            _User = new clsUser();
            _ChangeHeader("Add New User");
            _ChangeFormText("Add New User");

            _ChageEnaplityOfSaveButton(false);
            _ChangeEnaplityForLoginInfoPanel(false);
            ctrlPersonCardWithFilter1.FilterFocus();
        }
        void _LoadData()
        {
            lblUserID.Text = _User.UserID.ToString();
            gtxtUserName.Text = _User.UserName;
            gtxtPassword.Text = _User.Password;
            gtxtConfirmPassword.Text = _User.Password;
            gchkIsActive.Checked = _User.IsActive;
            ctrlPersonCardWithFilter1.LoadPersonInfo(_User.PersonID);
        }
        void _UpdateUserMode()
        {
            _User = clsUser.FindByUserID(_UserID);
            ctrlPersonCardWithFilter1.FilterEnabled = false;

            if(_User == null)
            {
                MessageBox.Show("No User with ID = " + _User, "User Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
                return;
            }

            _ChangeHeader("Update User");
            _ChangeFormText("Update User");          
            _ChageEnaplityOfSaveButton(true);
            _ChangeEnaplityForLoginInfoPanel(true);
            _LoadData();
        }

        private void frmAdd_EditNewUser_Load(object sender, EventArgs e)
        {
            switch (_Mode)
            {
                case enMode.AddNew:
                    _AddNewUserMode();
                    break;

                case enMode.Update:
                    _UpdateUserMode();
                    break;
            }
        }

        private void gbtnSave_Click(object sender, EventArgs e)
        {
            //if (ctrlPersonCardWithFilter1.PersonID == -1)
            //{
            //    MessageBox.Show("Please Select a Person", "Select a Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    ctrlPersonCardWithFilter1.FilterFocus();
            //    return;
            //}

            if (!ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro",
                   "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _User.PersonID = ctrlPersonCardWithFilter1.PersonID;
            _User.UserName = gtxtUserName.Text.Trim();
            _User.Password = gtxtPassword.Text.Trim();
            _User.IsActive = gchkIsActive.Checked;


            if (_User.Save())
            {
                lblUserID.Text = _User.UserID.ToString();
                _Mode = enMode.Update;
                _ChangeHeader("Update User");
                _ChangeFormText("Update User");
                _UserID = _User.UserID;

                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (OnSave != null)
                    OnSave();

            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void gtxtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (gtxtConfirmPassword.Text.Trim() != gtxtPassword.Text.Trim())
            {
                e.Cancel = true;
                errorProvider1.SetError(gtxtConfirmPassword, "Password Confirmation does not match Password!");

            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(gtxtConfirmPassword, "");
            }
        }

        private void gtxtPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(gtxtPassword.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(gtxtPassword, "Password cannot be blank!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(gtxtPassword, "");
            }
        }

        private void gtxtUserName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(gtxtUserName.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(gtxtUserName, "UserName cannot be blank!");
                return;
            }

            if (clsUser.IsUserExist(gtxtUserName.Text.Trim()) && _User.UserName.ToLower() != gtxtUserName.Text.Trim().ToLower())
            {
                e.Cancel = true;
                errorProvider1.SetError(gtxtUserName, "Username is used by another user");
                return;
            }

            e.Cancel = false;
            errorProvider1.SetError(gtxtUserName, "");
        }

        private void gbtnNext_Click(object sender, EventArgs e)
        {        
            //if(_Mode == enMode.Update)
            //{
            //    gtcUserInfo.SelectedTab = gtpLoginInfo;
            //    return;
            //}

            //Add new Mode
            //if (ctrlPersonCardWithFilter1.PersonID != -1)
            //{
            //    if (clsUser.IsUserExistForPersonID(ctrlPersonCardWithFilter1.PersonID))
            //    {
            //        MessageBox.Show("Selected Person already has a user, choose another one.", "Select another Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        ctrlPersonCardWithFilter1.FilterFocus();
            //    }
            //    else
            //    {
            //        _ChageEnaplityOfSaveButton(true);
            //        _ChangeEnaplityForLoginInfoPanel(true);
            //        gtcUserInfo.SelectedTab = gtpLoginInfo;
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("Please Select a Person", "Select a Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    ctrlPersonCardWithFilter1.FilterFocus();
            //}   

            gtcUserInfo.SelectedTab = gtpLoginInfo;
        }

        private void gbtnClose_Click(object sender, EventArgs e)
        {          
            this.Close();             
        }

        private void ctrlPersonCardWithFilter1_OnPersonSelected(int obj)
        {
            if (ctrlPersonCardWithFilter1.PersonID != -1)
            {
                if (clsUser.IsUserExistForPersonID(ctrlPersonCardWithFilter1.PersonID))
                {
                    MessageBox.Show("Selected Person already has a user, choose another one.", "Select another Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    _ChageEnaplityOfSaveButton(false);
                    _ChangeEnaplityForLoginInfoPanel(false);
                    ctrlPersonCardWithFilter1.FilterFocus();
                }
                else
                {
                    _ChageEnaplityOfSaveButton(true);
                    _ChangeEnaplityForLoginInfoPanel(true);
                    //gtcUserInfo.SelectedTab = gtpLoginInfo;
                }
            }
            else
            {
                MessageBox.Show("Please Select a Person", "Select a Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _ChageEnaplityOfSaveButton(false);
                _ChangeEnaplityForLoginInfoPanel(false);
                ctrlPersonCardWithFilter1.FilterFocus();
            }
        }
    }
}

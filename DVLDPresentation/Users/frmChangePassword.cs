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
using DVLDConstant;
using Guna.UI2.WinForms;

namespace DVLDPresentation.Users
{
    public partial class frmChangePassword : Form
    {
        int _UserID;
        clsUser _User;
        public frmChangePassword(int UserID)
        {
            InitializeComponent();
            this._UserID = UserID;
        }

        void _ResetDefaultValues()
        {
            gtxtCurrentPassword.Text = "";
            gtxtNewPassword.Text = "";
            gtxtConfirmPassword.Text = "";
            gtxtCurrentPassword.Focus();
        }

        private void frmChangePassword_Load(object sender, EventArgs e)
        {
            _ResetDefaultValues();

            _User = clsUser.FindByUserID(_UserID);

            if (_User == null)
            {
                MessageBox.Show("Could not Find User with id = " + _UserID,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();

                return;
            }

            ctrUserCard1.LoadUserInfo(_UserID);
        }

        private void gtxtCurrentPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(gtxtCurrentPassword.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(gtxtCurrentPassword, "Current Password cannot be blank!");
                return;
            }

            if (clsSecurity.ComputeHash(gtxtCurrentPassword.Text.Trim() + _User.Salt) != _User.Password)
            {
                e.Cancel = true;
                errorProvider1.SetError(gtxtCurrentPassword, "Current password is wrong!");
                return;
            }
    

            e.Cancel = false;
            errorProvider1.SetError(gtxtCurrentPassword, "");
        }

        private void gtxtNewPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(gtxtNewPassword.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(gtxtNewPassword, "Password cannot be blank!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(gtxtNewPassword, "");
            }
        }

        private void gtxtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (gtxtConfirmPassword.Text.Trim() != gtxtNewPassword.Text.Trim())
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

        private void gbtnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro",
                   "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _User.Password = gtxtNewPassword.Text.Trim();

            if (_User.ChangePassword())
            {
                MessageBox.Show("Password Changed Successfully.",
                  "Saved.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _ResetDefaultValues();
                clsLogger.LogAtEventLog($"{_User.UserName}'s Password Changed by {_User.UserName} at {DateTime.Now}", System.Diagnostics.EventLogEntryType.Information);
            }
            else
            {
                MessageBox.Show("An Error Occured, Password did not change.",
                  "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                clsLogger.LogAtEventLog($"{_User.UserName}'s Password failed to change by {_User.UserName} at {DateTime.Now}", System.Diagnostics.EventLogEntryType.Error);

            }

        }

        private void gbtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

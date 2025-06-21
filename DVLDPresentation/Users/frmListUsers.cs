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
using DVLDPresentation.Global_Classes;

namespace DVLDPresentation.Users
{
    public partial class frmListUsers : Form
    {
        public frmListUsers()
        {
            InitializeComponent();
        }

        DataView _dvUsers;

        private void _AddNewUser()
        {
            frmAddUpdateUser frm = new frmAddUpdateUser();
            frm.OnSave += _RefreshUsersList;
            frm.ShowDialog();
        }
        void _ShowDetails()
        {
            frmUserInfo frm = new frmUserInfo(Convert.ToInt32(dgvUsers.CurrentRow.Cells[0].Value));
            frm.ShowDialog();
        }
        private void _Show_HideTextFilter(bool value)
        {
            gtxtFilterValue.Visible = value;
        }
        private void _Show_HideCBIsActiveFilter(bool value)
        {
            gcbIsActive.Visible = value;
        }
        private void _FilterData(string FilterText)
        {
            if (_dvUsers != null)
            {
                _dvUsers.RowFilter = FilterText;
                lblNumOfRecords.Text = _dvUsers.Count.ToString();
            }
        }
        private void _FilterByAtDesign()
        {
            if (gcbFilterBy.Text == "Is Active")
            {
                _Show_HideTextFilter(false);
                _Show_HideCBIsActiveFilter(true);

                if (gcbIsActive.SelectedIndex != 0)
                    gcbIsActive.SelectedIndex = 0;
                else
                    _FilterData("");

                gcbIsActive.Focus();
                return;
            }

            _Show_HideTextFilter(gcbFilterBy.Text != "None");
            _Show_HideCBIsActiveFilter(false);

            if (gtxtFilterValue.Text != "")
                gtxtFilterValue.Text = "";
            else
                _FilterData("");

            gtxtFilterValue.Focus();
        }
        void _RefreshUsersList()
        {
            _dvUsers = clsUser.GetAllUsers().DefaultView;
            dgvUsers.DataSource = _dvUsers;
            gcbFilterBy.SelectedIndex = 0;
        }
        private void frmManageUsers_Load(object sender, EventArgs e)
        {
            _RefreshUsersList();

            if (_dvUsers.Count > 0)
            {
                dgvUsers.Columns[0].HeaderText = "User ID";
                dgvUsers.Columns[0].Width = 105;

                dgvUsers.Columns[1].HeaderText = "Person ID";
                dgvUsers.Columns[1].Width = 110;

                dgvUsers.Columns[2].HeaderText = "Full Name";
                dgvUsers.Columns[2].Width = 350;

                dgvUsers.Columns[3].HeaderText = "UserName";
                dgvUsers.Columns[3].Width = 120;

                dgvUsers.Columns[4].HeaderText = "Is Active";
                dgvUsers.Columns[4].Width = 90;
            }
        }

        private void gcmFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            _FilterByAtDesign();
        }

        private void gtxtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";

            switch (gcbFilterBy.Text)
            {
                case "User ID":
                    FilterColumn = "UserID";
                    break;

                case "UserName":
                    FilterColumn = "UserName";
                    break;

                case "Person ID":
                    FilterColumn = "PersonID";
                    break;

                case "Full Name":
                    FilterColumn = "FullName";
                    break;

                default:
                    FilterColumn = "None";
                    break;

            }

            if (string.IsNullOrWhiteSpace(gtxtFilterValue.Text) || FilterColumn == "None")
            {
                //to make filter is none get all people
                _FilterData("");
                return;
            }

            if (FilterColumn == "PersonID" || FilterColumn == "UserID")
                _FilterData($"{FilterColumn} = " + gtxtFilterValue.Text.Trim());
            else
                _FilterData($"{FilterColumn}  like '{gtxtFilterValue.Text.Trim()}%'");
        }

        private void gcbIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (gcbIsActive.Text == "All")
                _FilterData("");

            else if (gcbIsActive.Text == "Yes")
                _FilterData("IsActive = 1");

            else if (gcbIsActive.Text == "No")
                _FilterData("IsActive = 0");
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            _AddNewUser();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //User ID
            frmAddUpdateUser frm = new frmAddUpdateUser(Convert.ToInt32(dgvUsers.CurrentRow.Cells[0].Value));
            frm.OnSave += _RefreshUsersList;
            frm.ShowDialog();
        }

        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _AddNewUser();
        }

        private void dgvUsers_DoubleClick(object sender, EventArgs e)
        {
            _ShowDetails();
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _ShowDetails();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChangePassword frm = new frmChangePassword(Convert.ToInt32(dgvUsers.CurrentRow.Cells[0].Value));
            frm.ShowDialog();
        }

        private void gtxtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (gcbFilterBy.Text == "Person ID" || gcbFilterBy.Text == "User ID")
                e.Handled = (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar));
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Cannot be deleted
            int AdminID = 1;

            int UserID = Convert.ToInt32(dgvUsers.CurrentRow.Cells[0].Value);

            if(UserID == AdminID)
            {
                MessageBox.Show("Admin Cannot be deleted!", "Not Allowed", MessageBoxButtons.OK,
                       MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show("Are you sure you want to delete User With ID = [" + UserID+ "]", "Confirm Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                clsUser user = clsUser.FindByUserID(UserID);
                //User ID
                if (clsUser.DeleteUser(UserID))
                {
                    MessageBox.Show("User has been deleted successfully", "Deleted", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    _RefreshUsersList();
                    clsLogger.LogAtEventLog($"User With UserName = {user.UserName} has beed deleted by UserName = {clsGlobal.CurrentUser.UserName} At Time = {DateTime.Now}");
                }
                else
                    MessageBox.Show("User is not deleted due to data connected to it.", "Failed", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
            }
        }

        private void gbtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void sendEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsUtil.FeatureIsNotImplemented();
        }

        private void phoneCallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsUtil.FeatureIsNotImplemented();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            _RefreshUsersList();
        }
    }
}

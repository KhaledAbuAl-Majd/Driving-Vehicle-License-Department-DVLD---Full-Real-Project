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
            frmUserInfo frm = new frmUserInfo(Convert.ToInt32(dgvUsers.SelectedCells[0].Value));
            frm.ShowDialog();
        }
        private void _GetTextFilterEmpty()
        {
            gtxtFilterValue.Text = "";
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
            if (gcbFilterBy.Text == "None")
            {
                _Show_HideTextFilter(false);
                _Show_HideCBIsActiveFilter(false);
                _FilterData("");
            }
            else if(gcbFilterBy.Text == "Is Active")
            {
                _Show_HideTextFilter(false);
                _Show_HideCBIsActiveFilter(true);
                gcbIsActive.SelectedIndex = 0;
            }
            else
            {
                _Show_HideTextFilter(true);
                _Show_HideCBIsActiveFilter(false);
                _FilterData("");
            }

            _GetTextFilterEmpty();
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

        private void gcmFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            _FilterByAtDesign();
        }

        private void gtxtFilterValue_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(gtxtFilterValue.Text))
            {
                //to make filter is none get all people
                _FilterData("");
                return;
            }
            switch (gcbFilterBy.Text)
            {
                case "None":
                    _FilterData("");
                    break;

                case "User ID":
                    _FilterData("UserID = " + gtxtFilterValue.Text);
                    break;

                case "UserName":
                    _FilterData($"UserName like '{gtxtFilterValue.Text}%'");
                    break;

                case "Person ID":
                    _FilterData("PersonID = " + gtxtFilterValue.Text);
                    break;

                case "Full Name":
                    _FilterData($"[Full Name] like '{gtxtFilterValue.Text}%'");
                    break;

            }
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
            frmAddUpdateUser frm = new frmAddUpdateUser(Convert.ToInt32(dgvUsers.SelectedCells[0].Value));
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
            frmChangePassword frm = new frmChangePassword(Convert.ToInt32(dgvUsers.SelectedCells[0].Value));
            frm.ShowDialog();
        }

        private void gtxtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (gcbFilterBy.Text == "Person ID" || gcbFilterBy.Text == "User ID")
                e.Handled = (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar));
       
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int UserID = Convert.ToInt32(dgvUsers.SelectedCells[0].Value);
            if (MessageBox.Show("Are you sure you want to delete User With ID = [" + UserID+ "]", "Confirm Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                //User ID
                if (clsUser.DeleteUser(UserID))
                {
                    MessageBox.Show("User has been deleted successfully", "Deleted", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    _RefreshUsersList();
                }
                else
                    MessageBox.Show("User is not deleted de to data connected to it.", "Failed", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
            }
        }

        private void gbtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void sendEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsGlobalSettings.FeatureIsNotImplemented();
        }

        private void phoneCallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsGlobalSettings.FeatureIsNotImplemented();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            _RefreshUsersList();
        }
    }
}

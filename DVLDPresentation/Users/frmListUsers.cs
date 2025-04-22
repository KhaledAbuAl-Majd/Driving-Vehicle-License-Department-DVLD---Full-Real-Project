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

        DataView _Users;

        private void _Add_EditNewUser(int UserID)
        {
            frmAddUpdateUser frm = new frmAddUpdateUser(UserID);
            frm.OnClose += FrmAdd_EditOnClose;
            frm.ShowDialog();
        }
    
        private void FrmAdd_EditOnClose()
        {
            _Load_RefereshUsersInDGV();
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
            if (_Users != null)
            {
                _Users.RowFilter = FilterText;
                dgvUsers.DataSource = _Users;
                lblNumOfRecords.Text = _Users.Count.ToString();
            }
        }
        private void _GetTextFilterEmpty()
        {
            gtxtFilterValue.Text = "";
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
        void _EditSizeOfDGVColumns()
        {
            dgvUsers.Columns["UserID"].Width = 100;
            dgvUsers.Columns["PersonID"].Width = 100;
            dgvUsers.Columns["Full Name"].Width = 350;
            dgvUsers.Columns["UserName"].Width = 120;
            dgvUsers.Columns["IsActive"].Width = 100;
        }
        void _Load_RefereshUsersInDGV()
        {
            DataTable dt = clsUser.GetAllUsers();

            if (dt.Rows.Count > 0)
            {
                dt.Columns.Add("Full Name", typeof(string));

                clsPerson Person;

                foreach (DataRow row in dt.Rows)
                {
                    Person = clsPerson.Find(Convert.ToInt32(row["PersonID"]));

                    row["Full Name"] = string.Concat(Person.FirstName," ", Person.SecondName, " ",
                        Person.ThirdName, " ", Person.LastName);
                }

                DataTable dt2 = dt.DefaultView.ToTable(false, "UserID", "PersonID", "Full Name", "UserName", "IsActive");

                _Users = dt2.DefaultView;
                dgvUsers.DataSource = dt2;
                _EditSizeOfDGVColumns();
                lblNumOfRecords.Text = _Users.Count.ToString();
                _GetTextFilterEmpty();
                gcbFilterBy.SelectedIndex = 0;
            }
            else
            {
                lblNumOfRecords.Text = "0";
            }
        }
        private void _DeleteUser()
        {
            
            if (clsUser.DeleteUser(Convert.ToInt32(dgvUsers.SelectedCells[0].Value)))
            {
                MessageBox.Show("User has been deleted successfully", "Deleted", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                _Load_RefereshUsersInDGV();
            }
            else
                MessageBox.Show("User is not deleted de to data connected to it.", "Failed", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
        }
       
        private void _FilterByIsActive()
        {
            if (gcbIsActive.Text == "All")
                _FilterData("");

            else if (gcbIsActive.Text == "Yes")
                _FilterData("IsActive = 1");

            else if (gcbIsActive.Text == "No")
                _FilterData("IsActive = 0");
        }

        private void frmManageUsers_Load(object sender, EventArgs e)
        {
            _Load_RefereshUsersInDGV();
            //gcbFilterBy.SelectedIndex = 0;
        }

        private void gtxtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (gcbFilterBy.Text != "Person ID" && gcbFilterBy.Text != "User ID")
                return;

            if(!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
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
            _FilterByIsActive();
        }

        private void btnAddPerson_Click(object sender, EventArgs e)
        {
            _Add_EditNewUser(-1);
        }

        private void gbtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void sendEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsGlobalSettings.FeatureIsNotImplemented();
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUserInfo frm = new frmUserInfo(Convert.ToInt32(dgvUsers.SelectedCells[0].Value));
            //frm.OnClose += FrmAdd_EditOnClose;
            frm.ShowDialog();
        }

        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _Add_EditNewUser(-1);
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _Add_EditNewUser(Convert.ToInt32(dgvUsers.SelectedCells[0].Value));
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _DeleteUser();
        }

        private void phoneCallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsGlobalSettings.FeatureIsNotImplemented();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChangePassword frm = new frmChangePassword(Convert.ToInt32(dgvUsers.SelectedCells[0].Value));
            frm.OnClose += FrmChangePassword_OnClose;
            frm.ShowDialog();
        }

        private void FrmChangePassword_OnClose()
        {
            _Load_RefereshUsersInDGV();
        }
    }
}

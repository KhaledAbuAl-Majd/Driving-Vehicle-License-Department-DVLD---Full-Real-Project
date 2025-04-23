using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLDBusiness;
using DVLDPresentation.Global_Classes;

namespace DVLDPresentation.People
{
    public partial class frmListPeople : Form
    {
        //private DataTable _dtPeople;
        public frmListPeople()
        {
            InitializeComponent();
        }

        DataView _dvPeople;
        
        private void _AddNewPerson()
        {
            frmAddUpdatePerson frm = new frmAddUpdatePerson();
            //to refresh only if he save 
            frm.DataBack += FrmAdd_UpdatePerson_OnCloseDataBack;
            frm.ShowDialog();
            
        }
        void _PersonInfo()
        {
            //int PersonID = Convert.ToInt32(dgvPeople.CurrentRow.Cells[0].Value);
            frmShowPersonInfo frm = new frmShowPersonInfo(Convert.ToInt32(dgvPeople.CurrentRow.Cells[0].Value));
            frm.ShowDialog();
        }
        private void _Show_HideTextFilter(bool value)
        {
            gtxtFilterValue.Visible = value;
        }
        private void _Show_HideCBGendorFilter(bool value)
        {
            gcbFilterByGendor.Visible = value;
        }
        private void _FilterData(string FilterText)
        {
            if (_dvPeople != null)
            {
                _dvPeople.RowFilter = FilterText;
                //dgvPeople.DataSource = _People;
                /*_dtPeople.DefaultView.Sort = "PersonID ASC"*/;
                lblRecordsCount.Text = _dvPeople.Count.ToString();
            }
        }
        private void _FilterByAtDesign()
        {
            if (gcbFilterBy.Text == "None")
            {
                _Show_HideTextFilter(false);
                _Show_HideCBGendorFilter(false);
                _FilterData("");
            }
            else if(gcbFilterBy.Text == "Gendor")
            {
                _Show_HideTextFilter(false);
                _Show_HideCBGendorFilter(true);
                gcbFilterByGendor.SelectedIndex = 0;
            }
            else
            {
                _Show_HideTextFilter(true);
                _Show_HideCBGendorFilter(false);
                _FilterData("");
                gtxtFilterValue.Focus();
            }

            gtxtFilterValue.Text = "";
        }
        private void _RefreshPeoplList()
        {
            DataTable _dtAllPeople = clsPerson.GetAllPeople();
            _dvPeople = _dtAllPeople.DefaultView.ToTable(false, "PersonID", "NationalNo",
                                                         "FirstName", "SecondName", "ThirdName", "LastName",
                                                         "GendorCaption", "DateOfBirth", "CountryName",
                                                         "Phone", "Email").DefaultView;

            dgvPeople.DataSource = _dvPeople;
            gcbFilterBy.SelectedIndex = 0;
        }
        private void _FilterByGendor()
        {
            if (gcbFilterByGendor.Text == "All")
                _FilterData("");

            else if (gcbFilterByGendor.Text == "Male")
                _FilterData("GendorCaption = 'Male'");

            else if (gcbFilterByGendor.Text == "Female")
                _FilterData("GendorCaption = 'Female'");
        }
        private void frmManagePeople_Load(object sender, EventArgs e)
        {
            _RefreshPeoplList();

            if (dgvPeople.Rows.Count > 0)
            {
                dgvPeople.Columns[0].HeaderText = "Person ID";
                dgvPeople.Columns[0].Width = 110;

                dgvPeople.Columns[1].HeaderText = "National No.";
                dgvPeople.Columns[1].Width = 120;


                dgvPeople.Columns[2].HeaderText = "First Name";
                dgvPeople.Columns[2].Width = 120;

                dgvPeople.Columns[3].HeaderText = "Second Name";
                dgvPeople.Columns[3].Width = 140;


                dgvPeople.Columns[4].HeaderText = "Third Name";
                dgvPeople.Columns[4].Width = 120;

                dgvPeople.Columns[5].HeaderText = "Last Name";
                dgvPeople.Columns[5].Width = 120;

                dgvPeople.Columns[6].HeaderText = "Gendor";
                dgvPeople.Columns[6].Width = 120;

                dgvPeople.Columns[7].HeaderText = "Date Of Birth";
                dgvPeople.Columns[7].Width = 140;

                dgvPeople.Columns[8].HeaderText = "Nationality";
                dgvPeople.Columns[8].Width = 120;


                dgvPeople.Columns[9].HeaderText = "Phone";
                dgvPeople.Columns[9].Width = 120;


                dgvPeople.Columns[10].HeaderText = "Email";
                dgvPeople.Columns[10].Width = 230;
            }
            //_Load_RefreshPeopleInDGV();       

        }

        private void gtxtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";

            switch (gcbFilterBy.Text)
            {
                case "Person ID":
                    FilterColumn = "PersonID";
                    break;

                case "National No":
                    FilterColumn = "NationalNo";
                    break;

                case "First Name":
                    FilterColumn = "FirstName";
                    break;

                case "Second Name":
                    FilterColumn = "SecondName";
                    break;

                case "Third Name":
                    FilterColumn = "ThirdName";
                    break;

                case "Last Name":
                    FilterColumn = "LastName";
                    break;

                case "Nationality":
                    FilterColumn = "CountryName";
                    break;

                case "Phone":
                    FilterColumn = "Phone";
                    break;

                case "Email":
                    FilterColumn = "Email";
                    break;

                default:
                    FilterColumn = "None";
                    break;
            }

            //to avoid error when the text is empty when you filter by int
            if (string.IsNullOrWhiteSpace(gtxtFilterValue.Text) || FilterColumn == "None")
            {
                //to make filter is none get all people
                _FilterData("");
                return;
            }

            if (FilterColumn == "PersonID")
                _FilterData($"{FilterColumn} = " + gtxtFilterValue.Text.Trim());
            else
                _FilterData($"{FilterColumn}  like '{gtxtFilterValue.Text.Trim()}%'");
        }

        private void gcmFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            _FilterByAtDesign();
        }

        private void gcbFilterByGendor_SelectedIndexChanged(object sender, EventArgs e)
        {
            _FilterByGendor();
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _PersonInfo();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = Convert.ToInt32(dgvPeople.CurrentRow.Cells[0].Value);
            frmAddUpdatePerson frm = new frmAddUpdatePerson(PersonID);
            //to refresh only if he save 
            frm.DataBack += FrmAdd_UpdatePerson_OnCloseDataBack;
            frm.ShowDialog();
        }

        private void sendEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
           clsUtil.FeatureIsNotImplemented();
            
        }

        private void phoneCallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsUtil.FeatureIsNotImplemented();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete Person [" + dgvPeople.CurrentRow.Cells[0].Value + "]", "Confirm Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                if (clsPerson.DeletePerson(Convert.ToInt32(dgvPeople.CurrentRow.Cells[0].Value)))
                {
                    MessageBox.Show("Person Deleted Successfully", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _RefreshPeoplList();

                }
                else
                    MessageBox.Show("Person want not deleted because it has data linked to it.", "Failed"
                        , MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _AddNewPerson();
        }

        private void btnAddPerson_Click(object sender, EventArgs e)
        {
            _AddNewPerson();
        }

        private void gbtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvPeople_DoubleClick(object sender, EventArgs e)
        {
            _PersonInfo();
        }

        private void gtxtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            // السماح بالأرقام فقط +زر المسح(Backspace)
            if (gcbFilterBy.Text == "Person ID")
                e.Handled = (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar));
        }

        private void FrmAdd_UpdatePerson_OnCloseDataBack(object sender, int PersonID)
        {
            //to refresh only if he save 
            _RefreshPeoplList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _RefreshPeoplList();
        }
    }
}

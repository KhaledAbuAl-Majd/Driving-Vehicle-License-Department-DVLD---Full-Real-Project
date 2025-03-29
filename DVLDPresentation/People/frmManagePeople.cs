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

namespace DVLDPresentation.People
{
    public partial class frmManagePeople : Form
    {
        public frmManagePeople()
        {
            InitializeComponent();
        }

        DataView _People;
        
        private void _AddNewPerson()
        {
            frmAdd_EditPerson frm = new frmAdd_EditPerson(-1);

            frm.OnClose += OnCloseAddPersonToUpdateData;
            frm.ShowDialog();
        }
        private void _Show_HideTextFilter(bool value)
        {
            gtxtFilterValue.Visible = value;
        }

        private void _FilterData(string FilterText)
        {
            _People.RowFilter = FilterText;
            dgvPeople.DataSource = _People;
        }
        private void _GetTextFilterEmpty()
        {
            gtxtFilterValue.Text = "";
        }
        private void _FilterByAtDesign()
        {
            if (gcmFilterBy.Text == "None")
            {
                _Show_HideTextFilter(false);
            }
            else
            {
                _Show_HideTextFilter(true);
            }

            _GetTextFilterEmpty();
        }
        private void _Load_RefreshPeopleInDGV()
        {
            DataTable dt = clsPeople.GetAllPeople();

            dt.Columns["Gendor"].ColumnName = "NumberGendor";
            dt.Columns.Add("Gendor", typeof(string));
            dt.Columns.Add("Nationality", typeof(string));


            foreach (DataRow row in dt.Rows)
            {
                row["Gendor"] = (Convert.ToInt16(row["NumberGendor"]) == 0) ? "Male" : "Female";

                row["Nationality"] = clsCountries.Find((int)row["NationalityCountryID"]).CountryName;
            }


            DataTable dt2 = dt.DefaultView.ToTable(false, "PersonID", "NationalNo", "FirstName",
                "SecondName", "ThirdName",
                "LastName", "Gendor", "Nationality", "DateOfBirth", "Phone", "Email");


            dgvPeople.DataSource = dt2;

            _People = dt2.DefaultView;
        }
        private void _DeletePerson()
        {
            if (clsPeople.DeletePerson(Convert.ToInt32(dgvPeople.SelectedCells[0].Value)))
            {
                MessageBox.Show("Person Deleted Successfully", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _Load_RefreshPeopleInDGV();
            }
            else
                MessageBox.Show("Person want not deleted because it has data linked to it.", "Result"
                    , MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void _FeatureIsNotImplemented()
        {
            MessageBox.Show("This Feature Is Not Implemented Yet!", "Not Ready!",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        private void frmManagePeople_Load(object sender, EventArgs e)
        {
            _Load_RefreshPeopleInDGV();
            gcmFilterBy.SelectedIndex = 0;
            
        }

        private void gcmFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            _FilterByAtDesign();
        }

        private void gtxtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (gcmFilterBy.Text != "Person ID")
                return;

            // السماح بالأرقام فقط +زر المسح(Backspace)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // يمنع إدخال الحرف
            }
        }

        private void gtxtFilterValue_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(gtxtFilterValue.Text))
            {
                //to make filter is none get all people
                _FilterData("");
                return;
            }
            switch (gcmFilterBy.Text)
            {
                case "None":
                    _FilterData("");
                    break;

                case "Person ID":
                    _FilterData("PersonID = " + gtxtFilterValue.Text);
                    break;

                case "National No":
                    _FilterData($"NationalNo like '{ gtxtFilterValue.Text}%'");
                    break;

                case "First Name":
                    _FilterData($"FirstName like '{ gtxtFilterValue.Text}%'");

                    break;

                case "Second Name":
                    _FilterData($"SecondName like '{ gtxtFilterValue.Text}%'");
                    break;

                case "Third Name":
                    _FilterData($"ThirdName like '{gtxtFilterValue.Text}%'");
                    break;

                case "Last Name":
                    _FilterData($"LastName like '{gtxtFilterValue.Text}%'");
                    break;

                case "Nationality":
                    _FilterData($"Nationality like '{gtxtFilterValue.Text}%'");
                    break;

                case "Gendor":
                    _FilterData($"Gendor like '{gtxtFilterValue.Text}%'");
                    break;

                case "Phone":
                    _FilterData($"Phone like '{ gtxtFilterValue.Text}%'");
                    break;

                case "Email":
                    _FilterData($"Email  like '{ gtxtFilterValue.Text}%'");
                    break;
            }
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPersonDetails frm = new frmPersonDetails(Convert.ToInt32(dgvPeople.SelectedCells[0].Value));

            frm.OnClose += OnCloseAddPersonToUpdateData;
            frm.ShowDialog();
        }

        private void btnAddPerson_Click(object sender, EventArgs e)
        {
            _AddNewPerson();
        }

        private void OnCloseAddPersonToUpdateData()
        {
            _Load_RefreshPeopleInDGV();
        }

        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _AddNewPerson();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAdd_EditPerson frm = new frmAdd_EditPerson(Convert.ToInt32(dgvPeople.SelectedCells[0].Value));
            frm.OnClose += FrmEdit_OnClose;
            frm.ShowDialog();
        }

        private void FrmEdit_OnClose()
        {
            _Load_RefreshPeopleInDGV();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _DeletePerson();
        }

        private void sendEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _FeatureIsNotImplemented();
        }

        private void phoneCallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _FeatureIsNotImplemented();
        }
    }
}

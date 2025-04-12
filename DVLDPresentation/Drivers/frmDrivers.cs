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
using DVLDPresentation.Applications.Manage_Applications.LocalDrivingLicenseApplications;
using DVLDPresentation.People;

namespace DVLDPresentation
{
    public partial class frmDrivers : Form
    {
        DataView _Drivers;
        public frmDrivers()
        {
            InitializeComponent();
        }

        private void _Show_HideTextFilter(bool value)
        {
            gtxtFilterValue.Visible = value;
        }
        private void _FilterData(string FilterText)
        {
            if (_Drivers != null)
            {
                _Drivers.RowFilter = FilterText;
                dgvDrivers.DataSource = _Drivers;
                lblNumOfRecords.Text = _Drivers.Count.ToString();
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
                _FilterData("");
            }
            else
            {
                _Show_HideTextFilter(true);
                _FilterData("");
            }

            _GetTextFilterEmpty();
        }
        void _EditSizeOfDGVColumns()
        {
            dgvDrivers.Columns["Driver ID"].Width = 110;
            dgvDrivers.Columns["Person ID"].Width = 110;
            dgvDrivers.Columns["National No."].Width = 110;
            dgvDrivers.Columns["Full Name"].Width = 300;
            dgvDrivers.Columns["Date"].Width = 180;
            dgvDrivers.Columns["Active Licenses"].Width = 130;
        }
        void _Load_RefereshUsersInDGV()
        {
            DataTable dtAllDrivers = clsDrivers.GetAllDrivers();

            if (dtAllDrivers.Rows.Count > 0)
            {
                dtAllDrivers.Columns.Add("Full Name", typeof(string));
                dtAllDrivers.Columns.Add("National No.", typeof(string));
                dtAllDrivers.Columns.Add("Active Licenses", typeof(byte));


                dtAllDrivers.Columns["DriverID"].ColumnName = "Driver ID";
                dtAllDrivers.Columns["PersonID"].ColumnName = "Person ID";
                dtAllDrivers.Columns["CreatedDate"].ColumnName = "Date";

                clsPeople Person;

                foreach (DataRow row in dtAllDrivers.Rows)
                {
                    Person = clsPeople.Find(Convert.ToInt32(row["Person ID"]));

                    row["Full Name"] = Person.GetFullName();
                    row["National No."] = Person.NationalNo;
                    row["Active Licenses"] = clsLicenses.GetNumberOfActiveLicnesesByDriverID(Convert.ToInt32(row["Driver ID"]));
                }

                DataTable dt2 = dtAllDrivers.DefaultView.ToTable(false, "Driver ID", "Person ID",
                    "National No.", "Full Name", "Date", "Active Licenses");

                _Drivers = dt2.DefaultView;
                dgvDrivers.DataSource = dt2;
                _EditSizeOfDGVColumns();
                lblNumOfRecords.Text = _Drivers.Count.ToString();
                _GetTextFilterEmpty();
                gcbFilterBy.SelectedIndex = 0;
            }
            else
            {
                lblNumOfRecords.Text = "0";
            }
        }

        private void frmDrivers_Load(object sender, EventArgs e)
        {
            _Load_RefereshUsersInDGV();
        }

        private void gcbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            _FilterByAtDesign();
        }

        private void gtxtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (gcbFilterBy.Text != "Driver ID" && gcbFilterBy.Text != "Person ID" && gcbFilterBy.Text != "Active Licenses")
                return;

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
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

                case "Driver ID":
                    _FilterData("[Driver ID] = " + gtxtFilterValue.Text);
                    break;   

                case "Person ID":
                    _FilterData("[Person ID] = " + gtxtFilterValue.Text);
                    break; 

                case "National No.":
                    _FilterData($"[National No.] like '{gtxtFilterValue.Text}%'");
                    break;

                case "Full Name":
                    _FilterData($"[Full Name] like '{gtxtFilterValue.Text}%'");
                    break; 
                    
                case "Active Licenses":
                    _FilterData($"[Active Licenses] = {gtxtFilterValue.Text}");
                    break;

            }
        }

        private void gbtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = Convert.ToInt32(dgvDrivers.SelectedCells[1].Value);
            frmPersonDetails frm = new frmPersonDetails(PersonID);
            frm.OnClose += _Load_RefereshUsersInDGV;
            frm.ShowDialog();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = Convert.ToInt32(dgvDrivers.SelectedCells[1].Value);
            frmLicenseHistory frm = new frmLicenseHistory(PersonID);
            frm.ShowDialog();
        }
    }
}

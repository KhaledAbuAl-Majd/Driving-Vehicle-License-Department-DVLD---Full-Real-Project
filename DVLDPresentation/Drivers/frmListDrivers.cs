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
    public partial class frmListDrivers : Form
    {
        DataView _dvAllDrivers;
        public frmListDrivers()
        {
            InitializeComponent();
        }
        private void _Show_HideTextFilter(bool value)
        {
            gtxtFilterValue.Visible = value;
        }

        private void _FilterData(string FilterText)
        {
            if (_dvAllDrivers != null)
            {
                _dvAllDrivers.RowFilter = FilterText;
                lblNumOfRecords.Text = _dvAllDrivers.Count.ToString();
                //dgvDrivers.DataSource = _dvAllDrivers;
            }
        }

        private void gbtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmDrivers_Load(object sender, EventArgs e)
        {
            gcbFilterBy.SelectedIndex = 0;
            _dvAllDrivers = clsDriver.GetAllDrivers().DefaultView;
            dgvDrivers.DataSource = _dvAllDrivers;
            //lblNumOfRecords.Text = _dvAllDrivers.Count.ToString();

            if (dgvDrivers.Rows.Count > 0)
            {
                dgvDrivers.Columns[0].HeaderText = "Driver ID";
                dgvDrivers.Columns[0].Width = 120;

                dgvDrivers.Columns[1].HeaderText = "Person ID";
                dgvDrivers.Columns[1].Width = 120;

                dgvDrivers.Columns[2].HeaderText = "National No.";
                dgvDrivers.Columns[2].Width = 140;

                dgvDrivers.Columns[3].HeaderText = "Full Name";
                dgvDrivers.Columns[3].Width = 320;

                dgvDrivers.Columns[4].HeaderText = "Date";
                dgvDrivers.Columns[4].Width = 170;

                dgvDrivers.Columns[5].HeaderText = "Active Licenses";
                dgvDrivers.Columns[5].Width = 120;
            }
        }

        private void gcbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            _Show_HideTextFilter(gcbFilterBy.Text != "None");

            //_FilterData("");
            //it will go text change event
            gtxtFilterValue.Text = "";
        }

        private void gtxtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            //Map Selected Filter to real Column name 

            switch (gcbFilterBy.Text)
            {
                case "Driver ID":
                    FilterColumn = "DriverID";
                    break;

                case "Person ID":
                    FilterColumn = "PersonID";
                    break;

                case "National No.":
                    FilterColumn = "NationalNo";
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

            //in this case we deal with numbers not string.)
            if (FilterColumn != "FullName" && FilterColumn != "NationalNo")
                _FilterData($"{FilterColumn} = {gtxtFilterValue.Text.Trim()}");
            else
                _FilterData($"{FilterColumn} like '{gtxtFilterValue.Text.Trim()}%'");
        }

        private void gtxtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (gcbFilterBy.Text == "Driver ID" || gcbFilterBy.Text == "Person ID")
                e.Handled = (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar));
        }


        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = Convert.ToInt32(dgvDrivers.CurrentRow.Cells[1].Value);
            frmShowPersonInfo frm = new frmShowPersonInfo(PersonID);
            frm.ShowDialog();
            frmDrivers_Load(null, null);
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = Convert.ToInt32(dgvDrivers.CurrentRow.Cells[1].Value);

            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(PersonID);
            frm.ShowDialog();
        }
    }
}

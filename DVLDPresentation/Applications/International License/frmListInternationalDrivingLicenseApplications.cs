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
using DVLDPresentation.Applications.Driving_License_Services.New_Driving_License;
using DVLDPresentation.Applications.Manage_Applications.LocalDrivingLicenseApplications;
using DVLDPresentation.People;

namespace DVLDPresentation.Applications.Manage_Applications.International_Driving_License_Application
{
    public partial class frmListInternationalDrivingLicenseApplications : Form
    {
        DataView _dvInternatinalApplications;
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
            if (_dvInternatinalApplications != null)
            {
                _dvInternatinalApplications.RowFilter = FilterText;
                //dgvInternationalLicenses.DataSource = _dvInternatinalApplications;
                lblNumOfRecords.Text = _dvInternatinalApplications.Count.ToString();
            }
        }

        public frmListInternationalDrivingLicenseApplications()
        {
            InitializeComponent();
        }

        private void gbtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void _RefreshInternationalLicensesList()
        {
            _dvInternatinalApplications = clsInternationalLicense.GetAllInternationalLicenses().DefaultView;
            dgvInternationalLicenses.DataSource = _dvInternatinalApplications;
            gcbFilterBy.SelectedIndex = 0;

            if (dgvInternationalLicenses.Rows.Count > 0)
            {
                dgvInternationalLicenses.Columns[0].HeaderText = "Int.License ID";
                dgvInternationalLicenses.Columns[0].Width = 130;

                dgvInternationalLicenses.Columns[1].HeaderText = "Application ID";
                dgvInternationalLicenses.Columns[1].Width = 130;

                dgvInternationalLicenses.Columns[2].HeaderText = "Driver ID";
                dgvInternationalLicenses.Columns[2].Width = 130;

                dgvInternationalLicenses.Columns[3].HeaderText = "L.License ID";
                dgvInternationalLicenses.Columns[3].Width = 130;

                dgvInternationalLicenses.Columns[4].HeaderText = "Issue Date";
                dgvInternationalLicenses.Columns[4].Width = 180;

                dgvInternationalLicenses.Columns[5].HeaderText = "Expiration Date";
                dgvInternationalLicenses.Columns[5].Width = 150;

                dgvInternationalLicenses.Columns[6].HeaderText = "Is Active";
                dgvInternationalLicenses.Columns[6].Width = 100;

            }
        }

        private void frmListInternationalDrivingLicenseApplications_Load(object sender, EventArgs e)
        {
            _RefreshInternationalLicensesList();
        }

        private void btnAddInternationLicenesApplicatoins_Click(object sender, EventArgs e)
        {
            frmNewInternationalLicneseApplication frm = new frmNewInternationalLicneseApplication();
            frm.OnClose += _RefreshInternationalLicensesList;
            frm.ShowDialog();
        }

        private void CMSIshowLicenseDetails_Click(object sender, EventArgs e)
        {
            int InternationalLicenseID = Convert.ToInt32(dgvInternationalLicenses.CurrentRow.Cells[0].Value);
            frmShowInternationalLicenseInfo frm = new frmShowInternationalLicenseInfo(InternationalLicenseID);
            frm.ShowDialog();
        }

        private void showPersonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int DriverID = Convert.ToInt32(dgvInternationalLicenses.CurrentRow.Cells[2].Value);
            int PersonID = clsDriver.FindByDriverID(DriverID).PersonID;

            frmShowPersonInfo frm = new frmShowPersonInfo(PersonID);
            frm.ShowDialog();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int DriverID = Convert.ToInt32(dgvInternationalLicenses.CurrentRow.Cells[2].Value);
            int PersonID = clsDriver.FindByDriverID(DriverID).PersonID;
            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(PersonID);
            frm.ShowDialog();
        }

        private void gcbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (gcbFilterBy.Text == "Is Active")
            {
                _Show_HideTextFilter(false);
                _Show_HideCBIsActiveFilter(true);

                if (gcbIsActive.SelectedIndex != 0)
                    gcbIsActive.SelectedIndex = 0;
                else
                    _FilterData("");

                gcbFilterBy.Focus();
                return;
            }

            //if (gcbFilterBy.Text == "None")
            //{
            //    _Show_HideTextFilter(false);
            //    _Show_HideCBIsActiveFilter(false);
            //    _FilterData("");
            //}
            //else
            //{
            //    _FilterData("");
            //}

            _Show_HideTextFilter(gcbFilterBy.Text != "None");
            _Show_HideCBIsActiveFilter(false);

            if (gtxtFilterValue.Text != "")
                gtxtFilterValue.Text = "";
            else
                _FilterData("");
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

        private void gtxtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            //Map Selected Filter to real Column name 
            switch (gcbFilterBy.Text)
            {
                case "International License ID":
                    FilterColumn = "InternationalLicenseID";
                    break;

                case "Application ID":

                    FilterColumn = "ApplicationID";
                    break;

                case "Driver ID":
                    FilterColumn = "DriverID";
                    break;

                case "Local License ID":
                    FilterColumn = "IssuedUsingLocalLicenseID";
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

            _FilterData($"{FilterColumn} = {gtxtFilterValue.Text.Trim()}");
        }

        private void gtxtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            //All Is Number
            e.Handled = (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar));
        }
    }
}

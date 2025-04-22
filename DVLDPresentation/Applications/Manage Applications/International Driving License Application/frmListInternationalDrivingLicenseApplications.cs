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
        public frmListInternationalDrivingLicenseApplications()
        {
            InitializeComponent();
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
            if (_dvInternatinalApplications != null)
            {
                _dvInternatinalApplications.RowFilter = FilterText;
                dgvIntLApplications.DataSource = _dvInternatinalApplications;
                lblNumOfRecords.Text = _dvInternatinalApplications.Count.ToString();
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
            else if (gcbFilterBy.Text == "Is Active")
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
            dgvIntLApplications.Columns["Int.License ID"].Width = 130;
            dgvIntLApplications.Columns["Application ID"].Width = 120;
            dgvIntLApplications.Columns["Driver ID"].Width = 120;
            dgvIntLApplications.Columns["L.License ID"].Width = 120;
            dgvIntLApplications.Columns["Issue Date"].Width = 190;
            dgvIntLApplications.Columns["Expiration Date"].Width = 170;
            dgvIntLApplications.Columns["IsActive"].Width = 110;
        }
        void _Load_RefereshIntLApplicationsInDGV()
        {
            DataTable dtAllIntApplications = clsInternationalLicenses.GetAllLicneses();

            if (dtAllIntApplications.Rows.Count > 0)
            {
                dtAllIntApplications.Columns["InternationalLicenseID"].ColumnName = "Int.License ID";
                dtAllIntApplications.Columns["ApplicationID"].ColumnName = "Application ID";
                dtAllIntApplications.Columns["DriverID"].ColumnName = "Driver ID";
                dtAllIntApplications.Columns["IssuedUsingLocalLicenseID"].ColumnName = "L.License ID";
                dtAllIntApplications.Columns["IssueDate"].ColumnName = "Issue Date";
                dtAllIntApplications.Columns["ExpirationDate"].ColumnName = "Expiration Date";
                dtAllIntApplications.Columns["IsActive"].ColumnName = "IsActive";

                DataTable dt2 = dtAllIntApplications.DefaultView.ToTable(false, "Int.License ID", "Application ID",
                    "Driver ID", "L.License ID", "Issue Date", "Expiration Date", "IsActive");

                _dvInternatinalApplications = dt2.DefaultView;
                dgvIntLApplications.DataSource = dt2;
                _EditSizeOfDGVColumns();
                lblNumOfRecords.Text = _dvInternatinalApplications.Count.ToString();
                gcbFilterBy.SelectedIndex = 0;
            }
            else
            {
                lblNumOfRecords.Text = "0";
            }
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
        private void frmListInternationalDrivingLicenseApplications_Load(object sender, EventArgs e)
        {
            _Load_RefereshIntLApplicationsInDGV();
        }

        private void gcbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            _FilterByAtDesign();
        }
        private void gtxtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (gcbFilterBy.Text == "Is Active")
                return;

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
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
            switch (gcbFilterBy.Text)
            {
                case "None":
                    _FilterData("");
                    break;

                case "Int.LicenseID":
                    _FilterData("[Int.License ID] = " + gtxtFilterValue.Text);
                    break;
                
                case "Application ID":
                    _FilterData("[Application ID] = " + gtxtFilterValue.Text);
                    break;

                case "Driver ID":
                    _FilterData("[Driver ID] = " + gtxtFilterValue.Text);
                    break;

                case "L.License ID":
                    _FilterData("[L.License ID] = " + gtxtFilterValue.Text);
                    break;

            }
        }

        private void gcbIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            _FilterByIsActive();
        }

        private void btnAddInternationLicenesApplicatoins_Click(object sender, EventArgs e)
        {
            frmNewInternationalLicneseApplication frm = new frmNewInternationalLicneseApplication();
            frm.OnClose += _Load_RefereshIntLApplicationsInDGV;
            frm.ShowDialog();
        }

        private void showPersonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int DriverID = Convert.ToInt32(dgvIntLApplications.SelectedCells[2].Value);
            int PersonID = clsDrivers.FindByDriverID(DriverID).PersonID;
            frmShowPersonInfo frm = new frmShowPersonInfo(PersonID);
            frm.ShowDialog();
        }

        private void CMSIshowLicenseDetails_Click(object sender, EventArgs e)
        {
            int IntLicenseID = Convert.ToInt32(dgvIntLApplications.SelectedCells[0].Value);
            frmInternationalLicenseInfo frm = new frmInternationalLicenseInfo(IntLicenseID);
            frm.ShowDialog();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int DriverID = Convert.ToInt32(dgvIntLApplications.SelectedCells[2].Value);
            int PersonID = clsDrivers.FindByDriverID(DriverID).PersonID;
            frmLicenseHistory frm = new frmLicenseHistory(PersonID);
            frm.ShowDialog();
        }

        private void gbtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

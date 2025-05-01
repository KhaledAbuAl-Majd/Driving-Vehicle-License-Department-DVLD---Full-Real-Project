using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Lifetime;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLDBusiness;
using DVLDPresentation.Applications.Manage_Applications.LocalDrivingLicenseApplications;
using DVLDPresentation.People;

namespace DVLDPresentation.Applications.Detain_Licenses
{
    public partial class frmListDetainedLicenses : Form
    {
        //All Detained and Not Detained
        DataView _dvDetainedLicenses;
        public frmListDetainedLicenses()
        {
            InitializeComponent();
        }
        private void _Show_HideTextFilter(bool value)
        {
            gtxtFilterValue.Visible = value;
        }
        private void _Show_HideCBIsReleasedFilter(bool value)
        {
            gcbIsReleased.Visible = value;
        }

        private void _FilterData(string FilterText)
        {
            if (_dvDetainedLicenses != null)
            {
                _dvDetainedLicenses.RowFilter = FilterText;
                //dgvDetainedLicenses.DataSource = _dvDetainedLicenses;
                lblNumOfRecords.Text = _dvDetainedLicenses.Count.ToString();
            }
        }

        private void gbtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void _RefreshDetainedLicensesList()
        {
            gcbFilterBy.SelectedIndex = 0;
            _dvDetainedLicenses = clsDetainedLicense.GetAllDetainedLicenses().DefaultView;
            dgvDetainedLicenses.DataSource = _dvDetainedLicenses;
            lblNumOfRecords.Text = _dvDetainedLicenses.Count.ToString();

            if (dgvDetainedLicenses.Rows.Count > 0)
            {
                dgvDetainedLicenses.Columns[0].HeaderText = "D.ID";
                dgvDetainedLicenses.Columns[0].Width = 100;

                dgvDetainedLicenses.Columns[1].HeaderText = "L.ID";
                dgvDetainedLicenses.Columns[1].Width = 100;

                dgvDetainedLicenses.Columns[2].HeaderText = "D.Date";
                dgvDetainedLicenses.Columns[2].Width = 160;

                dgvDetainedLicenses.Columns[3].HeaderText = "Is Released";
                dgvDetainedLicenses.Columns[3].Width = 100;

                dgvDetainedLicenses.Columns[4].HeaderText = "Fine Fees";
                dgvDetainedLicenses.Columns[4].Width = 110;

                dgvDetainedLicenses.Columns[5].HeaderText = "Release Date";
                dgvDetainedLicenses.Columns[5].Width = 160;

                dgvDetainedLicenses.Columns[6].HeaderText = "N.No.";
                dgvDetainedLicenses.Columns[6].Width = 90;

                dgvDetainedLicenses.Columns[7].HeaderText = "Full Name";
                dgvDetainedLicenses.Columns[7].Width = 300;

                dgvDetainedLicenses.Columns[8].HeaderText = "Rlease App.ID";
                dgvDetainedLicenses.Columns[8].Width = 110;

            }
        }

        private void frmListDetainedLicenses_Load(object sender, EventArgs e)
        {
            _RefreshDetainedLicensesList();
            //lblNumOfRecords.Text = _dvDetainedLicenses.Count.ToString();
        }

        private void gtxtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            //Map Selected Filter to real Column name 
            switch (gcbFilterBy.Text)
            {
                case "Detain ID":
                    FilterColumn = "DetainID";
                    break;

                case "National No.":
                    FilterColumn = "NationalNo";
                    break;


                case "Full Name":
                    FilterColumn = "FullName";
                    break;

                case "Release Application ID":
                    FilterColumn = "ReleaseApplicationID";
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

            if (FilterColumn == "DetainID" || FilterColumn == "ReleaseApplicationID")
                _FilterData($"{FilterColumn} = {gtxtFilterValue.Text.Trim()}");
            else
                _FilterData($"{FilterColumn} LIKE '{gtxtFilterValue.Text.Trim()}%'");
        }

        private void gcbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (gcbFilterBy.Text == "Is Released")
            {
                _Show_HideTextFilter(false);
                _Show_HideCBIsReleasedFilter(true);

                if (gcbFilterBy.SelectedIndex != 0)
                    gcbIsReleased.SelectedIndex = 0;
                else
                    _FilterData("");

                gcbIsReleased.Focus();
                return;
            }

            _Show_HideCBIsReleasedFilter(false);

            _Show_HideTextFilter(gcbFilterBy.Text != "None");

            if (gtxtFilterValue.Text != "")
                gtxtFilterValue.Text = "";
            else
                _FilterData("");

                gtxtFilterValue.Focus();
        }

        private void gcbIsReleased_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (gcbIsReleased.Text == "All")
                _FilterData("");

            else if (gcbIsReleased.Text == "Yes")
                _FilterData("IsReleased = 1");

            else if (gcbIsReleased.Text == "No")
                _FilterData("IsReleased = 0");
        }

        private void gtxtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (gcbFilterBy.Text == "Detain ID" || gcbFilterBy.Text == "Release Application ID")
                e.Handled = (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar));
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dgvDetainedLicenses.CurrentRow.Cells[1].Value;
            int PersonID = clsLicense.Find(LicenseID).DriverInfo.PersonID;
            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(PersonID);
            frm.ShowDialog();
        }

        private void PesonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dgvDetainedLicenses.CurrentRow.Cells[1].Value;
            int PersonID = clsLicense.Find(LicenseID).DriverInfo.PersonID;

            frmShowPersonInfo frm = new frmShowPersonInfo(PersonID);
            frm.ShowDialog();
        }

        private void CMSIshowLicenseDetails_Click(object sender, EventArgs e)
        {
            int LicenseID = Convert.ToInt32(dgvDetainedLicenses.CurrentRow.Cells[1].Value);

            frmShowLicenseInfo frm = new frmShowLicenseInfo(LicenseID);
            frm.ShowDialog();
        }

        private void btnDetainLicense_Click(object sender, EventArgs e)
        {
            frmDetainLicenseApplication frm = new frmDetainLicenseApplication();
            frm.OnClose += _RefreshDetainedLicensesList;
            frm.ShowDialog();
        }

        private void btnReleaseDetainedLicense_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicenseApplication frm = new frmReleaseDetainedLicenseApplication();
            frm.OnClose += _RefreshDetainedLicensesList;
            frm.ShowDialog();
        }

        private void ReleaseDetainedLicenseToolMenueStrip_Click(object sender, EventArgs e)
        {
            int LicenseID = Convert.ToInt32(dgvDetainedLicenses.CurrentRow.Cells[1].Value);

            frmReleaseDetainedLicenseApplication frm = new frmReleaseDetainedLicenseApplication(LicenseID);
            frm.OnClose += _RefreshDetainedLicensesList;
            frm.ShowDialog();
        }

        private void cmpListDetainedLicensesOptoins_Opening(object sender, CancelEventArgs e)
        {
            ReleaseDetainedLicenseToolMenueStrip.Enabled = !(bool)dgvDetainedLicenses.CurrentRow.Cells[3].Value;
        }
    }
}

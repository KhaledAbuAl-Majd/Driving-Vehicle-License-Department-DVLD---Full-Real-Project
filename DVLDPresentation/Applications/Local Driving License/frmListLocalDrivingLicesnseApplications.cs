using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design.Data;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLDBusiness;
using DVLDPresentation.Applications.Driving_License_Services.New_Driving_License;
using DVLDPresentation.Applications.Manage_Applications.LocalDrivingLicenseApplications;
using DVLDPresentation.Applications.Manage_Applications.LocalDrivingLicenseApplications.Schedule_Test;
using DVLDPresentation.Applications.Manage_Applications.Schedule_Test;

namespace DVLDPresentation.Applications.Manage_Applications
{
    public partial class frmListLocalDrivingLicesnseApplications : Form
    {
        DataView _dvAllLDLApplications;

        public frmListLocalDrivingLicesnseApplications()
        {
            InitializeComponent();
            _dvAllLDLApplications = new DataView();
        }

        private void _Show_HideTextFilter(bool value)
        {
            gtxtFilterValue.Visible = value;
        }

        private void _Show_HideCBStatusFilter(bool value)
        {
            gcbFilterByStatus.Visible = value;
        }

        private void _FilterData(string FilterText)
        {
            if (_dvAllLDLApplications != null)
            {
                _dvAllLDLApplications.RowFilter = FilterText;
                lblNumOfRecords.Text = _dvAllLDLApplications.Count.ToString();
                //dgvLocalDrivingLicenseApplications.DataSource = _dvAllLDLApplications;
            }
        }

        private void _GetTextFilterEmpty()
        {
            gtxtFilterValue.Text = "";
        }

        private void _FilterByFilterByStatus()
        {
            if (gcbFilterByStatus.Text.ToLower() == "all")
            {
                _FilterData("");
            }
            else 
            {
                _FilterData($"Status = '{gcbFilterByStatus.Text}'");
            }
        }

        void _ScheduleTest(clsTestType.enTestType TestType)
        {
            int LocalDrivingLicenseApplicationID = Convert.ToInt32(dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value);
            frmListTestAppointments frm = new frmListTestAppointments(LocalDrivingLicenseApplicationID, TestType);
            frm.ShowDialog();
            _RefreshLDLApplicationsList();
        }

        void _RefreshLDLApplicationsList()
        {
            _dvAllLDLApplications = clsLocalDrivingLicenseApplication.GetAllLocalDrivingLicenseApplications().DefaultView;
            dgvLocalDrivingLicenseApplications.DataSource = _dvAllLDLApplications;
            //lblNumOfRecords.Text = _dvAllLDLApplications.Count.ToString();
            gcbFilterBy.SelectedIndex = 0;
        }

        private void frmLocalDrivingLicenseApplications_Load(object sender, EventArgs e)
        {
            _RefreshLDLApplicationsList();

            if (_dvAllLDLApplications.Count > 0)
            {
                dgvLocalDrivingLicenseApplications.Columns[0].HeaderText = "L.D.L.AppID";
                dgvLocalDrivingLicenseApplications.Columns[0].Width = 120;

                dgvLocalDrivingLicenseApplications.Columns[1].HeaderText = "Driving Class";
                dgvLocalDrivingLicenseApplications.Columns[1].Width = 300;

                dgvLocalDrivingLicenseApplications.Columns[2].HeaderText = "National No.";
                dgvLocalDrivingLicenseApplications.Columns[2].Width = 150;

                dgvLocalDrivingLicenseApplications.Columns[3].HeaderText = "Full Name";
                dgvLocalDrivingLicenseApplications.Columns[3].Width = 350;

                dgvLocalDrivingLicenseApplications.Columns[4].HeaderText = "Application Date";
                dgvLocalDrivingLicenseApplications.Columns[4].Width = 170;

                dgvLocalDrivingLicenseApplications.Columns[5].HeaderText = "Passed Tests";
                dgvLocalDrivingLicenseApplications.Columns[5].Width = 110;

                dgvLocalDrivingLicenseApplications.Columns[6].HeaderText = "Status";
                dgvLocalDrivingLicenseApplications.Columns[6].Width = 100;
            }
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Local Driving License Application ID
            frmLocalDrivingLicenseApplicationInfo frm = new 
            frmLocalDrivingLicenseApplicationInfo(Convert.ToInt32(dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value));
            frm.ShowDialog();

            //Refresh
            _RefreshLDLApplicationsList();
        }

        private void gcbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (gcbFilterBy.Text == "Status")
            {
                _Show_HideTextFilter(false);
                _Show_HideCBStatusFilter(true);
                gcbFilterByStatus.SelectedIndex = 0;
                return;
            }

            _Show_HideTextFilter(gcbFilterBy.Text != "None");
            _FilterData("");
            _Show_HideCBStatusFilter(false);
            _GetTextFilterEmpty();
        }

        private void gtxtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";

            switch (gcbFilterBy.Text)
            {
                case "L.D.L.AppID":
                    FilterColumn = "LocalDrivingLicenseApplicationID";
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

            //Reset the filters in case nothing selected or filter value conains nothing.
            if (gtxtFilterValue.Text.Trim() == "" || FilterColumn == "None")
            {
                _FilterData("");
                return;
            }

            if (FilterColumn == "LocalDrivingLicenseApplicationID")
                _FilterData($"{FilterColumn} = {gtxtFilterValue.Text}");
            else
                _FilterData($"{FilterColumn} like '{gtxtFilterValue.Text}%'");
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateLocalDrivingLicesnseApplication frm = 
                new frmAddUpdateLocalDrivingLicesnseApplication(Convert.ToInt32(dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value));

            frm.OnClose += _RefreshLDLApplicationsList;
            frm.ShowDialog();
        }

        private void gtxtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (gcbFilterBy.Text == "L.D.L.AppID")
                e.Handled = (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar));
        }

        private void scheduleVisionTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _ScheduleTest(clsTestType.enTestType.VisionTest);
        }

        private void scheduleWrittenTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _ScheduleTest(clsTestType.enTestType.WrittenTest);
        }

        private void scheduleStreetTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _ScheduleTest(clsTestType.enTestType.StreetTest);
        }

        private void btnAddLDLApplication_Click(object sender, EventArgs e)
        {
            frmAddUpdateLocalDrivingLicesnseApplication frm = new frmAddUpdateLocalDrivingLicesnseApplication();
            frm.OnClose += _RefreshLDLApplicationsList;
            frm.ShowDialog();
        }

        private void issueDrivingLicenseFirstTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Local Driving License Application ID
            frmIssueDriverLicenseForFirstTime frm = new frmIssueDriverLicenseForFirstTime(Convert.ToInt32(dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value));
            frm.OnClose += _RefreshLDLApplicationsList;
            frm.ShowDialog();
        }

        private void cmpLDLApplicationsOptoins_Opening(object sender, CancelEventArgs e)
        {
            int LocalDrivingLicenseApplicationID = Convert.ToInt32(dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value);

            clsLocalDrivingLicenseApplication LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(LocalDrivingLicenseApplicationID);

            int TotalPassedTests = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[5].Value;

            //have an active license from this class, note person cannot apply New License if he has license (Active ant not)
            //This Application for first time only!!
            bool LicenseExists = LocalDrivingLicenseApplication.IsLicenseIssuedForPerson();

            //Enabled only if person passed all tests and Does not have license. 
            issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = (TotalPassedTests == 3) && !LicenseExists;

            showLicenseToolStripMenuItem.Enabled = LicenseExists && (LocalDrivingLicenseApplication.ApplicationStatus == clsApplication.enApplicationStatus.Completed);
            editToolStripMenuItem.Enabled = !LicenseExists && (LocalDrivingLicenseApplication.ApplicationStatus == clsApplication.enApplicationStatus.New);
            //ScheduleTestsMenue.Enabled = !LicenseExists;

            //We only canel the applications with status=new.
            CancelApplicaitonToolStripMenuItem.Enabled = LocalDrivingLicenseApplication.ApplicationStatus == clsApplication.enApplicationStatus.New;

            //We only canel the applications with status=new.
            DeleteApplicationToolStripMenuItem.Enabled = LocalDrivingLicenseApplication.ApplicationStatus == clsApplication.enApplicationStatus.New;

            bool PassedVisionTest = LocalDrivingLicenseApplication.DoesPassTestType(clsTestType.enTestType.VisionTest);
            bool PassedWrittenTest = LocalDrivingLicenseApplication.DoesPassTestType(clsTestType.enTestType.WrittenTest);
            bool PassedStreetTest = LocalDrivingLicenseApplication.DoesPassTestType(clsTestType.enTestType.StreetTest);

            ScheduleTestsMenue.Enabled = (!PassedVisionTest || !PassedWrittenTest || !PassedStreetTest) && (LocalDrivingLicenseApplication.ApplicationStatus == clsApplication.enApplicationStatus.New);

            if (ScheduleTestsMenue.Enabled)
            {
                scheduleVisionTestToolStripMenuItem.Enabled = !PassedVisionTest;
                scheduleWrittenTestToolStripMenuItem.Enabled = PassedVisionTest && !PassedWrittenTest;
                scheduleStreetTestToolStripMenuItem.Enabled = PassedVisionTest && PassedWrittenTest && !PassedStreetTest;
            }
        }

        private void showLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Local Driving License Application ID
            //int LDLApplicationID = Convert.ToInt32(dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value);
            //clsLocalDrivingLicenseApplication LDLApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(LDLApplicationID);
            //clsApplication Application = clsApplication.FindBaseApplication(LDLApplication.ApplicationID);
            //clsLicense License = clsLicense.FindByApplicationID(Application.ApplicationID);
            //frmShowLicenseInfo frm = new frmShowLicenseInfo(License.LicenseID);
            //frm.ShowDialog();

            //I Git Active LIcense Because , Person Can Apply to New license for First Time Only


            int LocalDrivingLicenseApplicationID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;

            int LicenseID = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(
              LocalDrivingLicenseApplicationID).GetActiveLicenseID();

            if (LicenseID != -1)
            {
                frmShowLicenseInfo frm = new frmShowLicenseInfo(LicenseID);
                frm.ShowDialog();

            }
            else
            {
                MessageBox.Show("No License Found!", "No License", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void CancelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure do want to cancel this application?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            int LocalDrivingLicenseApplicationID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;

            clsLocalDrivingLicenseApplication LocalDrivingLicenseApplication =
                 clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(LocalDrivingLicenseApplicationID);

            if(LocalDrivingLicenseApplication != null)
            {
                if (LocalDrivingLicenseApplication.Cancel())
                {
                    MessageBox.Show("Application Cancelled Successfully.", "Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //refresh the form again.
                    _RefreshLDLApplicationsList();
                }
                else
                    MessageBox.Show("Could not cancel applicatoin.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DeletetoolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure do want to delete this application?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            int LocalDrivingLicenseApplicationID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;

            clsLocalDrivingLicenseApplication LocalDrivingLicenseApplication =
               clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(LocalDrivingLicenseApplicationID);

            if (LocalDrivingLicenseApplication != null)
            {
                if (LocalDrivingLicenseApplication.Delete())
                {
                    MessageBox.Show("Application Deleted Successfully.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //refresh the form again.
                    _RefreshLDLApplicationsList();
                }
                else
                {
                    MessageBox.Show("Could not delete applicatoin, other data depends on it.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LocalDrivingLicenseApplicationID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;
            clsLocalDrivingLicenseApplication localDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(LocalDrivingLicenseApplicationID);

            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(localDrivingLicenseApplication.ApplicantPersonID);
            frm.ShowDialog();
        }

        private void gcbFilterByStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            _FilterByFilterByStatus();
        }

        private void gbtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            _RefreshLDLApplicationsList();
        }
    }
}

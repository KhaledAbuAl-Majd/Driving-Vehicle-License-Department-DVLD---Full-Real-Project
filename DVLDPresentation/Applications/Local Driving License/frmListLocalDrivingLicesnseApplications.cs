using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design.Data;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
                //dgvLocalDrivingLicenseApplications.DataSource = _dvAllLDLApplications;
                lblNumOfRecords.Text = _dvAllLDLApplications.Count.ToString();
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
                _Show_HideCBStatusFilter(false);
                _FilterData("");
            }
            else if (gcbFilterBy.Text == "Status")
            {
                _Show_HideTextFilter(false);
                _Show_HideCBStatusFilter(true);
                gcbFilterByStatus.SelectedIndex = 0;
            }
            else
            {
                _Show_HideTextFilter(true);
                _Show_HideCBStatusFilter(false);
                _FilterData("");
            }

            _GetTextFilterEmpty();
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
        void _CancelApplication()
        {
            clsLocalDrivingLicenseApplication LDLApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(Convert.ToInt32(dgvLocalDrivingLicenseApplications.SelectedCells[0].Value));
            clsApplication Application = clsApplication.FindBaseApplication(LDLApplication.ApplicationID);
            //Application.ApplicationStatus = clsApplicationStatuses.Find("Cancelled").ApplicationStatusID;
            Application.LastStatusDate = DateTime.Now;

            if(MessageBox.Show("Are you sure you want to Cancel this application?","Confirm",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Question,MessageBoxDefaultButton.Button1) == DialogResult.OK)
            {
                if (Application.Save())
                {
                    MessageBox.Show("Application Cancelled Successfully!", "Succed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _RefreshLDLApplicationsList();
                }
                else
                {
                    MessageBox.Show("Failed To Save", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Cancel Operatoin Cancelled ", "Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        void _AddNewLDLApplication()
        {
            frmAddUpdateLocalDrivingLicesnseApplication frm = new frmAddUpdateLocalDrivingLicesnseApplication(-1);

            frm.OnClose += FrmAddNewLDLApplication_OnClose;

            frm.ShowDialog();
        }
        private void FrmAddNewLDLApplication_OnClose()
        {
            _RefreshLDLApplicationsList();
        }
        void _EnapleAndDisableMainToolSripItem(int Index,bool Value)
        {
            cmpLDLApplicationsOptoins.Items[Index].Enabled = Value;
        }
        void _EnapleAndDisableScehduleToolSripItem(int Index,bool Value)
        {
            ToolStripMenuItem scheduleTestsItems = (ToolStripMenuItem)cmpLDLApplicationsOptoins.Items[4];
            scheduleTestsItems.DropDownItems[Index].Enabled = Value;
        }
        void _EnapleAndDisableLDLAOptions()
        {
            byte PassedTests = Convert.ToByte(dgvLocalDrivingLicenseApplications.SelectedCells[5].Value);

            switch ((dgvLocalDrivingLicenseApplications.SelectedCells[6].Value).ToString().ToLower())
            {
                case "new":
                    {
                        _EnapleAndDisableMainToolSripItem(0, true);

                        if (PassedTests < 3 && PassedTests >= 0)
                        {
                            _EnapleAndDisableMainToolSripItem(3, true);
                            _EnapleAndDisableMainToolSripItem(4, true);
                            _EnapleAndDisableMainToolSripItem(5, false);
                            _EnapleAndDisableMainToolSripItem(6, false);

                            switch (PassedTests)
                            {
                                case 0:
                                    _EnapleAndDisableScehduleToolSripItem(0, true);
                                    _EnapleAndDisableScehduleToolSripItem(1, false);
                                    _EnapleAndDisableScehduleToolSripItem(2, false);

                                    _EnapleAndDisableMainToolSripItem(1, true);
                                    _EnapleAndDisableMainToolSripItem(2, true); 
                                    break;

                                case 1:
                                    _EnapleAndDisableScehduleToolSripItem(0, false);
                                    _EnapleAndDisableScehduleToolSripItem(1, true);
                                    _EnapleAndDisableScehduleToolSripItem(2, false);

                                    _EnapleAndDisableMainToolSripItem(1, false);
                                    _EnapleAndDisableMainToolSripItem(2, false);
                                    break;

                                case 2:
                                    _EnapleAndDisableScehduleToolSripItem(0, false);
                                    _EnapleAndDisableScehduleToolSripItem(1, false);
                                    _EnapleAndDisableScehduleToolSripItem(2, true);

                                    _EnapleAndDisableMainToolSripItem(1, false);
                                    _EnapleAndDisableMainToolSripItem(2, false);
                                    break;
                            }
                            break;
                        }
                        else if (PassedTests == 3)
                        {
                            _EnapleAndDisableMainToolSripItem(1, false);
                            _EnapleAndDisableMainToolSripItem(2, false);
                            _EnapleAndDisableMainToolSripItem(3, false);
                            _EnapleAndDisableMainToolSripItem(4, false);
                            _EnapleAndDisableMainToolSripItem(5, true);
                            _EnapleAndDisableMainToolSripItem(6, false);
                        }
                        break;
                    }

                case "cancelled":
                    {
                        _EnapleAndDisableMainToolSripItem(0, true);
                        _EnapleAndDisableMainToolSripItem(1, false);

                        if (PassedTests == 0)
                            _EnapleAndDisableMainToolSripItem(2, true);
                        else
                            _EnapleAndDisableMainToolSripItem(2, false);

                        _EnapleAndDisableMainToolSripItem(3, false);
                        _EnapleAndDisableMainToolSripItem(4, false);
                        _EnapleAndDisableMainToolSripItem(5, false);
                        _EnapleAndDisableMainToolSripItem(6, false);
                        break;
                    }

                case "completed":
                    {
                        _EnapleAndDisableMainToolSripItem(0, true);
                        _EnapleAndDisableMainToolSripItem(1, false);
                        _EnapleAndDisableMainToolSripItem(2, false);
                        _EnapleAndDisableMainToolSripItem(3, false);
                        _EnapleAndDisableMainToolSripItem(4, false);
                        _EnapleAndDisableMainToolSripItem(5, false);
                        _EnapleAndDisableMainToolSripItem(6, true);
                        break;
                    }
            }
        }
        void _SchduleNewTest()
        {
            int LDLAppID = Convert.ToInt32(dgvLocalDrivingLicenseApplications.SelectedCells[0].Value);
            byte PassedTests = Convert.ToByte(dgvLocalDrivingLicenseApplications.SelectedCells[5].Value);
            frmListTestAppointments frm = new frmListTestAppointments(LDLAppID, PassedTests);
            frm.OnClose += _RefreshLDLApplicationsList;
            frm.ShowDialog();
        }
        void _DeleteLDLApplication()
        {
            if (MessageBox.Show("Are you sure you want to delete this Application?", "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                //if (clsLocalDrivingLicenseApplication.Delete(Convert.ToInt32(dgvLocalDrivingLicenseApplications.SelectedCells[0].Value)))
                //{
                //    MessageBox.Show("Application Deleted Successfully!", "Succed",
                //MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    _RefreshLDLApplicationsList();
                //}
                //else
                //{
                //    MessageBox.Show("Failed to delete!, this application has linked to another things", "Failed",
                //                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                //}
            }
            else
                MessageBox.Show("Delete Operation Was Cancelled!", "Cancelled",
              MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void btnAddLDLApplication_Click(object sender, EventArgs e)
        {
            _AddNewLDLApplication();
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Local Driving License Application ID
            frmLocalDrivingLicenseApplicationInfo frm = new frmLocalDrivingLicenseApplicationInfo(Convert.ToInt32(dgvLocalDrivingLicenseApplications.SelectedCells[0].Value));
            frm.ShowDialog();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateLocalDrivingLicesnseApplication frm = new frmAddUpdateLocalDrivingLicesnseApplication(Convert.ToInt32(dgvLocalDrivingLicenseApplications.SelectedCells[0].Value));
        }

        private void DeletetoolStripMenuItem1_Click(object sender, EventArgs e)
        {
            _DeleteLDLApplication();
        }

        private void CancelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _CancelApplication();
        }

        private void gtxtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (gcbFilterBy.Text != "L.D.L.AppID")
                return;

            if(!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void gcbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            _FilterByAtDesign();
        }

        private void gcbFilterByStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            _FilterByFilterByStatus();
        }

        private void gtxtFilterValue_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(gtxtFilterValue.Text))
                _FilterData("");

            else if (gcbFilterBy.Text.ToLower() == "L.D.L.AppID".ToLower())
                _FilterData($"[L.D.L.AppID] = '{gtxtFilterValue.Text}'");

            else if (gcbFilterBy.Text.ToLower() == "National No.".ToLower())
                _FilterData($"[National No]. like '{gtxtFilterValue.Text}%'");

            else if (gcbFilterBy.Text.ToLower() == "Full Name".ToLower())
                _FilterData($"[Full Name] like '{gtxtFilterValue.Text}%'");
        }

        private void gbtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void scheduleVisionTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _SchduleNewTest();
        }

        private void cmpLDLApplicationsOptoins_Opening(object sender, CancelEventArgs e)
        {
            _EnapleAndDisableLDLAOptions();
        }

        private void scheduleWrittenTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _SchduleNewTest();
        }

        private void scheduleStreetTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _SchduleNewTest();
        }

        private void CMSIIssueDrivingLicenseFirstTime_Click(object sender, EventArgs e)
        {
            //Local Driving License Application ID
            frmIssueDriverLicenseForFirstTime frm = new frmIssueDriverLicenseForFirstTime(Convert.ToInt32(dgvLocalDrivingLicenseApplications.SelectedCells[0].Value));
            frm.OnClose += _RefreshLDLApplicationsList;
            frm.ShowDialog();
        }

        private void CMSIshowLicense_Click(object sender, EventArgs e)
        {
            //Local Driving License Application ID
            int LDLApplicationID = Convert.ToInt32(dgvLocalDrivingLicenseApplications.SelectedCells[0].Value);
            clsLocalDrivingLicenseApplication LDLApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(LDLApplicationID);
            clsApplication Application = clsApplication.FindBaseApplication(LDLApplication.ApplicationID);
            clsLicense License =  clsLicense.FindByApplicationID(Application.ApplicationID);
            frmLicneseInfo frm = new frmLicneseInfo(License.LicenseID);
            frm.ShowDialog();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // National No 
            int PersonID = clsPerson.Find((string)dgvLocalDrivingLicenseApplications.SelectedCells[2].Value).PersonID;

            frmLicenseHistory frm = new frmLicenseHistory(PersonID);
            frm.ShowDialog();
        }
    }
}

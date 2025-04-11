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
    public partial class frmLocalDrivingLicenseApplications : Form
    {
        DataView _dvAllLDLApplications;
        public frmLocalDrivingLicenseApplications()
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
                dgvLDLApplications.DataSource = _dvAllLDLApplications;
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
            clsLocalDrivingApplictions LDLApplication = clsLocalDrivingApplictions.Find(Convert.ToInt32(dgvLDLApplications.SelectedCells[0].Value));
            clsApplications Application = clsApplications.Find(LDLApplication.ApplicationID);
            Application.ApplicationStatusID = clsApplicationStatuses.Find("Cancelled").ApplicationStatusID;

            if(MessageBox.Show("Are you sure you want to Cancel this application?","Confirm",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Question,MessageBoxDefaultButton.Button1) == DialogResult.OK)
            {
                if (Application.Save())
                {
                    MessageBox.Show("Application Cancelled Successfully!", "Succed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _Load_RefreshDataInDGV();
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
        void _EditSizeOfDGVColumns()
        {
            dgvLDLApplications.Columns["L.D.L.AppID"].Width = 110;
            dgvLDLApplications.Columns["Driving Class"].Width = 250;
            dgvLDLApplications.Columns["National No."].Width = 110;
            dgvLDLApplications.Columns["Full Name"].Width = 350;
            dgvLDLApplications.Columns["Application Date"].Width = 150;
            dgvLDLApplications.Columns["Passed Tests"].Width = 120;
            dgvLDLApplications.Columns["Status"].Width = 100;
        }
        void _AddNewLDLApplication()
        {
            frmNewLocalLicenseApplication frm = new frmNewLocalLicenseApplication(-1);

            frm.OnClose += FrmAddNewLDLApplication_OnClose;

            frm.ShowDialog();
        }
        private void FrmAddNewLDLApplication_OnClose()
        {
            _Load_RefreshDataInDGV();
        }
        void _Load_RefreshDataInDGV()
        {
            DataTable dtAllLDLApplications = clsLocalDrivingApplictions.GetAllLocalDrivingApplicationsWithINNERJOIN();

            if (dtAllLDLApplications.Rows.Count > 0)
            {
                dtAllLDLApplications.Columns.Add("Full Name", typeof(string));
                dtAllLDLApplications.Columns["LocalDrvingApplicationID"].ColumnName = "L.D.L.AppID";
                dtAllLDLApplications.Columns["ClassName"].ColumnName = "Driving Class";
                dtAllLDLApplications.Columns["NationalNo"].ColumnName = "National No.";
                dtAllLDLApplications.Columns["ApplicationDate"].ColumnName = "Application Date";
                dtAllLDLApplications.Columns["PassedTests"].ColumnName = "Passed Tests";
                dtAllLDLApplications.Columns["ApplicationStatus"].ColumnName = "Status";

                foreach (DataRow row in dtAllLDLApplications.Rows)
                {
                    row["Full Name"] = string.Concat(row["FirstName"], " ", row["SecondName"], " ",
                        row["ThirdName"], " ", row["LastName"]);
                }
                DataTable dtAfterChooseColumns =  dtAllLDLApplications.DefaultView.ToTable(false, "L.D.L.AppID", "Driving Class",
                   "National No.", "Full Name", "Application Date", "Passed Tests", "Status");


                _dvAllLDLApplications = dtAfterChooseColumns.DefaultView;
                dgvLDLApplications.DataSource = _dvAllLDLApplications;
                _EditSizeOfDGVColumns();
                _GetTextFilterEmpty();

                lblNumOfRecords.Text = _dvAllLDLApplications.Count.ToString();
                gcbFilterBy.SelectedIndex = 0;
            }
            else
            {
                lblNumOfRecords.Text = "0";
            }
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
            byte PassedTests = Convert.ToByte(dgvLDLApplications.SelectedCells[5].Value);

            switch ((dgvLDLApplications.SelectedCells[6].Value).ToString().ToLower())
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
            int LDLAppID = Convert.ToInt32(dgvLDLApplications.SelectedCells[0].Value);
            byte PassedTests = Convert.ToByte(dgvLDLApplications.SelectedCells[5].Value);
            frmTestAppointments frm = new frmTestAppointments(LDLAppID, PassedTests);
            frm.OnClose += _Load_RefreshDataInDGV;
            frm.ShowDialog();
        }
        void _DeleteLDLApplication()
        {
            if (MessageBox.Show("Are you sure you want to delete this Application?", "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                if (clsLocalDrivingApplictions.Delete(Convert.ToInt32(dgvLDLApplications.SelectedCells[0].Value)))
                {
                    MessageBox.Show("Application Deleted Successfully!", "Succed",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _Load_RefreshDataInDGV();
                }
                else
                {
                    MessageBox.Show("Failed to delete!, this application has linked to another things", "Failed",
                                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
                MessageBox.Show("Delete Operation Was Cancelled!", "Cancelled",
              MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void frmLocalDrivingLicenseApplications_Load(object sender, EventArgs e)
        {
            _Load_RefreshDataInDGV();
            gcbFilterBy.SelectedIndex = 0;
        }

        private void btnAddLDLApplication_Click(object sender, EventArgs e)
        {
            _AddNewLDLApplication();
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Local Driving License Application ID
            frmApplicationDetails frm = new frmApplicationDetails(Convert.ToInt32(dgvLDLApplications.SelectedCells[0].Value));
            frm.ShowDialog();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmNewLocalLicenseApplication frm = new frmNewLocalLicenseApplication(Convert.ToInt32(dgvLDLApplications.SelectedCells[0].Value));
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
            frmIssueDriverLicenseForFirstTime frm = new frmIssueDriverLicenseForFirstTime(Convert.ToInt32(dgvLDLApplications.SelectedCells[0].Value));
            frm.OnClose += _Load_RefreshDataInDGV;
            frm.ShowDialog();
        }

        private void CMSIshowLicense_Click(object sender, EventArgs e)
        {
            //Local Driving License Application ID
            frmLicneseInfo frm = new frmLicneseInfo(Convert.ToInt32(dgvLDLApplications.SelectedCells[0].Value));
            frm.ShowDialog();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // National No 
            int PersonID = clsPeople.Find((string)dgvLDLApplications.SelectedCells[2].Value).PersonID;

            frmLicenseHistory frm = new frmLicenseHistory(PersonID);
            frm.ShowDialog();
        }
    }
}

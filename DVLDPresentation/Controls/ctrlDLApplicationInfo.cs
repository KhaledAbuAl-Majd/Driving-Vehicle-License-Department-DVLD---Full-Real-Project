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

namespace DVLDPresentation.Controls
{
    public partial class ctrlDLApplicationInfo : UserControl
    {
        public int LDLApplicationID;

        public ctrlDLApplicationInfo()
        {
            InitializeComponent();
        }
        int _PersonID;

        public void _ChangePassedTestsLabel()
        {
            lblPassedTests.Text = clsLocalDrivingApplictions.Find(LDLApplicationID).PassedTests.ToString() + "/3";
        }

        private void _FillDataInLables()
        {
            clsLocalDrivingApplictions LDLApplicatoin = clsLocalDrivingApplictions.Find(LDLApplicationID);

            if (LDLApplicatoin != null)
            {
                clsApplications Application = clsApplications.Find(LDLApplicatoin.ApplicationID);
                clsLicneseClasses LicenseClass = clsLicneseClasses.Find(LDLApplicatoin.LicenseClassID);
                if (Application.ApplicationStatusID == clsApplicationStatuses.Find("Completed").ApplicationStatusID)
                    _ChangeEnablityForllblShowLicneseInfo(true);
                else
                    _ChangeEnablityForllblShowLicneseInfo(false);

                    lblD_L_ApplicationID.Text = LDLApplicatoin.LocalDrvingApplicationID.ToString();
                lblClassName.Text = LicenseClass.ClassName;
                lblPassedTests.Text = LDLApplicatoin.PassedTests.ToString() + "/3";
                lblApplicationID.Text = LDLApplicatoin.ApplicationID.ToString(); ;
                lblStatus.Text = clsApplicationStatuses.Find(Application.ApplicationStatusID).ApplicationStatus;
                clsApplicationTypes ApplicationType = clsApplicationTypes.FindApplicationType(Application.ApplicationTypeID);
                lblFees.Text = ApplicationType.ApplicationFees.ToString();
                lblType.Text = ApplicationType.ApplicationTypeTitle;
                lblApplicant.Text = clsPeople.Find(Application.PersonID).GetFullName();
                lblDate.Text = Application.ApplicationDate.ToString("dd/MMM/yyyy");
                lblStatusDate.Text = Application.LastStatusDate.ToString("dd/MMM/yyyy");
                lblCreatedBy.Text = clsUsers.Find(Application.CreatedByUserID).UserName;
                _PersonID = Application.PersonID;
                llblViewPersonInfo.Enabled = (_PersonID == -1) ? false : true;
            }
            else
                _ChangeEnablityForllblShowLicneseInfo(false);
     
        }
        private void _ChangeEnablityForllblShowLicneseInfo(bool value)
        {
            llblShowLicenseInfo.Enabled = value;
        }
        private void llblViewPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmPersonDetails frm = new frmPersonDetails(_PersonID);

            frm.OnClose += FrmViewPersonInfo_OnClose;
            frm.ShowDialog();
        }

        private void FrmViewPersonInfo_OnClose()
        {
            _FillDataInLables();
        }

        private void ctrlDLApplicationInfo_Load(object sender, EventArgs e)
        {
            _FillDataInLables();
            
        }

        private void llblShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmLicneseInfo frm = new frmLicneseInfo(LDLApplicationID);
            frm.ShowDialog();
        }
    }
}

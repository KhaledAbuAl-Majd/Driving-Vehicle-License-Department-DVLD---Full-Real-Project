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
            //lblPassedTests.Text = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(LDLApplicationID).PassedTests.ToString() + "/3";
        }

        private void _FillDataInLables()
        {
            clsLocalDrivingLicenseApplication LDLApplicatoin = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(LDLApplicationID);

            if (LDLApplicatoin != null)
            {
                clsApplication Application = clsApplication.FindBaseApplication(LDLApplicatoin.ApplicationID);
                clsLicenseClass LicenseClass = clsLicenseClass.Find(LDLApplicatoin.LicenseClassID);
                //if (Application.ApplicationStatus == clsApplicationStatuses.Find("Completed").ApplicationStatusID)
                //    _ChangeEnablityForllblShowLicneseInfo(true);
                //else
                //    _ChangeEnablityForllblShowLicneseInfo(false);

                    lblD_L_ApplicationID.Text = LDLApplicatoin.LocalDrivingLicenseApplicationID.ToString();
                lblClassName.Text = LicenseClass.ClassName;
                //lblPassedTests.Text = LDLApplicatoin.PassedTests.ToString() + "/3";
                lblApplicationID.Text = LDLApplicatoin.ApplicationID.ToString(); ;
                //lblStatus.Text = clsApplicationStatuses.Find(Application.ApplicationStatus).ApplicationStatus;
                clsApplicationType ApplicationType = clsApplicationType.Find(Application.ApplicationTypeID);
                lblFees.Text = ApplicationType.ApplicationFees.ToString();
                lblType.Text = ApplicationType.ApplicationTypeTitle;
                lblApplicant.Text = clsPerson.Find(Application.ApplicantPersonID).FullName;
                lblDate.Text = Application.ApplicationDate.ToString("dd/MMM/yyyy");
                lblStatusDate.Text = Application.LastStatusDate.ToString("dd/MMM/yyyy");
                lblCreatedBy.Text = clsUser.FindByUserID(Application.CreatedByUserID).UserName;
                _PersonID = Application.ApplicantPersonID;
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
            frmShowPersonInfo frm = new frmShowPersonInfo(_PersonID);

            //frm.OnClose += FrmViewPersonInfo_OnClose;
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
            clsLicense License = clsLicense.FindByApplicationID(Convert.ToInt32(lblApplicationID.Text));
            frmLicneseInfo frm = new frmLicneseInfo(License.LicenseID);
            frm.ShowDialog();
        }
    }
}

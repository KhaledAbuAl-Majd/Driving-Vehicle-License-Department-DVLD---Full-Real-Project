using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLDPresentation.Applications;
using DVLDPresentation.Applications.Detain_Licenses;
using DVLDPresentation.Applications.Driving_License_Services.New_Driving_License;
using DVLDPresentation.Applications.Driving_License_Services.Renew_Driving_License;
using DVLDPresentation.Applications.Driving_License_Services.Replacment_For_Damage_Or_Lost;
using DVLDPresentation.Applications.Manage_Applications;
using DVLDPresentation.Applications.Manage_Applications.International_Driving_License_Application;
using DVLDPresentation.Applications.Test_Types;
using DVLDPresentation.Login_MainPage;
using DVLDPresentation.People;
using DVLDPresentation.Users;

namespace DVLDPresentation.Login_HomePage
{
    public partial class frmMain : Form
    {
        frmLogin _frmLogin;

        void _SignOut()
        {
            //When You Return
            clsGlobalSettings.CurrentUser = null;
            _frmLogin.Show();
        }
        public frmMain(frmLogin frm)
        {
            InitializeComponent();
            _frmLogin = frm;
        }
        private void peopleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListPeople frm = new frmListPeople();
            frm.ShowDialog();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
           
        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListUsers frm = new frmListUsers();

            frm.ShowDialog();
        }

        private void currentUserInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUserInfo frm = new frmUserInfo(clsGlobalSettings.CurrentUser.UserID);
            frm.ShowDialog();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChangePassword frm = new frmChangePassword(clsGlobalSettings.CurrentUser.UserID);

            frm.ShowDialog();
        }

        private void signOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //it will call closing event and it will call sign out function
            this.Close();
        }

        private void drivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void manageApplicationTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListApplicationTypes frm = new frmListApplicationTypes();

            frm.ShowDialog();
        }

        private void manageTestTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListTestTypes frm = new frmListTestTypes();

            frm.ShowDialog();
        }

        private void tmslocalLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmNewLocalLicenseApplication frm = new frmNewLocalLicenseApplication(-1);

            frm.ShowDialog();
        }

        private void localDrivingLicenseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLocalDrivingLicenseApplications frm = new frmLocalDrivingLicenseApplications();

            frm.ShowDialog();
        }

        private void driversToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDrivers frm = new frmDrivers();

            frm.ShowDialog();
        }

        private void tmsInterantionalLicense_Click(object sender, EventArgs e)
        {
            frmNewInternationalLicneseApplication frm = new frmNewInternationalLicneseApplication();
            frm.ShowDialog();
        }

        private void internationalLicenseApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListInternationalDrivingLicenseApplications frm = new frmListInternationalDrivingLicenseApplications();
            frm.ShowDialog();
        }

        private void renewDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRenewLocalLicense frm = new frmRenewLocalLicense();
            frm.ShowDialog();
        }

        private void replacementForLostOrDamagedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReplacementForDamagedOrLost frm = new frmReplacementForDamagedOrLost();
            frm.ShowDialog();
        }

        private void releaseDetainedDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void retakeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLocalDrivingLicenseApplications frm = new frmLocalDrivingLicenseApplications();

            frm.ShowDialog();
        }

        private void manageDetainedLicensesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListDetainedLicenses frm = new frmListDetainedLicenses();
            frm.ShowDialog();
        }

        private void detainLicenseToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmDetainLicense frm = new frmDetainLicense();
            frm.ShowDialog();
        }

        private void releaseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicense frm = new frmReleaseDetainedLicense(-1);
            frm.ShowDialog();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            _SignOut();
        }
    }
}

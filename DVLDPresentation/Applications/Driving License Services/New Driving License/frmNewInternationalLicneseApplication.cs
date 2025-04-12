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
using DVLDPresentation.Applications.Manage_Applications.International_Driving_License_Application;
using DVLDPresentation.Applications.Manage_Applications.LocalDrivingLicenseApplications;

namespace DVLDPresentation.Applications.Driving_License_Services.New_Driving_License
{
    public partial class frmNewInternationalLicneseApplication : Form
    {
        public event Action OnClose;

        private bool _IsIssued = false;
        //International License 
        int _ApplicationTypeID = 6;
        int _PersonID;
        clsLicenses _LocalLicense;
        int _InternationLicenseID;

        public frmNewInternationalLicneseApplication()
        {
            InitializeComponent();
        }

        //Application, Issue, Expiration Date, Created By and Fees
        void _ChangeEnaplityOfIssueButton(bool Value)
        {
            gbtnIssue.Enabled = Value;
        }
        void _ChangeEnaplityOfLinkLabel(LinkLabel llbl,bool Value)
        {
            llbl.Enabled = Value;
        }
        
        void _InitializeDataInLoad()
        {
            lblApplicationDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            lblIssueDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            lblFees.Text = clsApplicationTypes.FindApplicationType(_ApplicationTypeID).ApplicationFees.ToString();
            lblExpirationDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            lblCreatedBy.Text = clsGlobalSettings.LoggedInUser.UserName;
        }
        void _ErrorAtSearch(string Text,string Caption)
        {
            MessageBox.Show(Text, Caption , MessageBoxButtons.OK, MessageBoxIcon.Error);
            gtxtFilterValue.Focus();
            ctrlDriverLicenseInfo1._EmptyLabels();
            _ChangeEnaplityOfLinkLabel(llblShowLicenseHistory, false);
            _ChangeEnaplityOfLinkLabel(llblShowLicenseInfo, false);
            _ChangeEnaplityOfIssueButton(false);
            _IsIssued = false;
        }
        void _SearchAtLicense()
        {
            if (string.IsNullOrWhiteSpace((gtxtFilterValue.Text)))
            {
                _ErrorAtSearch("Licnese ID Cannot by empty, Please Enter a License ID!", "Error");            
                return;
            }

            int EnteredLicenseID = Convert.ToInt32(gtxtFilterValue.Text);

            if (!clsLicenses.IsLicenseExist(EnteredLicenseID))
            {
                _ErrorAtSearch($"Not License With Licnese ID  = {EnteredLicenseID}, Please Enter a Correct One!", "Not Exist");
                return;
            }

            clsLicenses LocalLicense = clsLicenses.FindByLicenseID(EnteredLicenseID);
            clsDrivers Driver = clsDrivers.FindByDriverID(LocalLicense.DriverID);

            int SearchedInternationalLicenseID = clsInternationalLicenses.GetInternationalLicenseIfPersonHasActiveOne(Driver.PersonID);
            if (SearchedInternationalLicenseID != -1)
            {
                _ErrorAtSearch($"Person already have an active international license with ID = {SearchedInternationalLicenseID}", "Not allowed");
                return;
            }

            //Class 3 - Ordinary Driving License
            if (LocalLicense.LicenseClassID != 3)
            {
                _ErrorAtSearch("License Must be Class 3 - Ordinary Driving License!", "Not Allowed");
                return;
            }

            clsLocalDrivingApplictions LDLApplication = clsLocalDrivingApplictions.FindByApplicationID(LocalLicense.ApplicationID);

            ctrlDriverLicenseInfo1.FillDataInLabels(LDLApplication.LocalDrvingApplicationID);
            lblLocalLicenseID.Text = LocalLicense.LicneseID.ToString();
            _LocalLicense = LocalLicense;
            _ChangeEnaplityOfLinkLabel(llblShowLicenseHistory, true);
            _PersonID = Driver.PersonID;
            _ChangeEnaplityOfIssueButton(true);
        }
        private void frmNewInternationalLicneseApplication_Load(object sender, EventArgs e)
        {
            _ChangeEnaplityOfIssueButton(false);
            _InitializeDataInLoad();
            _ChangeEnaplityOfLinkLabel(llblShowLicenseHistory, false);
            _ChangeEnaplityOfLinkLabel(llblShowLicenseInfo, false);
        }
        private void gtxtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        private void btnLicenseSearch_Click(object sender, EventArgs e)
        {
            _SearchAtLicense();
        }

        private void llblShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmLicenseHistory frm = new frmLicenseHistory(_PersonID);
            frm.ShowDialog();
        }

        private void gbtnIssue_Click(object sender, EventArgs e)
        {
            clsInternationalLicenses NewInternationalLicense = new clsInternationalLicenses
                (_LocalLicense.LicneseID, clsGlobalSettings.LoggedInUser.UserID);

            if (NewInternationalLicense.Save())
            {
                _IsIssued = true;
                MessageBox.Show($"International License Issued Successfully With ID = {NewInternationalLicense.InternationalLicenseID}",
                    "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);

                _InternationLicenseID = NewInternationalLicense.InternationalLicenseID;
                _ChangeEnaplityOfIssueButton(false);
                _ChangeEnaplityOfLinkLabel(llblShowLicenseHistory, true);
                _ChangeEnaplityOfLinkLabel(llblShowLicenseInfo, true);
                lblILApplicationID.Text = NewInternationalLicense.ILApplicationID.ToString();
                lblILLicenseID.Text = NewInternationalLicense.InternationalLicenseID.ToString();
                gbFilterBy.Enabled = false;
            }
            else
            {
                _IsIssued = false;
                MessageBox.Show($"Error To Issue License!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void llblShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmInternationalLicenseInfo frm = new frmInternationalLicenseInfo(_InternationLicenseID);

            frm.ShowDialog();
        }

        private void gbtnClose_Click(object sender, EventArgs e)
        {
            if (_IsIssued)
                if (OnClose != null)
                    OnClose();

            this.Close();
        }
    }
}

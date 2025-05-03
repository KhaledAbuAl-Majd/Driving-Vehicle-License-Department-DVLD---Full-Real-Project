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
using DVLDPresentation.Controls;
using DVLDPresentation.Global_Classes;

namespace DVLDPresentation.Applications.Driving_License_Services.New_Driving_License
{
    public partial class frmNewInternationalLicneseApplication : Form
    {
        public event Action OnClose;
        private bool _IsIssued = false;

        private int _InternationalLicenseID = -1;

        void _ChangeEnaplityOfIssueButton(bool Value)
        {
            gbtnIssue.Enabled = Value;
        }
        void _ChangeEnaplityOfLinkLabel(LinkLabel llbl,bool Value)
        {
            llbl.Enabled = Value;
        }
        void _ErrorAtSearch()
        {
            _ChangeEnaplityOfLinkLabel(llblShowLicenseHistory, false);
            _ChangeEnaplityOfLinkLabel(llblShowLicenseInfo, false);
            _ChangeEnaplityOfIssueButton(false);
            _IsIssued = false;
            lblLocalLicenseID.Text = "???";
        }

        public frmNewInternationalLicneseApplication()
        {
            InitializeComponent();       
        }
    
        private void ctrlDriverLicenseInfoCardWithFilter1_OnLicenseSelected(int obj)
        {
            int SelectedLicenseID = obj;


            if (SelectedLicenseID == -1)
                return;


            if (!ctrlDriverLicenseInfoCardWithFilter1.SelectedLicenseInfo.IsActive)
            {
                MessageBox.Show("Selected License is not Not Active, choose an active license.", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _ErrorAtSearch();
                return;
            }

            if (ctrlDriverLicenseInfoCardWithFilter1.SelectedLicenseInfo.IsDetained)
            {
                MessageBox.Show("Selected License Is Detained,Release It First", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _ErrorAtSearch();
                return;
            }

            if (ctrlDriverLicenseInfoCardWithFilter1.SelectedLicenseInfo.LicenseClassID !=3)
            {
                MessageBox.Show("Selected License should be Class 3, select another one.", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _ErrorAtSearch();
                return;
            }


            // Is Not Expired Yet
            int ActiveInternaionalLicenseID = clsInternationalLicense.GetActiveInternationalLicenseIDByDriverID(ctrlDriverLicenseInfoCardWithFilter1.SelectedLicenseInfo.DriverID);

            if (ActiveInternaionalLicenseID != -1)
            {
                MessageBox.Show("Person already have an active international license with ID = " + ActiveInternaionalLicenseID.ToString(), "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);;
                _InternationalLicenseID = ActiveInternaionalLicenseID;
                _ErrorAtSearch();
                return;
            }

            lblLocalLicenseID.Text = SelectedLicenseID.ToString();
            _ChangeEnaplityOfLinkLabel(llblShowLicenseHistory, true);
            _ChangeEnaplityOfIssueButton(true);
        }

        private void frmNewInternationalLicneseApplication_Load(object sender, EventArgs e)
        {
            _ChangeEnaplityOfIssueButton(false);
            _ChangeEnaplityOfLinkLabel(llblShowLicenseHistory, false);
            _ChangeEnaplityOfLinkLabel(llblShowLicenseInfo, false);

            lblApplicationDate.Text = clsFormat.DateToShort(DateTime.Now);
            lblIssueDate.Text = lblApplicationDate.Text;
            lblExpirationDate.Text = clsFormat.DateToShort(DateTime.Now.AddYears(1));//Add One year
            lblFees.Text = clsApplicationType.Find((int)clsApplication.enApplicationType.NewInternationalLicense).ApplicationFees.ToString();
            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;
        }

        private void gbtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gbtnIssue_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to issue the license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            clsLicense LocalLicense = ctrlDriverLicenseInfoCardWithFilter1.SelectedLicenseInfo;

            clsInternationalLicense InternationalLicense = new clsInternationalLicense();

            InternationalLicense.ApplicantPersonID = LocalLicense.DriverInfo.PersonID;
            InternationalLicense.ApplicationDate = DateTime.Now;
            InternationalLicense.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
            InternationalLicense.LastStatusDate = DateTime.Now;
            InternationalLicense.PaidFees = clsApplicationType.Find((int)clsApplication.enApplicationType.NewInternationalLicense).ApplicationFees;
            InternationalLicense.CreatedByUserID = clsGlobal.CurrentUser.UserID;

            InternationalLicense.DriverID = LocalLicense.DriverID;
            InternationalLicense.IssuedUsingLocalLicenseID = LocalLicense.LicenseID;
            InternationalLicense.IssueDate = DateTime.Now;
            InternationalLicense.ExpirationDate = DateTime.Now.AddYears(1);//Add One year

            if (!InternationalLicense.Save())
            {
                MessageBox.Show("Faild to Issue International License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            lblILApplicationID.Text = InternationalLicense.ApplicationID.ToString();
            _InternationalLicenseID = InternationalLicense.InternationalLicenseID;
            lblInternationalLicenseID.Text = InternationalLicense.InternationalLicenseID.ToString();
            MessageBox.Show("International License Issued Successfully with ID = " + InternationalLicense.InternationalLicenseID.ToString(), "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);

            _ChangeEnaplityOfIssueButton(false);
            ctrlDriverLicenseInfoCardWithFilter1.FilterEnabled = false;
            _ChangeEnaplityOfLinkLabel(llblShowLicenseInfo, true);
        }

        private void llblShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(ctrlDriverLicenseInfoCardWithFilter1.SelectedLicenseInfo.DriverInfo.PersonID);
            frm.ShowDialog();
        }

        private void llblShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowInternationalLicenseInfo frm = new frmShowInternationalLicenseInfo(_InternationalLicenseID);

            frm.ShowDialog();
        }

        private void frmNewInternationalLicneseApplication_Activated(object sender, EventArgs e)
        {
            ctrlDriverLicenseInfoCardWithFilter1.txtLicenseIDFocus();
        }

        private void ctrlDriverLicenseInfoCardWithFilter1_OnErrorAtSearch()
        {
            _ErrorAtSearch();
        }

        private void frmNewInternationalLicneseApplication_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_IsIssued)
                if (OnClose != null)
                    OnClose();
        }
    }
}

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
        clsLicense _LocalLicense;
        int _InternationLicenseID;

        public frmNewInternationalLicneseApplication()
        {
            InitializeComponent();
            ctrlDriverLicenseInfoCardWithFilter1.Mode = DVLDPresentation.Controls.ctrlDriverLicenseInfoCardWithFilter.enMode.NewInternationalLicense;
            ctrlDriverLicenseInfoCardWithFilter1.OnErrorAtSearch += _OnErrorAtSearch;
            ctrlDriverLicenseInfoCardWithFilter1.OnSuccedAtSearch += OnSuccedAtSearch_OnSuccedAtSearch;
        }

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
            lblFees.Text = clsApplicationType.Find(_ApplicationTypeID).ApplicationFees.ToString();
            lblExpirationDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            lblCreatedBy.Text = clsGlobal.CurrentUser.UserName;
        }
        void _OnErrorAtSearch()
        {
            //MessageBox.Show(Text, Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //gtxtFilterValue.Focus();
            //ctrlDriverLicenseInfo1._EmptyLabels();
            _ChangeEnaplityOfLinkLabel(llblShowLicenseHistory, false);
            _ChangeEnaplityOfLinkLabel(llblShowLicenseInfo, false);
            _ChangeEnaplityOfIssueButton(false);
            _IsIssued = false;
            lblLocalLicenseID.Text = "???";
        }
        private void OnSuccedAtSearch_OnSuccedAtSearch(int PersonID, clsLicense LocalLicense, object sender)
        {
            _PersonID = PersonID;
            _LocalLicense = LocalLicense;
            lblLocalLicenseID.Text = _LocalLicense.LicenseID.ToString();
            _ChangeEnaplityOfLinkLabel(llblShowLicenseHistory, true);
            _ChangeEnaplityOfIssueButton(true);
        }
        private void frmNewInternationalLicneseApplication_Load(object sender, EventArgs e)
        {
            _ChangeEnaplityOfIssueButton(false);
            _InitializeDataInLoad();
            _ChangeEnaplityOfLinkLabel(llblShowLicenseHistory, false);
            _ChangeEnaplityOfLinkLabel(llblShowLicenseInfo, false);
        }

        private void llblShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(_PersonID);
            frm.ShowDialog();
        }

        private void gbtnIssue_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("Are you sure you want to Issue this License?", "Confirm",
               MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)) == DialogResult.Yes)
            {

                //clsInternationalLicense NewInternationalLicense = new clsInternationalLicense
                //(_LocalLicense.LicenseID, clsGlobalSettings.CurrentUser.UserID);

                //if (NewInternationalLicense.Save())
                //{
                //    _IsIssued = true;
                //    MessageBox.Show($"International License Issued Successfully With ID = {NewInternationalLicense.InternationalLicenseID}",
                //        "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //    _InternationLicenseID = NewInternationalLicense.InternationalLicenseID;
                //    _ChangeEnaplityOfIssueButton(false);
                //    _ChangeEnaplityOfLinkLabel(llblShowLicenseHistory, true);
                //    _ChangeEnaplityOfLinkLabel(llblShowLicenseInfo, true);
                //    lblILApplicationID.Text = NewInternationalLicense.ApplicationID.ToString();
                //    lblILLicenseID.Text = NewInternationalLicense.InternationalLicenseID.ToString();
                //    ctrlDriverLicenseInfoCardWithFilter1.ChangeEnaplityOfGBFilterBy(false);
                //}
                //else
                //{
                //    _IsIssued = false;
                //    MessageBox.Show($"Error To Issue License!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //}
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

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

namespace DVLDPresentation.Applications.Driving_License_Services.Renew_Driving_License
{
    public partial class frmRenewLocalLicense : Form
    {
        //Renew Driving License Service ID
        int _ApplicationTypeID = 2;
        int _PersonID;
        clsLicenses _OLDLocalLicense;
        int _NewLicenseID;
        public frmRenewLocalLicense()
        {
            InitializeComponent();
            ctrlDriverLicenseInfoCardWithFilter1.Mode = DVLDPresentation.Controls.ctrlDriverLicenseInfoCardWithFilter.enMode.RenewLocalLicense;
            ctrlDriverLicenseInfoCardWithFilter1.OnErrorAtSearch += _OnErrorAtSearch;
            ctrlDriverLicenseInfoCardWithFilter1.OnSuccedAtSearch += OnSuccedAtSearch_OnSuccedAtSearch;
        }

        void _DeActivateOldLicense()
        {
            _OLDLocalLicense.DeActiveLicense();
        }
        void _ChangeEnaplityOfRenewButton(bool Value)
        {
            gbtnRenew.Enabled = Value;
        }
        void _ChangeEnaplityOfLinkLabel(LinkLabel llbl, bool Value)
        {
            llbl.Enabled = Value;
        }

        void _InitializeDataInLoad()
        {
            lblApplicationDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            lblIssueDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            lblApplicationFees.Text = clsApplicationType.Find(_ApplicationTypeID).ApplicationFees.ToString();
            lblCreatedBy.Text = clsGlobalSettings.CurrentUser.UserName;
        }
        void _OnErrorAtSearch()
        {
            _ChangeEnaplityOfLinkLabel(llblShowLicenseHistory, false);
            _ChangeEnaplityOfLinkLabel(llblShowNewLicenseInfo, false);
            _ChangeEnaplityOfRenewButton(false);
            lblOldLicenseID.Text = "???";
        }
        private void OnSuccedAtSearch_OnSuccedAtSearch(int PersonID, clsLicenses LocalLicense, object sender)
        {
            _PersonID = PersonID;
            _OLDLocalLicense = LocalLicense;
            lblOldLicenseID.Text = LocalLicense.LicneseID.ToString();
            lblLicenseFees.Text = clsLicneseClasses.Find(LocalLicense.LicenseClassID).ClassFees.ToString();
            lblTotalFees.Text = (Convert.ToSingle(lblApplicationFees.Text) + Convert.ToSingle(lblLicenseFees.Text)).ToString();
            _ChangeEnaplityOfLinkLabel(llblShowLicenseHistory, true);
            _ChangeEnaplityOfRenewButton(true);
        }

        private void ctrlDriverLicenseInfoCardWithFilter1_Load(object sender, EventArgs e)
        {
            _ChangeEnaplityOfRenewButton(false);
            _InitializeDataInLoad();
            _ChangeEnaplityOfLinkLabel(llblShowLicenseHistory, false);
            _ChangeEnaplityOfLinkLabel(llblShowNewLicenseInfo, false);
        }

        private void llblShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmLicenseHistory frm = new frmLicenseHistory(_PersonID);
            frm.ShowDialog();
        }

        private void llblShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmLicneseInfo frm = new frmLicneseInfo(_NewLicenseID);

            frm.ShowDialog();
        }

        private void gbtnRenew_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("Are you sure you want to Renew this License?", "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)) == DialogResult.Yes)
            {


                clsApplications NewApplication = new clsApplications
                {
                    ApplicationDate = DateTime.Now,
                    ApplicationStatusID = clsApplicationStatuses.Find("Completed").ApplicationStatusID,
                    ApplicationTypeID = this._ApplicationTypeID,
                    CreatedByUserID = clsGlobalSettings.CurrentUser.UserID,
                    LastStatusDate = DateTime.Now,
                    PersonID = this._PersonID
                };

                if (NewApplication.Save())
                {


                    clsLicenses _NewLicense = new clsLicenses(NewApplication, _OLDLocalLicense, "Renew");

                    if (_NewLicense.Save())
                    {
                        _DeActivateOldLicense();

                        MessageBox.Show($"License Issued Successfully With ID = {_NewLicense.LicneseID}",
                            "License Renewed", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        _NewLicenseID = _NewLicense.LicneseID;
                        _ChangeEnaplityOfRenewButton(false);
                        _ChangeEnaplityOfLinkLabel(llblShowLicenseHistory, true);
                        _ChangeEnaplityOfLinkLabel(llblShowNewLicenseInfo, true);
                        lblRenewILApplicationID.Text = _NewLicense.ApplicationID.ToString();
                        lblRenewedLicenseID.Text = _NewLicense.LicneseID.ToString();
                        lblExpirationDate.Text = _NewLicense.ExpirationDate.ToString("dd/MMM/yyyy");
                        ctrlDriverLicenseInfoCardWithFilter1.ChangeEnaplityOfGBFilterBy(false);
                    }
                    else
                    {
                        MessageBox.Show($"Error To Renew License!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void gbtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

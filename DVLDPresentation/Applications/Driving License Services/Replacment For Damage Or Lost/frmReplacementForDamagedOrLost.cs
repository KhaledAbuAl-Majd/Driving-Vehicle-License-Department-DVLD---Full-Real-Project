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

namespace DVLDPresentation.Applications.Driving_License_Services.Replacment_For_Damage_Or_Lost
{
    public partial class frmReplacementForDamagedOrLost : Form
    {
        enum enApplicationTypeID { ReplacementForLost = 3, ReplacementForDamaged = 4 }
        enApplicationTypeID _ApplicationTypeID_Mode;
        int _PersonID;
        clsLicense _OLDLocalLicense;
        int _NewLicenseID;
        public frmReplacementForDamagedOrLost()
        {
            InitializeComponent();
            ctrlDriverLicenseInfoCardWithFilter1.Mode = DVLDPresentation.Controls.ctrlDriverLicenseInfoCardWithFilter.enMode.ReplacementForDamaged_Lost;
            ctrlDriverLicenseInfoCardWithFilter1.OnErrorAtSearch += _OnErrorAtSearch;
            ctrlDriverLicenseInfoCardWithFilter1.OnSuccedAtSearch += OnSuccedAtSearch_OnSuccedAtSearch;
        }

        void _ChangeFormText(string Text)
        {
            this.Text = Text;
        }
        void _ChangeHeaderText(string Text)
        {
            lblHeader.Text = Text;
        }
        void _ChangeApplicationFeesLabels()
        {
            lblApplicationFees.Text = clsApplicationType.Find(Convert.ToInt32(_ApplicationTypeID_Mode)).ApplicationFees.ToString();
        }
        void _ReplacementForDamagedMode()
        {
            _ApplicationTypeID_Mode = enApplicationTypeID.ReplacementForDamaged;
            _ChangeHeaderText("Replacement for Damaged License");
            _ChangeFormText("Replacement for Damaged License");
            _ChangeApplicationFeesLabels();
        }
        void _ReplacementForLostMode()
        {
            _ApplicationTypeID_Mode = enApplicationTypeID.ReplacementForLost;
            _ChangeHeaderText("Replacement for Lost License");
            _ChangeFormText("Replacement for Lost License");
            _ChangeApplicationFeesLabels();
        }
        void _DeActivateOldLicense()
        {
            _OLDLocalLicense.DeactivateCurrentLicense();
        }
        void _ChangeEnaplityOfIssueReplacementButton(bool Value)
        {
            gbtnIssueReplacement.Enabled = Value;
        }
        void _ChangeEnaplityOfLinkLabel(LinkLabel llbl, bool Value)
        {
            llbl.Enabled = Value;
        }
        void _InitializeDataInLoad()
        {
            lblApplicationDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");  
            lblCreatedBy.Text = clsGlobal.CurrentUser.UserName;
        }
        void _OnErrorAtSearch()
        {
            _ChangeEnaplityOfLinkLabel(llblShowLicenseHistory, false);
            _ChangeEnaplityOfLinkLabel(llblShowNewLicenseInfo, false);
            _ChangeEnaplityOfIssueReplacementButton(false);
            lblOldLicenseID.Text = "???";
        }
        private void OnSuccedAtSearch_OnSuccedAtSearch(int PersonID, clsLicense LocalLicense, object sender)
        {
            _PersonID = PersonID;
            _OLDLocalLicense = LocalLicense;
            lblOldLicenseID.Text = LocalLicense.LicenseID.ToString();
            _ChangeEnaplityOfLinkLabel(llblShowLicenseHistory, true);
            _ChangeEnaplityOfIssueReplacementButton(true);
        }

        private void frmReplacementForDamagedOrLost_Load(object sender, EventArgs e)
        {
            grbDamagedLicense.Checked = true;
            _ChangeEnaplityOfIssueReplacementButton(false);
            _InitializeDataInLoad();
            _ChangeEnaplityOfLinkLabel(llblShowLicenseHistory, false);
            _ChangeEnaplityOfLinkLabel(llblShowNewLicenseInfo, false);
        }

        private void llblShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(_PersonID);
            frm.ShowDialog();
        }

        private void llblShowNewLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicenseInfo frm = new frmShowLicenseInfo(_NewLicenseID);
            frm.ShowDialog();
        }

        private void gbtnIssueReplacement_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("Are you sure you want to Replace this License?", "Confirm",
             MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)) == DialogResult.Yes)
            {


                clsApplication NewApplication = new clsApplication
                {
                    ApplicationDate = DateTime.Now,
                    //ApplicationStatus = clsApplicationStatuses.Find("Completed").ApplicationStatusID,
                    ApplicationTypeID = Convert.ToInt32(this._ApplicationTypeID_Mode),
                    CreatedByUserID = clsGlobal.CurrentUser.UserID,
                    LastStatusDate = DateTime.Now,
                    ApplicantPersonID = this._PersonID
                };

                if (NewApplication.Save())
                {
                    string IssueReasonText = "";

                    switch (_ApplicationTypeID_Mode)
                    {
                        case enApplicationTypeID.ReplacementForLost:
                            IssueReasonText = "Replacement For Lost";
                            break;

                        case enApplicationTypeID.ReplacementForDamaged:
                            IssueReasonText = "Replacement For Damage";
                            break;
                    }

                    //clsLicense _NewLicense = new clsLicense(NewApplication, _OLDLocalLicense, IssueReasonText);

                    //if (_NewLicense.Save())
                    //{
                    //    _DeActivateOldLicense();

                    //    MessageBox.Show($"License Replacement Successfully With ID = {_NewLicense.LicenseID}",
                    //        "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    //    _NewLicenseID = _NewLicense.LicenseID;
                    //    _ChangeEnaplityOfIssueReplacementButton(false);
                    //    _ChangeEnaplityOfLinkLabel(llblShowLicenseHistory, true);
                    //    _ChangeEnaplityOfLinkLabel(llblShowNewLicenseInfo, true);
                    //    lblLicenseReplacementApplicationID.Text = _NewLicense.ApplicationID.ToString();
                    //    lblReplacedLicenseID.Text = _NewLicense.LicenseID.ToString();
                    //    ctrlDriverLicenseInfoCardWithFilter1.ChangeEnaplityOfGBFilterBy(false);
                    //    gbReplacementFor.Enabled = false;
                    //}
                    //else
                    //{
                    //    MessageBox.Show($"Error To Replace License!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //}
                }
            }
        }

        private void gbtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void grbDamagedLicense_CheckedChanged(object sender, EventArgs e)
        {
            if (grbDamagedLicense.Checked)
                _ReplacementForDamagedMode();
            else
                _ReplacementForLostMode();
        }
    }
}

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

namespace DVLDPresentation.Applications.Detain_Licenses
{
    public partial class frmReleaseDetainedLicense : Form
    {
        public frmReleaseDetainedLicense(int LicenseID)
        {
            InitializeComponent();
            //ctrlDriverLicenseInfoWithFilter1.Mode = DVLDPresentation.Controls.ctrlDriverLicenseInfoWithFilter.enMode.ReleaseLicense;
            //ctrlDriverLicenseInfoWithFilter1.OnErrorAtSearch += _ErrorAtSearch;
            //ctrlDriverLicenseInfoWithFilter1.OnSuccedAtSearch += OnSuccedAtSearch_OnSuccedAtSearch;
            //_LicenseID = LicenseID;
        }

        public event Action OnClose;

        int _PersonID;
        int _LicenseID;
        bool _IsReleased = false;
        clsDetainedLicense _DetainedLicense;

        void _ChangeEnaplityOfReleaseButton(bool Value)
        {
            gbtnRelease.Enabled = Value;
        }
        void _ChangeEnaplityOfLinkLabel(LinkLabel llbl, bool Value)
        {
            llbl.Enabled = Value;
        }
        void _FillLabelsAfterFindLicense()
        {
            //lblDetainID.Text = _DetainedLicense.DetainID.ToString();
            //lblDetainDate.Text = _DetainedLicense.DetainDate.ToString("dd/MMM/yyyy");
            //float ApplicationFees = clsApplicationType.Find(_DetainedLicense.ApplicatoinTypeID).ApplicationFees;
            //lblApplicationFees.Text = ApplicationFees.ToString();
            //lblFineFees.Text = _DetainedLicense.FineFees.ToString();
            //lblTotalFees.Text = (ApplicationFees + _DetainedLicense.FineFees).ToString();
            //lblLicenseID.Text = _DetainedLicense.LicenseID.ToString();
            //lblCreatedByUser.Text = clsUser.FindByUserID(_DetainedLicense.CreatedByUserID).UserName;
        }
        void _OnErrorAtSearch()
        {
            _ChangeEnaplityOfLinkLabel(llblShowLicenseHistory, false);
            _ChangeEnaplityOfLinkLabel(llblShowReleasedLicenseInfo, false);
            _ChangeEnaplityOfReleaseButton(false);
            lblLicenseID.Text = "???";
            _IsReleased = false;
        }
        private void OnSuccedAtSearch_OnSuccedAtSearch(int PersonID, clsLicense LocalLicense, object sender)
        {
            _PersonID = PersonID;
            _LicenseID = LocalLicense.LicenseID;
           
            _ChangeEnaplityOfLinkLabel(llblShowLicenseHistory, true);
            _ChangeEnaplityOfReleaseButton(true);
            _DetainedLicense = clsDetainedLicense.FindByLicenseID(LocalLicense.LicenseID);
            if (_DetainedLicense != null)
            {
                _FillLabelsAfterFindLicense();
            }
        }
        private void ctrlDriverLicenseInfoCardWithFilter1_Load(object sender, EventArgs e)
        {
            //_ChangeEnaplityOfReleaseButton(false);
            //_ChangeEnaplityOfLinkLabel(llblShowLicenseHistory, false);
            //_ChangeEnaplityOfLinkLabel(llblShowReleasedLicenseInfo, false);

            //if (_LicenseID != -1)
            //    ctrlDriverLicenseInfoWithFilter1.EnterLicenseIDByCode(_LicenseID);
        }

        private void llblShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(_PersonID);
            frm.ShowDialog();
        }

        private void llblShowReleasedLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicenseInfo frm = new frmShowLicenseInfo(_LicenseID);
            frm.ShowDialog();
        }

        private void gbtnRelease_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("Are you sure you want to Release this License?", "Confirm",
         MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)) == DialogResult.Yes)
            {
                if (_DetainedLicense != null)
                {
                    _DetainedLicense.ReleasedByUserID = clsGlobal.CurrentUser.UserID;
                    if (_DetainedLicense.Save())
                    {
                        _IsReleased = true;
                        MessageBox.Show($"License Released Successfully With Application ID = {_DetainedLicense.ReleaseApplicationID}",
                            "License Released", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        _ChangeEnaplityOfReleaseButton(false);
                        _ChangeEnaplityOfLinkLabel(llblShowLicenseHistory, true);
                        _ChangeEnaplityOfLinkLabel(llblShowReleasedLicenseInfo, true);
                        lblReleaseApplicationID.Text = _DetainedLicense.ReleaseApplicationID.ToString();
                        //ctrlDriverLicenseInfoWithFilter1.ChangeEnaplityOfGBFilterBy(false);
                    }
                    else
                    {
                        _IsReleased = false;
                        MessageBox.Show($"Error To Release License!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        private void gbtnClose_Click(object sender, EventArgs e)
        {
            if (_IsReleased)
                if (OnClose != null)
                    OnClose();

            this.Close();
        }
    }
}

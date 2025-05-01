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
using DVLDPresentation.Controls;
using DVLDPresentation.Global_Classes;

namespace DVLDPresentation.Applications.Detain_Licenses
{
    public partial class frmReleaseDetainedLicenseApplication : Form
    {
        public event Action OnClose;
        bool _IsReleased = false;

        private int _SelectedLicenseID = -1;

        void _ChangeEnaplityOfReleaseButton(bool Value)
        {
            gbtnRelease.Enabled = Value;
        }
        void _ChangeEnaplityOfLinkLabel(LinkLabel llbl, bool Value)
        {
            llbl.Enabled = Value;
        }
        void _ErrorAtSearch()
        {
            _ChangeEnaplityOfLinkLabel(llblShowLicenseHistory, false);
            _ChangeEnaplityOfLinkLabel(llblShowReleasedLicenseInfo, false);
            _ChangeEnaplityOfReleaseButton(false);
            lblLicenseID.Text = "???";
            _IsReleased = false;
        }

        public frmReleaseDetainedLicenseApplication()
        {
            InitializeComponent();

            _ChangeEnaplityOfReleaseButton(false);
            _ChangeEnaplityOfLinkLabel(llblShowLicenseHistory, false);
            _ChangeEnaplityOfLinkLabel(llblShowReleasedLicenseInfo, false);
        }

        public frmReleaseDetainedLicenseApplication(int LicenseID)
        {
            InitializeComponent();
            _SelectedLicenseID = LicenseID;

            _ChangeEnaplityOfReleaseButton(false);
            _ChangeEnaplityOfLinkLabel(llblShowLicenseHistory, false);
            _ChangeEnaplityOfLinkLabel(llblShowReleasedLicenseInfo, false);

            ctrlDriverLicenseInfoWithFilter1.LoadLicenseInfo(_SelectedLicenseID);
            ctrlDriverLicenseInfoWithFilter1.FilterEnabled = false;
        }

        private void gbtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ctrlDriverLicenseInfoCardWithFilter1_OnLicenseSelected(int obj)
        {
            _SelectedLicenseID = obj;

            _SelectedLicenseID = obj;

            if (_SelectedLicenseID == -1)
                return;


            if (!ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.IsDetained)
            {
                MessageBox.Show("Selected License is not detained, choose another one.", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _ErrorAtSearch();
                return;
            }

            clsDetainedLicense DetainedLicense = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DetainedInfo;

            if(DetainedLicense == null)
            {
                MessageBox.Show("Error: Detain License Is Not Found!", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _ErrorAtSearch();
                return;
            }

            lblApplicationFees.Text = clsApplicationType.Find((int)clsApplication.enApplicationType.ReleaseDetainedDrivingLicsense).ApplicationFees.ToString();

            lblDetainID.Text = DetainedLicense.DetainID.ToString();
            lblLicenseID.Text = DetainedLicense.LicenseID.ToString();

            lblCreatedByUser.Text = DetainedLicense.CreatedByUserInfo.UserName;
            lblDetainDate.Text = clsFormat.DateToShort(DetainedLicense.DetainDate);
          
            lblFineFees.Text = DetainedLicense.FineFees.ToString();
            lblTotalFees.Text = (Convert.ToSingle(lblApplicationFees.Text) + Convert.ToSingle(lblFineFees.Text)).ToString();

            _ChangeEnaplityOfLinkLabel(llblShowLicenseHistory, true);
            _ChangeEnaplityOfReleaseButton(true);
        }

        private void frmReleaseDetainedLicenseApplication_Activated(object sender, EventArgs e)
        {
            ctrlDriverLicenseInfoWithFilter1.txtLicenseIDFocus();
        }

        private void llblShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DriverInfo.PersonID);
            frm.ShowDialog();
        }

        private void llblShowReleasedLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicenseInfo frm = new frmShowLicenseInfo(_SelectedLicenseID);
            frm.ShowDialog();
        }

        private void gbtnRelease_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to release this detained  license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            int ApplicationID = -1;

            bool IsReleased = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.ReleaseDetainedLicense(clsGlobal.CurrentUser.UserID, ref ApplicationID);

            lblReleaseApplicationID.Text = ApplicationID.ToString();

            if (!IsReleased)
            {
                MessageBox.Show("Faild to to release the Detain License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _IsReleased = true;

            MessageBox.Show("Detained License released Successfully ", "Detained License Released", MessageBoxButtons.OK, MessageBoxIcon.Information);

            _ChangeEnaplityOfReleaseButton(false);
            ctrlDriverLicenseInfoWithFilter1.FilterEnabled = false;
            _ChangeEnaplityOfLinkLabel(llblShowReleasedLicenseInfo, true);
        }

        private void ctrlDriverLicenseInfoCardWithFilter1_OnErrorAtSearch()
        {
            _ErrorAtSearch();
        }

        private void frmReleaseDetainedLicenseApplication_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_IsReleased)
                if (OnClose != null)
                    OnClose();
        }
    }
}

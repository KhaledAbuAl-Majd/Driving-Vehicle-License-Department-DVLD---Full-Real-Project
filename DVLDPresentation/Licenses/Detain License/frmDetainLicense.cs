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

    public partial class frmDetainLicense : Form
    {
        public event Action OnClose;

        int _PersonID;
        int _LicenseID;
        bool _IsDetained = false;
        public frmDetainLicense()
        {
            InitializeComponent();
            //ctrlDriverLicenseInfoWithFilter1.Mode = DVLDPresentation.Controls.ctrlDriverLicenseInfoWithFilter.enMode.DetainLicense;
            //ctrlDriverLicenseInfoWithFilter1.OnErrorAtSearch += _ErrorAtSearch;
            //ctrlDriverLicenseInfoWithFilter1.OnSuccedAtSearch += OnSuccedAtSearch_OnSuccedAtSearch;
        }

        void _ChangeEnaplityOfDetainButton(bool Value)
        {
            gbtnDetain.Enabled = Value;
        }
        void _ChangeEnaplityOfLinkLabel(LinkLabel llbl, bool Value)
        {
            llbl.Enabled = Value;
        }
        void _InitializeDataInLoad()
        {
            lblDetainDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            lblCreatedBy.Text = clsGlobal.CurrentUser.UserName;
        }
        void _OnErrorAtSearch()
        {
            _ChangeEnaplityOfLinkLabel(llblShowLicenseHistory, false);
            _ChangeEnaplityOfLinkLabel(llblShowDetainedLicenseInfo, false);
            _ChangeEnaplityOfDetainButton(false);
            lblLicenseID.Text = "???";
            _IsDetained = false;
        }
        private void OnSuccedAtSearch_OnSuccedAtSearch(int PersonID, clsLicense LocalLicense, object sender)
        {
            _PersonID = PersonID;
            _LicenseID = LocalLicense.LicenseID;
            lblLicenseID.Text = LocalLicense.LicenseID.ToString();
            _ChangeEnaplityOfLinkLabel(llblShowLicenseHistory, true);
            _ChangeEnaplityOfDetainButton(true);
        }

        private void frmDetainLicense_Load(object sender, EventArgs e)
        {
            _ChangeEnaplityOfDetainButton(false);
            _InitializeDataInLoad();
            _ChangeEnaplityOfLinkLabel(llblShowLicenseHistory, false);
            _ChangeEnaplityOfLinkLabel(llblShowDetainedLicenseInfo, false);
        }

        private void llblShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(_PersonID);
            frm.ShowDialog();
        }

        private void llblShowDetainedLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicenseInfo frm = new frmShowLicenseInfo(_LicenseID);
            frm.ShowDialog();
        }

        private void gbtnDetain_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(gtxtFineFees.Text))
            {
                MessageBox.Show("Fine Fees Must have value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            if ((MessageBox.Show("Are you sure you want to Detain this License?", "Confirm",
            MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)) == DialogResult.Yes)
            {
                float FineFees = Convert.ToSingle(gtxtFineFees.Text);
                //clsDetainedLicense NewDetainLicense = new clsDetainedLicense(_LicenseID, FineFees, clsGlobalSettings.CurrentUser.UserID);

                //if (NewDetainLicense.Save())
                //{
                //    _IsDetained = true;
                //    MessageBox.Show($"License Detained Successfully With ID = {NewDetainLicense.DetainID}",
                //        "License Detained", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //    _ChangeEnaplityOfDetainButton(false);
                //    _ChangeEnaplityOfLinkLabel(llblShowLicenseHistory, true);
                //    _ChangeEnaplityOfLinkLabel(llblShowDetainedLicenseInfo, true);
                //    lblDetainID.Text = NewDetainLicense.DetainID.ToString();
                //    ctrlDriverLicenseInfoWithFilter1.ChangeEnaplityOfGBFilterBy(false);
                //}
                //else
                //{
                //    _IsDetained = false;    
                //    MessageBox.Show($"Error To Detaine License!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //}
            }
        }
        private void gbtnClose_Click(object sender, EventArgs e)
        {
            if (_IsDetained)
                if (OnClose != null)
                    OnClose();

            this.Close();
        }

        private void gtxtFineFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

    }
}

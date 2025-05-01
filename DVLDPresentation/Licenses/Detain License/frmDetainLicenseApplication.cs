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

    public partial class frmDetainLicenseApplication : Form
    {
        public event Action OnClose;
        bool _IsDetained = false;

        private int _DetainID = -1;
        private int _SelectedLicenseID = -1;
        public frmDetainLicenseApplication()
        {
            InitializeComponent();
        }

        void _ChangeEnaplityOfDetainButton(bool Value)
        {
            gbtnDetain.Enabled = Value;
        }
        void _ChangeEnaplityOfLinkLabel(LinkLabel llbl, bool Value)
        {
            llbl.Enabled = Value;
        }
        void _ErrorAtSearch()
        {
            _ChangeEnaplityOfLinkLabel(llblShowLicenseHistory, false);
            _ChangeEnaplityOfLinkLabel(llblShowDetainedLicenseInfo, false);
            _ChangeEnaplityOfDetainButton(false);
            lblLicenseID.Text = "???";
            _IsDetained = false;
        }

        private void gbtnDetain_Click(object sender, EventArgs e)
        {
            if (!ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                gtxtFineFees.Focus();
                return;
            }

            if (MessageBox.Show("Are you sure you want to detain this license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            _DetainID = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.Detain(Convert.ToSingle(gtxtFineFees.Text.Trim()), clsGlobal.CurrentUser.UserID);

            if (_DetainID == -1)
            {
                MessageBox.Show("Faild to Detain License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _IsDetained = true;

            lblDetainID.Text = _DetainID.ToString();
            MessageBox.Show("License Detained Successfully with ID=" + _DetainID.ToString(), "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);

            _ChangeEnaplityOfDetainButton(false);
            ctrlDriverLicenseInfoWithFilter1.FilterEnabled = false;
            gtxtFineFees.Enabled = false;
            _ChangeEnaplityOfLinkLabel(llblShowDetainedLicenseInfo, true);
        }

        private void frmDetainLicense_Load(object sender, EventArgs e)
        {
            _ChangeEnaplityOfDetainButton(false);        
            _ChangeEnaplityOfLinkLabel(llblShowLicenseHistory, false);
            _ChangeEnaplityOfLinkLabel(llblShowDetainedLicenseInfo, false);

            lblDetainDate.Text = clsFormat.DateToShort(DateTime.Now);
            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;

            ctrlDriverLicenseInfoWithFilter1.txtLicenseIDFocus();
        }

        private void gbtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ctrlDriverLicenseInfoCardWithFilter1_OnLicenseSelected(int obj)
        {
            _SelectedLicenseID = obj;

            if (_SelectedLicenseID == -1)
                return;

            if (!ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.IsActive)
            {
                MessageBox.Show("Selected License is not Not Active, choose an active license.", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _ErrorAtSearch();
                return;
            }

            if (ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.IsDetained)
            {
                MessageBox.Show("Selected License is already detained, choose another one.", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _ErrorAtSearch();
                return;
            }

            if (ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.IsLicenseExpired())
            {
                MessageBox.Show("Selected License is expiared, Cannot Detain an Expired License ", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _ErrorAtSearch();
                return;
            }

            lblLicenseID.Text = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.LicenseID.ToString();

            _ChangeEnaplityOfLinkLabel(llblShowLicenseHistory, true);
            _ChangeEnaplityOfDetainButton(true);

            gtxtFineFees.Focus();
        }

        private void frmDetainLicenseApplication_Activated(object sender, EventArgs e)
        {
            ctrlDriverLicenseInfoWithFilter1.txtLicenseIDFocus();
        }

        private void llblShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DriverInfo.PersonID);
            frm.ShowDialog();
        }

        private void llblShowDetainedLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicenseInfo frm = new frmShowLicenseInfo(_SelectedLicenseID);
            frm.ShowDialog();
        }

        private void gtxtFineFees_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(gtxtFineFees.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(gtxtFineFees, "Fees cannot be empty!");
                return;
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(gtxtFineFees, null);
            }
        }

        private void gtxtFineFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.');

            if (e.KeyChar == '.' && gtxtFineFees.Text.Contains("."))
                e.Handled = true;
        }

        private void frmDetainLicenseApplication_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_IsDetained)
                if (OnClose != null)
                    OnClose();
        }

        private void ctrlDriverLicenseInfoCardWithFilter1_OnErrorAtSearch()
        {
            _ErrorAtSearch();
        }

    }
}

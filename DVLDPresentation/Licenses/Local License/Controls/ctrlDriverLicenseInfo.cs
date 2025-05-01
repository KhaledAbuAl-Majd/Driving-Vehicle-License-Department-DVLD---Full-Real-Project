using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLDBusiness;
using DVLDPresentation.Global_Classes;
using DVLDPresentation.Properties;

namespace DVLDPresentation.Controls
{
    public partial class ctrlDriverLicenseInfo : UserControl
    {
        public event Action OnErrorAtSearch;

        private int _LicenseID;
        private clsLicense _License;
        public ctrlDriverLicenseInfo()
        {
            InitializeComponent();
        }

        public int LicenseID
        {
            get { return _LicenseID; }
        }

        public clsLicense SelectedLicenseInfo
        { get { return _License; } }

        void _LoadPersonImage()
        {
            if (_License.DriverInfo.PersonInfo.ImagePath != "")
            {
                if (File.Exists(_License.DriverInfo.PersonInfo.ImagePath))
                    pbImage.ImageLocation = _License.DriverInfo.PersonInfo.ImagePath;
                else
                    MessageBox.Show("Could not find this image: = " + _License.DriverInfo.PersonInfo.ImagePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                pbImage.Image = (_License.DriverInfo.PersonInfo.Gendor == (int)clsPerson.enGendor.Male) ? Resources.Male_512 : Resources.Female_512;
            }
        }

        public void LoadInfo(int LicenseID)
        {
            _LicenseID = LicenseID;
            _License = clsLicense.Find(_LicenseID);
            if (_License == null)
            {
                MessageBox.Show("Could not find License ID = " + _LicenseID.ToString(),"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _LicenseID = -1;

                _EmptyLabels();

                if (OnErrorAtSearch != null)
                    OnErrorAtSearch();

                return;
            }

            lblLicneseID.Text = _License.LicenseID.ToString();
            lblIsActive.Text = (_License.IsActive) ? "Yes" : "No";
            lblIsDetained.Text = (_License.IsDetained) ? "Yes" : "No";
            lblCalss.Text = _License.LicenseClassInfo.ClassName;
            lblFullName.Text = _License.DriverInfo.PersonInfo.FullName;
            lblNationalNo.Text = _License.DriverInfo.PersonInfo.NationalNo;
            lblGendor.Text = _License.DriverInfo.PersonInfo.GendorText;
            pbGendorIcon.Image = (_License.DriverInfo.PersonInfo.Gendor == (int)clsPerson.enGendor.Male) ? Resources.Man_32 : Resources.Woman_32;
            lblDateOfBirth.Text = clsFormat.DateToShort(_License.DriverInfo.PersonInfo.DateOfBirth);

            lblDriverID.Text = _License.DriverInfo.DriverID.ToString();
            lblIssueDate.Text = clsFormat.DateToShort(_License.IssueDate);
            lblExpirationDate.Text = clsFormat.DateToShort(_License.ExpirationDate);
            lblIssueReason.Text = _License.IssueReasonText;
            lblNotes.Text = (string.IsNullOrEmpty(_License.Notes)) ? "No Notes" : _License.Notes;
            _LoadPersonImage();
        }

        void _ChangeLabelValue(Label lbl,string Text)
        {
            lbl.Text = Text;
        }

        public void _EmptyLabels()
        {
            _ChangeLabelValue(lblCalss, "???");
            _ChangeLabelValue(lblFullName, "???");
            _ChangeLabelValue(lblLicneseID, "???");
            _ChangeLabelValue(lblNationalNo, "???");
            _ChangeLabelValue(lblGendor, "???");
            _ChangeLabelValue(lblIssueDate, "???");
            _ChangeLabelValue(lblIssueReason, "???");
            _ChangeLabelValue(lblNotes, "???");
            _ChangeLabelValue(lblIsActive, "???");
            _ChangeLabelValue(lblDateOfBirth, "???");
            _ChangeLabelValue(lblDriverID, "???");
            _ChangeLabelValue(lblExpirationDate, "???");
            _ChangeLabelValue(lblIsDetained, "???");
        }
    }
}

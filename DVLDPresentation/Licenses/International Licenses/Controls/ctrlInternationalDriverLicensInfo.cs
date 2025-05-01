using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLDBusiness;
using DVLDPresentation.Global_Classes;
using DVLDPresentation.Properties;

namespace DVLDPresentation.Controls
{
    public partial class ctrlInternationalDriverLicensInfo : UserControl
    {
        public int _InternationalLicenseID = -1;
        private clsInternationalLicense _InternationalLicense;
        public ctrlInternationalDriverLicensInfo()
        {
            InitializeComponent();
        }

        public int InternationalLicenseID
        {
            get { return _InternationalLicenseID; }
        }

        void _LoadPersonImage()
        {
            if (_InternationalLicense.DriverInfo.PersonInfo.ImagePath != "")
            {
                if (File.Exists(_InternationalLicense.DriverInfo.PersonInfo.ImagePath))
                    pbImage.ImageLocation = _InternationalLicense.DriverInfo.PersonInfo.ImagePath;
                else
                    MessageBox.Show("Could not find this image: = " + _InternationalLicense.DriverInfo.PersonInfo.ImagePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                pbImage.Image = (_InternationalLicense.DriverInfo.PersonInfo.Gendor == (int)clsPerson.enGendor.Male) ? Resources.Male_512 : Resources.Female_512;
            }
        }

        public void LoadInfo(int InternationalLicenseID)
        {
            _InternationalLicenseID = InternationalLicenseID;
            _InternationalLicense = clsInternationalLicense.Find(_InternationalLicenseID);
            if (_InternationalLicense == null)
            {
                MessageBox.Show("Could not find Internationa License ID = " + _InternationalLicenseID.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _InternationalLicenseID = -1;
                return;
            }

            lblInternationalLicenseID.Text = _InternationalLicense.InternationalLicenseID.ToString();
            lblIntApplicationID.Text = _InternationalLicense.ApplicationID.ToString();
            lblIsActive.Text = (_InternationalLicense.IsActive) ? "Yes" : "No";
            lblLocalLicenseID.Text = _InternationalLicense.IssuedUsingLocalLicenseID.ToString();
            lblFullName.Text = _InternationalLicense.PersonInfo.FullName;
            lblNationalNo.Text = _InternationalLicense.PersonInfo.NationalNo;
            lblGendor.Text = _InternationalLicense.PersonInfo.GendorText;
            pbGendorIcon.Image = (_InternationalLicense.PersonInfo.Gendor == (int)clsPerson.enGendor.Male) ? Resources.Man_32 : Resources.Woman_32;
            lblDateOfBirth.Text = clsFormat.DateToShort(_InternationalLicense.PersonInfo.DateOfBirth);
        
            lblDriverID.Text = _InternationalLicense.DriverID.ToString();
            lblIssueDate.Text = clsFormat.DateToShort(_InternationalLicense.IssueDate);
            lblExpirationDate.Text = clsFormat.DateToShort(_InternationalLicense.ExpirationDate);

            _LoadPersonImage();
        }
  
    }
}

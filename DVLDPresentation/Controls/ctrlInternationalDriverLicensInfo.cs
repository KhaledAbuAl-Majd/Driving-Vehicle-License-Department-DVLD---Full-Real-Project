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
using DVLDPresentation.Properties;

namespace DVLDPresentation.Controls
{
    public partial class ctrlInternationalDriverLicensInfo : UserControl
    {
        public int IntLicenseID = -1;
        public ctrlInternationalDriverLicensInfo()
        {
            InitializeComponent();
        }

        private void FillDataInLabels()
        {
            clsInternationalLicenses IntLicense = clsInternationalLicenses.FindByIntLicenseID(IntLicenseID);

            if (IntLicense != null)
            {
                clsApplications Application = clsApplications.Find(IntLicense.ILApplicationID);
                clsDrivers Driver = clsDrivers.FindByDriverID(IntLicense.DriverID);
                clsPerson Person = clsPerson.Find(Driver.PersonID);

                lblName.Text = Person.GetFullName();
                lblIntLicenseID.Text = IntLicense.InternationalLicenseID.ToString();
                lblLicneseID.Text = IntLicense.IssuedUsingLocalLicenseID.ToString();
                lblNationalNo.Text = Person.NationalNo;
                _ChangeGendorData(Person.Gendor, Person.ImagePath);
                lblIssueDate.Text = IntLicense.IssueDate.ToString("dd/MMM/yyyy");
                lblIntApplicationID.Text = IntLicense.ILApplicationID.ToString();
                lblIsActive.Text = (IntLicense.IsActive) ? "Yes" : "No";
                lblDateOfBirth.Text = Person.DateOfBirth.ToString("dd/MMM/yyyy");
                lblDriverID.Text = IntLicense.DriverID.ToString();
                lblExpirationDate.Text = IntLicense.ExpirationDate.ToString("dd/MMM/yyyy"); 
            }
        }
        void _ChangeGendorData(short Gendor, string PersonImagePath)
        {
            if (Gendor == 0)
            {
                lblGendor.Text = "Male";
                pbGendor.Image = Resources.Man_32;
            }
            else
            {
                lblGendor.Text = "Female";
                pbGendor.Image = Resources.Woman_32;
            }

            if (PersonImagePath != "")
            {
                pbImage.ImageLocation = PersonImagePath;
            }
            else
            {
                pbImage.Image = (Gendor == 0) ? Resources.Male_512 : Resources.Female_512;
            }
        }

        private void gbInternationalDriverLicneseInfo_Enter(object sender, EventArgs e)
        {

        }

        private void ctrlInternationalDriverLicensInfo_Load(object sender, EventArgs e)
        {
            if (IntLicenseID != -1)
                FillDataInLabels();
        }
    }
}

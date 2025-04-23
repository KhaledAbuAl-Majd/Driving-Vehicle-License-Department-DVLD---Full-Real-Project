using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLDBusiness;
using DVLDPresentation.Properties;

namespace DVLDPresentation.Controls
{
    public partial class ctrlDriverLicenseInfo : UserControl
    {
        public int LicenseID = -1;
        public ctrlDriverLicenseInfo()
        {
            InitializeComponent();
        }

        void _ChangeLabelValue(Label lbl,string Text)
        {
            lbl.Text = Text;
        }
        public void _EmptyLabels()
        {
            _ChangeLabelValue(lblCalss, "???");
            _ChangeLabelValue(lblName, "???");
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
        public void FillDataInLabels(int LicenseID)
        {
            clsLicenses License = clsLicenses.FindByLicenseID(LicenseID);
            if (License != null)
            {
                clsDrivers Driver = clsDrivers.FindByDriverID(License.DriverID);
                clsPerson Person = clsPerson.Find(Driver.PersonID);
                lblCalss.Text = clsLicneseClasses.Find(License.LicenseClassID).ClassName;
                lblName.Text = Person.FullName;
                lblLicneseID.Text = License.LicneseID.ToString();
                lblNationalNo.Text = Person.NationalNo;
                _ChangeGendorData(Person.Gendor, Person.ImagePath);
                lblIssueDate.Text = License.IsuueDate.ToString("dd/MMM/yyyy");
                lblIssueReason.Text = License.IssueReason;
                lblNotes.Text = (License.Notes == "") ? "No Notes" : License.Notes;
                lblIsActive.Text = (License.IsAcitve) ? "Yes" : "No";
                lblDateOfBirth.Text = Person.DateOfBirth.ToString("dd/MMM/yyyy");
                lblDriverID.Text = License.DriverID.ToString();
                lblExpirationDate.Text = License.ExpirationDate.ToString("dd/MMM/yyyy");
                //is Detained
                lblIsDetained.Text = (clsDetainedLicenses.IsLicenseDetainedByLicenseID(License.LicneseID)) ? "Yes" : "No";
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
        private void ctrlDriverLicenseInfo_Load(object sender, EventArgs e)
        {
            if (LicenseID != -1)
                FillDataInLabels(LicenseID);
        }

        private void gbDriverLicneseInfo_Enter(object sender, EventArgs e)
        {

        }
    }
}

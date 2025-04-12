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
        public int LDLApplicationID = -1;
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
        public void FillDataInLabels(int LDLApplicationID)
        {
            clsLocalDrivingApplictions LDLApplication = clsLocalDrivingApplictions.FindByLDLApplicationID(LDLApplicationID);
            if (LDLApplication != null)
            {
                clsApplications Application = clsApplications.Find(LDLApplication.ApplicationID);
                clsPeople Person = clsPeople.Find(Application.PersonID);
                clsLicenses License = clsLicenses.FindByApplicationID(LDLApplication.ApplicationID);

                lblCalss.Text = clsLicneseClasses.Find(LDLApplication.LicenseClassID).ClassName;
                lblName.Text = Person.GetFullName();
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
                lblIsDetained.Text = (!License.IsAcitve) ? "Yes" : "No";
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
            if (LDLApplicationID != -1)
                FillDataInLabels(LDLApplicationID);
        }

        private void gbDriverLicneseInfo_Enter(object sender, EventArgs e)
        {

        }
    }
}

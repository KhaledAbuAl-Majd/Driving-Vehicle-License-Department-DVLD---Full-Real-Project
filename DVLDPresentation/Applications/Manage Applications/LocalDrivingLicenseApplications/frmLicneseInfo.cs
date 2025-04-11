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

namespace DVLDPresentation.Applications.Manage_Applications.LocalDrivingLicenseApplications
{
    public partial class frmLicneseInfo : Form
    {
        clsLocalDrivingApplictions _LDLApplication;
        public frmLicneseInfo(int LDLApplicationID)
        {
            InitializeComponent();
            _LDLApplication = clsLocalDrivingApplictions.Find(LDLApplicationID);
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
        private void _FillDataInLabels()
        {
            clsApplications Application = clsApplications.Find(_LDLApplication.ApplicationID);
            clsPeople Person = clsPeople.Find(Application.PersonID);
            clsLicenses License = clsLicenses.FindByApplicationID(_LDLApplication.ApplicationID);

            lblCalss.Text = clsLicneseClasses.Find(_LDLApplication.LicenseClassID).ClassName;
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
        private void frmLicneseInfo_Load(object sender, EventArgs e)
        {
            _FillDataInLabels();
        }

        private void gbtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

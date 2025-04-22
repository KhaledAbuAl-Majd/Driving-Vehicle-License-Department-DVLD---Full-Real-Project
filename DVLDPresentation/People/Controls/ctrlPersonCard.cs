using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLDPresentation.Properties;
using DVLDBusiness;
using DVLDPresentation.People;
using System.IO;

namespace DVLDPresentation
{
    public partial class ctrPersonCard : UserControl
    {
        //public event Action OnClose;
        public enum enGendor { Male = 0, Female = 1 };
        public int PersonID { get; private set; }
        public clsPerson SelectedPersonInfo { get; private set; }
        public ctrPersonCard()
        {
            InitializeComponent();
            PersonID = -1;
        }
        public void LoadPersonInfo(int PersonID)
        {
            SelectedPersonInfo = clsPerson.Find(PersonID);
            if(SelectedPersonInfo == null)
            {
                ResetPersonInfo();
                MessageBox.Show("No Person with PersonID = " + PersonID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FillPersonInfo();
        }
        public void LoadPersonInfo(string NationalNo)
        {
            SelectedPersonInfo = clsPerson.Find(NationalNo);
            if (SelectedPersonInfo == null)
            {
                ResetPersonInfo();
                MessageBox.Show("No Person with NationalNo = " + NationalNo, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FillPersonInfo();
        }
        void _ChangeGendorDataAndLoadPersonImage(enGendor Gendor)
        {
            switch (Gendor)
            {
                case enGendor.Male:
                    lblGendor.Text = "Male";
                    pbGendor.Image = Resources.Man_32;
                    break;

                case enGendor.Female:
                    lblGendor.Text = "Female";
                    pbGendor.Image = Resources.Woman_32;
                    break;
            }

            if (SelectedPersonInfo.ImagePath != "")
            {
                if (File.Exists(SelectedPersonInfo.ImagePath))
                {
                    pbPersonImage.ImageLocation = SelectedPersonInfo.ImagePath;
                    return;

                }
                else
                {
                    MessageBox.Show("Could not find this image: = " + SelectedPersonInfo.ImagePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            pbPersonImage.Image = (SelectedPersonInfo.Gendor == (int)enGendor.Male) ? Resources.Male_512 : Resources.Female_512;
            
        }
        void _FillPersonInfo()
        {
            llblEditPersonInfo.Enabled = true;
            PersonID = SelectedPersonInfo.PersonID;
            lblPersonID.Text = SelectedPersonInfo.PersonID.ToString();
            lblName.Text = SelectedPersonInfo.FirstName + " " + SelectedPersonInfo.SecondName + " " + SelectedPersonInfo.ThirdName + " " + SelectedPersonInfo.LastName;
            lblNationalNo.Text = SelectedPersonInfo.NationalNo;

            _ChangeGendorDataAndLoadPersonImage((enGendor)SelectedPersonInfo.Gendor);

            if (SelectedPersonInfo.Email != "")
                lblEmail.Text = SelectedPersonInfo.Email;

            lblAddress.Text = SelectedPersonInfo.Address;
            lblDateOfBirth.Text = SelectedPersonInfo.DateOfBirth.ToShortDateString();
            lblPhone.Text = SelectedPersonInfo.Phone;
            lblCountry.Text = SelectedPersonInfo.CountryInfo?.CountryName.ToString();

            //clsCountry Country = clsCountry.Find(_Person.NationalityCountryID);

            //if (Country != null)
            //    lblCountry.Text = Country.CountryName;

        }
        public void ResetPersonInfo()
        {
            llblEditPersonInfo.Enabled = false;
            PersonID = -1;
            lblPersonID.Text = "[????]";
            lblName.Text = "[????]";
            lblNationalNo.Text = "[????]";
            lblGendor.Text = "[????]"; ;
            lblEmail.Text = "[????]";
            pbGendor.Image = Resources.Man_32;
            pbPersonImage.Image = Resources.Male_512;
            lblAddress.Text = "[????]";
            lblDateOfBirth.Text = "[????]";
            lblPhone.Text = "[????]";
            lblCountry.Text = "[????]";
        }
        private void llblEditPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmAddUpdatePerson frm = new frmAddUpdatePerson(PersonID);
            frm.DataBack += FrmAddEditOnClose_DataBack;
            frm.ShowDialog();

            ////refresh
            //LoadPersonInfo(PersonID);
        }
        private void FrmAddEditOnClose_DataBack(object sender, int PersonID)
        {
            //refresh
            LoadPersonInfo(PersonID);
        }
    }
}

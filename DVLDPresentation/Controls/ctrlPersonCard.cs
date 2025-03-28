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

namespace DVLDPresentation
{
    public partial class ctrPersonCard : UserControl
    {
        int _PersonID = -1;
        clsPeople _Person;
        public ctrPersonCard()
        {
            InitializeComponent();
        }
        void _ChangeGendorData(string Gendor, Image GendorIcon, string PersonImagePath)
        {
            lblGendor.Text = Gendor;
            pbGendor.Image = GendorIcon;

            if (PersonImagePath != "")
            {
                pbImage.ImageLocation = PersonImagePath;
            }
            else
            {
                pbImage.Image = (_Person.Gendor == 0) ? Resources.Male_512 : Resources.Female_512;
            }
        }
        void _FillData()
        {
            lblPersonID.Text = _Person.PersonID.ToString();
            lblName.Text = _Person.FirstName + " " + _Person.SecondName + " " + _Person.ThirdName + " " + _Person.LastName;
            lblNationalNo.Text = _Person.NationalNo;

            if (_Person.Gendor == 0)
                _ChangeGendorData("Male", Resources.Man_32, _Person.ImagePath);
            else
                _ChangeGendorData("Female", Resources.Woman_32, _Person.ImagePath);

            if (_Person.Email != "")
                lblEmail.Text = _Person.Email;

            lblAddress.Text = _Person.Address;
            lblDateOfBirth.Text = _Person.DateOfBirth.ToString();
            lblPhone.Text = _Person.Phone;

            clsCountries Country = clsCountries.Find(_Person.NationalityCountryID);

            if (Country != null)
                lblCountry.Text = Country.CountryName;
        }

        private void UserControl1_Load(object sender, EventArgs e)
        {
            _Person = clsPeople.Find(_PersonID);

            if (_Person != null)
                _FillData();
        }
    }
}

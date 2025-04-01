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

namespace DVLDPresentation
{
    public partial class ctrPersonCard : UserControl
    {
        public event Action OnClose;

        public int PersonID = -1;
        public clsPeople _Person { get; private set; }
        public void RefreshPersonData(int PersonID)
        {
            _Person = clsPeople.Find(PersonID);
            _FillDataAndShow_HideLLEdit();
        }
        public void RefreshPersonData(string NationalNo)
        {
            _Person = clsPeople.Find(NationalNo);
            _FillDataAndShow_HideLLEdit();
        }
        public void EmptyPersonInformationAtDesign()
        {
            lblPersonID.Text = "[????]";
            lblName.Text = "[????]";
            lblNationalNo.Text = "[????]";
            lblGendor.Text = "[????]"; ;
            lblEmail.Text = "[????]";
            pbImage.Image = Resources.Male_512;
            lblAddress.Text = "[????]";
            lblDateOfBirth.Text = "[????]";
            lblPhone.Text = "[????]";
            lblCountry.Text = "[????]";
        }
        public ctrPersonCard()
        {
            InitializeComponent();
        }

        void _FillDataAndShow_HideLLEdit()
        {
            if (_Person != null)
            {
                PersonID = _Person.PersonID;
                _FillData();
                _Show_HideLLEditPerosn(true);
            }
            else
                _Show_HideLLEditPerosn(false);
        }
        void _Show_HideLLEditPerosn(bool value)
        {
            llblEditPersonInfo.Visible = value;
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
            lblDateOfBirth.Text = _Person.DateOfBirth.ToShortDateString();
            lblPhone.Text = _Person.Phone;

            clsCountries Country = clsCountries.Find(_Person.NationalityCountryID);

            if (Country != null)
                lblCountry.Text = Country.CountryName;
        }
        private void UserControl1_Load(object sender, EventArgs e)
        {
            RefreshPersonData(PersonID);
        }

        private void llblEditPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmAdd_EditPerson frm = new frmAdd_EditPerson(Convert.ToInt32(lblPersonID.Text));

            frm.DataBackOnClose += FrmEdit_OnClose;
            frm.ShowDialog();

        }

        private void FrmEdit_OnClose(object sender,int PersonID)
        {
            RefreshPersonData(this.PersonID);

            if (OnClose != null)
                OnClose();
        }
    }
}

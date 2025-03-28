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
using DVLDPresentation.Properties;
using Guna.UI2.WinForms;

namespace DVLDPresentationTier.Controls
{
    public partial class ctrAdd_EditPerson : UserControl
    {
        public int _PersonID = -1;
        clsPeople _Person;
        public bool _IsSave { get; private set; }
        public ctrAdd_EditPerson()
        {
            InitializeComponent();
            _PersonID = -1;
            _VisibleOfLLRemove(false);
        }
        public void _RemoveImage()
        {
            string DestenationFile = pbImage.ImageLocation;

            if (File.Exists(DestenationFile))
            {
                File.Delete(DestenationFile);
                pbImage.ImageLocation = "";
            }
        }

        private void _AddNewPersonMode()
        {
            _Person = new clsPeople();
        }
        private void _UpdatePersonMode()
        {
            _Person = clsPeople.Find(_PersonID);

            if(_Person != null)
            {
                _FillDataFromObjectPersonToForm();
            }
        }
        private void _FillCountryCbAndChooseDefaultCountry(string DefaultCountryName)
        {
            DataTable dt = clsCountries.GetAllCountires();

            //foreach (DataRow row in dt.Rows)
            //{
            //    gcmCountry.Items.Add(row["CountryName"]);
            //}

            gcmCountry.DataSource = dt;
            gcmCountry.DisplayMember = "CountryName";
            gcmCountry.SelectedIndex = gcmCountry.FindString(DefaultCountryName);
        }
        private void _VisibleOfLLRemove(bool value)
        {
            llblRemoveImage.Visible = value;
        }
        private void _FillDataFromFormToObjectPerson()
        {
            _Person.FirstName = gtxtFirstName.Text;
            _Person.SecondName = gtxtSecondName.Text;
            _Person.ThirdName = gtxtThirdName.Text;
            _Person.LastName = gtxtLastName.Text;
            _Person.NationalNo = gtxtNationalNo.Text;
            _Person.DateOfBirth = gDTPDateOfBirth.Value;

            if (rbMale.Checked)
                _Person.Gendor = 0;
            else
                _Person.Gendor = 1;

            _Person.Phone = gtxtPhone.Text;
            _Person.Email = gtxtEmail.Text;
            _Person.NationalityCountryID = clsCountries.Find(gcmCountry.Text).CountryID;
            _Person.Address = gtxtAddress.Text;
            _Person.ImagePath = pbImage.ImageLocation;
        }
        private void _SetImage()
        {
            string SourceFile = openFileDialog1.FileName;

            if (string.IsNullOrEmpty(SourceFile))
            {
                MessageBox.Show("No File Selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (SourceFile == pbImage.ImageLocation)
                return;

            string Extension = Path.GetExtension(SourceFile);

            Guid guid = Guid.NewGuid();

            string DestanationFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                "DVLD-People-Image", guid.ToString() + Extension);

            _RemoveImage();
            File.Copy(SourceFile, DestanationFile);
            _VisibleOfLLRemove(true);
            pbImage.ImageLocation = DestanationFile;
        }
        private void _FillDataFromObjectPersonToForm()
        {
            gtxtFirstName.Text = _Person.FirstName;
            gtxtSecondName.Text = _Person.SecondName;
            gtxtThirdName.Text = _Person.ThirdName;
            gtxtLastName.Text = _Person.LastName;
            gtxtNationalNo.Text = _Person.NationalNo;
            gDTPDateOfBirth.Value = _Person.DateOfBirth;

            if (_Person.Gendor == 0)
            {
                rbMale.Checked = true;
            }
            else
            {
                rbFemale.Checked = false;
            }

            gtxtPhone.Text = _Person.Phone;
            gtxtEmail.Text = _Person.Email;
            gcmCountry.SelectedIndex = gcmCountry.FindString(clsCountries.Find(_Person.NationalityCountryID).CountryName);
            gtxtAddress.Text = _Person.Address;
            pbImage.ImageLocation = _Person.ImagePath;
        }
        private void _ChangeImageAccordingToGendor()
        {
            if (!string.IsNullOrEmpty(pbImage.ImageLocation))
                return;

            if (rbMale.Checked)
            {
                pbImage.Image = Resources.Male_512;
            }
            else
            {
                pbImage.Image = Resources.Female_512;
            }
        }
        bool _CheckErrorProviderForAllTextBoxs(Guna2TextBox txtbox,CancelEventArgs e,string ErrorString)
        {
            bool IsErrorFound = false;

            if (string.IsNullOrWhiteSpace(txtbox.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtbox, ErrorString); 
                IsErrorFound = true;
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtbox, "");
                IsErrorFound = false;
            }

            return IsErrorFound;
        }

        private void ctrAdd_EditPerson_Load(object sender, EventArgs e)
        {
            if(_PersonID == -1)
            {
                _AddNewPersonMode();
            }
            else
            {
                _UpdatePersonMode();
            }
                //to make the max date Today - 18
                gDTPDateOfBirth.MaxDate = DateTime.Today.AddYears(-18);
            _FillCountryCbAndChooseDefaultCountry("Egypt");
            //_VisibleOfLLRemove(false);
        }

        private void gtxtFirstName_Validating(object sender, CancelEventArgs e)
        {
            _CheckErrorProviderForAllTextBoxs((Guna2TextBox)sender, e,"FirstName must have a value!");
        }

        private void gtxtSecondName_Validating(object sender, CancelEventArgs e)
        {
            _CheckErrorProviderForAllTextBoxs((Guna2TextBox)sender, e, "SecondName must have a value!");
        }

        private void gtxtThirdName_Validating(object sender, CancelEventArgs e)
        {
            _CheckErrorProviderForAllTextBoxs((Guna2TextBox)sender, e, "ThirdName must have a value!");
        }

        private void gtxtLastName_Validating(object sender, CancelEventArgs e)
        {
            _CheckErrorProviderForAllTextBoxs((Guna2TextBox)sender, e, "LastName must have a value!");
        }

        private void gtxtNationalNo_Validating(object sender, CancelEventArgs e)
        {
            Guna2TextBox NationalNo = (Guna2TextBox)sender;

            if (!_CheckErrorProviderForAllTextBoxs(NationalNo, e,"NationalNo must have a value!"))
            {
                if (clsPeople.IsPersonExist(NationalNo.Text))
                {
                    e.Cancel = true;
                    errorProvider1.SetError(NationalNo, "NationalNo must be unique value!");
                }
                else
                {
                    e.Cancel = false;
                    errorProvider1.SetError(NationalNo, "");
                }
            }
        }

        private void gtxtPhone_Validating(object sender, CancelEventArgs e)
        {
            _CheckErrorProviderForAllTextBoxs((Guna2TextBox)sender, e, "Phone must have a value!");
        }

        private void gtxtAddress_Validating(object sender, CancelEventArgs e)
        {
            _CheckErrorProviderForAllTextBoxs((Guna2TextBox)sender, e, "Address must have a value!");
        }

        private void gtxtEmail_Validating(object sender, CancelEventArgs e)
        {
            Guna2TextBox Email = (Guna2TextBox)sender;

            if (!string.IsNullOrWhiteSpace(Email.Text) && !Email.Text.Contains("@gmail.com") )
            {
                errorProvider1.SetError(Email, "Email must have @gmail.com");
                e.Cancel = true;
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(Email, "");
            }
        }

        private void gbtnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateChildren())
            {
                MessageBox.Show("Please enter correct values!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _FillDataFromFormToObjectPerson();
            _IsSave = true;
            if (_Person.Save())
            {
                MessageBox.Show($"Person Addess Successfuly With ID = {_Person.PersonID}", "Result"
                    , MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Faild To Save", "Result"
                   , MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void llblEditPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.Multiselect = false;
            openFileDialog1.Filter = @"Image Files (*.gif;*.jpg;*.jpeg;*.bmp;*.wmf;*.png)|
                *.gif;*.jpg;*.jpeg;*.bmp;*.wmf;*.png";
            openFileDialog1.Title = "Choose Image";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                _SetImage();
            }
        }

        private void llblRemoveImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _RemoveImage();
            _ChangeImageAccordingToGendor();
        }

        private void rbMale_CheckedChanged(object sender, EventArgs e)
        {
            _ChangeImageAccordingToGendor();
        }

        private void gtxtAddress_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

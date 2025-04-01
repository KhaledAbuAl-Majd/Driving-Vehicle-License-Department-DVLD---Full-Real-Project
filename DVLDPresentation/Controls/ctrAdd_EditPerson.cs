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
        public delegate void SaveDataBackEventHandler(object sender, int PersonID);

        public event SaveDataBackEventHandler SaveDataBack;
        private void _InvokeRelatedEventSaveDataBack()
        {
            SaveDataBack.Invoke(this, _Person.PersonID);
        }

        public event Action OnClose;

        private void _CloseForm()
        {
            Action handler = OnClose;

            if (handler != null)
                handler();
        }

        public int PersonID = -1;
        clsPeople _Person;
        public bool IsSave { get; private set; }
        public ctrAdd_EditPerson()
        {
            InitializeComponent();
            _VisibleOfLLRemove(false);
        }
        private void _RemoveImageFromPictureBox()
        {
            pbImage.ImageLocation = "";
        }
        void _RemoveImageFromFile(string DestenationFile)
        {
            if (File.Exists(DestenationFile))
            {
                File.Delete(DestenationFile);
            }
        }

        private void _AddNewPersonMode()
        {
            _Person = new clsPeople();
        }
        private void _UpdatePersonMode()
        {
            _Person = clsPeople.Find(PersonID);

            if(_Person != null)
            {
                _FillDataFromObjectPersonToForm();

                if (!string.IsNullOrEmpty(pbImage.ImageLocation))
                {
                    _VisibleOfLLRemove(true);
                }
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

            if (_Person.ImagePath != "" && pbImage.ImageLocation == "")
                _RemoveImageFromFile(_Person.ImagePath);

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

            _RemoveImageFromFile(pbImage.ImageLocation);
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
                rbFemale.Checked = true;
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
        bool _CheckErrorProviderForAllTextBoxs(Guna2TextBox txtbox,string ErrorString)
        {
            bool IsErrorFound = false;

            if (string.IsNullOrWhiteSpace(txtbox.Text))
            {
                txtbox.Tag = false;
                errorProvider1.SetError(txtbox, ErrorString); 
                IsErrorFound = true;
            }
            else
            {
                txtbox.Tag = true;
                errorProvider1.SetError(txtbox, "");
                IsErrorFound = false;
            }

            return IsErrorFound;
        }
        private void _GiveTextBoxesInitialValueForValidating(bool Value)
        {
            gtxtFirstName.Tag = Value;
            gtxtSecondName.Tag = Value;
            gtxtThirdName.Tag = Value;
            gtxtLastName.Tag = Value;
            gtxtNationalNo.Tag = Value;
            gtxtPhone.Tag = Value;
            gtxtAddress.Tag = Value;
            gtxtEmail.Tag = true;
        }
        private void _IfTxtFailedInValidating(Guna2TextBox txt)
        {
            txt.Focus();
            MessageBox.Show("Please enter correct values!", "Validation Error",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        private void _Save()
        {
            bool FirstNameValidateResult = (bool)gtxtFirstName.Tag;
            bool SecondNameValidateResult = (bool)gtxtSecondName.Tag;
            bool ThirdNameValidateResult = (bool)gtxtThirdName.Tag;
            bool LastNameValidateResult = (bool)gtxtLastName.Tag;
            bool NationalNoValidateResult = (bool)gtxtNationalNo.Tag;
            bool PhoneValidateResult = (bool)gtxtPhone.Tag;
            bool EmailValidateResult = (bool)gtxtEmail.Tag;
            bool AddressValidateResult = (bool)gtxtAddress.Tag;

            if (!FirstNameValidateResult)
            {
                _IfTxtFailedInValidating(gtxtFirstName);
                return;
            }

            if (!SecondNameValidateResult)
            {
                _IfTxtFailedInValidating(gtxtSecondName);
                return;
            }

            if (!ThirdNameValidateResult)
            {
                _IfTxtFailedInValidating(gtxtThirdName);
                return;
            }

            if (!LastNameValidateResult)
            {
                _IfTxtFailedInValidating(gtxtLastName);
                return;
            }

            if (!NationalNoValidateResult)
            {
                _IfTxtFailedInValidating(gtxtNationalNo);
                return;
            }

            if (!PhoneValidateResult)
            {
                _IfTxtFailedInValidating(gtxtPhone);
                return;
            }

            if (!EmailValidateResult)
            {
                _IfTxtFailedInValidating(gtxtEmail);
                return;
            }

            if (!AddressValidateResult)
            {
                _IfTxtFailedInValidating(gtxtAddress);
                return;
            }

        
                _FillDataFromFormToObjectPerson();
                clsPeople.enMode prevMode = _Person._Mode;

            if (_Person.Save())
            {
                if (prevMode == clsPeople.enMode.Update)
                    MessageBox.Show($"Person Updated Successfuly", "Result"
                   , MessageBoxButtons.OK, MessageBoxIcon.Information);

                else
                    MessageBox.Show($"Person Addess Successfuly With ID = {_Person.PersonID}", "Result"
                        , MessageBoxButtons.OK, MessageBoxIcon.Information);
                IsSave = true;
                _InvokeRelatedEventSaveDataBack();
            }
            else
            {
                MessageBox.Show("Faild To Save", "Result"
                   , MessageBoxButtons.OK, MessageBoxIcon.Information);
                IsSave = false;
            }

         

         
        }

        private void ctrAdd_EditPerson_Load(object sender, EventArgs e)
        {
            if(PersonID == -1)
            {
                _AddNewPersonMode();
                _GiveTextBoxesInitialValueForValidating(false);
            }
            else
            {
                _UpdatePersonMode();
                _GiveTextBoxesInitialValueForValidating(true);
            }
                //to make the max date Today - 18
                gDTPDateOfBirth.MaxDate = DateTime.Today.AddYears(-18);
            _FillCountryCbAndChooseDefaultCountry("Egypt");
        }

        private void gtxtFirstName_Validating(object sender, CancelEventArgs e)
        {
            _CheckErrorProviderForAllTextBoxs((Guna2TextBox)sender,"FirstName must have a value!");
        }

        private void gtxtSecondName_Validating(object sender, CancelEventArgs e)
        {
            _CheckErrorProviderForAllTextBoxs((Guna2TextBox)sender, "SecondName must have a value!");
        }

        private void gtxtThirdName_Validating(object sender, CancelEventArgs e)
        {
            _CheckErrorProviderForAllTextBoxs((Guna2TextBox)sender,"ThirdName must have a value!");
        }

        private void gtxtLastName_Validating(object sender, CancelEventArgs e)
        {
            _CheckErrorProviderForAllTextBoxs((Guna2TextBox)sender, "LastName must have a value!");
        }

        private void gtxtNationalNo_Validating(object sender, CancelEventArgs e)
        {
            Guna2TextBox NationalNo = (Guna2TextBox)sender;

            if (!_CheckErrorProviderForAllTextBoxs(NationalNo,"NationalNo must have a value!"))
            {
                if (clsPeople.IsPersonExist(NationalNo.Text) && _Person.NationalNo.ToUpper() != NationalNo.Text.ToUpper())
                {
                    NationalNo.Tag = false;
                    errorProvider1.SetError(NationalNo, "NationalNo must be unique value!");
                }
                else
                {
                    NationalNo.Tag = true; 
                    errorProvider1.SetError(NationalNo, "");
                }
            }
        }

        private void gtxtPhone_Validating(object sender, CancelEventArgs e)
        {
            _CheckErrorProviderForAllTextBoxs((Guna2TextBox)sender, "Phone must have a value!");
        }

        private void gtxtAddress_Validating(object sender, CancelEventArgs e)
        {
            _CheckErrorProviderForAllTextBoxs((Guna2TextBox)sender, "Address must have a value!");
        }

        private void gtxtEmail_Validating(object sender, CancelEventArgs e)
        {
            Guna2TextBox Email = (Guna2TextBox)sender;

            if (!string.IsNullOrWhiteSpace(Email.Text) && !Email.Text.Contains("@gmail.com") )
            {
                errorProvider1.SetError(Email, "Email must have @gmail.com");
                Email.Tag = false; ;
            }
            else
            {
                Email.Tag = true;
                errorProvider1.SetError(Email, "");
            }
        }

        private void gbtnSave_Click(object sender, EventArgs e)
        {
            _Save();
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
            _RemoveImageFromPictureBox();
            _ChangeImageAccordingToGendor();
        }

        private void rbMale_CheckedChanged(object sender, EventArgs e)
        {
            _ChangeImageAccordingToGendor();
        }

        private void gtxtAddress_TextChanged(object sender, EventArgs e)
        {

        }

        private void gbtnClose_Click(object sender, EventArgs e)
        {
            if (!this.IsSave)
                this._RemoveImageFromFile(pbImage.ImageLocation);

            _CloseForm();
        }
    }
}

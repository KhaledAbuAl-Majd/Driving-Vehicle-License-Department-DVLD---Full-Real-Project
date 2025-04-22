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
using Guna.UI2.WinForms;

namespace DVLDPresentation.People
{
    public partial class frmAddUpdatePerson : Form
    {
        public delegate void DataBackEventHandler(object sender, int PersonID);

        public event DataBackEventHandler DataBack;
        public enum enMode { AddNew = 0, Update = 1 };
        public enum enGendor { Male = 0, Female = 1 };

        public enMode _Mode;
        int _PersonID = -1;
        clsPerson _Person;

        public frmAddUpdatePerson()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
        }

        public frmAddUpdatePerson(int PersonID)
        {
            InitializeComponent();
            _Mode = enMode.Update;
            this._PersonID = PersonID;
        }
        private void _VisibleOfLLRemove(bool value)
        {
            llblRemoveImage.Visible = value;
        }
        private void _FillDataFromFormToObjectPerson()
        {
            int NationalCountryID = clsCountry.Find(gcmCountry.Text).CountryID;

            _Person.FirstName = gtxtFirstName.Text.Trim();
            _Person.SecondName = gtxtSecondName.Text.Trim(); ;
            _Person.ThirdName = gtxtThirdName.Text.Trim(); ;
            _Person.LastName = gtxtLastName.Text.Trim(); ;
            _Person.NationalNo = gtxtNationalNo.Text.Trim(); ;
            _Person.Email = gtxtEmail.Text.Trim();
            _Person.Phone = gtxtPhone.Text.Trim();
            _Person.Address = gtxtAddress.Text.Trim(); ;
            _Person.DateOfBirth = gDTPDateOfBirth.Value;

            if (rbMale.Checked)
                _Person.Gendor = (short) enGendor.Male;
            else
                _Person.Gendor = (short) enGendor.Female;

            _Person.NationalityCountryID = NationalCountryID;

            if (!string.IsNullOrEmpty(pbPersonImage.ImageLocation))
                _Person.ImagePath = pbPersonImage.ImageLocation;
            else
                _Person.ImagePath = "";
        }
        private bool _HandlePersonImage()
        {
            //_Person.ImagePath contains the old Image, we check if it changed then we copy the new image
            if (_Person.ImagePath != pbPersonImage.ImageLocation)
            {
                if (_Person.ImagePath != "")
                {
                    //first we delete the old image from the folder in case there is any.
                    try
                    {
                        File.Delete(_Person.ImagePath);
                    }
                    catch (IOException)
                    {
                        // We could not delete the file.
                        //log it later  
                    }

                }
                if (pbPersonImage.ImageLocation != null)
                {
                    //then we copy the new image to the image folder after we rename it
                    string SourceImageFile = pbPersonImage.ImageLocation.ToString();

                    if (clsUtil.CopyImageToProjectImagesFolder(ref SourceImageFile))
                    {
                        pbPersonImage.ImageLocation = SourceImageFile;
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Error Copying Image File", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }

            }

            return true;
        }
        private void _LoadData()
        {
            //_FillDataFromObjectPersonToForm
            _ChangePersonIDValue();
            gtxtFirstName.Text = _Person.FirstName;
            gtxtSecondName.Text = _Person.SecondName;
            gtxtThirdName.Text = _Person.ThirdName;
            gtxtLastName.Text = _Person.LastName;
            gtxtNationalNo.Text = _Person.NationalNo;
            gDTPDateOfBirth.Value = _Person.DateOfBirth;

            if (_Person.Gendor == (int)enGendor.Male)
            {
                rbMale.Checked = true;
            }
            else
            {
                rbFemale.Checked = true;
            }

            gtxtPhone.Text = _Person.Phone;
            gtxtEmail.Text = _Person.Email;
            gcmCountry.SelectedIndex = gcmCountry.FindString(clsCountry.Find(_Person.NationalityCountryID).CountryName);
            gtxtAddress.Text = _Person.Address;

            if (_Person.ImagePath != "")
                pbPersonImage.ImageLocation = _Person.ImagePath;

            _VisibleOfLLRemove((pbPersonImage.ImageLocation != ""));
        }
        private void _ChangeImageAccordingToGendor()
        {
            if (!string.IsNullOrEmpty(pbPersonImage.ImageLocation))
                return;

            if (rbMale.Checked)
            {
                pbPersonImage.Image = Resources.Male_512;
            }
            else
            {
                pbPersonImage.Image = Resources.Female_512;
            }
        }
        void _FillCountiresInComboBox()
        {
            DataTable dtCountries = clsCountry.GetAllCountires();

            foreach (DataRow row in dtCountries.Rows)
            {
                gcmCountry.Items.Add(row["CountryName"]);
            }

            //gcmCountry.DataSource = dt;
            //gcmCountry.DisplayMember = "CountryName";
            gcmCountry.SelectedIndex = gcmCountry.FindString("Egypt");
        }
        private void _ChangeHeader(string Text)
        {
            lblHeader.Text = Text;
        }
        private void _ChangePersonIDValue()
        {
            lblPersonID.Text = _Person.PersonID.ToString();
        }
        private void _UpdateMode()
        {
            _Person = clsPerson.Find(_PersonID);

            if (_Person == null)
            {
                MessageBox.Show("No Person with ID = " + _PersonID, "Person Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
                return;
            }

            _LoadData();
            _ChangeHeader("Upadte Person");
        }
        private void _AddNewMode()
        {
            _Person = new clsPerson();
            gcmCountry.SelectedIndex = gcmCountry.FindString("Egypt");
            gDTPDateOfBirth.Value = gDTPDateOfBirth.MaxDate;
            rbMale.Checked = true;
            _ChangeHeader("Add New Person");
            _VisibleOfLLRemove(false);
        }
        private void frmAdd_EditPerson_Load(object sender, EventArgs e)
        {
            gDTPDateOfBirth.MaxDate = DateTime.Now.AddYears(-18);
            gDTPDateOfBirth.MinDate = DateTime.Now.AddYears(-100);
            _FillCountiresInComboBox();

            switch (_Mode)
            {
                case enMode.AddNew:
                    _AddNewMode();
                    break;

                case enMode.Update:
                    _UpdateMode();
                    break;
            }
        }

        //_CheckErrorProviderForAllTextBoxs
        void ValidateEmptyTextBox(object sender, CancelEventArgs e)
        {
            Guna2TextBox Temp = (Guna2TextBox)sender;

            if (string.IsNullOrWhiteSpace(Temp.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(Temp, "This Field is required!");            
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(Temp, "");

            }
        }

        private void gtxtNationalNo_Validating(object sender, CancelEventArgs e)
        {
            Guna2TextBox NationalNo = (Guna2TextBox)sender;

            if (string.IsNullOrWhiteSpace(NationalNo.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(NationalNo, "This Field is required!");
                return;
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(NationalNo, "");

            }

            string NationalNoText = NationalNo.Text.Trim();

            if (NationalNoText != _Person.NationalNo && clsPerson.IsPersonExist(NationalNoText))
            {
                e.Cancel = true;
                errorProvider1.SetError(NationalNo, "National Number is used for another person!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(NationalNo, "");
            }
            
        }

        private void gtxtEmail_Validating(object sender, CancelEventArgs e)
        {
            Guna2TextBox Email = (Guna2TextBox)sender;

            if (!string.IsNullOrWhiteSpace(Email.Text))
            {
                if (!clsValidation.ValidateEmail(Email.Text))
                {
                    e.Cancel = true;
                    errorProvider1.SetError(Email, "Invalid Email Address Format!");
                    return;
                }                
            }

            e.Cancel = false;
            errorProvider1.SetError(Email, "");
        }

        private void rbMale_CheckedChanged(object sender, EventArgs e)
        {
            _ChangeImageAccordingToGendor();
        }

        private void llSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.Multiselect = false;
            openFileDialog1.Filter = @"Image Files (*.gif;*.jpg;*.jpeg;*.bmp;*.wmf;*.png)|
                *.gif;*.jpg;*.jpeg;*.bmp;*.wmf;*.png";
            openFileDialog1.Title = "Choose Image";
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.FilterIndex = 1;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string SelectedFilepath = openFileDialog1.FileName;
                pbPersonImage.ImageLocation = SelectedFilepath;
                _VisibleOfLLRemove(true);
            }
        }

        private void llblRemoveImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pbPersonImage.ImageLocation = null;
            _VisibleOfLLRemove(false);
            _ChangeImageAccordingToGendor();
        }

        private void gbtnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!_HandlePersonImage())
                return;

            _FillDataFromFormToObjectPerson();

            if (_Person.Save())
            {
                _ChangePersonIDValue();
                _Mode = enMode.Update;
                _ChangeHeader("Update Person");

                MessageBox.Show("Data Saved Successfuly", "Saved" , MessageBoxButtons.OK, MessageBoxIcon.Information);

                DataBack?.Invoke(this, _Person.PersonID);
            }
            else
            {
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
               
            }
        }

        private void gbtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

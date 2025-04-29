using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using DVLDDataAccess;

namespace DVLDBusiness
{
    public class clsPerson
    {
        public enum enMode { AddNew = 0, Update = 1 };

         public enMode _Mode { get; private set; } = enMode.AddNew;
        public int PersonID { get; private set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }
        public string FullName
        {
            get
            {
                return this.FirstName + " " + this.SecondName + " " + ((this.ThirdName == "") ? "" : this.ThirdName + " ") + this.LastName;
            }
        }
        public string NationalNo { get; set; }
        public DateTime DateOfBirth { get; set; }
        public short Gendor { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int NationalityCountryID { get; set; }

        public clsCountry CountryInfo;
        public string ImagePath { get; set; }

        public clsPerson()
        {
            PersonID = -1;
            this.FirstName = "";
            this.SecondName = "";
            this.ThirdName = "";
            this.LastName = "";
            this.NationalNo = "";
            this.DateOfBirth = new DateTime(1900, 1, 1);
            this.Gendor = 0;
            this.Address = "";
            this.Phone = "";
            this.Email = "";
            this.NationalityCountryID = -1;
            this.ImagePath = "";
            _Mode = enMode.AddNew;
        }
        private clsPerson(int PersonID,string FirstName, string SecondName, string ThirdName, string LastName, string NationalNO, DateTime DateOfBirth
            , short Gendor, string Address, string Phone, string Email, int NationalityCountryID, string ImagePath)
        {
            this.PersonID = PersonID;
            this.FirstName = FirstName;
            this.SecondName = SecondName;
            this.ThirdName = ThirdName;
            this.LastName = LastName;
            this.NationalNo = NationalNO;
            this.DateOfBirth = DateOfBirth;
            this.Gendor = Gendor;
            this.Address = Address;
            this.Phone = Phone;
            this.Email = Email;
            this.NationalityCountryID = NationalityCountryID;
            this.ImagePath = ImagePath;
            this.CountryInfo = clsCountry.Find(NationalityCountryID);

            _Mode = enMode.Update;
        }

        private bool _AddNewPerson()
        {
            PersonID = clsPersonData.AddNewPerson(FirstName, SecondName, ThirdName, LastName, NationalNo,
                DateOfBirth, Gendor, Address, Phone, Email, NationalityCountryID, ImagePath);

            return (PersonID != -1);
        }
        private bool _UpdatePeron() 
        {
            return clsPersonData.UpdatePerson(this.PersonID, this.FirstName, this.SecondName, this.ThirdName, this.LastName, this.NationalNo,
                this.DateOfBirth, this.Gendor, this.Address, this.Phone, this.Email, this.NationalityCountryID, this.ImagePath);
        }

        public static clsPerson Find(int PersonID)
        {
            string FirstName = "", SecondName = "", ThirdName = "", LastName = "", NationalNO = ""
           , Address = "", Phone = "", Email = "", ImagePath = "";

            int NationalCountryID = -1;
            DateTime DateOfBirth = new DateTime(1900,2,3);
            short Gendor = 0;


            if (clsPersonData.GetPeronInfoByID(PersonID, ref FirstName, ref SecondName, ref ThirdName, ref LastName, ref NationalNO, ref DateOfBirth
                 , ref Gendor, ref Address, ref Phone, ref Email, ref NationalCountryID, ref ImagePath))
            {
                return new clsPerson(PersonID, FirstName, SecondName, ThirdName, LastName, NationalNO, DateOfBirth, Gendor, Address,
                    Phone, Email, NationalCountryID, ImagePath);
            }
            else
                return null; 
        }

        public static clsPerson Find(string NationalNO)
        {
            string FirstName = "", SecondName = "", ThirdName = "", LastName = ""
           , Address = "", Phone = "", Email = "", ImagePath = "";

            int PersonID = -1, NationalCountryID = -1;
            DateTime DateOfBirth = new DateTime(1900, 2, 3);
            short Gendor = 0;


            if (clsPersonData.GetPeronInfoByNationalNO(NationalNO,ref PersonID, ref FirstName, ref SecondName, ref ThirdName, ref LastName,ref DateOfBirth
                 , ref Gendor, ref Address, ref Phone, ref Email, ref NationalCountryID, ref ImagePath))
            {
                return new clsPerson(PersonID, FirstName, SecondName, ThirdName, LastName, NationalNO, DateOfBirth, Gendor, Address,
                    Phone, Email, NationalCountryID, ImagePath);
            }
            else
                return null;
        }

        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.AddNew:
                    if (_AddNewPerson())
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return _UpdatePeron();
            }

            return false;
        }

        public static DataTable GetAllPeople()
        {
            return clsPersonData.GetAllPeople();
        }

        static void _DeleteImageAfterDeletePerson(string ImagePath)
        {
            if (!string.IsNullOrEmpty(ImagePath))
            {
                try
                {
                    File.Delete(ImagePath);
                }
                catch (IOException)
                {
                    // We could not delete the file.
                    //log it later  
                }

            }
        }

        public static bool DeletePerson(int PersonID)
        {
            string ImagePath = clsPerson.Find(PersonID)?.ImagePath;

            if (clsPersonData.DeletePerson(PersonID))
            {
                _DeleteImageAfterDeletePerson(ImagePath);
                return true;
            }
            return false;
        }

        public static bool IsPersonExist(int PersonID)
        {
            return clsPersonData.IsPersonExist(PersonID);
        }

        public static bool IsPersonExist(string NationalNo)
        {
            return clsPersonData.IsPersonExist(NationalNo);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using DVLDDataAccess;

namespace DVLDBusiness
{
    public class clsPeople
    {
        public enum enMode { AddNew = 0, Update = 1 };

         public enMode _Mode { get; private set; } = enMode.AddNew;
        public int PersonID { get; private set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }
        public string NationalNo { get; set; }
        public DateTime DateOfBirth { get; set; }
        public short Gendor { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int NationalityCountryID { get; set; }
        public string ImagePath { get; set; }

        public clsPeople()
        {
            PersonID = -1;
            _Mode = enMode.AddNew;
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
        }
        private clsPeople(int PersonID,string FirstName, string SecondName, string ThirdName, string LastName, string NationalNO, DateTime DateOfBirth
            , short Gendor, string Address, string Phone, string Email, int NationalityCountryID, string ImagePath)
        {
            this.PersonID = PersonID;
            _Mode = enMode.Update;
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
        }

        private bool _AddNewPerson()
        {
            PersonID = clsPeopleData.AddNewPerson(FirstName, SecondName, ThirdName, LastName, NationalNo,
                DateOfBirth, Gendor, Address, Phone, Email, NationalityCountryID, ImagePath);

            return (PersonID != -1);
        }
        private bool _UpdatePeron() 
        {
            return clsPeopleData.UpdatePerson(this.PersonID, this.FirstName, this.SecondName, this.ThirdName, this.LastName, this.NationalNo,
                this.DateOfBirth, this.Gendor, this.Address, this.Phone, this.Email, this.NationalityCountryID, this.ImagePath);
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

        public static clsPeople Find(int PersonID)
        {
            string FirstName = "", SecondName = "", ThirdName = "", LastName = "", NationalNO = ""
           , Address = "", Phone = "", Email = "", ImagePath = "";

            int NationalCountryID = -1;
            DateTime DateOfBirth = new DateTime(1900,2,3);
            short Gendor = 0;


            if (clsPeopleData.GetPeronInfoByID(PersonID, ref FirstName, ref SecondName, ref ThirdName, ref LastName, ref NationalNO, ref DateOfBirth
                 , ref Gendor, ref Address, ref Phone, ref Email, ref NationalCountryID, ref ImagePath))
            {
                return new clsPeople(PersonID, FirstName, SecondName, ThirdName, LastName, NationalNO, DateOfBirth, Gendor, Address,
                    Phone, Email, NationalCountryID, ImagePath);
            }
            else
                return null; 
        }
        public static clsPeople Find(string NationalNO)
        {
            string FirstName = "", SecondName = "", ThirdName = "", LastName = ""
           , Address = "", Phone = "", Email = "", ImagePath = "";

            int PersonID = -1, NationalCountryID = -1;
            DateTime DateOfBirth = new DateTime(1900, 2, 3);
            short Gendor = 0;


            if (clsPeopleData.GetPeronInfoByNationalNO(NationalNO,ref PersonID, ref FirstName, ref SecondName, ref ThirdName, ref LastName,ref DateOfBirth
                 , ref Gendor, ref Address, ref Phone, ref Email, ref NationalCountryID, ref ImagePath))
            {
                return new clsPeople(PersonID, FirstName, SecondName, ThirdName, LastName, NationalNO, DateOfBirth, Gendor, Address,
                    Phone, Email, NationalCountryID, ImagePath);
            }
            else
                return null;
        }

        public static DataTable GetAllPeople()
        {
            return clsPeopleData.GetAllPeople();
        }

        public static bool DeletePerson(int PersonID)
        {
            return clsPeopleData.DeletePerson(PersonID);
        }

        public static bool IsPersonExist(int PersonID)
        {
            return clsPeopleData.IsPersonExist(PersonID);
        }

        public static bool IsPersonExist(string NationalNo)
        {
            return clsPeopleData.IsPersonExist(NationalNo);
        }

        public string GetFullName()
        {
            return this.FirstName + " " + this.SecondName + " " + this.ThirdName + " " + this.LastName;
        }
    }
}

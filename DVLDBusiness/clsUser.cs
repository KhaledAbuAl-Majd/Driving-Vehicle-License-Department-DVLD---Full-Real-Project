using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using DVLDConstant;
using DVLDDataAccess;

namespace DVLDBusiness
{
    public class clsUser
    {
        public enum enMode { AddNew, Update };
        public enMode Mode { get; private set; }
        public int UserID { get;private set; }
        public int PersonID { get; set; }

        public clsPerson PersonInfo;
        public string UserName { get; set; }

        /// <summary>
        /// HashedPassword
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Salt to add to password before hashing
        /// </summary>
        public string Salt { get;private set; }

        public bool IsActive { get; set; }

        public clsUser()
        {
            this.UserID = -1;
            this.PersonID = -1;
            this.UserName = "";
            this.Password = "";
            this.IsActive = false;
            Mode = enMode.AddNew;
        }

        private clsUser(int UserID,int PersonID,string UserName,string Password,string Salt,bool IsActive)
        {
            this.UserID = UserID;
            this.PersonID = PersonID;
            this.PersonInfo = clsPerson.Find(PersonID);
            this.UserName = UserName;
            this.Password = Password;
            this.Salt = Salt;
            this.IsActive = IsActive;
            this.Mode = enMode.Update;
        }

        private bool _AddNewUser()
        {
            var result = clsSecurity.HashPasswordWithSalt(this.Password);
            this.Salt = result.Salt;
            this.Password = result.HashedPassword;

            UserID = clsUserData.AddNewUser(this.PersonID, this.UserName, this.Password, this.Salt, this.IsActive);
            return (UserID != -1);
        }
        private bool _UpdateUser()
        {
            return clsUserData.UpdateUser(this.UserID, this.PersonID,this.UserName, this.IsActive);
        }
        public static clsUser FindByUserID(int UserID)
        {
            int PersonID = -1;
            string UserName = "", Password = "",Salt = "";
            bool IsActive = false;

            if (clsUserData.GetUserInfoByUserID(UserID, ref PersonID, ref UserName, ref Password,ref Salt, ref IsActive))
            {
                return new clsUser(UserID, PersonID, UserName, Password, Salt, IsActive);
            }
            else
                return null;
        }
        public static clsUser FindByPersonID(int PersonID)
        {
            int UserID = -1;
            string UserName = "", Password = "", Salt = "";
            bool IsActive = false;

            if (clsUserData.GetUserInfoByPersonID(PersonID, ref UserID, ref UserName, ref Password, ref Salt, ref IsActive))
            {
                return new clsUser(UserID, PersonID, UserName, Password, Salt, IsActive);
            }
            else
                return null;
        }
        public static clsUser FindByUserNameAndPassword(string UserName, string Password)
        {
            int UserID = -1, PersonID = -1;
            string Salt = "";
            bool IsActive = false;

            if (clsUserData.GetUserInfoByUsernameAndPassword(UserName, Password, ref UserID, ref PersonID, ref Salt, ref IsActive))
            {
                return new clsUser(UserID, PersonID, UserName, Password, Salt, IsActive);
            }
            else
                return null;
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewUser())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return _UpdateUser();
            }

            return false;
        }
        public static DataTable GetAllUsers()
        {
            return clsUserData.GetAllUsers();
        }
        public static bool DeleteUser(int UserID)
        {
            return clsUserData.DeleteUser(UserID);
        }
        public static bool IsUserExist(int UserID)
        {
            return clsUserData.IsUserExist(UserID);
        }
        public static bool IsUserExist(string UserName)
        {
            return clsUserData.IsUserExist(UserName);
        }
        public static bool IsUserExistForPersonID(int PersonID)
        {
            return clsUserData.IsUserExistForPersonID(PersonID);
        }
        public bool ChangePassword()
        {
            var result = clsSecurity.HashPasswordWithSalt(this.Password);
            this.Salt = result.Salt;
            this.Password = result.HashedPassword;

            return clsUserData.ChangePassword(this.UserID, this.Password, this.Salt);
        }

        public static string GetSaltByUserName(string UserName)
        {
            return clsUserData.GetSaltByUserName(UserName);
        }
    }
}

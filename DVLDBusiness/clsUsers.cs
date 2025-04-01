using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using DVLDDataAccess;

namespace DVLDBusiness
{
    public class clsUsers
    {
        public enum enMode { AddNew, Update };
        public enMode Mode { get; private set; }
        public int UserID { get;private set; }
        public int PersonID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }

        public clsUsers()
        {
            Mode = enMode.AddNew;
            this.UserID = -1;
            this.PersonID = -1;
            this.UserName = "";
            this.Password = "";
            this.IsActive = false;
        }

        private clsUsers(int UserID,int PersonID,string UserName,string Password,bool IsActive)
        {
            this.Mode = enMode.Update;
            this.UserID = UserID;
            this.PersonID = PersonID;
            this.UserName = UserName;
            this.Password = Password;
            this.IsActive = IsActive;
        }

        private bool _AddNewUser()
        {
            UserID = clsUsersData.AddNewUser(this.PersonID, this.UserName, this.Password, this.IsActive);

            return (UserID != -1);
        }

        private bool _UpdateUser()
        {
            return clsUsersData.UpdateUser(this.UserID, this.PersonID,this.UserName, this.Password, this.IsActive);
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

        public static clsUsers Find(int UserID)
        {
            int PersonID = -1;
            string UserName = "", Password = "";
            bool IsActive = false;

            if (clsUsersData.GetPersonInfoByUserID(UserID, ref PersonID, ref UserName, ref Password, ref IsActive))
            {
                return new clsUsers(UserID, PersonID, UserName, Password, IsActive);
            }
            else
                return null;
        }

        public static clsUsers Find(string UserName)
        {
            int PersonID = -1, UserID = -1;
            string Password = "";
            bool IsActive = false;

            if (clsUsersData.GetPersonInfoByUserName(UserName,ref UserID, ref PersonID, ref Password, ref IsActive))
            {
                return new clsUsers(UserID, PersonID, UserName, Password, IsActive);
            }
            else
                return null;
        }

        public static bool DeleteUser(int UserID)
        {
            return clsUsersData.DeleteUser(UserID);
        }

        public static bool IsUserExist(int UserID)
        {
            return clsUsersData.IsUserExist(UserID);
        }

        public static bool IsUserExist(string UserName)
        {
            return clsUsersData.IsUserExist(UserName);
        }

        public static DataTable GetAllUsers()
        {
            return clsUsersData.GetAllUsers();
        }
    }
}

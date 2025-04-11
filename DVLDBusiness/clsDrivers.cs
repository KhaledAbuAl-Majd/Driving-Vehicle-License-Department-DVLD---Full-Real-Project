using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DVLDDataAccess;

namespace DVLDBusiness
{
    public class clsDrivers
    {
        public enum enMode { AddNew, Update }
        public enMode Mode { get; private set; }
        public int DriverID { get; private set; }
        public int PersonID { get; set; }
        public int CreatedByUserID { get; set; }
        public DateTime CreatedDate { get; set; }

        public clsDrivers()
        {
            Mode = enMode.AddNew;
            this.DriverID = -1;
            this.PersonID = -1;
            this.CreatedByUserID = -1;
            this.CreatedDate = DateTime.Now;
        }
        private clsDrivers(int DriverID,int PersonID,int CreatedByUserID,DateTime CreatedDate)
        {
            Mode = enMode.Update;
            this.DriverID = DriverID;
            this.PersonID = PersonID;
            this.CreatedByUserID = CreatedByUserID;
            this.CreatedDate = CreatedDate;
        }
        private bool _AddNewDriver()
        {
            this.DriverID = clsDriversData.AddNewDriver(this.PersonID, this.CreatedByUserID, this.CreatedDate);

            return (DriverID != -1);
        }
        public bool save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewDriver())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;
            }
            return false;
        }
        public static clsDrivers FindByDriverID(int DriverID)
        {
            int personID = -1, CreatedByUserID = -1;
            DateTime CreatedDate = new DateTime(1900, 1, 1);

            if (clsDriversData.GetDriverInfoByDriverID(DriverID, ref personID, ref CreatedByUserID, ref CreatedDate))
                return new clsDrivers(DriverID, personID, CreatedByUserID, CreatedDate);
            else
                return null;
        }
        public static clsDrivers FindByPersonID(int PersonID)
        {
            int DriverID = -1, CreatedByUserID = -1;
            DateTime CreatedDate = new DateTime(1900, 1, 1);

            if (clsDriversData.GetDriverInfoByPersonID(PersonID, ref DriverID, ref CreatedByUserID, ref CreatedDate))
                return new clsDrivers(DriverID, PersonID, CreatedByUserID, CreatedDate);
            else
                return null;
        }
        public static DataTable GetAllDrivers()
        {
            return clsDriversData.GetAllDrivers();
        }
        public static bool IsDriverExist(int PerosnID)
        {
            return clsDriversData.IsDriverExist(PerosnID);
        }
    }
}

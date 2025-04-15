using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLDDataAccess;

namespace DVLDBusiness
{
    public class clsDetainedLicenses
    {
        public enum enMode { Detain, Release }
        public enMode Mode { get; private set; }
        public int DetainID { get; private set; }
        public int LicenseID { get; private set; }
        public DateTime DetainDate { get;private set; }
        public float FineFees { get;private set; }
        public int CreatedByUserID { get;private set; }
        public bool IsReleased { get; private set; }
        public DateTime ReleasedDate { get;private set; }
        public int ReleasedByUserID{ get; set; }
        public int ReleaseApplicationID { get;private set; }

         //Release Detained License
        public int ApplicatoinTypeID
        {
            get
            {
                return 5;
            }
        }
        public clsDetainedLicenses(int LicenseID, float FineFees, int CreatedByUserID)
        {
            Mode = enMode.Detain;
            this.DetainID = -1;
            this.LicenseID = LicenseID;
            this.DetainDate = DateTime.Now;
            this.FineFees = FineFees;
            this.CreatedByUserID = CreatedByUserID;
            this.IsReleased = false;
            this.ReleasedDate = new DateTime(1900, 1, 1);
            this.ReleasedByUserID = -1;
            this.ReleaseApplicationID = -1;
        }

        private clsDetainedLicenses(int DetainID, int LicenseID, DateTime DetainDate, float FineFees, int CreatedByUserID, bool IsReleased)
        {
            this.DetainID = DetainID;
            this.LicenseID = LicenseID;
            this.DetainDate = DetainDate;
            this.FineFees = FineFees;
            this.CreatedByUserID = CreatedByUserID;
            this.IsReleased = IsReleased;
            Mode = enMode.Release;
        }
        bool _DetainLicense()
        {
            this.DetainID = clsDetainedLicensesData.DetainLicense(this.LicenseID, this.DetainDate, this.FineFees, this.CreatedByUserID);
            

            return (DetainID != -1);
        }
        bool _ReleaseLicense()
        {
            if (IsLicenseDetainedByDetainID(this.DetainID))
            {
               
                int PersonID = clsDrivers.FindByDriverID(clsLicenses.FindByLicenseID(this.LicenseID).DriverID).PersonID;

                clsApplications ReleaseApplication = new clsApplications
                {
                    ApplicationDate = DateTime.Now,
                    ApplicationStatusID = 1,
                    ApplicationTypeID = ApplicatoinTypeID,
                    CreatedByUserID = this.CreatedByUserID,
                    PersonID = PersonID,      
                };

                if (ReleaseApplication.Save())
                {
                    this.ReleasedDate = DateTime.Now;
                    this.ReleaseApplicationID = ReleaseApplication.ApplicationID;

                    return clsDetainedLicensesData.ReleaseLicense(DetainID, ReleasedDate, ReleasedByUserID, ReleaseApplicationID);
                }

            }

            return false;
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.Detain:
                    if (_DetainLicense())
                    {
                        Mode = enMode.Release;
                        this.IsReleased = false;
                        return true;
                    }
                    else
                        return false;

                case enMode.Release:
                    if (_ReleaseLicense())
                    {
                        this.IsReleased = true;
                        return true;
                    }
                    else
                        return false;
            }

            return false;   
        }

        public static clsDetainedLicenses FindByDetainID(int DetainID)
        {
            int LicenseID = -1, CreatedByUserID = -1;
            DateTime DetainDate = DateTime.Now;
            float FineFees = 0;
            bool IsReleased = false;

            if (clsDetainedLicensesData.GetDetainLicenseInfoByDetainID(DetainID, ref LicenseID, ref DetainDate, ref FineFees, ref CreatedByUserID, ref IsReleased))
            {
                return new clsDetainedLicenses(DetainID, LicenseID, DetainDate, FineFees, CreatedByUserID, IsReleased);
            }
            else
                return null;
        }
        public static clsDetainedLicenses FindByLicenseID(int LicenseID)
        {
            int DetainID  = -1, CreatedByUserID = -1;
            DateTime DetainDate = DateTime.Now;
            float FineFees = 0;
            bool IsReleased = false;

            if (clsDetainedLicensesData.GetDetainLicenseInfoByLicenseID(LicenseID , ref DetainID, ref DetainDate, ref FineFees, ref CreatedByUserID, ref IsReleased))
            {
                return new clsDetainedLicenses(DetainID, LicenseID, DetainDate, FineFees, CreatedByUserID, IsReleased);
            }
            else
                return null;
        }
        public static bool IsLicenseDetainedByDetainID(int DetainID)
        {
            return clsDetainedLicensesData.IsLicenseDetainedByDetainID(DetainID);
        }
        public static bool IsLicenseDetainedByLicenseID(int LicenseID)
        {
            return clsDetainedLicensesData.IsLicenseDetainedByLicenseID(LicenseID);
        }
        public static DataTable GetAllLicensesDetainedAndNot()
        {
            return clsDetainedLicensesData.GetAllLicensesDetainedAndNot();
        }
    }
}

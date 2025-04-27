using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLDDataAccess;

namespace DVLDBusiness
{
    public class clsLicense
    {
        public enum enMode { AddNew, Update }
        public enMode Mode { get; private set; }

        public enum enIssueReason { FirstTime = 1, Renew = 2, DamagedReplacement = 3, LostReplacement = 4 };

        public clsDriver DriverInfo;
        public int LicenseID { get; private set; }
        public int ApplicationID { get; private set; }
        public int DriverID { get; private set; }
        public int LicenseClassID{ get; private set; }

        public clsLicenseClass LicenseClassIfo;
        public DateTime IssueDate { get; private set; }
        public DateTime ExpirationDate { get; private set; }
        public string Notes { get; set; }
        public float PaidFees { get; private set; }
        public bool IsActive{ get; set; }
        public enIssueReason IssueReason { get; set; }
        public string IssueReasonText
        {
            get
            {
                return GetIssueReasonText(this.IssueReason);
            }
        }
        public clsDetainedLicenses DetainedInfo { get; set; }
        public int CreatedByUserID { get; private set; }
        public bool IsDetained
        {
            get { return clsDetainedLicenses.IsLicenseDetainedByLicenseID(this.LicenseID); }
        }

        public clsLicense()
        {
            this.LicenseID = -1;
            this.ApplicationID = -1;
            this.DriverID = -1;
            this.LicenseClassID = -1;
            this.IssueDate = DateTime.Now;
            this.ExpirationDate = DateTime.Now;
            this.Notes = "";
            this.PaidFees = 0;
            this.IsActive = true;
            this.IssueReason = enIssueReason.FirstTime;
            this.CreatedByUserID = -1;

            Mode = enMode.AddNew;
        }

        private clsLicense(int LicneseID,int ApplicationID, int DriverID, int LicenseClassID, DateTime IsuueDate, DateTime ExpirationDate,
            string Notes, float PaidFees, bool IsAcitve, enIssueReason IssueReason, int CreatedByUserID)
        {
            this.LicenseID = LicenseID;
            this.ApplicationID = ApplicationID;
            this.DriverID = DriverID;
            this.LicenseClassID = LicenseClassID;
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.Notes = Notes;
            this.PaidFees = PaidFees;
            this.IsActive = IsActive;
            this.IssueReason = IssueReason;
            this.CreatedByUserID = CreatedByUserID;

            this.DriverInfo = clsDriver.FindByDriverID(this.DriverID);
            this.LicenseClassIfo = clsLicenseClass.Find(this.LicenseClassID);
            this.DetainedInfo = clsDetainedLicenses.FindByLicenseID(this.LicenseID);

            Mode = enMode.Update;
        }
        private bool _AddNewLicnese()
        {
            this.LicenseID = clsLicenseData.AddNewLicense(ApplicationID, DriverID, LicenseClassID, IssueDate, ExpirationDate,
                Notes, PaidFees, IsActive, (byte)IssueReason, CreatedByUserID);

            return (LicenseID != -1);
        }

        private bool _UpdateLicense()
        {
            //call DataAccess Layer 

            return clsLicenseData.UpdateLicense(this.ApplicationID, this.LicenseID, this.DriverID, this.LicenseClassID,
               this.IssueDate, this.ExpirationDate, this.Notes, this.PaidFees,
               this.IsActive, (byte)this.IssueReason, this.CreatedByUserID);
        }

        public static clsLicense Find(int LicneseID)
        {
            int ApplicationID = -1; int DriverID = -1; int LicenseClass = -1;
            DateTime IssueDate = DateTime.Now; DateTime ExpirationDate = DateTime.Now;
            string Notes = "";
            float PaidFees = 0; bool IsActive = true; int CreatedByUserID = 1;
            byte IssueReason = 1;
            if (clsLicenseData.GetLicenseInfoByID(LicneseID, ref ApplicationID, ref DriverID, ref LicenseClass,
            ref IssueDate, ref ExpirationDate, ref Notes,
            ref PaidFees, ref IsActive, ref IssueReason, ref CreatedByUserID))

                return new clsLicense(LicneseID, ApplicationID, DriverID, LicenseClass,
                                     IssueDate, ExpirationDate, Notes,
                                     PaidFees, IsActive, (enIssueReason)IssueReason, CreatedByUserID);
            else
                return null;
        }

        public static clsLicense FindByApplicationID(int ApplicationID)
        {
            int LicneseID = -1, DriverID = -1, LicneseClassID = -1, CreatedByUserID = -1;
            DateTime IssueDate = new DateTime(1900, 1, 1), ExpirationDate = new DateTime(1900, 1, 1);
            string Notes = "";byte IssueReason = 0;
            float PaidFees = 0;
            bool IsActive = false;

            if (clsLicenseData.GetLicneseInfoByApplicationID(ApplicationID, ref LicneseID,  ref DriverID, ref LicneseClassID, ref IssueDate, ref ExpirationDate,
                ref Notes, ref PaidFees, ref IsActive, ref IssueReason, ref CreatedByUserID))

                return new clsLicense(LicneseID, ApplicationID, DriverID, LicneseClassID, IssueDate, ExpirationDate,
                    Notes, PaidFees, IsActive, (enIssueReason)IssueReason, CreatedByUserID);
            else
                return null;
        }

        public static DataTable GetAllLicneses()
        {
            return clsLicenseData.GetAllLicenses();
        }

        public bool Save()
        {
            switch (this.Mode)
            {
                case enMode.AddNew:
                    if (_AddNewLicnese())
                    {
                        this.Mode = enMode.Update;
                        clsApplication Application = clsApplication.FindBaseApplication(this.ApplicationID);
                        //Application.ApplicationStatus = clsApplicationStatuses.Find("Completed").ApplicationStatusID;
                        Application.LastStatusDate = DateTime.Now;
                        Application.Save();
                        return true;
                    }
                    else
                        return false;
            }
            return false;   
        }

        public static bool IsLicenseExistByPersonID(int PersonID, int LicenseClassID)
        {
            return (GetActiveLicenseIDByPersonID(PersonID, LicenseClassID) != -1);
        }

        public static int GetActiveLicenseIDByPersonID(int PersonID, int LicenseClassID)
        {

            return clsLicenseData.GetActiveLicenseIDByPersonID(PersonID, LicenseClassID);

        }

        public static DataTable GetDriverLicenses(int DriverID)
        {
            return clsLicenseData.GetDriverLicenses(DriverID);
        }

        public static DataTable GetDriverLicensePersonID(int PersonID)
        {
            int DriverID = clsDriver.FindByPersonID(PersonID).DriverID;

            return clsLicenseData.GetDriverLicenses(DriverID);
        }

        public Boolean IsLicenseExpired()
        {

            return (this.ExpirationDate < DateTime.Now);

        }

        public bool DeactivateCurrentLicense()
        {
            return clsLicenseData.DeactivateLicense(this.LicenseID);
        }

        public static string GetIssueReasonText(enIssueReason IssueReason)
        {
            switch (IssueReason)
            {
                case enIssueReason.FirstTime:
                    return "First Time";
                case enIssueReason.Renew:
                    return "Renew";
                case enIssueReason.DamagedReplacement:
                    return "Replacement for Damaged";
                case enIssueReason.LostReplacement:
                    return "Replacement for Lost";
                default:
                    return "First Time";
            }
        }

        //
        public static bool IsPersonHaveAnActiveLicneseWithTheSameLicneseClass(int PersonID, int LicenseClassID)
        {
            return clsLicenseData.IsPersonHaveAnActiveLicneseWithTheSameLicneseClass(PersonID, LicenseClassID);
        }
     
        public static bool IsLicenseExist(int LicenseID)
        {
            return clsLicenseData.IsLicenseExist(LicenseID);
        }

        //

    }
}

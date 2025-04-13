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
    public class clsLicenses
    {
        public enum enMode { AddNew, Update }
        public enMode Mode { get; private set; }
        public int LicneseID { get; private set; }
        public int ApplicationID { get; private set; }
        public int DriverID { get; private set; }
        public int LicenseClassID{ get; private set; } 
        public DateTime IsuueDate { get; private set; }
        public DateTime ExpirationDate { get; private set; }
        public string Notes { get; set; }
        public float PaidFees { get; private set; }
        public bool IsAcitve{ get; set; }
        public string IssueReason { get; set; }
        public int CreatedByUserID { get; private set; }

        //For New Local Driving LIcense
        public clsLicenses(int LDLApplicationID,int CreatedByUseID,string IssueReasonText)
        {
            this.Mode = enMode.AddNew;
            this.LicneseID = -1;

            clsLocalDrivingApplictions LDLApplication = clsLocalDrivingApplictions.FindByLDLApplicationID(LDLApplicationID);

            if (LDLApplication != null)
            {
                clsApplications Application = clsApplications.Find(LDLApplication.ApplicationID);

                this.ApplicationID = LDLApplication.ApplicationID;
                this.DriverID = clsDrivers.FindByPersonID(Application.PersonID).DriverID;
                this.LicenseClassID = LDLApplication.LicenseClassID;
                this.IsuueDate = DateTime.Now;
                this.ExpirationDate = this.IsuueDate.AddYears(clsLicneseClasses.Find(LicenseClassID).DefaultValidityLength);
                this.Notes = "";
                this.PaidFees = clsApplicationTypes.FindApplicationType(Application.ApplicationTypeID).ApplicationFees;
                this.IsAcitve = true;
                this.IssueReason = IssueReasonText;
                this.CreatedByUserID = CreatedByUseID;
            }
            else
            {

                this.ApplicationID = -1;
                this.DriverID = -1;
                this.LicenseClassID = -1;
                this.IsuueDate = new DateTime(1900, 1, 1);
                this.ExpirationDate = new DateTime(1900, 1, 1);
                this.Notes = "";
                this.PaidFees = 0;
                this.IsAcitve = false;
                this.IssueReason = "";
                this.CreatedByUserID = -1;
            }
        }

        //For any thing else
        public clsLicenses(clsApplications Application, clsLicenses OldLicense ,string IssueReasonText)
        {
            this.Mode = enMode.AddNew;
            this.LicneseID = -1;

            if (Application != null)
            {
                this.ApplicationID =  Application.ApplicationID;
                this.DriverID = OldLicense.DriverID;
                this.LicenseClassID = OldLicense.LicenseClassID;
                this.IsuueDate = DateTime.Now;
                this.ExpirationDate = this.IsuueDate.AddYears(clsLicneseClasses.Find(LicenseClassID).DefaultValidityLength);
                this.Notes = "";
                this.PaidFees = clsApplicationTypes.FindApplicationType(Application.ApplicationTypeID).ApplicationFees;
                this.IsAcitve = true;
                this.IssueReason = IssueReasonText;
                this.CreatedByUserID = Application.CreatedByUserID;
            }
            else
            {

                this.ApplicationID = -1;
                this.DriverID = -1;
                this.LicenseClassID = -1;
                this.IsuueDate = new DateTime(1900, 1, 1);
                this.ExpirationDate = new DateTime(1900, 1, 1);
                this.Notes = "";
                this.PaidFees = 0;
                this.IsAcitve = false;
                this.IssueReason = "";
                this.CreatedByUserID = -1;
            }
        }
        private clsLicenses(int LicneseID,int ApplicationID, int DriverID, int LicenseClassID, DateTime IsuueDate, DateTime ExpirationDate,
            string Notes, float PaidFees, bool IsAcitve, string IssueReason, int CreatedByUserID)
        {
            this.Mode = enMode.Update;
            this.LicneseID = LicneseID;
            this.ApplicationID = ApplicationID;
            this.DriverID = DriverID;
            this.LicenseClassID = LicenseClassID;
            this.IsuueDate = IsuueDate;
            this.ExpirationDate = ExpirationDate;
            this.Notes = Notes;
            this.PaidFees = PaidFees;
            this.IsAcitve = IsAcitve;
            this.IssueReason = IssueReason;
            this.CreatedByUserID = CreatedByUserID;
        }
        private bool _AddNewLicnese()
        {
            this.LicneseID = clsLicensesData.AddNewLicnese(ApplicationID, DriverID, LicenseClassID, IsuueDate, ExpirationDate, Notes, PaidFees, IsAcitve, IssueReason, CreatedByUserID);
            return (LicneseID != -1);
        }
        public bool Save()
        {
            switch (this.Mode)
            {
                case enMode.AddNew:
                    if (_AddNewLicnese())
                    {
                        this.Mode = enMode.Update;
                        clsApplications Application = clsApplications.Find(this.ApplicationID);
                        Application.ApplicationStatusID = clsApplicationStatuses.Find("Completed").ApplicationStatusID;
                        Application.LastStatusDate = DateTime.Now;
                        Application.Save();
                        return true;
                    }
                    else
                        return false;
            }
            return false;   
        }
        public static clsLicenses FindByLicenseID(int LicneseID)
        {
            int ApplicationID = -1, DriverID = -1, LicneseClassID = -1, CreatedByUserID = -1;
            DateTime IssueDate = new DateTime(1900, 1, 1), ExpirationDate = new DateTime(1900, 1, 1);
            string Notes = "", IssueReason = "";
            float PaidFees = 0;
            bool IsActive = false;

            if (clsLicensesData.GetLicneseInfoByLicneseID(LicneseID, ref ApplicationID, ref DriverID, ref LicneseClassID, ref IssueDate, ref ExpirationDate,
                ref Notes, ref PaidFees, ref IsActive, ref IssueReason, ref CreatedByUserID))

                return new clsLicenses(LicneseID, ApplicationID, DriverID, LicneseClassID, IssueDate, ExpirationDate,
                    Notes, PaidFees, IsActive, IssueReason, CreatedByUserID);
            else
                return null;
        }
        public static clsLicenses FindByApplicationID(int ApplicationID)
        {
            int LicneseID = -1, DriverID = -1, LicneseClassID = -1, CreatedByUserID = -1;
            DateTime IssueDate = new DateTime(1900, 1, 1), ExpirationDate = new DateTime(1900, 1, 1);
            string Notes = "", IssueReason = "";
            float PaidFees = 0;
            bool IsActive = false;

            if (clsLicensesData.GetLicneseInfoByApplicationID(ApplicationID, ref LicneseID,  ref DriverID, ref LicneseClassID, ref IssueDate, ref ExpirationDate,
                ref Notes, ref PaidFees, ref IsActive, ref IssueReason, ref CreatedByUserID))

                return new clsLicenses(LicneseID, ApplicationID, DriverID, LicneseClassID, IssueDate, ExpirationDate,
                    Notes, PaidFees, IsActive, IssueReason, CreatedByUserID);
            else
                return null;
        }
        public static DataTable GetAllLicneses()
        {
            return clsLicensesData.GetAllLicneses();
        }
        public static DataTable GetAllLicnesesByDriverID(int DriverID)
        {
            return clsLicensesData.GetAllLicnesesByDriverID(DriverID);
        }
        public static DataTable GetAllLicnesesByPersonID(int PersonID)
        {
            int DriverID = clsDrivers.FindByPersonID(PersonID).DriverID;

            return clsLicensesData.GetAllLicnesesByDriverID(DriverID);
        }
        public static bool IsPersonHaveAnActiveLicneseWithTheSameLicneseClass(int PersonID, int LicenseClassID)
        {
            return clsLicensesData.IsPersonHaveAnActiveLicneseWithTheSameLicneseClass(PersonID, LicenseClassID);
        }
        public static byte GetNumberOfActiveLicnesesByDriverID(int DriverID)
        {
            return clsLicensesData.GetNumberOfActiveLicnesesByDriverID(DriverID);
        }
        public static bool IsLicenseExist(int LicenseID)
        {
            return clsLicensesData.IsLicenseExist(LicenseID);
        }
        public bool DeActiveLicense()
        {
            return clsLicensesData.DeActiveLicense(this.LicneseID);
        }
    }
}

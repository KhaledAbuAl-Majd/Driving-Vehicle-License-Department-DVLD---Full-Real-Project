using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLDDataAccess;

namespace DVLDBusiness
{
    public class clsInternationalLicenses
    {
        public enum enMode { AddNew, Update }
        public enMode Mode { get; private set; }
        public int InternationalLicenseID { get; private set; }
        public int ILApplicationID { get; private set; }
        public int DriverID { get; private set; }
        public int IssuedUsingLocalLicenseID { get; private set; }
        public DateTime IssueDate { get; private set; }
        public DateTime ExpirationDate { get; private set; }
        public bool IsActive { get; set; }
        public int CreatedByUserID { get; private set; }
        public int IntLicenseValidityLength
        {
            get
            {
                //year
                return 1;
            }
        }

        ~clsInternationalLicenses()
        {
            if (this.InternationalLicenseID == -1)
            {
                clsApplications.Delete(this.ILApplicationID);
            }
        }
        public clsInternationalLicenses(int IssuedUsingLocalLicenseID, int CreatedByUseID)
        {
            this.Mode = enMode.AddNew;
            this.InternationalLicenseID = -1;
            clsLicenses License = clsLicenses.FindByLicenseID(IssuedUsingLocalLicenseID);

            //clsApplications Application = clsApplications.Find(ApplicationID);

            if (License != null)
            {
                clsDrivers Driver = clsDrivers.FindByDriverID(License.DriverID);
                //New International Licnese
                int ApplicationTypeID = 6;

                clsApplications NewApplication = new clsApplications
                {
                    PersonID = Driver.PersonID,
                    ApplicationDate = DateTime.Now,
                    ApplicationTypeID = ApplicationTypeID,
                    ApplicationStatusID = clsApplicationStatuses.Find("Completed").ApplicationStatusID,
                    //LastStatusDate = DateTime.Now,
                    CreatedByUserID = CreatedByUseID
                };

                if (NewApplication.Save())
                {
                    this.ILApplicationID = NewApplication.ApplicationID;
                    this.DriverID = License.DriverID;
                    this.IssuedUsingLocalLicenseID = IssuedUsingLocalLicenseID;
                    this.IssueDate = DateTime.Now;
                    this.ExpirationDate = this.IssueDate.AddYears(IntLicenseValidityLength);
                    this.IsActive = true;
                    this.CreatedByUserID = CreatedByUseID;
                }
            }
            else
            {

                this.ILApplicationID = -1;
                this.DriverID = -1;
                this.IssuedUsingLocalLicenseID = -1;
                this.IssueDate = new DateTime(1900, 1, 1);
                this.ExpirationDate = new DateTime(1900, 1, 1);
                this.IsActive = false;
                this.CreatedByUserID = -1;
            }
        }
        private clsInternationalLicenses(int InternationalLicenseID, int ApplicationID, int DriverID, int IssuedUsingLocalLicenseID, DateTime IssueDate,
            DateTime ExpirationDate, bool IsAcitve,int CreatedByUserID)
        {
            this.Mode = enMode.Update;
            this.InternationalLicenseID = InternationalLicenseID;
            this.ILApplicationID = ApplicationID;
            this.DriverID = DriverID;
            this.IssuedUsingLocalLicenseID = IssuedUsingLocalLicenseID;
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.IsActive = IsAcitve;
            this.CreatedByUserID = CreatedByUserID;
        }
        private bool _AddNewLicnese()
        {
            this.InternationalLicenseID = clsInternationalLicensesData.AddNewInternationalLicense(ILApplicationID, DriverID, IssuedUsingLocalLicenseID, IssueDate, 
                ExpirationDate, IsActive, CreatedByUserID);

            return (InternationalLicenseID != -1);
        }
        public bool Save()
        {
            switch (this.Mode)
            {
                case enMode.AddNew:
                    if (_AddNewLicnese())
                    {
                        this.Mode = enMode.Update;
                        clsApplications Application = clsApplications.Find(this.ILApplicationID);
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
        public static clsInternationalLicenses FindByIntLicenseID(int InternationalLicenseID)
        {
            int ApplicationID = -1, DriverID = -1, IssuedUsingLocalLicenseID = -1, CreatedByUserID = -1;
            DateTime IssueDate = new DateTime(1900, 1, 1), ExpirationDate = new DateTime(1900, 1, 1);
            bool IsActive = false;

            if (clsInternationalLicensesData.GetInternationalLicenseInfoByID(InternationalLicenseID, ref ApplicationID, ref DriverID, ref IssuedUsingLocalLicenseID,
                ref IssueDate, ref ExpirationDate,ref IsActive,ref CreatedByUserID))

                return new clsInternationalLicenses(InternationalLicenseID, ApplicationID, DriverID, IssuedUsingLocalLicenseID, 
                    IssueDate, ExpirationDate,  IsActive, CreatedByUserID);
            else
                return null;
        }
        public static DataTable GetAllLicneses()
        {
            return clsInternationalLicensesData.GetAllInternationalLicenses();
        }
        public static DataTable GetAllLicnesesByDriverID(int DriverID)
        {
            return clsInternationalLicensesData.GetAllInternationalLicensesByDriverID(DriverID);
        }
        public static DataTable GetAllLicnesesByPersonID(int PersonID)
        {
            int DriverID = clsDrivers.FindByPersonID(PersonID).DriverID;

            return clsInternationalLicensesData.GetAllInternationalLicensesByDriverID(DriverID);
        }
        public static int GetInternationalLicenseIfPersonHasActiveOne(int PersonID)
        {
            return clsInternationalLicensesData.GetInternationalLicenseIfPersonHasActiveOne(PersonID);
        }
    }
}

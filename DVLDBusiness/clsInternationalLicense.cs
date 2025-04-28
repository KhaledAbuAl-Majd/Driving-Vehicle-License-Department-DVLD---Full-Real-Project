using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLDDataAccess;

namespace DVLDBusiness
{
    public class clsInternationalLicense : clsApplication
    {
        public enum enMode { AddNew, Update }
        public enMode Mode { get; private set; }

        public clsDriver DriverInfo;
        public int InternationalLicenseID { get; private set; }
        public int DriverID { get; set; }
        public int IssuedUsingLocalLicenseID { get; set; }
        public DateTime IssueDate { get; private set; }
        public DateTime ExpirationDate { get; private set; }
        public bool IsActive { get; set; }
   
        public clsInternationalLicense()
        {
          //here we set the applicaiton type to New International License.

            this.ApplicationTypeID = (int)clsApplication.enApplicationType.NewInternationalLicense;

            this.InternationalLicenseID = -1;
            this.DriverID = -1;
            this.IssuedUsingLocalLicenseID = -1;
            this.IssueDate = DateTime.Now;
            this.ExpirationDate = DateTime.Now;
            this.IsActive = true;

            Mode = enMode.AddNew;
        }

        private clsInternationalLicense(int ApplicationID, int ApplicantPersonID, DateTime ApplicationDate, enApplicationStatus ApplicationStatus,
            DateTime LastStatusDate, float PaidFees, int CreatedByUserID, int InternationalLicenseID, int DriverID, int IssuedUsingLocalLicenseID,
            DateTime IssueDate, DateTime ExpirationDate, bool IsActive)
        {
            //this is for the base clase
            base.ApplicationID = ApplicationID;
            base.ApplicantPersonID = ApplicantPersonID;
            base.ApplicationDate = ApplicationDate;
            base.ApplicationTypeID = (int)clsApplication.enApplicationType.NewInternationalLicense;
            base.ApplicationStatus = ApplicationStatus;
            base.LastStatusDate = LastStatusDate;
            base.PaidFees = PaidFees;
            base.CreatedByUserID = CreatedByUserID;

            this.InternationalLicenseID = InternationalLicenseID;
            this.ApplicationID = ApplicationID;
            this.DriverID = DriverID;
            this.IssuedUsingLocalLicenseID = IssuedUsingLocalLicenseID;
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.IsActive = IsActive;
            this.CreatedByUserID = CreatedByUserID;

            this.DriverInfo = clsDriver.FindByDriverID(this.DriverID);

            Mode = enMode.Update;
        }
        private bool _AddNewInternationalLicense()
        {
            this.InternationalLicenseID = clsInternationalLicenseData.AddNewInternationalLicense(ApplicationID, DriverID, IssuedUsingLocalLicenseID, IssueDate, 
                ExpirationDate, IsActive, CreatedByUserID);

            return (InternationalLicenseID != -1);
        }

        private bool _UpdateInternationalLicense()
        {
            //call DataAccess Layer 

            return clsInternationalLicenseData.UpdateInternationalLicense(
                this.InternationalLicenseID, this.ApplicationID, this.DriverID, this.IssuedUsingLocalLicenseID,
               this.IssueDate, this.ExpirationDate,
               this.IsActive, this.CreatedByUserID);
        }

        public static clsInternationalLicense Find(int InternationalLicenseID)
        {
            int ApplicationID = -1, DriverID = -1, IssuedUsingLocalLicenseID = -1, CreatedByUserID = -1;
            DateTime IssueDate = new DateTime(1900, 1, 1), ExpirationDate = new DateTime(1900, 1, 1);
            bool IsActive = false;

            if (clsInternationalLicenseData.GetInternationalLicenseInfoByID(InternationalLicenseID, ref ApplicationID, ref DriverID, ref IssuedUsingLocalLicenseID,
                ref IssueDate, ref ExpirationDate, ref IsActive, ref CreatedByUserID))
            {

                clsApplication Application = clsApplication.FindBaseApplication(ApplicationID);

                return new clsInternationalLicense(Application.ApplicationID,
                   Application.ApplicantPersonID, Application.ApplicationDate, (enApplicationStatus)Application.ApplicationStatus,
                   Application.LastStatusDate, Application.PaidFees, Application.CreatedByUserID, InternationalLicenseID, DriverID,
                   IssuedUsingLocalLicenseID, IssueDate, ExpirationDate, IsActive);
            }

            else
                return null;
        }

        public static DataTable GetAllInternationalLicenses()
        {
            return clsInternationalLicenseData.GetAllInternationalLicenses();
        }

        public bool Save()
        {
            //Because of inheritance first we call the save method in the base class,
            //it will take care of adding all information to the application table.
            if (!base.Save())
                return false;

            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewInternationalLicense())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateInternationalLicense();

            }

            return false;
        }

        public static int GetActiveInternationalLicenseIDByDriverID(int DriverID)
        {

            return clsInternationalLicenseData.GetActiveInternationalLicenseIDByDriverID(DriverID);

        }

        public static DataTable GetDriverInternationalLicenses(int DriverID)
        {
            return clsInternationalLicenseData.GetDriverInternationalLicenses(DriverID);
        }

        public static int GetActiveInternationalLicenseIDByPersonID(int PersonID)
        {
            return clsInternationalLicenseData.GetInternationalLicenseIfPersonHasActiveOne(PersonID);
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLDDataAccess;

namespace DVLDBusiness
{
    public class clsLocalDrivingLicenseApplication : clsApplication
    {
        public new enum enMode { AddNew, Update }

        public new enMode Mode;
        public int LocalDrivingLicenseApplicationID { get; private set; }
        public int LicenseClassID { get; set; }

        public clsLicenseClass LicenseClassInfo;

        public clsLocalDrivingLicenseApplication()
        {
            this.LocalDrivingLicenseApplicationID = -1;  
            this.LicenseClassID = -1;
            Mode = enMode.AddNew;
        }

        private clsLocalDrivingLicenseApplication(int LocalDrvingApplicationID, int ApplicationID, int LicenseClassID)
        {
            this.LocalDrivingLicenseApplicationID = LocalDrvingApplicationID;
            clsApplication Application = clsApplication.FindBaseApplication(ApplicationID);
            this.ApplicationID = ApplicationID;
            this.ApplicantPersonID = Application.ApplicantPersonID;
            this.PersonInfo = clsPerson.Find(ApplicantPersonID);
            this.ApplicationDate = Application.ApplicationDate;
            this.ApplicationTypeID = Application.ApplicationTypeID;
            this.ApplicationStatus = Application.ApplicationStatus;
            this.LastStatusDate = Application.LastStatusDate;
            this.PaidFees = Application.PaidFees;
            this.CreatedByUserID = Application.CreatedByUserID;
            this.CreatedByUserInfo = clsUser.FindByUserID(CreatedByUserID);
            this.LicenseClassID = LicenseClassID;
            this.LicenseClassInfo = clsLicenseClass.Find(LicenseClassID);
            Mode = enMode.Update;
        }

        bool _AddNewLocalDrivingLicenseApplication()
        {
            this.LocalDrivingLicenseApplicationID = clsLocalDrivingLicenseApplicationData.AddNewLocalDrivingLicenseApplication(ApplicationID,LicenseClassID);

            return (LocalDrivingLicenseApplicationID != -1);
        }

        bool _UpdateLocalDrivingLicenseApplication()
        {
            return clsLocalDrivingLicenseApplicationData.UpdateLocalDrivingLicenseApplication(LocalDrivingLicenseApplicationID, ApplicationID, LicenseClassID);
        }

        public static clsLocalDrivingLicenseApplication FindByLocalDrivingAppLicenseID(int LocalDrvingApplicationID)
        {
            int ApplicationID = -1, LicenseClassID = -1;
         
            if (clsLocalDrivingLicenseApplicationData.GetLocalDrivingLicenseApplicationInfoByID(LocalDrvingApplicationID, ref ApplicationID, ref LicenseClassID))

                return new clsLocalDrivingLicenseApplication(LocalDrvingApplicationID, ApplicationID, LicenseClassID);
            else
                return null;
        }

        public static clsLocalDrivingLicenseApplication FindByApplicationID(int ApplicationID)
        {
            int LocalDrvingApplicationID = -1, LicenseClassID = -1;
          
            if (clsLocalDrivingLicenseApplicationData.GetLocalDrivingLicenseApplicationInfoByApplicationID(ApplicationID, ref LocalDrvingApplicationID, ref LicenseClassID))

                return new clsLocalDrivingLicenseApplication(LocalDrvingApplicationID, ApplicationID,LicenseClassID);
            else
                return null;
        }

        public override bool Save()
        {
            //Because of inheritance first we call the save method in the base class,
            //it will take care of adding all information to the application table.
            base.Mode = (clsApplication.enMode)Mode;
            if (!base.Save())
                return false;


            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewLocalDrivingLicenseApplication())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return _UpdateLocalDrivingLicenseApplication();
            }

            return false;
        }

        public static DataTable GetAllLocalDrivingLicenseApplications()
        {
            return clsLocalDrivingLicenseApplicationData.GetAllLocalDrivingLicenseApplications();
        }

        public override bool Delete()
        {
            bool IsLocalDrivingApplicationDeleted = false;
            bool IsBaseApplicationDeleted = false;

            //First we delete the Local Driving License Application
            IsLocalDrivingApplicationDeleted = clsLocalDrivingLicenseApplicationData.DeleteLocalDrivingLicenseApplication(this.LocalDrivingLicenseApplicationID); ;

            if (!IsLocalDrivingApplicationDeleted)
                return false;

            //Then we delete the base Application
            IsBaseApplicationDeleted = base.Delete();

            return IsBaseApplicationDeleted;
        }

        public bool DoesPassTestType(clsTestType.enTestType TestTypeID)
        {
            return DoesPassTestType(this.LocalDrivingLicenseApplicationID, TestTypeID);
        }

        public bool DoesPassPreviousTest(clsTestType.enTestType CurrentTestType)
        {
            switch (CurrentTestType)
            {
                case clsTestType.enTestType.VisionTest:
                    return true;

                case clsTestType.enTestType.WrittenTest:
                    return this.DoesPassTestType(clsTestType.enTestType.VisionTest);

                case clsTestType.enTestType.StreetTest:
                    return this.DoesPassTestType(clsTestType.enTestType.WrittenTest);

                default:
                    return false;
            }
        }

        public static bool DoesPassTestType(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestTypeID)
        {
           return clsLocalDrivingLicenseApplicationData.DoesPassTestType(LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public bool DoesAttendTestType(clsTestType.enTestType TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationData.DoesAttendTestType(this.LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public byte TotalTrialsPerTest(clsTestType.enTestType TestTypeID)
        {
            return TotalTrialsPerTest(this.LocalDrivingLicenseApplicationID, TestTypeID);
        }

        public static byte TotalTrialsPerTest(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationData.TotalTrialsPerTest(LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public static bool IsThereAnActiveScheduledTest(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationData.IsThereAnActiveScheduledTest(LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public bool IsThereAnActiveScheduledTest(clsTestType.enTestType TestTypeID)
        {
            return IsThereAnActiveScheduledTest(this.LocalDrivingLicenseApplicationID,TestTypeID);
        }

        public clsTest GetLastTestPerTestType(clsTestType.enTestType TestTypeID)
        {
            return clsTest.FindLastTestPerPersonAndLicenseClass(this.ApplicantPersonID, this.LicenseClassID, TestTypeID);
        }

        public byte GetPassedTestCount()
        {
            return GetPassedTestCount(this.LocalDrivingLicenseApplicationID);
        }

        public static byte GetPassedTestCount(int LocalDrivingLicenseApplicationID)
        {
            return clsTest.GetPassedTestCount(LocalDrivingLicenseApplicationID);
        }

        public bool PassedAllTests()
        {
            return PassedAllTests(this.LocalDrivingLicenseApplicationID);
        }

        public static bool PassedAllTests(int LocalDrivingLicenseApplicationID)
        {
            //if total passed test less than 3 it will return false otherwise will return true
            return clsTest.PassedAllTests(LocalDrivingLicenseApplicationID);
        }

        public int IssueLicenseForTheFirtTime(string Notes, int CreatedByUserID)
        {
            int DriverID = -1;

            clsDriver Driver = clsDriver.FindByPersonID(this.ApplicantPersonID);

            if (Driver == null)
            {
                Driver = new clsDriver();
                Driver.PersonID = this.ApplicantPersonID;
                Driver.CreatedByUserID = CreatedByUserID;

                if (Driver.save())
                {
                    DriverID = Driver.DriverID;
                }
                else
                {
                    return -1;
                }
            }
            else
                DriverID = Driver.DriverID;

            //now we diver is there, so we add new licesnse

            clsLicense License = new clsLicense();
            License.ApplicationID = this.ApplicationID;
            License.DriverID = DriverID;
            License.LicenseClassID = this.LicenseClassID;
            License.IssueDate = DateTime.Now;
            License.ExpirationDate = DateTime.Now.AddYears(this.LicenseClassInfo.DefaultValidityLength);
            License.Notes = Notes;
            License.PaidFees = this.LicenseClassInfo.ClassFees;
            License.IsActive = true;
            License.IssueReason = clsLicense.enIssueReason.FirstTime;
            License.CreatedByUserID = CreatedByUserID;

            if (License.Save())
            {
                //now we should set the application status to complete.
                this.SetComplete();
                return License.LicenseID;
            }
            else
                return -1;
        }

        public bool IsLicenseIssuedForPerson()
        {
            return (GetActiveLicenseID() != -1);
        }

        public int GetActiveLicenseID()
        {
            //this will get the license id that belongs to this application
            return clsLicense.GetActiveLicenseIDByPersonID(this.ApplicantPersonID, this.LicenseClassID);
        }

        public int GetLicenseIDByLocalDrivingLicenseApplicationID()
        {
            return clsLicense.GetLicenseIDByLocalDrivingLicenseApplicationID(this.LocalDrivingLicenseApplicationID);
        }

        public bool IsLicenseIssuedForLocalDrivingLicenseApplication()
        {
            return (GetLicenseIDByLocalDrivingLicenseApplicationID() != -1);
        }
    }
}

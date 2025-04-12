using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using DVLDDataAccess;

namespace DVLDBusiness
{
    public class clsTests
    {
        public enum enMode { AddNew, Update }
        public enMode Mode { get; private set; }

        public int TestID { get; private set; }
        public int TestAppointmentID { get; set; }
        public bool TestResult { get; set; }
        public string Notes { get; set; }
        public int CreatedByUserID { get; set; }

        public clsTests(int TestAppointmentID, int CreatedByUserID)
        {
            Mode = enMode.AddNew;
            this.TestID = -1;
            this.TestAppointmentID = TestAppointmentID;
            this.TestResult = false;
            this.Notes = "";
            this.CreatedByUserID = CreatedByUserID;
        }
        bool _AddNewTest()
        {
            this.TestID = clsTestsData.AddNewTest(TestAppointmentID, TestResult, Notes, CreatedByUserID);

            return (this.TestID != -1);
        }
        void _SetPassedTests(int LocalDrivingLicenseApplicationID)
        {    
            clsLocalDrivingApplictions LDLApplication = clsLocalDrivingApplictions.FindByLDLApplicationID(LocalDrivingLicenseApplicationID);
            LDLApplication.PassedTests++;
            LDLApplication.Save();
            _CheckIsBecameADriver(LDLApplication);
        }
        void _CheckIsBecameADriver(clsLocalDrivingApplictions LDLApplication)
        {
            clsApplications Application = clsApplications.Find(LDLApplication.ApplicationID);

            if (clsDrivers.IsDriverExist(Application.PersonID))
                return;

           if(LDLApplication.PassedTests == 3)
            {

                clsDrivers Driver = new clsDrivers();
                Driver.PersonID = Application.PersonID;
                Driver.CreatedByUserID = this.CreatedByUserID;
                Driver.CreatedDate = DateTime.Now;

                Driver.save();
            }
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewTest())
                    {
                        Mode = enMode.Update;
                        clsTestAppointments testAppointment = clsTestAppointments.Find(this.TestAppointmentID);
                        testAppointment.LockTestAppointment();
                        if (TestResult)
                            _SetPassedTests(testAppointment.LocalDrivingLicenseApplicationID);

                        return true;
                    }
                    else
                        return false;
            }

            return false;
        }
        public static bool CheckPersonPassedThisTextBefore(int LocalDrvingApplicationID, int TestTypeID)
        {
            return clsTestsData.CheckPersonPassedThisTextBefore(LocalDrvingApplicationID, TestTypeID);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLDDataAccess;

namespace DVLDBusiness
{
    public class clsTestAppointments
    {
        public enum enMode { AddNew, Update }
        public enum enTestMode { FirstTest, RetakeTest }
        public  enTestMode TestMode { get; private set; }
        public enMode Mode { get; private set; }
        public int TestAppointmentID { get; private set; }
        public int TestTypeID { get; set; }
        public int LocalDrivingLicenseApplicationID { get; set; }
        public DateTime AppointmentDate { get; set; }
        public float PaidFees { get; private set; }
        public int CreatedByUserID { get; set; }
        public bool IsLocked { get; private set; }
        public int RetakeTestApplicationID { get; private set; }
        public short TrialNumber { get; private set; }
        public float RetakeTestFees{ get; private set; }

        public clsTestAppointments(int LocalDrivingLicenseApplicationID, int TestTypeID,int CreatedByUserID)
        {
            this.Mode = enMode.AddNew;
            this.TestAppointmentID = -1;
            this.TestTypeID = TestTypeID;
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.AppointmentDate = DateTime.Today;
            this.PaidFees = clsTestType.Find((clsTestType.enTestType)this.TestTypeID).TestTypeFees;
            this.CreatedByUserID = CreatedByUserID;
            this.IsLocked = false;
            _SetTrailNumber();
            _SetTestMode();
            RetakeTestApplicationID = -1;
        }

        private clsTestAppointments(int TestAppointmentID,int TestTypeID, int LocalDrivingLicenseApplicationID,
            DateTime AppointmentDate, float PaidFees, int CreatedByUserID, bool IsLocked,int RetakeTestApplicationID)
        {
            this.Mode = enMode.Update;
            this.TestAppointmentID = TestAppointmentID;
            this.TestTypeID = TestTypeID;
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.AppointmentDate = AppointmentDate;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;
            this.IsLocked = IsLocked;
            this.RetakeTestApplicationID = RetakeTestApplicationID;
            _SetTrailNumber();
            this.TrialNumber--;
            _SetTestMode();
        }

        void _SetTestMode()
        {
            TestMode = (this.TrialNumber == 0) ? enTestMode.FirstTest : enTestMode.RetakeTest;
        }
        void _SetRetakeTestApplication()
        {
            if(TestMode == enTestMode.FirstTest)
            {
                RetakeTestApplicationID = -1;
            }
            else
            {
                //Retake Test
                int ApplicationTypeID = 7;
                int PersonID = clsApplications.Find(clsLocalDrivingApplictions.FindByLDLApplicationID(this.LocalDrivingLicenseApplicationID).ApplicationID).PersonID;
                clsApplications RetakeTestApplication = new clsApplications
                {
                    ApplicationDate = DateTime.Now,
                     ApplicationTypeID = ApplicationTypeID,
                     //New
                     ApplicationStatusID = 1,
                     CreatedByUserID=this.CreatedByUserID,
                     PersonID = PersonID,         
                };

                RetakeTestApplication.Save();
                RetakeTestApplicationID = RetakeTestApplication.ApplicationID;
            }
        }
        void _SetTrailNumber()
        {
            TrialNumber = clsTestAppointmentsData.GetCountOfTrails_TestAppointmentsNumberForL_D_LApplicationIDAndTestType(LocalDrivingLicenseApplicationID,TestTypeID);     
        }
        bool _AddNewTestAppointment()
        {
            _SetRetakeTestApplication();

            TestAppointmentID = clsTestAppointmentsData.AddNewTestAppointment(TestTypeID, LocalDrivingLicenseApplicationID,
                AppointmentDate, PaidFees, CreatedByUserID, IsLocked, RetakeTestApplicationID);

            return (TestAppointmentID != -1);
        }
        bool _Update()
        {
            return clsTestAppointmentsData.UpdateTestAppointment(TestAppointmentID, AppointmentDate, CreatedByUserID);
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:

                    if (_AddNewTestAppointment())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return _Update();
            }

            return false;
        }
        public static clsTestAppointments Find(int TestAppointmentID)
        {
            int TestTypeID = -1, LocalDrivingLicenseApplicationID = -1, CreatedByUserID = -1;
            DateTime AppointmentDate = new DateTime(1900, 1, 1);
            float TotalPaidFees = -1;
            bool IsLocked = false;
            int RetakeTestApplicationID = -1;

            if (clsTestAppointmentsData.GetTestAppointmentInfoByTestAppointmentID(TestAppointmentID, ref TestTypeID, ref LocalDrivingLicenseApplicationID
                , ref AppointmentDate, ref TotalPaidFees, ref CreatedByUserID, ref IsLocked, ref RetakeTestApplicationID))

                return new clsTestAppointments(TestAppointmentID, TestTypeID, LocalDrivingLicenseApplicationID, AppointmentDate, TotalPaidFees, CreatedByUserID,
                    IsLocked, RetakeTestApplicationID);
            else
                return null;
        }
        public bool LockTestAppointment()
        {
            if (TestAppointmentID != -1)
            {
                this.IsLocked = true;
                return clsTestAppointmentsData.LockTestAppointment(this.TestAppointmentID);
            }

            return false;
        }
        public static DataTable GetAllTestAppointments()
        {
            return clsTestAppointmentsData.GetAllTestAppointments();
        }
        public static DataTable GetAllTestAppointmentsForSelectdL_D_LApplicationIDAndTestType(int LocalDrivingLicenseApplicationID,int TestTypeID)
        {
            return clsTestAppointmentsData.GetAllTestAppointmentsForSelectdL_D_LApplicationIDAndTestType(LocalDrivingLicenseApplicationID, TestTypeID);
        }

        //Un Locked
        public static bool CheckHasActiveTestAppointmentForL_D_LApplicationIDAndTestType(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            return clsTestAppointmentsData.CheckHasActiveTestAppointmentForL_D_LApplicationIDAndTestType(LocalDrivingLicenseApplicationID, TestTypeID);
        }
       
    }
}

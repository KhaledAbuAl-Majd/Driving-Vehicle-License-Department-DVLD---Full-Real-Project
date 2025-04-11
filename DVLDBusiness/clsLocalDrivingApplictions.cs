using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLDDataAccess;

namespace DVLDBusiness
{
    public class clsLocalDrivingApplictions
    {
        public enum enMode { AddNew, Update }

        public enMode Mode;
        public int LocalDrvingApplicationID { get; private set; }
        public int ApplicationID { get;set; }
        public int LicenseClassID { get; set; }
        public byte PassedTests { get; set; }

        public clsLocalDrivingApplictions()
        {
            Mode = enMode.AddNew;
            this.LocalDrvingApplicationID = -1;
            this.ApplicationID = -1;
            this.LicenseClassID = -1;
            this.PassedTests = 0;
        }

        private clsLocalDrivingApplictions(int LocalDrvingApplicationID, int ApplicationID, int LicenseClassID,byte PassedTests)
        {
            Mode = enMode.Update;
            this.LocalDrvingApplicationID = LocalDrvingApplicationID;
            this.ApplicationID = ApplicationID;
            this.LicenseClassID = LicenseClassID;
            this.PassedTests = PassedTests;
        }

        bool _AddNewApplication()
        {
            PassedTests = 0;
            this.LocalDrvingApplicationID = clsLocalDrivingApplictionsData.AddNewLocalDrivingAppliction(ApplicationID,LicenseClassID, PassedTests);

            return (LocalDrvingApplicationID != -1);
        }
        bool _UpdateApplication()
        {
            return clsLocalDrivingApplictionsData.UpdateAplication(LocalDrvingApplicationID,ApplicationID,LicenseClassID,PassedTests);
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewApplication())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return _UpdateApplication();
            }

            return false;
        }
        public static clsLocalDrivingApplictions Find(int LocalDrvingApplicationID)
        {
            int ApplicationID = -1, LicenseClassID = -1;
            byte PassedTests = 0;

            if (clsLocalDrivingApplictionsData.GetLocalDrivingApplicationsInfoByID(LocalDrvingApplicationID,ref ApplicationID,ref LicenseClassID,ref PassedTests))
                return new clsLocalDrivingApplictions(LocalDrvingApplicationID, ApplicationID,LicenseClassID, PassedTests);
            else
                return null;
        }
        public static bool Delete(int LocalDrvingApplicationID)
        {
            return clsLocalDrivingApplictionsData.DeleteLocalDrivingApplications(LocalDrvingApplicationID);
        }
        public static DataTable GetAllApplications()
        {
            return clsLocalDrivingApplictionsData.GetAllLocalDrivingApplications();
        }
        public int GetApplicationIDIfPersonHasActiveApplicationFromThisClass(int PersonID)
        {
            return clsLocalDrivingApplictionsData.GetApplicationIDIfPersonHasActiveApplicationFromThisClass(PersonID, LicenseClassID);
        }
        //To Show All Data For FK For All LDLApplications
        public static DataTable GetAllLocalDrivingApplicationsWithINNERJOIN()
        {
            return clsLocalDrivingApplictionsData.GetAllLocalDrivingApplicationsWithINNERJOIN();
        }

        //To Show All Data For FK For a selected LDLApplication
        public static DataTable GetLocalDrivingApplicationInfoWithINNERJOIN(int LocalDrvingApplicationID)
        {
            return clsLocalDrivingApplictionsData.GetLocalDrivingApplicationInfoWithINNERJOIN(LocalDrvingApplicationID);
        }

    }
}

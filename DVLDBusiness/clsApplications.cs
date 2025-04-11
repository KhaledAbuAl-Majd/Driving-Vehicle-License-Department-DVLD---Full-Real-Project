using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DVLDDataAccess;

namespace DVLDBusiness
{
    public class clsApplications
    {
        public enum enMode { AddNew, Update }

        public enMode Mode;
        public int ApplicationID { get; private set; }
        public int PersonID { get; set; }
        public DateTime ApplicationDate { get; set; }
        public int ApplicationTypeID { get; set; }
        public int ApplicationStatusID { get; set; }
        public DateTime LastStatusDate { get; set; }
        public float PaidFees { get; set; }
        public int CreatedByUserID { get; set; }

        public clsApplications()
        {
            Mode = enMode.AddNew;
            this.ApplicationID = -1;
            this.PersonID = -1;
            this.ApplicationDate = new DateTime(1900,1,1);
            this.ApplicationTypeID = -1;
            this.ApplicationStatusID = -1;
            this.LastStatusDate = new DateTime(1900, 1, 1);
            this.PaidFees = 0;
            this.CreatedByUserID = -1;
        }

        private clsApplications(int ApplicationID,int PersonID,DateTime ApplicationDate,int ApplicationTypeID 
            ,int ApplicationStatusID,  DateTime LastStatusDate,float PaidFees,int CreatedByUserID)
        {
            Mode = enMode.Update;
            this.ApplicationID = ApplicationID;
            this.PersonID = PersonID;
            this.ApplicationDate = ApplicationDate;
            this.ApplicationTypeID = ApplicationTypeID;
            this.ApplicationStatusID = ApplicationStatusID;
            this.LastStatusDate = LastStatusDate;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;
        }

        bool _AddNewApplication()
        {
            this.ApplicationStatusID = clsApplicationStatuses.Find("New").ApplicationStatusID;
            this.LastStatusDate = this.ApplicationDate;
            this. ApplicationID = clsApplicationsData.AddNewApplication(PersonID, ApplicationDate, ApplicationTypeID,
                ApplicationStatusID, LastStatusDate, PaidFees, CreatedByUserID);

            return (ApplicationID != -1);
        }
        bool _UpdateApplication()
        {
            return clsApplicationsData.UpdateAplication(ApplicationID, PersonID, ApplicationDate, ApplicationTypeID,
                ApplicationStatusID, LastStatusDate, PaidFees, CreatedByUserID);
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
        public static clsApplications Find(int ApplicationID)
        {
            int PersonID = -1, ApplicationTypeID = -1, CreatedByUseID = -1, ApplicationStatusID = -1;
            DateTime ApplicationDate = new DateTime(1900, 1, 1), LastStatusDate = new DateTime(1900, 1, 1);
            float PaidFees = 0;

            if (clsApplicationsData.GetApplicationInfoByApplicationID(ApplicationID, ref PersonID, ref ApplicationDate
                , ref ApplicationTypeID,  ref ApplicationStatusID, ref LastStatusDate, ref PaidFees, ref CreatedByUseID))
                return new clsApplications(ApplicationID, PersonID, ApplicationDate, ApplicationTypeID,
                    ApplicationStatusID, LastStatusDate, PaidFees, CreatedByUseID);
            else
                return null;
        }
        public static bool Delete(int ApplicationID)
        {
            return clsApplicationsData.DeleteApplication(ApplicationID);
        }
        public static DataTable GetAllApplications()
        {
            return clsApplicationsData.GetAllApplications();
        }

    }
}

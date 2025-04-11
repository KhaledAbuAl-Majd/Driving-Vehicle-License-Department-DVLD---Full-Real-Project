using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using DVLDDataAccess;

namespace DVLDBusiness
{
    public class clsApplicationStatuses
    {
        public int ApplicationStatusID { get; private set; }
        public string ApplicationStatus { get; private set; }

        clsApplicationStatuses(int ApplicationStatusID, string ApplicationStatus)
        {
            this.ApplicationStatusID = ApplicationStatusID;
            this.ApplicationStatus = ApplicationStatus;
        }

        public static clsApplicationStatuses Find(int ApplicationStatusID)
        {
            string ApplicationStatus = "";

            if (clsApplicationStatusesData.GetApplicationStatusByID(ApplicationStatusID, ref ApplicationStatus))
                return new clsApplicationStatuses(ApplicationStatusID, ApplicationStatus);
            else
                return null;
        }
        public static clsApplicationStatuses Find(string ApplicationStatus)
        {
            int ApplicationStatusID = -1;

            if (clsApplicationStatusesData.GetApplicationStatusByName(ApplicationStatus, ref ApplicationStatusID))
                return new clsApplicationStatuses(ApplicationStatusID, ApplicationStatus);
            else
                return null;
        }
        public static DataTable GetAllApplicationStatuses()
        {
            return clsApplicationStatusesData.GetAllApplicationStatuses();
        }

    }
}

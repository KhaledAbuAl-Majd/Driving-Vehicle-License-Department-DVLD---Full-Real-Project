using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLDDataAccess;

namespace DVLDBusiness
{
    public class clsApplicationType
    {
        public enum enMode { Update};
        public enMode Mode { get; private set; }
        public int ApplicationTypeID { get;private set; }
        public string ApplicationTypeTitle { get; set; }
        public float ApplicationFees { get; set; }

        private clsApplicationType(int ApplicationTypeID, string ApplicationTypeTitle, float ApplicationFees)
        {
            this.ApplicationTypeID = ApplicationTypeID;
            this.ApplicationTypeTitle = ApplicationTypeTitle;
            this.ApplicationFees = ApplicationFees;
            Mode = enMode.Update;
        }

        private bool _UpdateApplicationType()
        {
            return clsApplicationTypeData.UpdateApplicationType(ApplicationTypeID, ApplicationTypeTitle, ApplicationFees);
        }

        public static clsApplicationType Find(int ApplicationTypeID)
        {
            string ApplicationTypeTitle = "";
            float ApplicationFees = 0;

            if (clsApplicationTypeData.GetApplicationTypeInfoByID(ApplicationTypeID, ref ApplicationTypeTitle, ref ApplicationFees))
                return new clsApplicationType(ApplicationTypeID, ApplicationTypeTitle, ApplicationFees);
            else
                return null;
        }

        public static DataTable GetAllApplicationTypes()
        {
            return clsApplicationTypeData.GetAllApplicationTypes();
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.Update:
                    return _UpdateApplicationType();
            }

            return false;
        }
    }
}

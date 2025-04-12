using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLDDataAccess;

namespace DVLDBusiness
{
    public class clsLicneseClasses
    {
        public int LicenseClassID { get; private set; }
        public string ClassName { get; private set; }
        public string ClassDiscription { get; private set; }
        public byte MinumAllowedAge { get; private set; }
        public byte DefaultValidityLength { get; private set; }
        public float ClassFees { get; private set; }

        private clsLicneseClasses(int LicenseClassID,string ClassName,string ClassDiscription,byte MinumAllowedAge,byte DefaultValidityLength,float ClassFees)
        {
            this.LicenseClassID = LicenseClassID;
            this.ClassName = ClassName;
            this.ClassDiscription = ClassDiscription;
            this.MinumAllowedAge = MinumAllowedAge;
            this.DefaultValidityLength = DefaultValidityLength;
            this.ClassFees = ClassFees;
        }

        public static  clsLicneseClasses Find(int LicenseClassID)
        {
            string ClassName = "", ClassDiscription = "";
            byte MinumAllowedAge = 0, DefaultValidityLength = 0;
            float ClassFees = 0;

            if (clsLicneseClassesData.GetLicenseClassByID(LicenseClassID,ref ClassName, ref ClassDiscription,
                ref MinumAllowedAge, ref DefaultValidityLength, ref ClassFees))
                return new clsLicneseClasses(LicenseClassID, ClassName, ClassDiscription, MinumAllowedAge, DefaultValidityLength, ClassFees);
            else
                return null;
        }
        public static  clsLicneseClasses Find(string ClassName)
        {
            int LicenseClassID = -1;
            string ClassDiscription = "";
            byte MinumAllowedAge = 0, DefaultValidityLength = 0;
            float ClassFees = 0;

            if (clsLicneseClassesData.GetLicenseClassByClassName(ClassName, ref LicenseClassID, ref ClassDiscription,
                ref MinumAllowedAge, ref DefaultValidityLength, ref ClassFees))
                return new clsLicneseClasses(LicenseClassID, ClassName, ClassDiscription, MinumAllowedAge, DefaultValidityLength, ClassFees);
            else
                return null;
        }
        public static DataTable GetAllLicneseClasses()
        {
            return clsLicneseClassesData.GetAllLicenseClasses();
        }


    }
}

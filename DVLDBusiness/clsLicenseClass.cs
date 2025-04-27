using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLDDataAccess;

namespace DVLDBusiness
{
    public class clsLicenseClass
    {
        public int LicenseClassID { get; private set; }
        public string ClassName { get; private set; }
        public string ClassDiscription { get; private set; }
        public byte MinumAllowedAge { get; private set; }
        public byte DefaultValidityLength { get; private set; }
        public float ClassFees { get; private set; }

        private clsLicenseClass(int LicenseClassID,string ClassName,string ClassDiscription,byte MinumAllowedAge,byte DefaultValidityLength,float ClassFees)
        {
            this.LicenseClassID = LicenseClassID;
            this.ClassName = ClassName;
            this.ClassDiscription = ClassDiscription;
            this.MinumAllowedAge = MinumAllowedAge;
            this.DefaultValidityLength = DefaultValidityLength;
            this.ClassFees = ClassFees;
        }

        public static  clsLicenseClass Find(int LicenseClassID)
        {
            string ClassName = "", ClassDiscription = "";
            byte MinumAllowedAge = 0, DefaultValidityLength = 0;
            float ClassFees = 0;

            if (clsLicenseClasseData.GetLicenseClassInfoByID(LicenseClassID,ref ClassName, ref ClassDiscription,
                ref MinumAllowedAge, ref DefaultValidityLength, ref ClassFees))
                return new clsLicenseClass(LicenseClassID, ClassName, ClassDiscription, MinumAllowedAge, DefaultValidityLength, ClassFees);
            else
                return null;
        }
        public static  clsLicenseClass Find(string ClassName)
        {
            int LicenseClassID = -1;
            string ClassDiscription = "";
            byte MinumAllowedAge = 0, DefaultValidityLength = 0;
            float ClassFees = 0;

            if (clsLicenseClasseData.GetLicenseClassInfoByClassName(ClassName, ref LicenseClassID, ref ClassDiscription,
                ref MinumAllowedAge, ref DefaultValidityLength, ref ClassFees))
                return new clsLicenseClass(LicenseClassID, ClassName, ClassDiscription, MinumAllowedAge, DefaultValidityLength, ClassFees);
            else
                return null;
        }
        public static DataTable GetAllLicneseClasses()
        {
            return clsLicenseClasseData.GetAllLicenseClasses();
        }


    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLDDataAccess;

namespace DVLDBusiness
{
    public class clsTestType
    {
        public enum enMode { Update };
        public enMode Mode { get; private set; }
        public enum enTestType { VisionTest = 1, WrittenTest = 2, StreetTest = 3 }
        public enTestType TestTypeID { get; private set; }
        public string TestTypeTitle { get; set; }
        public string TestTypeDescription { get; set; }
        public float TestTypeFees { get; set; }

        private clsTestType(enTestType TestTypeID, string TestTypeTitle, string TestTypeDescription, float TestFees)
        {
            this.TestTypeID = TestTypeID;
            this.TestTypeTitle = TestTypeTitle;
            this.TestTypeDescription = TestTypeDescription;
            this.TestTypeFees = TestFees;
            Mode = enMode.Update;
        }

        private bool _UpdateTestType()
        {
            return clsTestTypeData.UpdateTestType((int)TestTypeID, TestTypeTitle, TestTypeDescription, TestTypeFees);
        }
        public static clsTestType Find(enTestType TestTypeID)
        {
            string TestTypeTitle = "", TestTypeDescription = "";
            float TestFees = 0;

            if (clsTestTypeData.GetTestTypeInfoByID((int)TestTypeID, ref TestTypeTitle, ref TestTypeDescription, ref TestFees))
                return new clsTestType(TestTypeID, TestTypeTitle, TestTypeDescription, TestFees);
            else
                return null;
        }
        public static DataTable GetAllTestTypes()
        {
            return clsTestTypeData.GetAllTestTypes();
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.Update:
                    return _UpdateTestType();
            }

            return false;
        }
    }
}

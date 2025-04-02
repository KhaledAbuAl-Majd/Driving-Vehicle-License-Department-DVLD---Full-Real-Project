using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLDDataAccess;

namespace DVLDBusiness
{
    public class clsTestTypes
    {
        public enum enMode { Update };
        public enMode Mode { get; private set; }
        public int TestTypeID { get; private set; }
        public string TestTypeTitle { get; set; }
        public string TestTypeDescription { get; set; }

        public float TestFees { get; set; }

        private clsTestTypes(int TestTypeID, string TestTypeTitle, string TestTypeDescription, float TestFees)
        {
            this.TestTypeID = TestTypeID;
            this.TestTypeTitle = TestTypeTitle;
            this.TestTypeDescription = TestTypeDescription;
            this.TestFees = TestFees;
            Mode = enMode.Update;
        }

        private bool _Update()
        {
            return clsTestTypesData.UpdateTestType(TestTypeID, TestTypeTitle, TestTypeDescription, TestFees);
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.Update:
                    return _Update();
            }

            return false;
        }
        public static clsTestTypes FindTestType(int TestTypeID)
        {
            string TestTypeTitle = "", TestTypeDescription = "";
            float TestFees = 0;

            if (clsTestTypesData.GetTestTypeByID(TestTypeID, ref TestTypeTitle, ref TestTypeDescription, ref TestFees))
                return new clsTestTypes(TestTypeID, TestTypeTitle, TestTypeDescription, TestFees);
            else
                return null;
        }

        public static DataTable GetAllTestTypes()
        {
            return clsTestTypesData.GetAllTestTypes();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DVLDDataAccess;

namespace DVLDBusiness
{
    public class clsCountries
    {
        public int CountryID { get; }
        public string CountryName { get; set; }

        private clsCountries(int CountryID,string CountryName)
        {
            this.CountryID = CountryID;
            this.CountryName = CountryName;
        }
        public static DataTable GetAllCountires()
        {
            return clsCountryData.GetAllCountries();
        }
        public static clsCountries Find(int CountryID)
        {
            string CountryName = "";
            if (clsCountryData.GetCountryInfoByID(CountryID, ref CountryName))
            {
                return new clsCountries(CountryID, CountryName);
            }
            else
                return null;
        }
        public static clsCountries Find(string CountryName)
        {
            int CountryID = -1;

            if (clsCountryData.GetCountryInfoByName(ref CountryID, CountryName))
            {
                
                return new clsCountries(CountryID, CountryName);
            }
            else
                return null;
        }
    }
}

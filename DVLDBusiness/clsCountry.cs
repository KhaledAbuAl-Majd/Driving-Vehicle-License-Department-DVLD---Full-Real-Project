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
    public class clsCountry
    {
        public int CountryID { get; }
        public string CountryName { get; set; }

        private clsCountry(int CountryID,string CountryName)
        {
            this.CountryID = CountryID;
            this.CountryName = CountryName;
        }
        public static clsCountry Find(int CountryID)
        {
            string CountryName = "";
            if (clsCountryData.GetCountryInfoByID(CountryID, ref CountryName))
            {
                return new clsCountry(CountryID, CountryName);
            }
            else
                return null;
        }
        public static clsCountry Find(string CountryName)
        {
            int CountryID = -1;

            if (clsCountryData.GetCountryInfoByName(ref CountryID, CountryName))
            {
                
                return new clsCountry(CountryID, CountryName);
            }
            else
                return null;
        }
        public static DataTable GetAllCountires()
        {
            return clsCountryData.GetAllCountries();
        }
    }
}

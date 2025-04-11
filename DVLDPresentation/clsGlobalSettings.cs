using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLDBusiness;

namespace DVLDPresentation
{
    public static class clsGlobalSettings
    {
        public static clsUsers LoggedInUser { get; set; }

        public static string PathOfRemeberMeFile
        {
            get
            {
                return @"E:\Visual Studio\source\Projects\Course19\DVLDProject\DVLDPresentation\bin\Debug\RememberMeFile\RemeberMeText.txt";
            }
        }
        public static void FeatureIsNotImplemented()
        {
            MessageBox.Show("This Feature Is Not Implemented Yet!", "Not Ready!",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}

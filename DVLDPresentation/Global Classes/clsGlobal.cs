using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLDBusiness;

namespace DVLDPresentation
{
    public class clsGlobal
    {
        public static clsUser CurrentUser { get; set; }
        public static string PathOfRemeberMeFile
        {
            get
            {
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "RememberMeFile", "RemeberMeText.txt");
            }
        }

        public static bool RememberUsernameAndPassword(string Username, string Password)
        {
            try
            {
                //When Remember Me is UnChecked
                if (Username == "" && File.Exists(PathOfRemeberMeFile))
                {
                    File.Delete(PathOfRemeberMeFile);
                    return true;
                }

                //If Remember Me Checked

                string DataToSave = Username + "#//#" + Password;

                //if the file not exist create one!)
                using (StreamWriter writer = new StreamWriter(PathOfRemeberMeFile))
                {
                    //Write the data to the file
                    writer.WriteLine(DataToSave);
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
                return false;
            }
        }

        public static bool GetStoredCredential(ref string Username, ref string Password)
        {
            try
            {
                if (File.Exists(PathOfRemeberMeFile))
                {

                    using (StreamReader reader = new StreamReader(PathOfRemeberMeFile))
                    {
                        string line = "";

                        while ((line = reader.ReadLine()) != null)
                        {
                            string[] result = line.Split(new string[] { "#//#" }, StringSplitOptions.None);

                            Username = result[0];
                            Password = result[1];
                        }

                        return true;
                    }
                }
                else
                {
                    //File is not exist
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
                return false;
            }
        }

       
    }
}

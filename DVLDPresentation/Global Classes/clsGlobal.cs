using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLDBusiness;
using DVLDConstant;
using Microsoft.Win32;

namespace DVLDPresentation
{
    public static class clsGlobal
    {
        private static class clsRegisteryConstants
        {
            public static string SubKeyName = @"Software\DVLD";

            public static string UserNameValueName = "UserName";

            public static string PasswordValueName = "Password";
        }
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
                using (RegistryKey baseKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64))
                {
                    using (RegistryKey key = baseKey.CreateSubKey(clsRegisteryConstants.SubKeyName, true))
                    {
                        if (key != null)
                        {
                            if (Username == null || Password == null)
                            {
                                key.DeleteValue(clsRegisteryConstants.UserNameValueName);
                                key.DeleteValue(clsRegisteryConstants.PasswordValueName);
                            }
                            else
                            {
                                key.SetValue(clsRegisteryConstants.UserNameValueName, Username, RegistryValueKind.String);

                                key.SetValue(clsRegisteryConstants.PasswordValueName, Password, RegistryValueKind.String);
                            }
                        }
                        else
                            return false;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");

                clsLogger.LogAtEventLog(ex.Message);
                return false;
            }
        }

        public static bool GetStoredCredential(ref string Username, ref string Password)
        {
            try
            {
                using(RegistryKey baseKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64))
                {
                    using(RegistryKey key = baseKey.OpenSubKey(clsRegisteryConstants.SubKeyName, true))
                    {
                        if (key != null)
                        {
                            Username = key.GetValue(clsRegisteryConstants.UserNameValueName) as string;

                            Password = key.GetValue(clsRegisteryConstants.PasswordValueName) as string;

                            if (Username == null || Password == null)
                                return false;
                        }
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
                clsLogger.LogAtEventLog(ex.Message);
                return false;
            }
        }
    }
}

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

            public static string EnctyptionKey = "A@*a13!~k@+1k=&5";
        }
        public static clsUser CurrentUser { get; set; }

        public static string PathOfRemeberMeFile
        {
            get
            {
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "RememberMeFile", "RemeberMeText.txt");
            }
        }

        /// <summary>
        /// Store UserName & Password After Encrypt it at Windows Registery 
        /// If UserName Or Password = null, it will remove the old value from registery
        /// </summary>
        /// <returns>Success Value</returns>
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
                                string EncryptedUserName = clsEncryption.clsSymmetricEncryption.Encrypt(Username, clsRegisteryConstants.EnctyptionKey);
                                string EncryptedPassword = clsEncryption.clsSymmetricEncryption.Encrypt(Password, clsRegisteryConstants.EnctyptionKey);

                                key.SetValue(clsRegisteryConstants.UserNameValueName, EncryptedUserName, RegistryValueKind.String);

                                key.SetValue(clsRegisteryConstants.PasswordValueName, EncryptedPassword, RegistryValueKind.String);
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
                           string EncryptedUsername = key.GetValue(clsRegisteryConstants.UserNameValueName) as string;

                           string EncryptedPassword = key.GetValue(clsRegisteryConstants.PasswordValueName) as string;

                            if (EncryptedUsername == null || EncryptedPassword == null)
                                return false;

                            Username = clsEncryption.clsSymmetricEncryption.Decrypt(EncryptedUsername, clsRegisteryConstants.EnctyptionKey);
                            Password = clsEncryption.clsSymmetricEncryption.Decrypt(EncryptedPassword, clsRegisteryConstants.EnctyptionKey);
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

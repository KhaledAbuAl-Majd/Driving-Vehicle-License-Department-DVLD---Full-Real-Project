using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLDConstant;

namespace DVLDPresentation.Global_Classes
{
    public class clsUtil
    {
        public static string GenerateGUID()
        {
            Guid newGuid = Guid.NewGuid();

            return newGuid.ToString();
        }
        public static bool CreateFolderIfDoesNotExist(string FolderPath)
        {
            if (!Directory.Exists(FolderPath))
            {
                try
                {
                    Directory.CreateDirectory(FolderPath);
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error creating folder: " + ex.Message);
                    clsLogger.LogAtEventLog(ex.Message);
                    return false;
                }
            }

            return true;
        }
        public static string ReplaceFileNameWithGUID(string sourceFile)
        {
            //New Name + Extension
            string FileName = sourceFile;
            FileInfo fi = new FileInfo(FileName);
            string extn = fi.Extension;
            return GenerateGUID() + extn;
        }
        public static bool CopyImageToProjectImagesFolder(ref string SourceFile)
        {
            string DestinationFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DVLD-People-Image");

            if (!CreateFolderIfDoesNotExist(DestinationFolder))
            {
                return false;   
            }

            string destinationFile = Path.Combine(DestinationFolder, ReplaceFileNameWithGUID(SourceFile));

            try
            {
                File.Copy(SourceFile, destinationFile, true);
            }
            catch (IOException iox)
            {
                MessageBox.Show(iox.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsLogger.LogAtEventLog(iox.Message);
                return false;
            }

            SourceFile = destinationFile;
            return true;
        }
        public static void FeatureIsNotImplemented()
        {
            MessageBox.Show("This Feature Is Not Implemented Yet!", "Not Ready!",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}

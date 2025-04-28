using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLDBusiness;
using DVLDPresentation.Properties;

namespace DVLDPresentation.Applications.Manage_Applications.LocalDrivingLicenseApplications
{
    public partial class frmShowLicenseInfo : Form
    {
        public frmShowLicenseInfo(int LicenseID)
        {
            InitializeComponent();
            ctrlDriverLicenseInfo1.LicenseID = LicenseID;
            
        }

        private void frmLicneseInfo_Load(object sender, EventArgs e)
        {
            
        }

        private void gbtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

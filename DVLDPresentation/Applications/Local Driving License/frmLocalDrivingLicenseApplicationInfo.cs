using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLDPresentation.Applications.Manage_Applications.LocalDrivingLicenseApplications
{
    public partial class frmLocalDrivingLicenseApplicationInfo : Form
    {
        int LocalDrivingLicenseApplicationID;
        public frmLocalDrivingLicenseApplicationInfo(int LDLApplicationID)
        {
            InitializeComponent();
            LocalDrivingLicenseApplicationID = LDLApplicationID;
        }

        private void gbtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmLocalDrivingLicenseApplicationInfo_Load(object sender, EventArgs e)
        {
            ctrlDLApplicationInfo1.LoadApplicationInfoByLocalDrivingAppID(LocalDrivingLicenseApplicationID);
        }
    }
}

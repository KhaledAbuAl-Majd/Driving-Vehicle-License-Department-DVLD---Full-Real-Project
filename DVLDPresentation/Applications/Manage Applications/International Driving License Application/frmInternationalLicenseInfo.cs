using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLDPresentation.Applications.Manage_Applications.International_Driving_License_Application
{
    public partial class frmInternationalLicenseInfo : Form
    {
        public frmInternationalLicenseInfo(int IntLicenseID)
        {
            InitializeComponent();
            ctrlInternationalDriverLicensInfo1.IntLicenseID = IntLicenseID;
        }

        private void gbtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

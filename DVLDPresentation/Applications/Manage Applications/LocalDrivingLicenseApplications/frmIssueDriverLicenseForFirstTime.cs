using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLDBusiness;

namespace DVLDPresentation.Applications.Manage_Applications.LocalDrivingLicenseApplications
{
    public partial class frmIssueDriverLicenseForFirstTime : Form
    {
        public event Action OnClose;
        bool _IsSave = false;
        int _LDLApplicationID;
        public frmIssueDriverLicenseForFirstTime(int LDLApplicationID)
        {
            InitializeComponent();
            ctrlDLApplicationInfo1.LDLApplicationID = LDLApplicationID;
            this._LDLApplicationID = LDLApplicationID;
        }

        void _Issue()
        {
            clsLicenses License = new clsLicenses(_LDLApplicationID, clsGlobalSettings.CurrentUser.UserID, "First Time");
            License.Notes = gtxtNotes.Text;

            if (License.Save())
            {
                _IsSave = true;
                MessageBox.Show($"License Issued Successfully with License ID = {License.LicneseID}", "Succeeded", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _Close();
            }
            else
            {
                _IsSave = false;
                MessageBox.Show("Failed To Issue Licnese", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        void _Close()
        {
            if (_IsSave)
                if (OnClose != null)
                    OnClose();

            this.Close();
        }
        private void frmIssueDriverLicenseForFirstTime_Load(object sender, EventArgs e)
        {

        }

        private void gbtnIssue_Click(object sender, EventArgs e)
        {
            _Issue();
        }

        private void gbtnClose_Click(object sender, EventArgs e)
        {
            _Close();
        }
    }
}

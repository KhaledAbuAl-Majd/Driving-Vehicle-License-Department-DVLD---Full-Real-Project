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
using static System.Net.Mime.MediaTypeNames;

namespace DVLDPresentation.Applications.Driving_License_Services.New_Driving_License
{
    public partial class frmNewLocalLicenseApplication : Form
    {
        public event Action OnClose;      
        bool _IsSave;
        int _CurrentLDLApplicationClassID;
        clsApplicationTypes _ApplicationType;
        clsApplications _Application;
        clsLocalDrivingApplictions _LDLApplication;
        public frmNewLocalLicenseApplication(int LDLApplicationID)
        {
            InitializeComponent();
            int _ApplicationTypeID = 1;
            //ctrlPersonCardWithFilter1.IsToAddNewUser = false;
            _ApplicationType = clsApplicationTypes.FindApplicationType(_ApplicationTypeID);
            if(LDLApplicationID != -1)
            {
                _LDLApplication = clsLocalDrivingApplictions.FindByLDLApplicationID(LDLApplicationID);
                _Application = clsApplications.Find(_LDLApplication.ApplicationID);
            }
            else
            {
                _LDLApplication = new clsLocalDrivingApplictions();
                _Application = new clsApplications();
            }
            _CurrentLDLApplicationClassID = _LDLApplication.LicenseClassID;
        }

        void _FillLicenseClassInComboBox()
        {
            DataTable dtLicneseClasses = clsLicneseClasses.GetAllLicneseClasses();

            gcbLicenseClass.DataSource = dtLicneseClasses;
            gcbLicenseClass.DisplayMember = "ClassName";
            gcbLicenseClass.ValueMember = "LicenseClassID";  
        }
        void _FillDataInLablesAddNewMode()
        {
            _FillLicenseClassInComboBox();
            gcbLicenseClass.SelectedIndex = 0;
            lblApplicationDate.Text = DateTime.Now.ToShortDateString();
            lblApplicationFees.Text = _ApplicationType.ApplicationFees.ToString();
            lblCreatedBy.Text = clsGlobalSettings.LoggedInUser.UserName;
        }
        bool _CheckFromPerson()
        {
            return (ctrlPersonCardWithFilter1.PersonID != -1);
        }
        bool _FillDataInNewApplicationObjectAndSaveResult()
        {
            _Application.PersonID = ctrlPersonCardWithFilter1.PersonID;
            _Application.ApplicationDate = DateTime.Now;
            _Application.ApplicationTypeID = _ApplicationType.ApplicationTypeID;
            //NewApplication.ApplicationStatus = "New";
            _Application.PaidFees = _ApplicationType.ApplicationFees;
            _Application.CreatedByUserID = clsGlobalSettings.LoggedInUser.UserID;

            if (_Application.Save())
            {
                return true;
            }
            else
            {
                _IsSave = false;
                MessageBox.Show("Failed To Add Application Pleas Enter a Correct Data!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        bool _CheckHasActiveApplicationFromThisClass()
        {
            if (_CurrentLDLApplicationClassID == _LDLApplication.LicenseClassID)
                return false;

            int ApplicationID = _LDLApplication.GetApplicationIDIfPersonHasActiveApplicationFromThisClass(_Application.PersonID);

            if (ApplicationID != -1)
            {

                _IsSave = false;
                MessageBox.Show($"Choose another License Class, the selected Person Already have " +
                    $"an active appliction for the selected class with id = {ApplicationID} ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true;
            }
            else
                return false;
        }
        bool _CheckHasActiveLicenseFromThisClass()
        {
            if (clsLicenses.IsPersonHaveAnActiveLicneseWithTheSameLicneseClass(_Application.PersonID, _LDLApplication.LicenseClassID))
            {
                _IsSave = false;
                MessageBox.Show("Person already have a license with the same applied driving class, " +
                    "Choose diffrent driving class", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true;
            }

            return false;
        }
        void _DeleteApplicationIfIsNotSave()
        {
            clsApplications.Delete(_Application.ApplicationID);
        }
        void _ChangeDLApplicationID(int ID)
        {
            lblD_L_ApplicationID.Text = ID.ToString();
        }
        void _ChageHeader(string Text)
        {
            lblHeader.Text = Text;
        }
        void _ChageToUpdateModeAfterSave()
        {
            //After Add New
            _ChageHeader("Update Local Driving License Application");
            _CurrentLDLApplicationClassID = _LDLApplication.LicenseClassID;
            _ChangeDLApplicationID(_LDLApplication.LocalDrvingApplicationID);

        }
        private void frmNewLocalLicenseApplication_Load(object sender, EventArgs e)
        {
            _FillDataInLablesAddNewMode();
        }

        private void gbtnSave_Click(object sender, EventArgs e)
        {
            if (!_CheckFromPerson())
            {
                MessageBox.Show("Please Select a Person!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            

            if (_FillDataInNewApplicationObjectAndSaveResult())
            {
                _LDLApplication.ApplicationID = _Application.ApplicationID;
                _LDLApplication.LicenseClassID = Convert.ToInt32(gcbLicenseClass.SelectedValue);
                //NewLDApplication.PassedTests = 0;

                if (_CheckHasActiveApplicationFromThisClass())
                    return;

                if (_CheckHasActiveLicenseFromThisClass())
                    return;

                clsLocalDrivingApplictions.enMode PrevMode = _LDLApplication.Mode;

                if (_LDLApplication.Save())
                {
                    _IsSave = true;
                    _ChageToUpdateModeAfterSave();

                    if (PrevMode == clsLocalDrivingApplictions.enMode.AddNew)
                        MessageBox.Show($"L.D.LApplication Addedd Successfully With ID = {_LDLApplication.LocalDrvingApplicationID}",
                            "Succed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("Data Upated Successfully", "Succed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    _IsSave = false;
                    MessageBox.Show("Failed To Save Pleas Enter a Correct Data!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    _DeleteApplicationIfIsNotSave();
                }
            }
        }

        private void gbtnClose_Click(object sender, EventArgs e)
        {
            if (!_IsSave)
            {
                if (_Application.ApplicationID != -1)
                    _DeleteApplicationIfIsNotSave();
            }

            else
            if (OnClose != null)
                OnClose();

            this.Close();
        }

        private void gbtnNext_Click(object sender, EventArgs e)
        {
            gtabNewLocalDrivingLicenseApplication.SelectedIndex = 1;
        }

        private void gtabNewLocalDrivingLicenseApplication_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (_CheckFromPerson())
                return;

            MessageBox.Show($"Please Select a Person", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            e.Cancel = true;
        }

        private void frmNewLocalLicenseApplication_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_IsSave)
                if (_Application.ApplicationID != -1)
                    _DeleteApplicationIfIsNotSave();
        }
    }
}

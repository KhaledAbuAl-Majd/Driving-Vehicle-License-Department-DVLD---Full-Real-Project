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
using DVLDPresentation.Global_Classes;
using static System.Net.Mime.MediaTypeNames;

namespace DVLDPresentation.Applications.Driving_License_Services.New_Driving_License
{
    public partial class frmAddUpdateLocalDrivingLicesnseApplication : Form
    {
        public event Action OnClose;      
        bool _IsSave;
        int _CurrentLDLApplicationClassID;
        clsApplicationType _ApplicationType;
        clsApplication _Application;

        public enum enMode { AddNew = 0, Update = 1 };

        private enMode _Mode;
        private int _LocalDrivingLicenseApplicationID = -1;

        clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication;

        public frmAddUpdateLocalDrivingLicesnseApplication()
        {
            InitializeComponent(); 
            this._Mode = enMode.AddNew;
        }
        public frmAddUpdateLocalDrivingLicesnseApplication(int LocalDrivingLicenseApplicationID)
        {
            InitializeComponent();
            this._Mode = enMode.Update;
            this._LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
        }

        void _FillLicenseClassesInComoboBox()
        {
            DataTable dtLicenseClasses = clsLicenseClass.GetAllLicneseClasses();

            // My Way using datasource to store className and classID to make it easier 
            //gcbLicenseClass.DataSource = dtLicenseClasses;
            //gcbLicenseClass.DisplayMember = "ClassName";
            //gcbLicenseClass.ValueMember = "LicenseClassID";

            //Hadhoud Way i will use it to be with him 

            foreach (DataRow row in dtLicenseClasses.Rows)
            {
                gcbLicenseClass.Items.Add(row["ClassName"]);
            }
        }
        bool _CheckFromPerson()
        {
            return (ctrlPersonCardWithFilter1.PersonID != -1);
        }
        bool _FillDataInNewApplicationObjectAndSaveResult()
        {
            //_Application.ApplicantPersonID = ctrlPersonCardWithFilter1.PersonID;
            //_Application.ApplicationDate = DateTime.Now;
            //_Application.ApplicationTypeID = _ApplicationType.ApplicationTypeID;
            ////NewApplication.ApplicationStatus = "New";
            //_Application.PaidFees = _ApplicationType.ApplicationFees;
            //_Application.CreatedByUserID = clsGlobalSettings.CurrentUser.UserID;

            //if (_Application.Save())
            //{
            //    return true;
            //}
            //else
            //{
            //    _IsSave = false;
            //    MessageBox.Show("Failed To Add Application Pleas Enter a Correct Data!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return false;
            //}

            return false;
        }
        bool _CheckHasActiveApplicationFromThisClass()
        {
            //if (_CurrentLDLApplicationClassID == _LocalDrivingLicenseApplication.LicenseClassID)
            //    return false;

            ////int ApplicationID = _LocalDrivingLicenseApplication.GetApplicationIDIfPersonHasActiveApplicationFromThisClass(_Application.ApplicantPersonID);

            //if (ApplicationID != -1)
            //{

            //    _IsSave = false;
            //    MessageBox.Show($"Choose another License Class, the selected Person Already have " +
            //        $"an active appliction for the selected class with id = {ApplicationID} ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return true;
            //}
            //else
                return false;
        }
        bool _CheckHasActiveLicenseFromThisClass()
        {
            //if (clsLicenses.IsPersonHaveAnActiveLicneseWithTheSameLicneseClass(_Application.ApplicantPersonID, _LocalDrivingLicenseApplication.LicenseClassID))
            //{
            //    _IsSave = false;
            //    MessageBox.Show("Person already have a license with the same applied driving class, " +
            //        "Choose diffrent driving class", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return true;
            //}

            return false;
        }
        void _DeleteApplicationIfIsNotSave()
        {
            //clsApplication.Delete(_Application.ApplicationID);
        }
        void _ChangeDLApplicationID(int ID)
        {
            lblLocalDrivingLicebseApplicationID.Text = ID.ToString();
        }
        void _ChageHeader(string Text)
        {
            lblHeader.Text = Text;
        }
        void _ChangeFormText(string Text)
        {
            this.Text = Text;
        }
        void _ChangeEnaplityOfApplicationInfoPanel(bool value)
        {
            pnlApplicationInfo.Enabled = value;
        }
        void _ChangeEnaplityOfSaveButton(bool value)
        {
            gbtnSave.Enabled = value;
        }
        void _ChageToUpdateModeAfterSave()
        {
            //After Add New
            //_ChageHeader("Update Local Driving License Application");
            //_CurrentLDLApplicationClassID = _LocalDrivingLicenseApplication.LicenseClassID;
            //_ChangeDLApplicationID(_LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID);

        }
        void _AddNewMode()
        {
            _ChageHeader("New Local Driving License Application");
            _ChangeFormText("New Local Driving License Application");
            _LocalDrivingLicenseApplication = new clsLocalDrivingLicenseApplication();
            ctrlPersonCardWithFilter1.FilterFocus();
            _ChangeEnaplityOfApplicationInfoPanel(false);
            _ChangeEnaplityOfSaveButton(false);

            gcbLicenseClass.SelectedIndex = 2;
            lblApplicationFees.Text = _ApplicationType.ApplicationFees.ToString();
            lblApplicationDate.Text = DateTime.Now.ToShortDateString();
            lblCreatedBy.Text = clsGlobalSettings.CurrentUser.UserName;
        }
        void _LoadData()
        {
            ctrlPersonCardWithFilter1.LoadPersonInfo(_LocalDrivingLicenseApplication.ApplicantPersonID);
            lblLocalDrivingLicebseApplicationID.Text = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
            lblApplicationDate.Text = clsFormat.DateToShort(_LocalDrivingLicenseApplication.ApplicationDate);
            gcbLicenseClass.SelectedIndex = gcbLicenseClass.FindString(_LocalDrivingLicenseApplication.LicenseClassInfo.ClassName);
            lblApplicationFees.Text = _LocalDrivingLicenseApplication.PaidFees.ToString();
            lblCreatedBy.Text = _LocalDrivingLicenseApplication.CreatedByUserInfo.UserName;
        }
        void _UpdateMode()
        {
            _ChageHeader("Update Local Driving License Application");
            _ChangeFormText("Update Local Driving License Application");
            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(_LocalDrivingLicenseApplicationID);

            if (_LocalDrivingLicenseApplication == null)
            {
                MessageBox.Show("No Application with ID = " + _LocalDrivingLicenseApplicationID, "Application Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();

                return;
            }

            ctrlPersonCardWithFilter1.FilterEnabled = false;
            _ChangeEnaplityOfApplicationInfoPanel(true);
            _ChangeEnaplityOfSaveButton(true);

            _LoadData();
        }
        private void frmNewLocalLicenseApplication_Load(object sender, EventArgs e)
        {
            _AddNewMode();
            _FillLicenseClassesInComoboBox();

            switch (_Mode)
            {
                case enMode.AddNew:
                    _AddNewMode();
                    break;

                case enMode.Update:
                    _UpdateMode();
                    break;
            }
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
                //_LocalDrivingLicenseApplication.ApplicationID = _Application.ApplicationID;
                //_LocalDrivingLicenseApplication.LicenseClassID = Convert.ToInt32(gcbLicenseClass.SelectedValue);
                ////NewLDApplication.PassedTests = 0;

                if (_CheckHasActiveApplicationFromThisClass())
                    return;

                if (_CheckHasActiveLicenseFromThisClass())
                    return;

                //clsLocalDrivingLicenseApplication.enMode PrevMode = _LocalDrivingLicenseApplication.Mode;

                //if (_LocalDrivingLicenseApplication.Save())
                //{
                //    _IsSave = true;
                //    _ChageToUpdateModeAfterSave();

                //    if (PrevMode == clsLocalDrivingLicenseApplication.enMode.AddNew)
                //        MessageBox.Show($"L.D.LApplication Addedd Successfully With ID = {_LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID}",
                //            "Succed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    else
                //        MessageBox.Show("Data Upated Successfully", "Succed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //}
                //else
                //{
                //    _IsSave = false;
                //    MessageBox.Show("Failed To Save Pleas Enter a Correct Data!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    _DeleteApplicationIfIsNotSave();
                //}
            }
        }

        private void gbtnClose_Click(object sender, EventArgs e)
        {
            //if (!_IsSave)
            //{
            //    if (_Application.ApplicationID != -1)
            //        _DeleteApplicationIfIsNotSave();
            //}

            //else
            //if (OnClose != null)
            //    OnClose();

            //this.Close();
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
            //if (!_IsSave)
            //    if (_Application.ApplicationID != -1)
            //        _DeleteApplicationIfIsNotSave();
        }
    }
}

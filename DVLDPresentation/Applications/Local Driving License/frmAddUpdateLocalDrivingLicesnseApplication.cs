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

        public enum enMode { AddNew = 0, Update = 1 };

        private enMode _Mode;
        private int _LocalDrivingLicenseApplicationID = -1;
        private int _SelectedPersonID = -1;
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

        void _AddNewMode()
        {
            _ChageHeader("New Local Driving License Application");
            _ChangeFormText("New Local Driving License Application");
            _LocalDrivingLicenseApplication = new clsLocalDrivingLicenseApplication();
            ctrlPersonCardWithFilter1.FilterFocus();
            _ChangeEnaplityOfApplicationInfoPanel(false);
            _ChangeEnaplityOfSaveButton(false);

            gcbLicenseClass.SelectedIndex = 2;
            lblApplicationFees.Text = clsApplicationType.Find((int)clsApplication.enApplicationType.NewDrivingLicense).ApplicationFees.ToString();
            lblApplicationDate.Text = DateTime.Now.ToShortDateString();
            lblCreatedBy.Text = clsGlobal.CurrentUser.UserName;
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

            ctrlPersonCardWithFilter1.FilterFocus();
        }

        private void gbtnNext_Click(object sender, EventArgs e)
        {
            if (_Mode == enMode.Update)
            {
                gtcApplicationInfo.SelectedTab = gtpApplicationInfo;
                return;
            }

            //Add new Mode
            if (ctrlPersonCardWithFilter1.PersonID != -1)
            {

                _ChangeEnaplityOfSaveButton(true);
                _ChangeEnaplityOfApplicationInfoPanel(true);
                gtcApplicationInfo.SelectedTab = gtpApplicationInfo;
            }
            else
            {
                MessageBox.Show("Please Select a Person", "Select a Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ctrlPersonCardWithFilter1.FilterFocus();
            }
        }

        private void gbtnSave_Click(object sender, EventArgs e)
        {
            int LicenseClassID = clsLicenseClass.Find(gcbLicenseClass.Text).LicenseClassID;

            int ActiveApplicationID = clsApplication.GetActiveApplicationIDForLicenseClass(_SelectedPersonID, clsApplication.enApplicationType.NewDrivingLicense, LicenseClassID);

            if (ActiveApplicationID != -1)
            {
                MessageBox.Show("Choose another License Class, the selected Person Already have an active application for the selected class with id=" + ActiveApplicationID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                gcbLicenseClass.Focus();
                return;
            }

            //check if user already have issued license of the same driving  class.
            if (clsLicense.IsPersonHaveLicenseActiveANDNOT(ctrlPersonCardWithFilter1.PersonID, LicenseClassID))
            {
                MessageBox.Show("Person already have a license with the same applied driving class, Choose diffrent driving class", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                gcbLicenseClass.Focus();
                return;
            }

            _LocalDrivingLicenseApplication.ApplicantPersonID = ctrlPersonCardWithFilter1.PersonID; ;
            _LocalDrivingLicenseApplication.ApplicationDate = DateTime.Now;
            _LocalDrivingLicenseApplication.ApplicationTypeID = 1;
            _LocalDrivingLicenseApplication.ApplicationStatus = clsApplication.enApplicationStatus.New;
            _LocalDrivingLicenseApplication.LastStatusDate = DateTime.Now;
            _LocalDrivingLicenseApplication.PaidFees = Convert.ToSingle(lblApplicationFees.Text);
            _LocalDrivingLicenseApplication.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            _LocalDrivingLicenseApplication.LicenseClassID = LicenseClassID;


            if (_LocalDrivingLicenseApplication.Save())
            {
                _LocalDrivingLicenseApplicationID = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID;
                lblLocalDrivingLicebseApplicationID.Text = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
                _ChageHeader("Update Local Driving License Application");
                _ChangeFormText("Update Local Driving License Application");
                _Mode = enMode.Update;

                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _IsSave = true;
            }
            else
            {
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _IsSave = false;
            }

        }

        private void ctrlPersonCardWithFilter1_OnPersonSelected(int obj)
        {
            _SelectedPersonID = obj;
        }

        private void gbtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmNewLocalLicenseApplication_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_IsSave)
                if (OnClose != null)
                    OnClose();
        }
    }
}

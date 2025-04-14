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
using DVLDPresentation.Properties;

namespace DVLDPresentation.Applications.Manage_Applications.LocalDrivingLicenseApplications.Schedule_Test
{
    public partial class frmScheduleTestAdd_Edit : Form
    {
        public event Action OnClose;
         public enum enTestType { VisionTest = 1, WrittenTest = 2, StreetTest = 3 }
        enTestType _TestType;

        clsLocalDrivingApplictions _LDLApplication;
        clsTestAppointments _TestAppointment;
        bool _IsSave = false;
        public frmScheduleTestAdd_Edit(int LDLApplicationID,int TestAppointmentID,int TestType)
        {
            InitializeComponent();
            _LDLApplication = clsLocalDrivingApplictions.FindByLDLApplicationID(LDLApplicationID);
            _TestType = (enTestType)TestType;
            if (TestAppointmentID == -1)
            {
                _TestAppointment = new clsTestAppointments(LDLApplicationID, Convert.ToInt32(_TestType), clsGlobalSettings.LoggedInUser.UserID);
            }
            else
            {
                _TestAppointment = clsTestAppointments.Find(TestAppointmentID); ;
            }
        }

        void _ChangeGroubBoxText(string Text)
        {
            gbScheduleTest.Text = Text;
        }
        void _ChageHeaderIcon(Image image)
        {
            pbHeaderICon.Image = image;
        }
        void _SetDesingAtTestType()
        {
            switch (_TestType)
            {
                case enTestType.VisionTest:
                    _ChageHeaderIcon(Resources.Vision_512);
                    _ChangeGroubBoxText("Vision Test");
                    break;

                case enTestType.WrittenTest:
                    _ChageHeaderIcon(Resources.Written_Test_512);
                    _ChangeGroubBoxText("Written Test");
                    break;

                case enTestType.StreetTest:
                    _ChageHeaderIcon(Resources.driving_test_512);
                    _ChangeGroubBoxText("Street Test");
                    break;
            }
        }
        void _SetMinDateOfDate()
        {
            gDTPDate.MinDate = DateTime.Today;
        }
        void _ChageHeader(string Text)
        {
            lblHeader.Text = Text;
        }
        void _ChangeAppointmentLockedLabelVisiblity(bool value)
        {
            lblAppointmentLocked.Visible = value;
        }
        void _ChangeRetakeTestInfoGroubBoxEnablity(bool value)
        {
            gbRetakeTestInfo.Enabled = value;
        }
        void _RetakeTestMode()
        {
            if (_TestAppointment.RetakeTestApplicationID != -1)
                lblRetakeTestApplicationID.Text = _TestAppointment.RetakeTestApplicationID.ToString();
            else
                lblRetakeTestApplicationID.Text = "N/A";
            //Retatke Test ID
            lblRetakeAppFees.Text = clsApplicationTypes.FindApplicationType(7).ApplicationFees.ToString();
            lblTotalFees.Text = (_TestAppointment.PaidFees + clsApplicationTypes.FindApplicationType(7).ApplicationFees).ToString();
            _ChageHeader("Schedule Retake Test");
            _ChangeRetakeTestInfoGroubBoxEnablity(true);
        } 
        void _FirstScheduleTestMode()
        {
            lblRetakeTestApplicationID.Text = "N/A";
            lblRetakeAppFees.Text = "0";
            lblTotalFees.Text = _TestAppointment.PaidFees.ToString();
            _ChangeRetakeTestInfoGroubBoxEnablity(false);
        }
        void _TestLockedMode()
        {
            _ChageHeader("Schedule Retake Test");
            _ChangeAppointmentLockedLabelVisiblity(true);
            gDTPDate.Enabled = false;
            gbtnSave.Enabled = false;
            gbtnClose.Focus();
        }
        void _FillDataInLabels()
        {

            lblLDLApplicationID.Text = _LDLApplication.LocalDrvingApplicationID.ToString();
            lblClassName.Text = clsLicneseClasses.Find(_LDLApplication.LicenseClassID).ClassName;
            lblName.Text = clsPeople.Find(clsApplications.Find(_LDLApplication.ApplicationID).PersonID).GetFullName();
            lblTrial.Text = _TestAppointment.TrialNumber.ToString();
            gDTPDate.Value = _TestAppointment.AppointmentDate;
            lblFees.Text = _TestAppointment.PaidFees.ToString();
            //lblRetakeAppFees.Text = _TestAppointment.RetakeTestFees.ToString();
            //lblTotalFees.Text = _TestAppointment.PaidFees.ToString();
            //lblRetakeTestApplicationID.Text = (_TestAppointment.TestAppointmentID == -1) ? "N/A" : _TestAppointment.TestAppointmentID.ToString();

            if (_TestAppointment.IsLocked)
                _TestLockedMode();
            else
                _ChangeAppointmentLockedLabelVisiblity(false);

            if (_TestAppointment.TestMode == clsTestAppointments.enTestMode.RetakeTest)
                _RetakeTestMode();
            else
                _FirstScheduleTestMode();
        }
        void _Save()
        {
            _TestAppointment.AppointmentDate = gDTPDate.Value;
            _TestAppointment.CreatedByUserID = clsGlobalSettings.LoggedInUser.UserID;
            if (_TestAppointment.Save())
            {
                _IsSave = true;
                lblRetakeTestApplicationID.Text = _TestAppointment.RetakeTestApplicationID.ToString();
                MessageBox.Show("Data Saved Successfully!", "Succed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _Close();
            }
            else
            {
                _IsSave = false;
                MessageBox.Show("Failed To Save!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        void _Close()
        {
            if (_IsSave)
                if (OnClose != null)
                    OnClose();

            this.Close();
        }
        private void frmScheduleTestAdd_Edit_Load(object sender, EventArgs e)
        {
            _SetMinDateOfDate();
            _FillDataInLabels();
            _SetDesingAtTestType();
        
        }

        private void gbtnSave_Click(object sender, EventArgs e)
        {
            _Save();
        }
        private void gbtnClose_Click_1(object sender, EventArgs e)
        {
            _Close();
        }
    }
}

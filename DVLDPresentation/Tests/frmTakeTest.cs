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

namespace DVLDPresentation.Applications.Manage_Applications.LocalDrivingLicenseApplications.Tests
{
    public partial class frmTakeTest : Form
    {
        public event Action OnClose;

        clsTestAppointments _TestAppointment;
        enum enTestType { VisionTest = 1, WrittenTest = 2, StreetTest = 3 }
        enTestType _TestType;
        bool _IsSave = false;


        public frmTakeTest(int TestAppointmentID)
        {
            InitializeComponent();
            _TestAppointment = clsTestAppointments.Find(TestAppointmentID);
            _TestType = (enTestType)_TestAppointment.TestTypeID;
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
        void _ChagelblTestID(string Text)
        {
            lblTestId.Text = Text;
        }
        void _FillDataInLabels()
        {
            clsLocalDrivingApplictions _LDLApplication = clsLocalDrivingApplictions.FindByLDLApplicationID(_TestAppointment.LocalDrivingLicenseApplicationID);


            lblLDLApplicationID.Text = _LDLApplication.LocalDrvingApplicationID.ToString();
            lblClassName.Text = clsLicneseClasses.Find(_LDLApplication.LicenseClassID).ClassName;
            lblName.Text = clsPerson.Find(clsApplications.Find(_LDLApplication.ApplicationID).PersonID).GetFullName();
            lblTrial.Text = _TestAppointment.TrialNumber.ToString();
            lblDate.Text = _TestAppointment.AppointmentDate.ToString("dd/MMM/yyyy");
            lblFees.Text = clsTestTypes.FindTestType(_TestAppointment.TestTypeID).TestFees.ToString();
            _ChagelblTestID("Not Taken Yet");
        }
        void _Save()
        {
            if ((MessageBox.Show("Are you sure you want to save? After that you cannot change the Pass/Faile results after you save",
               "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == DialogResult.Yes))
            {

                clsTests NewTest = new clsTests(_TestAppointment.TestAppointmentID, clsGlobalSettings.LoggedInUser.UserID);
                NewTest.Notes = gtxtNotes.Text;
                NewTest.TestResult = grbPass.Checked;

                if (NewTest.Save())
                {
                    _IsSave = true;
                    MessageBox.Show("Data Saved Successfully!", "Succed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _Close();
                }
                else
                {
                    _IsSave = false;
                    MessageBox.Show("Failed To Save !", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        void _Close()
        {
            if (_IsSave)
                if (OnClose != null)
                    OnClose();

            this.Close();
        }
        private void frmTakeTest_Load(object sender, EventArgs e)
        {
            _SetDesingAtTestType();
            _FillDataInLabels();
        }

        private void gbtnSave_Click(object sender, EventArgs e)
        {
            _Save();
        }

        private void gbtnClose_Click(object sender, EventArgs e)
        {
            _Close();
        }
    }
}

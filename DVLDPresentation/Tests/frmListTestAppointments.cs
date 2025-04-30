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
using DVLDPresentation.Applications.Manage_Applications.LocalDrivingLicenseApplications.Schedule_Test;
using DVLDPresentation.Applications.Manage_Applications.LocalDrivingLicenseApplications.Tests;
using DVLDPresentation.Controls;
using DVLDPresentation.Properties;

namespace DVLDPresentation.Applications.Manage_Applications.Schedule_Test
{
    public partial class frmListTestAppointments : Form
    {
        DataTable _dtLicenseTestAppointments;
        int _LocalDrivingLicenseApplicationID;
        clsTestType.enTestType _TestType;

        public frmListTestAppointments(int LocalDrivingLicenseApplicationID,clsTestType.enTestType TestType)
        {
            InitializeComponent();
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            _TestType = TestType;
        }

        void _ChangeFormText(string Text)
        {
            this.Text = Text;
        }

        void _ChangeHeader(string Text)
        {
            lblHeader.Text = Text;
        }

        void _ChageHeaderIcon(Image image)
        {
            pbHeaderICon.Image = image;
        }

        void _LoadTestTypeImageAndTitle()
        {
            switch (_TestType)
            {
                case clsTestType.enTestType.VisionTest:
                    {
                        _ChangeHeader("Vision Test Appointments");
                        _ChangeFormText("Vision Test Appointments");
                        _ChageHeaderIcon(Resources.Vision_512);
                        break;
                    }

                case clsTestType.enTestType.WrittenTest:
                    {
                        _ChangeHeader("Written Test Appointments");
                        _ChangeFormText("Written Test Appointments");
                        _ChageHeaderIcon(Resources.Written_Test_512);
                        break;
                    }

                case clsTestType.enTestType.StreetTest:
                    {
                        _ChangeHeader("Street Test Appointments");
                        _ChangeFormText("Street Test Appointments");
                        _ChageHeaderIcon(Resources.driving_test_512);
                        break;
                    }
            }
        }

        void _RefreshAppointmentsList()
        {
            _dtLicenseTestAppointments = clsTestAppointment.GetApplicationTestAppointmentsPerTestType(_LocalDrivingLicenseApplicationID, _TestType);
            dgvLicenseTestAppointments.DataSource = _dtLicenseTestAppointments;
            lblNumOfRecords.Text = _dtLicenseTestAppointments.Rows.Count.ToString();

            if (dgvLicenseTestAppointments.Rows.Count > 0)
            {
                dgvLicenseTestAppointments.Columns[0].HeaderText = "Appointment ID";
                dgvLicenseTestAppointments.Columns[0].Width = 150;

                dgvLicenseTestAppointments.Columns[1].HeaderText = "Appointment Date";
                dgvLicenseTestAppointments.Columns[1].Width = 200;

                dgvLicenseTestAppointments.Columns[2].HeaderText = "Total Paid Fees";
                dgvLicenseTestAppointments.Columns[2].Width = 150;

                dgvLicenseTestAppointments.Columns[3].HeaderText = "Is Locked";
                dgvLicenseTestAppointments.Columns[3].Width = 100;
            }
        }

        private void frmListTestAppointments_Load(object sender, EventArgs e)
        {
            _LoadTestTypeImageAndTitle();

            ctrlDLApplicationInfo1.LoadApplicationInfoByLocalDrivingAppID(_LocalDrivingLicenseApplicationID);
            _RefreshAppointmentsList();
        }

        private void btnAddAppointment_Click(object sender, EventArgs e)
        {
            clsLocalDrivingLicenseApplication localDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(_LocalDrivingLicenseApplicationID);


            if (localDrivingLicenseApplication.IsThereAnActiveScheduledTest(_TestType))
            {
                MessageBox.Show("Person Already have an active appointment for this test, You cannot add new appointment", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (clsTest.CheckPersonPassedThisTextBeforeByLocalDrivingLicenseApplicationID(_LocalDrivingLicenseApplicationID, Convert.ToInt32(_TestType)))
            {
                MessageBox.Show("This Person Already passed this test before, you can only retake failed test.",
                    "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            frmScheduleTest frm = new frmScheduleTest(_LocalDrivingLicenseApplicationID, _TestType);
            frm.ShowDialog();
            _RefreshAppointmentsList();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int TestAppointmentID = (int)dgvLicenseTestAppointments.CurrentRow.Cells[0].Value;

            frmScheduleTest frm = new frmScheduleTest(_LocalDrivingLicenseApplicationID,_TestType, TestAppointmentID);
            frm.ShowDialog();
            _RefreshAppointmentsList();
        }

        private void takeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int TestAppointmentID = (int)dgvLicenseTestAppointments.CurrentRow.Cells[0].Value;

            frmTakeTest frm = new frmTakeTest(TestAppointmentID, _TestType);
            frm.ShowDialog();
            _RefreshAppointmentsList();
        }

        private void gbtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

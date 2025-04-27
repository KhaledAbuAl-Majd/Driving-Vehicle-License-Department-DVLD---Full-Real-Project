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
        public event Action OnClose;
        //it equal Test Types ID
        enum enTestType { VisionTest = 1, WrittenTest = 2, StreetTest = 3 }
        enTestType _TestType;
        int _LDLApplicationID;
        bool _AnyChanges = false;
        void _SetFirstTypeInTestTypeMode(byte PassedTestsNum)
        {
            switch (PassedTestsNum)
            {
                case 0:
                    _TestType = enTestType.VisionTest;
                    break;

                case 1:
                    _TestType = enTestType.WrittenTest;
                    break;

                case 2:
                    _TestType = enTestType.StreetTest;
                    break;
            }
        }
        public frmListTestAppointments(int LDLApplicationID,byte PassedTestsNum)
        {
            InitializeComponent();
            _LDLApplicationID = LDLApplicationID;
            ctrlDLApplicationInfo1.LDLApplicationID = LDLApplicationID;
            _SetFirstTypeInTestTypeMode(PassedTestsNum);
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
        void _SetDesingAtTestType()
        {
            switch (_TestType)
            {
                case enTestType.VisionTest:
                    _ChangeHeader("Vision Test Appointments");
                    _ChageHeaderIcon(Resources.Vision_512);
                    _ChangeFormText("Vision Test Appointments");
                    break;

                case enTestType.WrittenTest:
                    _ChangeHeader("Written Test Appointments");
                    _ChageHeaderIcon(Resources.Written_Test_512);
                    _ChangeFormText("Written Test Appointments");
                    break;

                case enTestType.StreetTest:
                    _ChangeHeader("Street Test Appointments");
                    _ChageHeaderIcon(Resources.driving_test_512);
                    _ChangeFormText("Street Test Appointments");
                    break;
            }
        }
        void _EditSizeOfDGVColumns()
        {
            dgvAppointments.Columns["Appointment ID"].Width = 150;
            dgvAppointments.Columns["Appointment Date"].Width = 200;
            dgvAppointments.Columns["Paid Fees"].Width = 120;
            dgvAppointments.Columns["Is Locked"].Width = 120;
        } 
        void _Load_RefreshDataInAppointmentsDGV()
        {
            DataTable dtAppointments = clsTestAppointment.GetApplicationTestAppointmentsPerTestType(_LDLApplicationID, (clsTestType.enTestType)_TestType);

            if (dtAppointments.Rows.Count > 0)
            {
                dtAppointments.Columns["TestAppointmentID"].ColumnName = "Appointment ID";
                dtAppointments.Columns["AppointmentDate"].ColumnName = "Appointment Date";
                dtAppointments.Columns["PaidFees"].ColumnName = "Paid Fees";
                dtAppointments.Columns["IsLocked"].ColumnName = "Is Locked";


                dgvAppointments.DataSource = dtAppointments.DefaultView.ToTable(false, "Appointment ID", "Appointment Date", "Paid Fees", "Is Locked");
                _EditSizeOfDGVColumns();
                lblNumOfRecords.Text = dgvAppointments.RowCount.ToString();
            }
            else
            {
                lblNumOfRecords.Text = "0";
            }
        }
        private void frmScheduleTest_Load(object sender, EventArgs e)
        {
            _SetDesingAtTestType();
            _Load_RefreshDataInAppointmentsDGV();
        }

        private void btnAddAppointment_Click(object sender, EventArgs e)
        {
            //if (clsLocalDrivingLicenseApplication.IsThereAnActiveScheduledTest(_LDLApplicationID, Convert.ToInt32(_TestType)))
            //{
            //    MessageBox.Show("Person Already have an active appointment for this test,You cannot add new appointment.",
            //        "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}

            if (clsTest.CheckPersonPassedThisTextBefore(_LDLApplicationID, Convert.ToInt32(_TestType)))
            {
                MessageBox.Show("This Person Already passed this test before, you can only retake failed test.",
                    "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            frmScheduleTest frm = new frmScheduleTest(_LDLApplicationID, -1,Convert.ToInt32(_TestType));
            frm.OnClose += _Load_RefreshDataInAppointmentsDGV;
            frm.ShowDialog();
        }

        private void FrmTakeTest_OnClose()
        {
            _Load_RefreshDataInAppointmentsDGV();
            _AnyChanges = true;
            ctrlDLApplicationInfo1._ChangePassedTestsLabel();
        }



        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmScheduleTest frm = new frmScheduleTest(_LDLApplicationID,
                Convert.ToInt32(dgvAppointments.SelectedCells[0].Value), Convert.ToInt32(_TestType));
            frm.OnClose += _Load_RefreshDataInAppointmentsDGV;
            frm.ShowDialog();
        }

        private void takeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTakeTest frm = new frmTakeTest(Convert.ToInt32(dgvAppointments.SelectedCells[0].Value));
            frm.OnClose += FrmTakeTest_OnClose;
            frm.ShowDialog();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            //Is Locked
            if (Convert.ToBoolean(dgvAppointments.SelectedCells[3].Value))
            {
                //Edit Option
                contextMenuStrip1.Items[1].Enabled = false;
            }else
                contextMenuStrip1.Items[1].Enabled = true;
        }

        private void gbtnClose_Click(object sender, EventArgs e)
        {
            if (_AnyChanges)
                if (OnClose != null)
                    OnClose();

            this.Close();
        }
    }
}

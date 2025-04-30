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
        private int _AppointmentID;
        private clsTestType.enTestType _TestType;

        private int _TestID = -1;
        private clsTest _Test;

        public frmTakeTest(int AppointmentID, clsTestType.enTestType TestType)
        {
            InitializeComponent();
            _AppointmentID = AppointmentID;
            _TestType = TestType;
        }
 
        private void frmTakeTest_Load(object sender, EventArgs e)
        {
            ctrlSecheduledTest1.TestTypeID = _TestType;

            ctrlSecheduledTest1.LoadInfo(_AppointmentID);

            if (ctrlSecheduledTest1.TestAppointmentID == -1)
                gbtnSave.Enabled = false;
            else
                gbtnSave.Enabled = true;

            int _TestID = ctrlSecheduledTest1.TestID;
            if (_TestID != -1)
            {
                //Update Mode

                _Test = clsTest.Find(_TestID);

                if (_Test.TestResult)
                    grbPass.Checked = true;
                else
                    grbFail.Checked = true;

                gtxtNotes.Text = _Test.Notes;

                lblUserMessage.Visible = true;
                grbFail.Enabled = false;
                grbPass.Enabled = false;

                //You Can Change Notes Only!
                //gbtnSave.Enabled = false;
            }

            else
                _Test = new clsTest();
        }

        private void gbtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gbtnSave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to save? After that you cannot change the Pass/Fail results after you save?.",
                      "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
            {
                return;
            }

            _Test.TestAppointmentID = _AppointmentID;
            _Test.TestResult = grbPass.Checked;
            _Test.Notes = gtxtNotes.Text.Trim();
            _Test.CreatedByUserID = clsGlobal.CurrentUser.UserID;

            if (_Test.Save())
            {
                _TestID = _Test.TestID;
                ctrlSecheduledTest1.TestID = _TestID;
                
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

                grbPass.Enabled = false;
                grbFail.Enabled = false;

                //You Can Change Notes Only!
                //gbtnSave.Enabled = false;

            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}

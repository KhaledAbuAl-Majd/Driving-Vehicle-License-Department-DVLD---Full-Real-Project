using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Lifetime;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLDBusiness;
using DVLDPresentation.Applications.Manage_Applications.LocalDrivingLicenseApplications;
using DVLDPresentation.People;

namespace DVLDPresentation.Applications.Detain_Licenses
{
    public partial class frmListDetainedLicenses : Form
    {
        //All Detained and Not Detained
        DataView _dvDetainedLicenses;
        public frmListDetainedLicenses()
        {
            InitializeComponent();
        }

        void _ShowNewReleaseDetainedLicenseForm(int LicenseID)
        {
            frmReleaseDetainedLicense frm = new frmReleaseDetainedLicense(LicenseID);
            frm.OnClose += _Load_RefereshDetainedLicensesInDGV;
            frm.ShowDialog();
        }
        private void _Show_HideTextFilter(bool value)
        {
            gtxtFilterValue.Visible = value;
        }
        private void _Show_HideCBIsReleasedFilter(bool value)
        {
            gcbIsReleased.Visible = value;
        }
        private void _FilterData(string FilterText)
        {
            if (_dvDetainedLicenses != null)
            {
                _dvDetainedLicenses.RowFilter = FilterText;
                dgvDetainedLicenses.DataSource = _dvDetainedLicenses;
                lblNumOfRecords.Text = _dvDetainedLicenses.Count.ToString();
            }
        }
        private void _GetTextFilterEmpty()
        {
            gtxtFilterValue.Text = "";
        }
        private void _FilterByAtDesign()
        {
            if (gcbFilterBy.Text == "None")
            {
                _Show_HideTextFilter(false);
                _Show_HideCBIsReleasedFilter(false);
                _FilterData("");
            }
            else if (gcbFilterBy.Text == "Is Released")
            {
                _Show_HideTextFilter(false);
                _Show_HideCBIsReleasedFilter(true);
                gcbIsReleased.SelectedIndex = 0;
            }
            else
            {
                _Show_HideTextFilter(true);
                _Show_HideCBIsReleasedFilter(false);
                _FilterData("");
            }

            _GetTextFilterEmpty();
        }
        void _EditSizeOfDGVColumns()
        {
            dgvDetainedLicenses.Columns["D.ID"].Width = 100;
            dgvDetainedLicenses.Columns["L.ID"].Width = 100;
            dgvDetainedLicenses.Columns["D.Date"].Width = 170;
            dgvDetainedLicenses.Columns["Is Released"].Width = 100;
            dgvDetainedLicenses.Columns["Fine Fees"].Width = 100;
            dgvDetainedLicenses.Columns["Released Date"].Width = 170;
            dgvDetainedLicenses.Columns["N.No."].Width = 100;
            dgvDetainedLicenses.Columns["Full Name"].Width = 300;
            dgvDetainedLicenses.Columns["Release App.ID"].Width = 130; 
        }
        void _Load_RefereshDetainedLicensesInDGV()
        {
            DataTable dtAllDetainedLicenses = clsDetainedLicenses.GetAllLicensesDetainedAndNot();

            if (dtAllDetainedLicenses.Rows.Count > 0)
            {
                dtAllDetainedLicenses.Columns.Add("N.No.", typeof(string));
                dtAllDetainedLicenses.Columns.Add("Full Name", typeof(string));

                dtAllDetainedLicenses.Columns["DetainID"].ColumnName = "D.ID";
                dtAllDetainedLicenses.Columns["LicenseID"].ColumnName = "L.ID";
                dtAllDetainedLicenses.Columns["DetainDate"].ColumnName = "D.Date";
                dtAllDetainedLicenses.Columns["IsReleased"].ColumnName = "Is Released";
                dtAllDetainedLicenses.Columns["FineFees"].ColumnName = "Fine Fees";
                dtAllDetainedLicenses.Columns["ReleasedDate"].ColumnName = "Released Date";
                dtAllDetainedLicenses.Columns["ReleaseApplicationID"].ColumnName = "Release App.ID";

                clsPerson Person;

                foreach(DataRow row in dtAllDetainedLicenses.Rows)
                {
                    int PersonID = clsDriver.FindByDriverID(clsLicense.Find(Convert.ToInt32(row["L.ID"])).DriverID).PersonID;
                    Person = clsPerson.Find(PersonID);

                    row["N.No."] = Person.NationalNo;
                    row["Full Name"] = Person.FullName;
                }

                DataTable dt2 = dtAllDetainedLicenses.DefaultView.ToTable(false, "D.ID", "L.ID",
                    "D.Date", "Is Released", "Fine Fees", "Released Date", "N.No.", "Full Name", "Release App.ID");

                _dvDetainedLicenses = dt2.DefaultView;
                dgvDetainedLicenses.DataSource = dt2;
                _EditSizeOfDGVColumns();
                lblNumOfRecords.Text = _dvDetainedLicenses.Count.ToString();
                gcbFilterBy.SelectedIndex = 0;
            }
            else
            {
                lblNumOfRecords.Text = "0";
            }
        }
        private void _FilterByIsReleased()
        {
            if (gcbIsReleased.Text == "All")
                _FilterData("");

            else if (gcbIsReleased.Text == "Yes")
                _FilterData("[Is Released] = 1");

            else if (gcbIsReleased.Text == "No")
                _FilterData("[Is Released] = 0");
        }

        private void frmListDetainedLicenses_Load(object sender, EventArgs e)
        {
            _Load_RefereshDetainedLicensesInDGV();
        }

        private void gcbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            _FilterByAtDesign();
        }

        private void gtxtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(gcbFilterBy.Text == "Release Application ID" || gcbFilterBy.Text == "Detain ID"))
                return;

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void gtxtFilterValue_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(gtxtFilterValue.Text))
            {
                //to make filter is none get all people
                _FilterData("");
                return;
            }
            switch (gcbFilterBy.Text)
            {
                case "None":
                    _FilterData("");
                    break;

                case "Detain ID":
                    _FilterData("[D.ID] = " + gtxtFilterValue.Text);
                    break;

                case "National No.":
                    _FilterData($"[N.No.] = '{gtxtFilterValue.Text}'");
                    break;

                case "Full Name":
                    _FilterData($"[Full Name] like'{gtxtFilterValue.Text}%'");
                    break;

                case "Release Application ID":
                    _FilterData("[Release App.ID] = " + gtxtFilterValue.Text);
                    break;

            }
        }

        private void gcbIsReleased_SelectedIndexChanged(object sender, EventArgs e)
        {
            _FilterByIsReleased();
        }

        private void btnNewDetaineLicense_Click(object sender, EventArgs e)
        {
            frmDetainLicense frm = new frmDetainLicense();
            frm.OnClose += _Load_RefereshDetainedLicensesInDGV;
            frm.ShowDialog();
        }

        private void btnNewReleaseLicense_Click(object sender, EventArgs e)
        {
            _ShowNewReleaseDetainedLicenseForm(-1);
        }

        private void cmpListDetainedLicensesOptoins_Opening(object sender, CancelEventArgs e)
        {
            bool IsReleased = Convert.ToBoolean(dgvDetainedLicenses.SelectedCells[3].Value);

            if (IsReleased)
            {
                //Release Detained License Option
                cmpListDetainedLicensesOptoins.Items[3].Enabled = false;
            }
            else
            {
                cmpListDetainedLicensesOptoins.Items[3].Enabled = true;
            }
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string NationalNo = (string)dgvDetainedLicenses.SelectedCells[6].Value;
            int PersonID = clsPerson.Find(NationalNo).PersonID;

            frmShowPersonInfo frm = new frmShowPersonInfo(PersonID);
            //frm.OnClose += _Load_RefereshDetainedLicensesInDGV;
            frm.ShowDialog();
        }

        private void CMSIshowLicenseDetails_Click(object sender, EventArgs e)
        {
            int LicenseID = Convert.ToInt32(dgvDetainedLicenses.SelectedCells[1].Value);

            frmLicneseInfo frm = new frmLicneseInfo(LicenseID);
            frm.ShowDialog();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string NationalNo = (string)dgvDetainedLicenses.SelectedCells[6].Value;
            int PersonID = clsPerson.Find(NationalNo).PersonID;

            frmLicenseHistory frm = new frmLicenseHistory(PersonID);
            frm.ShowDialog();
        }

        private void ReleaseDetainedLicenseToolMenueStrip_Click(object sender, EventArgs e)
        {
            int LicenseID = Convert.ToInt32(dgvDetainedLicenses.SelectedCells[1].Value);
            _ShowNewReleaseDetainedLicenseForm(LicenseID);
        }

        private void gbtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

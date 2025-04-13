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
using DVLDPresentation.Applications.Manage_Applications.International_Driving_License_Application;

namespace DVLDPresentation.Applications.Manage_Applications.LocalDrivingLicenseApplications
{
    public partial class frmLicenseHistory : Form
    {
        int _PersonID;
        public frmLicenseHistory(int PersonID)
        {
            InitializeComponent();
            _PersonID = PersonID;
        }

        void _EditLocalDGVSize()
        {
            dgvLocalLicenseHistory.Columns["Lic.ID"].Width = 110;
            dgvLocalLicenseHistory.Columns["App.ID"].Width = 110;
            dgvLocalLicenseHistory.Columns["Class Name"].Width = 300;
            dgvLocalLicenseHistory.Columns["Issue Date"].Width = 185;
            dgvLocalLicenseHistory.Columns["Expiration Date"].Width = 130;
            dgvLocalLicenseHistory.Columns["Is Acitve"].Width = 90;
        }
        void _LoadDataInLocalDGV()
        {
            DataTable dtAllLocalLicenses = clsLicenses.GetAllLicnesesByPersonID(_PersonID);

            if (dtAllLocalLicenses.Rows.Count > 0)
            {
                dtAllLocalLicenses.Columns.Add("Class Name", typeof(string));

                dtAllLocalLicenses.Columns["LicneseID"].ColumnName = "Lic.ID";
                dtAllLocalLicenses.Columns["ApplicationID"].ColumnName = "App.ID";
                dtAllLocalLicenses.Columns["IsuueDate"].ColumnName = "Issue Date";
                dtAllLocalLicenses.Columns["ExpirationDate"].ColumnName = "Expiration Date";
                dtAllLocalLicenses.Columns["IsAcitve"].ColumnName = "Is Acitve";


                foreach(DataRow row in dtAllLocalLicenses.Rows)
                {
                    row["Class Name"] = clsLicneseClasses.Find(Convert.ToInt32(row["LicenseClassID"])).ClassName;
                }

                dgvLocalLicenseHistory.DataSource = dtAllLocalLicenses.DefaultView.ToTable(false, "Lic.ID", "App.ID",
                    "Class Name", "Issue Date", "Expiration Date", "Is Acitve");

                _EditLocalDGVSize();
                lblLocalNumOfRecords.Text = dtAllLocalLicenses.Rows.Count.ToString();
            }
            else
                lblLocalNumOfRecords.Text = "0";
        }
        void _EditInternationalDGVSize()
        {
            dgvInternationalLicenseHistory.Columns["Int.License ID"].Width = 130;
            dgvInternationalLicenseHistory.Columns["Application ID"].Width = 130;
            dgvInternationalLicenseHistory.Columns["L.License ID"].Width = 130;
            dgvInternationalLicenseHistory.Columns["Issue Date"].Width = 190;
            dgvInternationalLicenseHistory.Columns["Expiration Date"].Width = 170;
            dgvInternationalLicenseHistory.Columns["Is Acitve"].Width = 100;
        }
        void _LoadDataInInternationalDGV()
        {
            DataTable dtAllInternationalLicenses = clsInternationalLicenses.GetAllLicnesesByPersonID(_PersonID);

            if (dtAllInternationalLicenses.Rows.Count > 0)
            {
                dtAllInternationalLicenses.Columns["InternationalLicenseID"].ColumnName = "Int.License ID";
                dtAllInternationalLicenses.Columns["ApplicationID"].ColumnName = "Application ID";
                dtAllInternationalLicenses.Columns["IssuedUsingLocalLicenseID"].ColumnName = "L.License ID";
                dtAllInternationalLicenses.Columns["IssueDate"].ColumnName = "Issue Date";
                dtAllInternationalLicenses.Columns["ExpirationDate"].ColumnName = "Expiration Date";
                dtAllInternationalLicenses.Columns["IsActive"].ColumnName = "Is Acitve";

                dgvInternationalLicenseHistory.DataSource = dtAllInternationalLicenses.DefaultView.ToTable(false, "Int.License ID", "Application ID",
                    "L.License ID", "Issue Date", "Expiration Date", "Is Acitve");

                _EditInternationalDGVSize();
                lblInternationalNumOfRecords.Text = dtAllInternationalLicenses.Rows.Count.ToString();
            }
            else
                lblInternationalNumOfRecords.Text = "0";
        }
        private void frmLicenseHistory_Load(object sender, EventArgs e)
        {
            ctrlPersonCardWithFilter1.UpdateMode(_PersonID);
            _LoadDataInLocalDGV();
            _LoadDataInInternationalDGV();
        }

        private void gbtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CMSIshowLocalLicenseInfo_Click(object sender, EventArgs e)
        {
            //LicenseiD
            int LicenseID = Convert.ToInt32(dgvLocalLicenseHistory.SelectedCells[0].Value);
            frmLicneseInfo frm = new frmLicneseInfo(LicenseID);
            frm.ShowDialog();
        }

        private void tmsIshowInternationalLicenseInfo_Click(object sender, EventArgs e)
        {
            int IntLicenseID = Convert.ToInt32(dgvInternationalLicenseHistory.SelectedCells[0].Value);
            frmInternationalLicenseInfo frm = new frmInternationalLicenseInfo(IntLicenseID);
            frm.ShowDialog();
        }
    }
}

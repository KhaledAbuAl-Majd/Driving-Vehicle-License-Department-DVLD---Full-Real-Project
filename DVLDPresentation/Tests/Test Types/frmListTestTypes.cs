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
using DVLDPresentation.Applications.Application_Types;

namespace DVLDPresentation.Applications.Test_Types
{
    public partial class frmListTestTypes : Form
    {
        DataTable _dtAllTestTypes;
        public frmListTestTypes()
        {
            InitializeComponent();
        }

        void _RefreshTestTypesList()
        {
            _dtAllTestTypes = clsTestType.GetAllTestTypes();
            dgvTestTypes.DataSource = _dtAllTestTypes;
            lblNumOfRecords.Text = _dtAllTestTypes.Rows.Count.ToString();
        }

        private void frmManageTestTypes_Load(object sender, EventArgs e)
        {
            _RefreshTestTypesList();

            if (_dtAllTestTypes.Rows.Count > 0)
            {
                dgvTestTypes.Columns[0].HeaderText = "ID";
                dgvTestTypes.Columns[0].Width = 120;

                dgvTestTypes.Columns[1].HeaderText = "Title";
                dgvTestTypes.Columns[1].Width = 200;

                dgvTestTypes.Columns[2].HeaderText = "Description";
                dgvTestTypes.Columns[2].Width = 400;

                dgvTestTypes.Columns[3].HeaderText = "Fees";
                dgvTestTypes.Columns[3].Width = 100;
            }
        }

        private void toolStripMenuItem3_Click_1(object sender, EventArgs e)
        {
            frmUpdateTestTypes frm = new frmUpdateTestTypes((clsTestType.enTestType)dgvTestTypes.CurrentRow.Cells[0].Value);
            frm.OnSave += _RefreshTestTypesList;
            frm.ShowDialog();
        }

        private void gbtnClose_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

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
    public partial class frmManageTestTypes : Form
    {
        public frmManageTestTypes()
        {
            InitializeComponent();
        }

        void _Load_RefreshApplicationTypesInDGV()
        {
            DataTable dt = clsTestTypes.GetAllTestTypes();

            if (dt.Rows.Count > 0)
            {
                dt.Columns["TestTypeID"].ColumnName = "ID";
                dt.Columns["TestTypeTitle"].ColumnName = "Title";
                dt.Columns["TestTypeDescription"].ColumnName = "Description";
                dt.Columns["TestFees"].ColumnName = "Fees";

                dgvTestTypes.DataSource = dt.DefaultView;
                dgvTestTypes.Columns["ID"].FillWeight = 15;
                dgvTestTypes.Columns["Title"].FillWeight = 25;
                dgvTestTypes.Columns["Description"].FillWeight = 50;
                dgvTestTypes.Columns["Fees"].FillWeight = 10;

                lblNumOfRecords.Text = dt.Rows.Count.ToString();
            }
            else
            {
                lblNumOfRecords.Text = "0";
            }
        }
        private void FrmUpdate_OnClose()
        {
            _Load_RefreshApplicationTypesInDGV();
        }

        private void gbtnClose_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmManageTestTypes_Load(object sender, EventArgs e)
        {
            _Load_RefreshApplicationTypesInDGV();
        }

        private void toolStripMenuItem3_Click_1(object sender, EventArgs e)
        {
            frmUpdateTestTypes frm = new frmUpdateTestTypes
               (Convert.ToInt32(dgvTestTypes.SelectedCells[0].Value));

            frm.OnClose += FrmUpdate_OnClose; ;
            frm.ShowDialog();
        }
    }
}

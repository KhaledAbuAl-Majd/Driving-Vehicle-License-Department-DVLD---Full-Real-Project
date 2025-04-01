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

namespace DVLDPresentation.Applications
{
    public partial class frmManageApplicationTypes : Form
    {
        public frmManageApplicationTypes()
        {
            InitializeComponent();
        }

        void _Load_RefreshApplicationTypesInDGV()
        {
            DataTable dt = clsApplicationTypes.GetAllApplicationTypes();

            if (dt.Rows.Count > 0)
            {
                dt.Columns["ApplicationTypeID"].ColumnName = "ID";
                dt.Columns["ApplicationTypeTitle"].ColumnName = "Title";
                dt.Columns["ApplicationFees"].ColumnName = "Fees";

                dgvApplicationTypes.DataSource = dt.DefaultView;
                dgvApplicationTypes.Columns["ID"].FillWeight = 15;
                dgvApplicationTypes.Columns["Title"].FillWeight = 65;
                dgvApplicationTypes.Columns["Fees"].FillWeight = 15;

                lblNumOfRecords.Text = dt.Rows.Count.ToString();
            }
            else
            {
                lblNumOfRecords.Text = "0";
            }
        }

        private void frmManageApplicationTypes_Load(object sender, EventArgs e)
        {
            _Load_RefreshApplicationTypesInDGV();
        }

        private void gbtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            frmUpdateApplicationTypes frm = new frmUpdateApplicationTypes
                (Convert.ToInt32(dgvApplicationTypes.SelectedCells[0].Value));

            frm.OnClose += FrmUpdate_OnClose;
            frm.ShowDialog();
        }

        private void FrmUpdate_OnClose()
        {
            _Load_RefreshApplicationTypesInDGV();
        }
    }
}

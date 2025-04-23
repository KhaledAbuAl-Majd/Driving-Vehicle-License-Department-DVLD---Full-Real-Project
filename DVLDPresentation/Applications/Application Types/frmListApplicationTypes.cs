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
    public partial class frmListApplicationTypes : Form
    {
        private DataTable _dtAllApplicationTypes;
        public frmListApplicationTypes()
        {
            InitializeComponent();
        }

        void _RefereshApplicationTypesList()
        {
            _dtAllApplicationTypes = clsApplicationType.GetAllApplicationTypes();
            dgvApplicationTypes.DataSource = _dtAllApplicationTypes;
            lblNumOfRecords.Text = _dtAllApplicationTypes.Rows.Count.ToString();
        }

        private void frmManageApplicationTypes_Load(object sender, EventArgs e)
        {
            _RefereshApplicationTypesList();

            if (_dtAllApplicationTypes.Rows.Count > 0)
            {
                dgvApplicationTypes.Columns[0].HeaderText = "ID";
                dgvApplicationTypes.Columns[0].Width = 110;

                dgvApplicationTypes.Columns[1].HeaderText = "Title";
                dgvApplicationTypes.Columns[1].Width = 400;

                dgvApplicationTypes.Columns[2].HeaderText = "Fees";
                dgvApplicationTypes.Columns[2].Width = 100;

            }
        }

        private void gbtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void EdittoolStripMenuItem3_Click(object sender, EventArgs e)
        {
            frmEditApplicationTypes frm = new frmEditApplicationTypes(Convert.ToInt32(dgvApplicationTypes.SelectedCells[0].Value));

            frm.OnSave += _RefereshApplicationTypesList;
            frm.ShowDialog();
        }
    }
}

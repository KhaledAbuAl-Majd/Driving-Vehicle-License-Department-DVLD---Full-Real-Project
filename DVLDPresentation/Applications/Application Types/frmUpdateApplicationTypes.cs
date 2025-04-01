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
using Guna.UI2.WinForms;

namespace DVLDPresentation.Applications.Application_Types
{
    public partial class frmUpdateApplicationTypes : Form
    {
        public event Action OnClose;
        bool _IsSave = false;

        clsApplicationTypes _ApplicationType;
        public frmUpdateApplicationTypes(int ApplicationTypeID)
        {
            InitializeComponent();

            _ApplicationType = clsApplicationTypes.FindApplicationType(ApplicationTypeID);
        }
        void _FillDataFromObjectToForm()
        {
            lblID.Text = _ApplicationType.ApplicationTypeID.ToString();
            gtxtTitle.Text = _ApplicationType.ApplicationTypeTitle;
            gtxtFees.Text = _ApplicationType.ApplicationFees.ToString();

        }
        private void frmUpdateApplicationTypes_Load(object sender, EventArgs e)
        {
            if (_ApplicationType != null)
                _FillDataFromObjectToForm();
        }
  
        private void gbtnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(gtxtTitle.Text))
            {
                MessageBox.Show("Title Cannot be empty !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(gtxtFees.Text))
            {
                MessageBox.Show("Fees Cannot be empty !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _ApplicationType.ApplicationTypeTitle = gtxtTitle.Text;
            _ApplicationType.ApplicationFees = Convert.ToSingle(gtxtFees.Text);

            if (_ApplicationType.Save())
            {
                MessageBox.Show("Data Saved successfully", "Suscced", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _IsSave = true;
            }
            else
            {
                MessageBox.Show("Failed To Save", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _IsSave = false;
            }
        }

        private void gtxtFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            if(e.KeyChar == '.' && ((Guna2TextBox)sender).Text.Contains('.'))
            {
                e.Handled = true;
            }
        }

        private void gbtnClose_Click(object sender, EventArgs e)
        {
            if (_IsSave)
                if (OnClose != null)
                    OnClose();

            this.Close();
        }
    }
}

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

namespace DVLDPresentation.Applications.Test_Types
{
    public partial class frmUpdateTestTypes : Form
    {
        public event Action OnClose;
        private clsTestTypes _TestType;
        bool _IsSave = false;
        public frmUpdateTestTypes(int TestTypeID)
        {
            InitializeComponent();
            _TestType = clsTestTypes.FindTestType(TestTypeID);
        }

        void _FillDataFromObjectToForm()
        {
            lblID.Text = _TestType.TestTypeID.ToString();
            gtxtTitle.Text = _TestType.TestTypeTitle;
            gtxtDescription.Text = _TestType.TestTypeDescription;
            gtxtFees.Text = _TestType.TestFees.ToString();
        }

        private void frmUpdateTestTypes_Load(object sender, EventArgs e)
        {
            if (_TestType != null)
                _FillDataFromObjectToForm();
        }

        private void gbtnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(gtxtTitle.Text))
            {
                MessageBox.Show("Title Cannot be empty !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(gtxtDescription.Text))
            {
                MessageBox.Show("Description Cannot be empty !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(gtxtFees.Text))
            {
                MessageBox.Show("Fees Cannot be empty !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _TestType.TestTypeTitle = gtxtTitle.Text;
            _TestType.TestTypeDescription = gtxtDescription.Text;
            _TestType.TestFees = Convert.ToSingle(gtxtFees.Text);

            if (_TestType.Save())
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

            if (e.KeyChar == '.' && ((Guna2TextBox)sender).Text.Contains('.'))
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

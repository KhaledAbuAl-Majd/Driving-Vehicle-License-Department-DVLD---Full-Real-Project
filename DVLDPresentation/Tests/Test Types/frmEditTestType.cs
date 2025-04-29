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
        public event Action OnSave;

        private clsTestType.enTestType _TestTypeID = clsTestType.enTestType.VisionTest;
        private clsTestType _TestType;

        public frmUpdateTestTypes(clsTestType.enTestType TestTypeID)
        {
            InitializeComponent();
            _TestTypeID = TestTypeID;
        }

        private void frmUpdateTestTypes_Load(object sender, EventArgs e)
        {
            _TestType = clsTestType.Find(_TestTypeID);

            if (_TestType != null)
            {
                lblID.Text = Convert.ToInt32(_TestType.TestTypeID).ToString();
                gtxtTitle.Text = _TestType.TestTypeTitle;
                gtxtDescription.Text = _TestType.TestTypeDescription;
                gtxtFees.Text = _TestType.TestTypeFees.ToString();
            }

            else

            {
                MessageBox.Show("Could not find Test Type with id = " + _TestTypeID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
        }

        private void gbtnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _TestType.TestTypeTitle = gtxtTitle.Text;
            _TestType.TestTypeDescription = gtxtDescription.Text;
            _TestType.TestTypeFees = Convert.ToSingle(gtxtFees.Text);

            if (_TestType.Save())
            {
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (OnSave != null)
                    OnSave();
            }
            else
            {
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            this.Close();
        }

        private void gtxtTitle_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(gtxtTitle.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(gtxtTitle, "Title cannot be empty!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(gtxtTitle, null);
            }
        }

        private void gtxtDescription_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(gtxtDescription.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(gtxtDescription, "Description cannot be empty!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(gtxtDescription, null);
            }
        }

        private void gtxtFees_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(gtxtFees.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(gtxtFees, "Fees cannot be empty!");
                return;
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(gtxtFees, null);
            }

            //Valiated it by Key Press
            //int and float
            //if (!clsValidation.IsNumber(gtxtFees.Text))
            //{
            //    e.Cancel = true;
            //    errorProvider1.SetError(gtxtFees, "Invalid Number.");
            //}
            //else
            //{
            //    e.Cancel = false;
            //    errorProvider1.SetError(gtxtFees, null);
            //}
        }
    }
}

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
using DVLDPresentation.Global_Classes;
using Guna.UI2.WinForms;

namespace DVLDPresentation.Applications.Application_Types
{
    public partial class frmEditApplicationTypes : Form
    {
        public event Action OnSave;

        int _ApplicationTypeID;
        clsApplicationType _ApplicationType;
        public frmEditApplicationTypes(int ApplicationTypeID)
        {
            InitializeComponent();
            this._ApplicationTypeID = ApplicationTypeID;          
        }
        private void frmUpdateApplicationTypes_Load(object sender, EventArgs e)
        {
            _ApplicationType = clsApplicationType.Find(_ApplicationTypeID);

            if (_ApplicationType == null)
            {
                MessageBox.Show($"No Application Type With ID = [{_ApplicationTypeID}]", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            lblID.Text = _ApplicationType.ApplicationTypeID.ToString();
            gtxtTitle.Text = _ApplicationType.ApplicationTypeTitle;
            gtxtFees.Text = _ApplicationType.ApplicationFees.ToString();
        } 

        private void gbtnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _ApplicationType.ApplicationTypeTitle = gtxtTitle.Text.Trim();
            _ApplicationType.ApplicationFees = Convert.ToSingle(gtxtFees.Text.Trim());

            if (_ApplicationType.Save())
            {
                MessageBox.Show("Data Saved successfully", "Suscced", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (OnSave != null)
                    OnSave();
            }
            else
            {
                MessageBox.Show("Failed To Save", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gtxtFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            //to allow only one Dot
            if(e.KeyChar == '.' && ((Guna2TextBox)sender).Text.Contains('.'))
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

        private void gtxtFees_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(gtxtFees.Text.Trim()))
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

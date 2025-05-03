using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLDBusiness;
using DVLDPresentation.People;

namespace DVLDPresentation.Controls
{
    public partial class ctrlPersonCardWithFilter : UserControl
    {
        public event Action<int> OnPersonSelected;
    
        private bool _ShowAddPerson = true;
        public bool ShowAddPerson
        {
            get { return _ShowAddPerson; }
            set
            {
                _ShowAddPerson = value;
                btnAddPerson.Visible = _ShowAddPerson;
            }
        }

        private bool _FilterEnabled = true;
        public bool FilterEnabled
        {
            get { return _FilterEnabled; }
            set
            {
                _FilterEnabled = value;
                gbFilterBy.Enabled = _FilterEnabled;
            }
        }
        public ctrlPersonCardWithFilter()
        {
            InitializeComponent();
        }
        public int PersonID
        {
            get
            {
                return ctrlPersonCard1.PersonID;
            }
        }
        public clsPerson SelectedPersonInfo
        {
            get { return ctrlPersonCard1.SelectedPersonInfo; }
        }

        public void LoadPersonInfo(int PersonID)
        {
            gcbFilterBy.SelectedIndex = 1;
            gtxtFilterValue.Text = PersonID.ToString();
            _FindNow();
        }
        void _FindNow()
        {
            switch (gcbFilterBy.Text)
            {
                case "Person ID":
                    ctrlPersonCard1.LoadPersonInfo(int.Parse(gtxtFilterValue.Text));
                    break;

                case "National No":
                    ctrlPersonCard1.LoadPersonInfo(gtxtFilterValue.Text);
                    break;
            }

            //FilterEnabled to know if he search for first time and don't load data
            if (OnPersonSelected != null && _FilterEnabled)
                OnPersonSelected(PersonID);
        }
        private void gcbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            gtxtFilterValue.Text = "";
            FilterFocus();
        }
        private void btnFindPerson_Click(object sender, EventArgs e)
        {
            if (!ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FindNow();
        }
        private void ctrlPersonCardWithFilter_Load(object sender, EventArgs e)
        {
            gcbFilterBy.SelectedIndex = 1;
            FilterFocus();
        }
        private void gtxtFilterValue_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(gtxtFilterValue.Text) && PersonID == -1)
            {
                e.Cancel = true;
                errorProvider1.SetError(gtxtFilterValue, $"{gcbFilterBy.Text} Must have a value!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(gtxtFilterValue, "");
            }
        }
        private void btnAddPerson_Click(object sender, EventArgs e)
        {
            frmAddUpdatePerson frm = new frmAddUpdatePerson();
            frm.DataBack += DataBackEvent;
            frm.ShowDialog();
        }
        private void DataBackEvent(object sender, int PersonID)
        {
            gcbFilterBy.SelectedIndex = gcbFilterBy.FindString("Person ID");
            gtxtFilterValue.Text = PersonID.ToString();
            ctrlPersonCard1.LoadPersonInfo(PersonID);

            if (OnPersonSelected != null && _FilterEnabled)
                OnPersonSelected(PersonID);
        }
        public void FilterFocus()
        {
            gtxtFilterValue.Focus();
        }
        private void gtxtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Check if the pressed key is Enter (character code 13)
            //e.KeyChar == (char)13

            if (e.KeyChar == (char)Keys.Enter)
                btnFindPerson.PerformClick();

            if (gcbFilterBy.Text == "Person ID")
                e.Handled = (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar));
        }
    }
}

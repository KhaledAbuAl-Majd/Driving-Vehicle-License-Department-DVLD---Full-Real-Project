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
        //to mark if i add new user or add a new person in another usage
        public bool IsToAddNewUser = true;
        public ctrlPersonCardWithFilter()
        {
            InitializeComponent();
        }
        public void UpdateMode(int PersonID)
        {
            ctrlPersonCard1.RefreshPersonData(PersonID);
            this.PersonID = PersonID;
            gtxtFilterValue.Text = PersonID.ToString();
            gbFilterBy.Enabled = false;
        }

        public int PersonID = -1;
        private void _EmptyFilterText()
        {
            gtxtFilterValue.Text = "";
        }
        private void _SearchPerson()
        {
            if (string.IsNullOrEmpty(gtxtFilterValue.Text))
            {
                MessageBox.Show($"{gcbFilterBy.Text} Must have a value!",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                gtxtFilterValue.Focus();
                return;
            }

            if (gcbFilterBy.Text == "National No")
            {
                string NationalNo = gtxtFilterValue.Text;

                if (clsPeople.IsPersonExist(NationalNo))
                {
                    int PersonID = clsPeople.Find(gtxtFilterValue.Text).PersonID;

                    if (IsToAddNewUser)
                        if (clsUsers.IsUserExistByPersonID(PersonID))
                        {
                            MessageBox.Show("Selected Person already has a user, choose antoher one.",
                            "Select another Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            gtxtFilterValue.Focus();
                            ctrlPersonCard1.EmptyPersonInformationAtDesign();
                        }
                        else
                        {
                            ctrlPersonCard1.RefreshPersonData(PersonID);
                            this.PersonID = ctrlPersonCard1._Person.PersonID;
                        }
                    else
                    {
                        ctrlPersonCard1.RefreshPersonData(PersonID);
                        this.PersonID = ctrlPersonCard1._Person.PersonID;
                    }

                }
                else
                {
                    MessageBox.Show("No Person With National No = " + NationalNo,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.PersonID = -1;

                    ctrlPersonCard1.EmptyPersonInformationAtDesign();
                }


            }
            else
            {
                int PersonID = Convert.ToInt32(gtxtFilterValue.Text);

                if (clsPeople.IsPersonExist(PersonID))
                {
                    if (IsToAddNewUser)
                        if (clsUsers.IsUserExistByPersonID(PersonID))
                        {
                            MessageBox.Show("Selected Person already has a user, choose antoher one.",
                            "Select another Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            gtxtFilterValue.Focus();
                            ctrlPersonCard1.EmptyPersonInformationAtDesign();
                        }
                        else
                        {
                            ctrlPersonCard1.RefreshPersonData(PersonID);
                            this.PersonID = ctrlPersonCard1._Person.PersonID;
                        }
                    else
                    {
                        ctrlPersonCard1.RefreshPersonData(PersonID);
                        this.PersonID = ctrlPersonCard1._Person.PersonID;
                    }
                }
                else
                {
                    MessageBox.Show("No Person With Person ID = " + PersonID,
                      "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.PersonID = -1;
                    ctrlPersonCard1.EmptyPersonInformationAtDesign();
                }
                
            }
        }
        private void _AddNewPerson()
        {
            frmAdd_EditPerson frm = new frmAdd_EditPerson(-1);

            frm.DataBackOnClose += FrmAddEdit_DataBackOnClose;

            frm.ShowDialog();
        }

        private void FrmAddEdit_DataBackOnClose(object sender, int PersonID)
        {
            this.PersonID = PersonID;
            ctrlPersonCard1.RefreshPersonData(PersonID);
            gcbFilterBy.SelectedIndex = gcbFilterBy.FindString("Person ID");
            gtxtFilterValue.Text = PersonID.ToString();
        }

        private void gtxtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (gcbFilterBy.Text != "Person ID")
                return;

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        private void btnSearchPerson_Click(object sender, EventArgs e)
        {
            _SearchPerson();
        }

        private void ctrlPersonCardWithFilter_Load(object sender, EventArgs e)
        {
            gcbFilterBy.SelectedIndex = gcbFilterBy.FindString("Person ID");
            ctrlPersonCard1.RefreshPersonData(PersonID);
        }

        private void gcbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            _EmptyFilterText();
        }

        private void btnAddPerson_Click(object sender, EventArgs e)
        {
            _AddNewPerson();
        }

        private void gtxtFilterValue_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(gtxtFilterValue.Text))
            {
                errorProvider1.SetError(gtxtFilterValue, $"{gcbFilterBy.Text} Must have a value!");
            }
            else
                errorProvider1.SetError(gtxtFilterValue, "");
        }
    }
}

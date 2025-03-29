using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLDPresentation.People
{
    public partial class frmAdd_EditPerson : Form
    {
        public event Action OnClose;
        int _PersonID;
        private void _CloseFormAndUpdateData()
        {
            Action handler = OnClose;

            if (handler != null)
                handler();
        }
        public frmAdd_EditPerson(int PersonID)
        {
            InitializeComponent();
            ctrAdd_EditPerson1.PersonID = PersonID;
            ctrAdd_EditPerson1.SaveDataBack += CtrAdd_EditPerson1_SaveDataBack;
            ctrAdd_EditPerson1.OnClose += ctrAdd_EditPerson1_OnClose;
            _PersonID = PersonID;
        }

        private void _ChangeHeader(string Text)
        {
            lblHeader.Text = Text;
        }
        private void _ChangePersonIDValue()
        {
            lblPersonID.Text = _PersonID.ToString();
        }
        private void _UpdateMode()
        {
            if (lblHeader.Text == "Update Person")
                return;

            _ChangePersonIDValue();
            _ChangeHeader("Upadte Person");
        }
        private void _AddNewMode()
        {
            _ChangeHeader("Add New Person");
        }

        private void CtrAdd_EditPerson1_SaveDataBack(object sender, int PersonID)
        {
            _PersonID = PersonID;
            lblPersonID.Text = PersonID.ToString();
            _UpdateMode();
        }

        private void ctrAdd_EditPerson1_OnClose()
        {
            if (ctrAdd_EditPerson1._IsSave)
                _CloseFormAndUpdateData();
            this.Close();
        }

        private void frmAdd_EditPerson_Load(object sender, EventArgs e)
        {
            if (_PersonID == -1)
                _AddNewMode();
            else
                _UpdateMode();
        }

        private void ctrAdd_EditPerson1_Load(object sender, EventArgs e)
        {

        }
    }
}

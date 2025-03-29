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

namespace DVLDPresentation.People
{
    public partial class frmPersonDetails : Form
    {
        public event Action OnClose;
        public frmPersonDetails(int PersonID)
        {
            InitializeComponent();
            ctrPersonCard1._PersonID = PersonID;
            ctrPersonCard1.OnClose += CtrPersonCard1_OnClose;
        }

        private void CtrPersonCard1_OnClose()
        {
            if (OnClose != null)
                OnClose();
        }

        private void gbtnClose_Click(object sender, EventArgs e)
        {    
            this.Close();
        }

      
    }
}

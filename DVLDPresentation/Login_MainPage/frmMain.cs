using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLDPresentation.People;
using DVLDPresentation.Users;

namespace DVLDPresentation.Login_HomePage
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }


        //frmManagePeople frm = new frmManagePeople();
        private void peopleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (frm.IsDisposed)
            //{
            //    frm = new frmManagePeople();
            //}

            //frm.MdiParent = this;
            //frm.Show();
            //frm.BringToFront();

            frmManagePeople frm = new frmManagePeople();
            frm.ShowDialog();

        }

        private void frmMain_Load(object sender, EventArgs e)
        {
          
        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManageUsers frm = new frmManageUsers();

            frm.ShowDialog();
        }
    }
}

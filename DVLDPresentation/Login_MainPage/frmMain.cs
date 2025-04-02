using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLDPresentation.Applications;
using DVLDPresentation.Applications.Test_Types;
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

        private void currentUserInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUserInfo frm = new frmUserInfo(clsGlobalSettings.LoginedInUserID);

            frm.ShowDialog();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChangePassword frm = new frmChangePassword(clsGlobalSettings.LoginedInUserID);

            frm.ShowDialog();
        }

        private void signOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void drivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void manageApplicationTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManageApplicationTypes frm = new frmManageApplicationTypes();

            frm.ShowDialog();
        }

        private void manageTestTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManageTestTypes frm = new frmManageTestTypes();

            frm.ShowDialog();
        }
    }
}

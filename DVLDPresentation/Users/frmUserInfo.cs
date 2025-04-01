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

namespace DVLDPresentation.Users
{
    public partial class frmUserInfo : Form
    {

        clsUsers User;

        public event Action OnClose;
        public frmUserInfo(int UserID)
        {
            InitializeComponent();
            User = clsUsers.Find(UserID);

            if (User != null)
            {
                ctrPersonCard1.PersonID = User.PersonID;
                _FillLoginInfomationFromUserToForm();
            }
        }

        void _FillLoginInfomationFromUserToForm()
        {
            lblUserID.Text = User.UserID.ToString();
            lblUserName.Text = User.UserName;
            lblIsActive.Text = (User.IsActive) ? "Yes" : "No";
        }
        private void gbtnClose_Click_1(object sender, EventArgs e)
        {
            if (OnClose != null)
                OnClose();

            this.Close();
        }
    }
}

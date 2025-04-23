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

namespace DVLDPresentation.Users.Controls
{
    public partial class ctrUserCard : UserControl
    {
        public int UserID { get; private set; }
        public clsUser SelectedUserInfo { get; private set; }

        public ctrUserCard()
        {
            InitializeComponent();
        }


        public void LoadUserInfo(int UserID)
        {
            this.UserID = UserID;
            SelectedUserInfo = clsUser.FindByUserID(UserID);
            if (SelectedUserInfo == null)
            {
                _ResetUserInfo();
                MessageBox.Show("No User with UserID = " + UserID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FillUserInfo();
        }
        void _FillUserInfo()
        {
            ctrPersonCard1.LoadPersonInfo(SelectedUserInfo.PersonID);

            lblUserID.Text = SelectedUserInfo.UserID.ToString();
            lblUserName.Text = SelectedUserInfo.UserName;
            lblIsActive.Text = (SelectedUserInfo.IsActive) ? "Yes" : "No";

        }
        void _ResetUserInfo()
        {
            ctrPersonCard1.ResetPersonInfo();
            lblUserID.Text = "N/A";
            lblUserName.Text = "[????]";
            lblIsActive.Text = "[????]";
        }

    }
}


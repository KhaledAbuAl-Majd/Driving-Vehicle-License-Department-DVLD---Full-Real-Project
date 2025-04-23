using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLDPresentation.Users
{
    public partial class frmUserInfo : Form
    {
        //public event Action OnClose;

        int _UserID;
        public frmUserInfo(int UserID)
        {
            InitializeComponent();
            this._UserID = UserID;
        }

        private void gbtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmUserInfo_Load(object sender, EventArgs e)
        {
            ctrUserCard1.LoadUserInfo(_UserID);
        }
    }
}

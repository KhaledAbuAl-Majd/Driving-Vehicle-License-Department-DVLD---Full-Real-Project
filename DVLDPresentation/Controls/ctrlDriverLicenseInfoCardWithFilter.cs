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

namespace DVLDPresentation.Controls
{
    public partial class ctrlDriverLicenseInfoCardWithFilter : UserControl
    {
        public event Action OnErrorAtSearch;

        public delegate void SearchDataBack(int PersonID, clsLicenses LocalLicense, object sender);
        public event SearchDataBack OnSuccedAtSearch;
        public enum enMode { NewInternationalLicense, RenewLocalLicense, ReplacementForDamaged_Lost }
        public enMode Mode;
        public void ChangeEnaplityOfGBFilterBy(bool value)
        {
            gbFilterBy.Enabled = value;
        }
        void _RaiseEventErrorAtSearch()
        {
            if (OnErrorAtSearch != null)
                OnErrorAtSearch();
        }
        void _InvokeDelegateSearchDataBack(int PersonID, clsLicenses LocalLicense)
        {
            if (OnSuccedAtSearch != null)
                OnSuccedAtSearch.Invoke(PersonID, LocalLicense,this);
        }
        public ctrlDriverLicenseInfoCardWithFilter()
        {
            InitializeComponent();
        }
        void _ErrorAtSearch(string Text, string Caption)
        {
            MessageBox.Show(Text, Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            gtxtFilterValue.Focus();
            ctrlDriverLicenseInfo1._EmptyLabels();
            _RaiseEventErrorAtSearch();
        }
        bool _CheckIsNullOrWhiteSpace()
        {
            if (string.IsNullOrWhiteSpace((gtxtFilterValue.Text)))
            {
                _ErrorAtSearch("Licnese ID Cannot by empty, Please Enter a License ID!", "Error");
                return true;
            }
            return false;
        }
        bool _CheckIsLicenseNotExist(int EnteredLicenseID)
        {
            if (!clsLicenses.IsLicenseExist(EnteredLicenseID))
            {
                _ErrorAtSearch($"Not License With Licnese ID  = {EnteredLicenseID}, Please Enter a Correct One!", "Not Exist");
                return true;
            }

            return false;
        }
        bool _CheckIsLicenseNotExpired(clsLicenses LocalLicense)
        {
            if (!(LocalLicense.ExpirationDate < DateTime.Today))
            {
                _ErrorAtSearch($"Selected License is not yet expiared, it will expire on:{LocalLicense.ExpirationDate}", "Not allowed");
                return true;
            }

            return false;
        }
        bool _CheckIsLicenseNotActive(clsLicenses License)
        {
            if (!License.IsAcitve)
            {
                _ErrorAtSearch("Selected License is Not Active, Choose an active License", "Not allowed");
                return true;
            }

            return false;
        }
        bool _CheckIsPersonHaveActiveInternationalLicense(int PersonID)
        {
            int SearchedInternationalLicenseID = clsInternationalLicenses.GetInternationalLicenseIfPersonHasActiveOne(PersonID);
            if (SearchedInternationalLicenseID != -1)
            {
                _ErrorAtSearch($"Person already have an active international license with ID = {SearchedInternationalLicenseID}", "Not allowed");
                return true;
            }

            return false;
        }
        void _SearchAtLicenseForInternatinalLicense()
        {
            if (_CheckIsNullOrWhiteSpace())
                return;

            int EnteredLicenseID = Convert.ToInt32(gtxtFilterValue.Text);

            if (_CheckIsLicenseNotExist(EnteredLicenseID))
                return;

            clsLicenses LocalLicense = clsLicenses.FindByLicenseID(EnteredLicenseID);
            int PersonID = clsDrivers.FindByDriverID(LocalLicense.DriverID).PersonID;

            if (_CheckIsPersonHaveActiveInternationalLicense(PersonID))
                return;
            

            //Class 3 - Ordinary Driving License
            if (LocalLicense.LicenseClassID != 3)
            {
                _ErrorAtSearch("License Must be Class 3 - Ordinary Driving License!", "Not Allowed");
                return;
            }

            ctrlDriverLicenseInfo1.FillDataInLabels(LocalLicense.LicneseID);

            _InvokeDelegateSearchDataBack(PersonID, LocalLicense);
        }
        void _SearchAtLocalLicenseToRenew()
        {
            if (_CheckIsNullOrWhiteSpace())
                return;
            
            int EnteredLicenseID = Convert.ToInt32(gtxtFilterValue.Text);

            if (_CheckIsLicenseNotExist(EnteredLicenseID))
                return;         

            clsLicenses LocalLicense = clsLicenses.FindByLicenseID(EnteredLicenseID);
            int PersonID = clsDrivers.FindByDriverID(LocalLicense.DriverID).PersonID;

            if (_CheckIsLicenseNotExpired(LocalLicense))
                return;
            

            if (_CheckIsLicenseNotActive(LocalLicense))
                return;
            

            ctrlDriverLicenseInfo1.FillDataInLabels(LocalLicense.LicneseID);

            _InvokeDelegateSearchDataBack(PersonID, LocalLicense);
        }
        void _SearchAtLocalLicenseToReplacementForDamagedOrLost()
        {
            if (_CheckIsNullOrWhiteSpace())
                return;

            int EnteredLicenseID = Convert.ToInt32(gtxtFilterValue.Text);

            if (_CheckIsLicenseNotExist(EnteredLicenseID))
                return;

            clsLicenses LocalLicense = clsLicenses.FindByLicenseID(EnteredLicenseID);
            int PersonID = clsDrivers.FindByDriverID(LocalLicense.DriverID).PersonID;

            if (_CheckIsLicenseNotActive(LocalLicense))
                return;

            ctrlDriverLicenseInfo1.FillDataInLabels(LocalLicense.LicneseID);

            _InvokeDelegateSearchDataBack(PersonID, LocalLicense);
        }
        private void btnLicenseSearch_Click(object sender, EventArgs e)
        {
            switch (Mode)
            {
                case enMode.NewInternationalLicense:
                    _SearchAtLicenseForInternatinalLicense();
                    break;

                case enMode.RenewLocalLicense:
                    _SearchAtLocalLicenseToRenew();
                    break;

                case enMode.ReplacementForDamaged_Lost:
                    _SearchAtLocalLicenseToReplacementForDamagedOrLost();
                    break;
            }
        }

        private void gtxtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }
    }
}

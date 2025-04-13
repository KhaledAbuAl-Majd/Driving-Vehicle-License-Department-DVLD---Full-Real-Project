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
        public enum enMode { NewInternationalLicense,RenewLocalLicense}
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
        void _SearchAtLicenseForInternatinalLicense()
        {
            if (string.IsNullOrWhiteSpace((gtxtFilterValue.Text)))
            {
                _ErrorAtSearch("Licnese ID Cannot by empty, Please Enter a License ID!", "Error");
                return;
            }

            int EnteredLicenseID = Convert.ToInt32(gtxtFilterValue.Text);

            if (!clsLicenses.IsLicenseExist(EnteredLicenseID))
            {
                _ErrorAtSearch($"Not License With Licnese ID  = {EnteredLicenseID}, Please Enter a Correct One!", "Not Exist");
                return;
            }

            clsLicenses LocalLicense = clsLicenses.FindByLicenseID(EnteredLicenseID);
            clsDrivers Driver = clsDrivers.FindByDriverID(LocalLicense.DriverID);

            int SearchedInternationalLicenseID = clsInternationalLicenses.GetInternationalLicenseIfPersonHasActiveOne(Driver.PersonID);
            if (SearchedInternationalLicenseID != -1)
            {
                _ErrorAtSearch($"Person already have an active international license with ID = {SearchedInternationalLicenseID}", "Not allowed");
                return;
            }

            //Class 3 - Ordinary Driving License
            if (LocalLicense.LicenseClassID != 3)
            {
                _ErrorAtSearch("License Must be Class 3 - Ordinary Driving License!", "Not Allowed");
                return;
            }

            ctrlDriverLicenseInfo1.FillDataInLabels(LocalLicense.LicneseID);

            _InvokeDelegateSearchDataBack(Driver.PersonID, LocalLicense);
        }
        bool _IsLicenseExpired(clsLicenses LocalLicense)
        {
            return (LocalLicense.ExpirationDate < DateTime.Today);
        }
        void _SearchAtLocalLicenseToRenew()
        {
            if (string.IsNullOrWhiteSpace((gtxtFilterValue.Text)))
            {
                _ErrorAtSearch("Licnese ID Cannot by empty, Please Enter a License ID!", "Error");
                return;
            }

            int EnteredLicenseID = Convert.ToInt32(gtxtFilterValue.Text);

            if (!clsLicenses.IsLicenseExist(EnteredLicenseID))
            {
                _ErrorAtSearch($"Not License With Licnese ID  = {EnteredLicenseID}, Please Enter a Correct One!", "Not Exist");
                return;
            }

            clsLicenses LocalLicense = clsLicenses.FindByLicenseID(EnteredLicenseID);
            clsDrivers Driver = clsDrivers.FindByDriverID(LocalLicense.DriverID);

            if (!_IsLicenseExpired(LocalLicense))
            {
                _ErrorAtSearch($"Selected License is not yet expiared, it will expire on:{LocalLicense.ExpirationDate}","Not allowed");
                return;
            }

            if (!LocalLicense.IsAcitve)
            {
                _ErrorAtSearch("Selected License is Not Active!", "Not allowed");
                return;
            }

            ctrlDriverLicenseInfo1.FillDataInLabels(LocalLicense.LicneseID);

            _InvokeDelegateSearchDataBack(Driver.PersonID, LocalLicense);
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
            }
        }

        private void gtxtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }
    }
}

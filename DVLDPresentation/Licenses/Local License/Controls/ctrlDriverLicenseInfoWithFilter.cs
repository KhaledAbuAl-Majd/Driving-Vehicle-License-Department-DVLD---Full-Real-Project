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
    public partial class ctrlDriverLicenseInfoWithFilter : UserControl
    {
        public event Action OnErrorAtSearch;
        public event Action<int> OnLicenseSelected;

        public ctrlDriverLicenseInfoWithFilter()
        {
            InitializeComponent();
        }

        private bool _FilterEnabled = true;

        public bool FilterEnabled
        {
            get
            {
                return _FilterEnabled;
            }
            set
            {
                _FilterEnabled = value;
                gbFilters.Enabled = _FilterEnabled;
            }
        }

        private int _LicenseID = -1;

        public int LicenseID
        {
            get { return ctrlDriverLicenseInfo1.LicenseID; }
        }

        public clsLicense SelectedLicenseInfo
        { get { return ctrlDriverLicenseInfo1.SelectedLicenseInfo; } }

        public void LoadLicenseInfo(int LicenseID)
        {
            gtxtFilterValue.Text = LicenseID.ToString();
            ctrlDriverLicenseInfo1.LoadInfo(LicenseID);
            _LicenseID = ctrlDriverLicenseInfo1.LicenseID;

            if (OnLicenseSelected != null && FilterEnabled)
                // Raise the event with a parameter
                OnLicenseSelected(_LicenseID);
        }

        private void gtxtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar));

            if (e.KeyChar == (char)Keys.Enter)
                btnFind.PerformClick();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtLicenseIDFocus();
                return;

            }

            _LicenseID = int.Parse(gtxtFilterValue.Text);
            LoadLicenseInfo(_LicenseID);
        }

        public void txtLicenseIDFocus()
        {
            gtxtFilterValue.Focus();
        }

        private void gtxtFilterValue_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(gtxtFilterValue.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(gtxtFilterValue, "This field is required!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(gtxtFilterValue, null);
            }
        }


        private void ctrlDriverLicenseInfo1_OnErrorAtSearch()
        {
            if (OnErrorAtSearch != null)
                OnErrorAtSearch();
        }



        //bool _CheckIsNullOrWhiteSpace()
        //{
        //    if (string.IsNullOrWhiteSpace((gtxtFilterValue.Text)))
        //    {
        //        _ErrorAtSearch("Licnese ID Cannot by empty, Please Enter a License ID!", "Error");
        //        return true;
        //    }
        //    return false;
        //}
        //bool _CheckIsLicenseNotExist(int EnteredLicenseID)
        //{
        //    if (!clsLicense.IsLicenseExist(EnteredLicenseID))
        //    {
        //        _ErrorAtSearch($"Not License With Licnese ID  = {EnteredLicenseID}, Please Enter a Correct One!", "Not Exist");
        //        return true;
        //    }

        //    return false;
        //}
        //bool _CheckIsLicenseExpired(clsLicense LocalLicense)
        //{
        //    if (LocalLicense.ExpirationDate < DateTime.Today)
        //    {
        //        _ErrorAtSearch($"Selected License is expiared, Choose another one!", "Not allowed");
        //        return true;
        //    }

        //    return false;
        //}
        //bool _CheckIsLicenseNotExpired(clsLicense LocalLicense)
        //{
        //    if (!(LocalLicense.ExpirationDate < DateTime.Today))
        //    {
        //        _ErrorAtSearch($"Selected License is not yet expiared, it will expire on:{LocalLicense.ExpirationDate}", "Not allowed");
        //        return true;
        //    }

        //    return false;
        //}
        //bool _CheckIsLicenseNotActive(clsLicense License)
        //{
        //    if (!License.IsActive)
        //    {
        //        _ErrorAtSearch("Selected License is Not Active, Choose an active License", "Not allowed");
        //        return true;
        //    }

        //    return false;
        //}
        //bool _CheckIsPersonHaveActiveInternationalLicense(int PersonID)
        //{
        //    int SearchedInternationalLicenseID = clsInternationalLicense.GetActiveInternationalLicenseIDByPersonID(PersonID);
        //    if (SearchedInternationalLicenseID != -1)
        //    {
        //        _ErrorAtSearch($"Person already have an active international license with ID = {SearchedInternationalLicenseID}", "Not allowed");
        //        return true;
        //    }

        //    return false;
        //}
        //bool _CheckIsLicenseDetained(int LicenseID)
        //{
        //    if (clsDetainedLicense.IsLicenseDetained(LicenseID))
        //    {
        //        _ErrorAtSearch("Selected License is already detained, Choose another one.", "Not allowed");
        //        return true;
        //    }

        //    return false;
        //}
        //bool _CheckIsLicenseNotDetained(int LicenseID)
        //{
        //    if (!clsDetainedLicense.IsLicenseDetained(LicenseID))
        //    {
        //        _ErrorAtSearch("Selected License is not detained, Choose another one.", "Not allowed");
        //        return true;
        //    }

        //    return false;
        //}
        //void _SearchAtLicenseForInternatinalLicense()
        //{
        //    if (_CheckIsNullOrWhiteSpace())
        //        return;

        //    int EnteredLicenseID = Convert.ToInt32(gtxtFilterValue.Text);

        //    if (_CheckIsLicenseNotExist(EnteredLicenseID))
        //        return;

        //    clsLicense LocalLicense = clsLicense.Find(EnteredLicenseID);
        //    int PersonID = clsDriver.FindByDriverID(LocalLicense.DriverID).PersonID;

        //    if (_CheckIsLicenseExpired(LocalLicense))
        //        return;

        //    if (_CheckIsPersonHaveActiveInternationalLicense(PersonID))
        //        return;

        //    if (_CheckIsLicenseNotActive(LocalLicense))
        //        return;

        //    //Class 3 - Ordinary Driving License
        //    if (LocalLicense.LicenseClassID != 3)
        //    {
        //        _ErrorAtSearch("License Must be Class 3 - Ordinary Driving License!", "Not Allowed");
        //        return;
        //    }

        //    //ctrlDriverLicenseInfo1.FillDataInLabels(LocalLicense.LicenseID);

        //    _InvokeDelegateSearchDataBack(PersonID, LocalLicense);
        //}
        //void _SearchAtLocalLicenseToRenew()
        //{
        //    if (_CheckIsNullOrWhiteSpace())
        //        return;

        //    int EnteredLicenseID = Convert.ToInt32(gtxtFilterValue.Text);

        //    if (_CheckIsLicenseNotExist(EnteredLicenseID))
        //        return;         

        //    clsLicense LocalLicense = clsLicense.Find(EnteredLicenseID);
        //    int PersonID = clsDriver.FindByDriverID(LocalLicense.DriverID).PersonID;

        //    if (_CheckIsLicenseNotExpired(LocalLicense))
        //        return;


        //    if (_CheckIsLicenseNotActive(LocalLicense))
        //        return;


        //    //ctrlDriverLicenseInfo1.FillDataInLabels(LocalLicense.LicenseID);

        //    _InvokeDelegateSearchDataBack(PersonID, LocalLicense);
        //}
        //void _SearchAtLocalLicenseToReplacementForDamagedOrLost()
        //{
        //    if (_CheckIsNullOrWhiteSpace())
        //        return;

        //    int EnteredLicenseID = Convert.ToInt32(gtxtFilterValue.Text);

        //    if (_CheckIsLicenseNotExist(EnteredLicenseID))
        //        return;

        //    clsLicense LocalLicense = clsLicense.Find(EnteredLicenseID);
        //    int PersonID = clsDriver.FindByDriverID(LocalLicense.DriverID).PersonID;

        //    if (_CheckIsLicenseExpired(LocalLicense))
        //        return;

        //    if (_CheckIsLicenseNotActive(LocalLicense))
        //        return;

        //    if (_CheckIsLicenseDetained(LocalLicense.LicenseID))
        //        return;

        //    //ctrlDriverLicenseInfo1.FillDataInLabels(LocalLicense.LicenseID);

        //    _InvokeDelegateSearchDataBack(PersonID, LocalLicense);
        //}
        //void _SearchAtLocalLicenseToDetain()
        //{
        //    if (_CheckIsNullOrWhiteSpace())
        //        return;

        //    int EnteredLicenseID = Convert.ToInt32(gtxtFilterValue.Text);

        //    if (_CheckIsLicenseNotExist(EnteredLicenseID))
        //        return;

        //    clsLicense LocalLicense = clsLicense.Find(EnteredLicenseID);
        //    int PersonID = clsDriver.FindByDriverID(LocalLicense.DriverID).PersonID;

        //    if (_CheckIsLicenseExpired(LocalLicense))
        //        return;

        //    if (_CheckIsLicenseNotActive(LocalLicense))
        //        return;

        //    if (_CheckIsLicenseDetained(LocalLicense.LicenseID))
        //        return;

        //    //ctrlDriverLicenseInfo1.FillDataInLabels(LocalLicense.LicenseID);

        //    _InvokeDelegateSearchDataBack(PersonID, LocalLicense);
        //}
        //void _SearchAtLocalLicenseToRelease()
        //{
        //    if (_CheckIsNullOrWhiteSpace())
        //        return;

        //    int EnteredLicenseID = Convert.ToInt32(gtxtFilterValue.Text);

        //    if (_CheckIsLicenseNotExist(EnteredLicenseID))
        //        return;

        //    clsLicense LocalLicense = clsLicense.Find(EnteredLicenseID);
        //    int PersonID = clsDriver.FindByDriverID(LocalLicense.DriverID).PersonID;

        //    if (_CheckIsLicenseExpired(LocalLicense))
        //        return;

        //    if (_CheckIsLicenseNotActive(LocalLicense))
        //        return;

        //    if (_CheckIsLicenseNotDetained(LocalLicense.LicenseID))
        //        return;

        //    //ctrlDriverLicenseInfo1.FillDataInLabels(LocalLicense.LicenseID);

        //    _InvokeDelegateSearchDataBack(PersonID, LocalLicense);
        //}
        //public void EnterLicenseIDByCode(int LicenseID)
        //{
        //    gtxtFilterValue.Text = LicenseID.ToString();
        //    btnFind.PerformClick();
        //    gbFilters.Enabled = false;
        //}
        //public void ChangeEnaplityOfGBFilterBy(bool value)
        //{
        //    gbFilters.Enabled = value; 
        //}
        //void _RaiseEventErrorAtSearch()
        //{
        //    if (OnErrorAtSearch != null)
        //        OnErrorAtSearch();
        //}
    }
}

using MyApp.Helper;
using MyApp.Services.APIServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using resx = MyApp.Resources.Languages;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MyApp.MVVM.ViewModels;
using MyApp.Services.SQLiteServices;

namespace MyApp.MVVM.Views.Account
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChangePasswordPage : ContentPage
    {
        AccountServices apiAccount;
        SQLiteSettingServices sqlSetting;
        public ChangePasswordPage()
        {
            InitializeComponent();
            apiAccount = new AccountServices();
            sqlSetting = new SQLiteSettingServices();
        }

        async void btnCancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        async void btnSave_Clicked(object sender, EventArgs e)
        {
            if(!EastariaHelper.IsOnline())
            {
                await DisplayAlert("", resx.AppResource.pleaseOpenNetConn, resx.AppResource.ok);
                return;
            }

            // current password
            if(string.IsNullOrEmpty(txtCurrentPass.Text.Trim()))
            {
                await DisplayAlert("", resx.AppResource.currentPassword + " " + resx.AppResource.required, resx.AppResource.ok);
                return;
            }

            // new password
            if (string.IsNullOrEmpty(txtNewPass.Text.Trim()))
            {
                await DisplayAlert("", resx.AppResource.newPassword + " " + resx.AppResource.required, resx.AppResource.ok);
                return;
            }
            if (txtNewPass.Text.Trim().Length < 6)
            {
                await DisplayAlert("", resx.AppResource.newPassword + " " + resx.AppResource.soShort, resx.AppResource.ok);
                return;
            }

            // confirm password
            if (string.IsNullOrEmpty(txtConfirmPass.Text.Trim()))
            {
                await DisplayAlert("", resx.AppResource.confirmPassword + " " + resx.AppResource.required, resx.AppResource.ok);
                return;
            }
            if (txtNewPass.Text != txtConfirmPass.Text)
            {
                await DisplayAlert("", resx.AppResource.passNotMatched, resx.AppResource.ok);
                return;
            }

            indicator.IsRunning = true;

            var result = await apiAccount.UpdateUserPassword(new UpdateUserPasswordVM
            {
                userId = sqlSetting.GetSetting().userId,
                currentPassword = txtCurrentPass.Text.Trim(),
                newPassword = txtNewPass.Text.Trim()
            });


            if (result.state)
                await DisplayAlert("", resx.AppResource.updatedSuccessfully, resx.AppResource.ok);
            else
                await DisplayAlert("", resx.AppResource.somethingWrong, resx.AppResource.ok);

            indicator.IsRunning = false;

            await Navigation.PopAsync();
        }

        private void btnCurrent_Clicked(object sender, EventArgs e)
        {
            txtCurrentPass.IsPassword = !txtCurrentPass.IsPassword;
            btnCurrent.Source = txtCurrentPass.IsPassword? ImageSource.FromFile("ic_fas_eye") : ImageSource.FromFile("ic_fas_eye_slash");
        }

        private void btnNew_Clicked(object sender, EventArgs e)
        {
            txtNewPass.IsPassword = !txtNewPass.IsPassword;
            btnNew.Source = txtNewPass.IsPassword ? ImageSource.FromFile("ic_fas_eye") : ImageSource.FromFile("ic_fas_eye_slash");
        }

        private void btnConfirm_Clicked(object sender, EventArgs e)
        {
            txtConfirmPass.IsPassword = !txtConfirmPass.IsPassword;
            btnConfirm.Source = txtConfirmPass.IsPassword ? ImageSource.FromFile("ic_fas_eye") : ImageSource.FromFile("ic_fas_eye_slash");
        }
    }
}
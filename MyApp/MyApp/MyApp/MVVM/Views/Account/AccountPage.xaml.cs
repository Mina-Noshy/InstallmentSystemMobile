using MyApp.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using resx = MyApp.Resources.Languages;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MyApp.Services.APIServices;
using MyApp.MVVM.ViewModels;
using MyApp.Services.SQLiteServices;

namespace MyApp.MVVM.Views.Account
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AccountPage : ContentPage
    {
        AccountServices apiAccount;
        SQLiteSettingServices sqlSetting;
        public AccountPage()
        {
            InitializeComponent();

            apiAccount = new AccountServices();
            sqlSetting = new SQLiteSettingServices();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var _setting = sqlSetting.GetSetting();

            
            txtUserName.Text = _setting.userName;
            txtEmail.Text = _setting.email;
            txtPhone.Text = _setting.phone;
            txtFirstName.Text = _setting.firstName;
            txtLastName.Text = _setting.lastName;

            int _days = 0;

            if(_setting.stopAt.Value.Date > DateTime.UtcNow.Date)
            {
                var _span = _setting.stopAt.Value.Date - DateTime.UtcNow.Date;
                _days = (int)_span.TotalDays;
            }

            lblExpire.Text = resx.AppResource.account + " " + resx.AppResource.expireAt + " " +
                _setting.stopAt.Value.ToString("yyyy-MM-dd") + " " + resx.AppResource.after + " " +
                _days + " " + resx.AppResource.days;

        }

        async void btnSave_Clicked(object sender, EventArgs e)
        {
            if(!EastariaHelper.IsOnline())
            {
                await DisplayAlert("", resx.AppResource.pleaseOpenNetConn, resx.AppResource.ok);
                return;
            }

            // username
            if (string.IsNullOrEmpty(txtUserName.Text))
            {
                await DisplayAlert("", resx.AppResource.username + " " + resx.AppResource.required, resx.AppResource.ok);
                return;
            }

            if (txtUserName.Text.Length < 3)
            {
                await DisplayAlert("", resx.AppResource.username + " " + resx.AppResource.soShort, resx.AppResource.ok);
                return;
            }

            // email
            if (string.IsNullOrEmpty(txtEmail.Text))
            {
                await DisplayAlert("", resx.AppResource.email + " " + resx.AppResource.required, resx.AppResource.ok);
                return;
            }

            if (txtEmail.Text.Length < 9)
            {
                await DisplayAlert("", resx.AppResource.email + " " + resx.AppResource.notCorrect, resx.AppResource.ok);
                return;
            }

            if(!EastariaHelper.IsEmail(txtEmail.Text))
            {
                await DisplayAlert("", resx.AppResource.email + " " + resx.AppResource.notCorrect, resx.AppResource.ok);
                return;
            }

            // phone
            if (string.IsNullOrEmpty(txtPhone.Text))
            {
                await DisplayAlert("", resx.AppResource.phone + " " + resx.AppResource.required, resx.AppResource.ok);
                return;
            }

            if (txtPhone.Text.Length < 10)
            {
                await DisplayAlert("", resx.AppResource.phone + " " + resx.AppResource.notCorrect, resx.AppResource.ok);
                return;
            }

            // first name
            if (string.IsNullOrEmpty(txtFirstName.Text))
            {
                await DisplayAlert("", resx.AppResource.firstName + " " + resx.AppResource.required, resx.AppResource.ok);
                return;
            }

            if (txtFirstName.Text.Length < 3)
            {
                await DisplayAlert("", resx.AppResource.firstName + " " + resx.AppResource.soShort, resx.AppResource.ok);
                return;
            }

            // last name
            if (string.IsNullOrEmpty(txtLastName.Text))
            {
                await DisplayAlert("", resx.AppResource.lastName + " " + resx.AppResource.required, resx.AppResource.ok);
                return;
            }

            if (txtLastName.Text.Length < 3)
            {
                await DisplayAlert("", resx.AppResource.lastName + " " + resx.AppResource.soShort, resx.AppResource.ok);
                return;
            }

            indicator.IsRunning = true;

            // init and send request to update user info.

            var result = await apiAccount.UpdateUserInfo(new UpdateUserInfoVM
            {
                userId = sqlSetting.GetSetting().userId,
                userName = txtUserName.Text.Trim(),
                email = txtEmail.Text.Trim(),
                phoneNumber = txtPhone.Text.Trim(),
                firstName = txtFirstName.Text.Trim(),
                lastName = txtLastName.Text.Trim()
            });

            if (result.state)
            {
                var _setting = sqlSetting.GetSetting();

                _setting.userName = txtUserName.Text.Trim();
                _setting.email = txtEmail.Text.Trim();
                _setting.phone = txtPhone.Text.Trim();
                _setting.firstName = txtFirstName.Text.Trim();
                _setting.lastName = txtLastName.Text.Trim();

                sqlSetting.Update(_setting);

                toolBarItem.IsEnabled = true;

                await DisplayAlert("", resx.AppResource.updatedSuccessfully, resx.AppResource.ok);

            }
            else
                await DisplayAlert("", resx.AppResource.somethingWrong, resx.AppResource.ok);

            indicator.IsRunning = false;
        }

        private void btnCancel_Clicked(object sender, EventArgs e)
        {
            toolBarItem.IsEnabled = true;
        }

        private void toolBarItem_Clicked(object sender, EventArgs e)
        {
            toolBarItem.IsEnabled = false;
        }

        async void btnChangePass_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ChangePasswordPage());
        }
    }
}
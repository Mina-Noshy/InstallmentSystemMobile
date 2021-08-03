using MyApp.Services.APIServices;
using MyApp.Services.SQLiteServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using resx = MyApp.Resources.Languages;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MyApp.Helper;
using MyApp.MVVM.ViewModels;
using MyApp.MVVM.Views.Tabbed;

namespace MyApp.MVVM.Views.Account
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        AccountServices apiAccount;

        SQLiteSettingServices sqlSetting;
        public LoginPage()
        {
            InitializeComponent();

            apiAccount = new AccountServices();

            sqlSetting = new SQLiteSettingServices();
        }

        private void swhLogin_Toggled(object sender, ToggledEventArgs e)
        {

            if (swhLogin.IsToggled)
            {
                btnLogin.Text = lblLogin.Text = resx.AppResource.register;
            }
            else
            {
                btnLogin.Text = lblLogin.Text = resx.AppResource.login;
            }

        }


        async void btnLogin_Clicked(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txtUserName.Text))
            {
                await DisplayAlert("", resx.AppResource.mobileOrEmailRequired, resx.AppResource.ok);
                return;
            }

            if (swhLogin.IsToggled)
            {
                Register();
            }
            else
            {
                Login();
            }

        }

        async void Login()
        {
            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                await DisplayAlert("", resx.AppResource.passRequired, resx.AppResource.ok);
                return;
            }


            if (!EastariaHelper.IsOnline())
            {
                await DisplayAlert("", resx.AppResource.pleaseOpenNetConn, resx.AppResource.ok);
                return;
            }

            indicator.IsRunning = true;

            AuthenticationVM _authModel = await EastariaHelper.GetToken(txtUserName.Text, txtPassword.Text);

            indicator.IsRunning = false;

            if (!_authModel.isAuthenticated)
            {
                await DisplayAlert("", resx.AppResource.userOrPassIncorrect, resx.AppResource.ok);
                return;
            }

            //if (_authModel.roles is null)
            //{
            //    await DisplayAlert("", resx.AppResource.pleaseCallToActivateAccount, resx.AppResource.ok);
            //    return;
            //}

            //if (_authModel.roles.Count == 0)
            //{
            //    await DisplayAlert("", resx.AppResource.pleaseCallToActivateAccount, resx.AppResource.ok);
            //    return;
            //}

            await DisplayAlert("", resx.AppResource.welcome + " " + _authModel.firstName, resx.AppResource.login);

            string _roles = string.Join(",", _authModel.roles);

            Application.Current.MainPage = new NavigationPage(new MainTabbedPage(IsInRole(_authModel.roles, "admin")));


        }

        async void Register()
        {
            if (string.IsNullOrEmpty(txtFullName.Text))
            {
                await DisplayAlert("", resx.AppResource.fullNameRequired, resx.AppResource.ok);
                return;
            }

            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                await DisplayAlert("", resx.AppResource.passRequired, resx.AppResource.ok);
                return;
            }

            if (txtPassword.Text != txtConfirmPassword.Text)
            {
                await DisplayAlert("", resx.AppResource.passNotMatched, resx.AppResource.ok);
                return;
            }

            if (!EastariaHelper.IsOnline())
            {
                await DisplayAlert("", resx.AppResource.pleaseOpenNetConn, resx.AppResource.ok);
                return;
            }

            indicator.IsRunning = true;

            if (!await apiAccount.IsEmailAvailable(EastariaHelper.GetEmailFormatting(txtUserName.Text)))
            {
                await DisplayAlert("", resx.AppResource.emailOrPhoneUsed, resx.AppResource.ok);
                return;
            }

            RegisterVM _regesterModel = await RegisterFormatting(
                txtUserName.Text,
                txtFullName.Text,
                txtPassword.Text);

            // send data to register
            ResponseVM response = await apiAccount.Register(_regesterModel);

            await Task.Delay(100);

            if (response.state)
            {
                AuthenticationVM _authModel = await EastariaHelper.GetToken(_regesterModel.email, _regesterModel.password);

                indicator.IsRunning = false;

                if (!_authModel.isAuthenticated)
                {
                    await DisplayAlert("", resx.AppResource.somethingWrong, resx.AppResource.ok);
                    return;
                }

                //if (_authModel.roles is null)
                //{
                //    await DisplayAlert("", resx.AppResource.pleaseCallToActivateAccount, resx.AppResource.ok);
                //    return;
                //}

                //if (_authModel.roles.Count == 0)
                //{
                //    await DisplayAlert("", resx.AppResource.pleaseCallToActivateAccount, resx.AppResource.ok);
                //    return;
                //}

                await DisplayAlert("", resx.AppResource.welcome + " " + _authModel.firstName, resx.AppResource.login);

                Application.Current.MainPage = new NavigationPage(new MainTabbedPage(IsInRole(_authModel.roles, "admin")));
            }
            else
            {
                await DisplayAlert("", response.message, resx.AppResource.ok);
                indicator.IsRunning = false;
            }

            indicator.IsRunning = false;
        }

        private async Task<RegisterVM> RegisterFormatting(string _userName, string _fullName, string _password)
        {
            int phone;
            bool isPhone = int.TryParse(_userName, out phone);


            RegisterVM register = new RegisterVM
            {
                email = EastariaHelper.GetEmailFormatting(_userName),
                userName = EastariaHelper.GetEmailFormatting(_userName),
                password = _password
            };

            _fullName = _fullName.Trim();

            if (_fullName.Contains(" "))
            {
                register.firstName = _fullName.Substring(0, _fullName.IndexOf(" "));
                register.lastName = _fullName.Substring(_fullName.IndexOf(" "));
            }
            else
                register.firstName = _fullName;

            if (isPhone)
                register.phoneNumber = _userName.ToString();

            return await Task.FromResult(register);

        }

        private void btnPass_Clicked(object sender, EventArgs e)
        {
            txtPassword.IsPassword = !txtPassword.IsPassword;
            btnPass.Source = txtPassword.IsPassword ? ImageSource.FromFile("ic_fas_eye") : ImageSource.FromFile("ic_fas_eye_slash");

        }

        private void btnConfirm_Clicked(object sender, EventArgs e)
        {
            txtConfirmPassword.IsPassword = !txtConfirmPassword.IsPassword;
            btnConfirm.Source = txtConfirmPassword.IsPassword ? ImageSource.FromFile("ic_fas_eye") : ImageSource.FromFile("ic_fas_eye_slash");

        }

        private bool IsInRole(List<string> lst, string role)
        {
            if (lst is null)
                return false;

            string txt = string.Join(",", lst);
            return txt.ToLower().Contains(role.ToLower());
        }
    }
}
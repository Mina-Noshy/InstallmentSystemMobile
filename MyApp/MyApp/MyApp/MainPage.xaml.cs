using MyApp.Helper;
using MyApp.MVVM.Views;
using MyApp.MVVM.Views.Account;
using MyApp.MVVM.Views.Tabbed;
using MyApp.Services.SQLiteServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

using resx = MyApp.Resources.Languages;

namespace MyApp
{
    public partial class MainPage : ContentPage
    {
        SQLiteSettingServices sqlSetting;

        public MainPage()
        {
            InitializeComponent();

            sqlSetting = new SQLiteSettingServices();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            Task ts2 = fade(700);
        }

        async Task fade(uint length)
        {
            lblDescription.Text = resx.AppResource.appDescription;
            await lblDescription.FadeTo(1, length);
            await lblDescription.FadeTo(0, length);

            lblDescription.Text = resx.AppResource.hint1;
            await lblDescription.FadeTo(1, length);
            await lblDescription.FadeTo(0, length);

            lblDescription.Text = resx.AppResource.hint2;
            await lblDescription.FadeTo(1, length);
            await lblDescription.FadeTo(0, length);

            lblDescription.Text = resx.AppResource.hint3;
            await lblDescription.FadeTo(1, length);
            await lblDescription.FadeTo(0, length);

            btnGetStarted.IsVisible = true;

            await fade(length);
        }

        async void btnGetStarted_Clicked(object sender, EventArgs e)
        {
            if (sqlSetting.GetSetting().isAuthenticated)
            {
                // when be false then this user not approved by admin yet.
                if (string.IsNullOrEmpty(sqlSetting.GetSetting().roles))
                {
                    indicator.IsRunning = true;

                    await EastariaHelper.GetToken();

                    indicator.IsRunning = false;

                    if (string.IsNullOrEmpty(sqlSetting.GetSetting().roles))
                    {
                        await DisplayAlert("", resx.AppResource.pleaseCallToActivateAccount, resx.AppResource.ok);

                        return;
                    }
                }

                if (sqlSetting.IsTokenExpired())
                {
                    if (EastariaHelper.IsOnline())
                    {
                        indicator.IsRunning = true;

                        await EastariaHelper.GetToken();

                        indicator.IsRunning = false;
                    }
                    else
                    {
                        await DisplayAlert("", resx.AppResource.pleaseOpenNetConn, resx.AppResource.ok);

                        return;
                    }
                }
                else
                    Application.Current.MainPage = new NavigationPage(new MainTabbedPage());
            }
            else
                Application.Current.MainPage = new NavigationPage(new LoginPage());

        }

        private void btnStart_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new LoginPage());
        }
    }
}

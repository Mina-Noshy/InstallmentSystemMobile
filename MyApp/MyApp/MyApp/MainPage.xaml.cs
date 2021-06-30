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
        //CountryServices apiCountry;
        //SQLiteCountryServices sqlCountry;

        SQLiteSettingServices sqlSetting;

        public MainPage()
        {
            InitializeComponent();

            //apiCountry = new CountryServices();
            //sqlCountry = new SQLiteCountryServices();

            sqlSetting = new SQLiteSettingServices();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            Task ts2 = ReConnect();
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

        private async Task FillLocalDB()
        {

            if (sqlSetting.IsSettingsUpdated())
            {
                return;
            }
            else if (EastariaHelper.IsOnline())
            {

                //progress.Progress = 0.090;
                //await Task.Delay(100);
                //var _lstCountry = await apiCountry.GetAll();
                //sqlCountry.UpdateAll(_lstCountry);


                progress.Progress = 0.900;
                await Task.Delay(100);
                sqlSetting.EditSettingsLastUpdet();

                progress.Progress = 0.990;

                await Task.Delay(100);
                progress.Progress = 1;
            }

        }

        private async Task ReConnect()
        {
            while (true)
            {
                if (!sqlSetting.IsSettingsUpdated())
                {
                    if (!EastariaHelper.IsOnline())
                        await DisplayAlert("", resx.AppResource.pleaseOpenNetConn, resx.AppResource.ok);

                    await FillLocalDB();
                }
                else
                {
                    indicator.IsRunning = false;
                    await fade(800);
                    break;
                }
                await Task.Delay(2000);
            }
            return;
        }


        async void btnGetStarted_Clicked(object sender, EventArgs e)
        {
            if (sqlSetting.GetSetting().isAuthenticated)
            {
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
            }

            if (EastariaHelper.settingServices.GetSetting().isAuthenticated)
                Application.Current.MainPage = new NavigationPage(new MainTabbedPage());
            else
                Application.Current.MainPage = new NavigationPage(new LoginPage());
        }
    }
}

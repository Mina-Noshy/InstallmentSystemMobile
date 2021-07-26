using MyApp.Helper;
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

namespace MyApp.MVVM.Views.Installment
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InstallmentsPage : ContentPage
    {
        InstallmentServices apiInstallment;
        SQLiteSettingServices sqlSetting;

        public InstallmentsPage()
        {
            InitializeComponent();

            apiInstallment = new InstallmentServices();
            sqlSetting = new SQLiteSettingServices();
        }


        async void btnSearch_Clicked(object sender, EventArgs e)
        {
            if (!EastariaHelper.IsOnline())
            {
                await DisplayAlert("", resx.AppResource.pleaseOpenNetConn, resx.AppResource.ok);
                return;
            }
            lstInstallments.IsRefreshing = true;
            lstInstallments.ItemsSource = await apiInstallment.GetByDayVM(sqlSetting.GetSetting().userId, picDay.Date.ToString("yyyy-MM-dd"));
            lstInstallments.IsRefreshing = false;
        }

        async void rdoAllUnReceived_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (!EastariaHelper.IsOnline())
            {
                await DisplayAlert("", resx.AppResource.pleaseOpenNetConn, resx.AppResource.ok);
                return;
            }

            lstInstallments.IsRefreshing = true;

            if (rdoAllUnReceived.IsChecked)
                lstInstallments.ItemsSource = await apiInstallment.GetAllUnreceivedVM(sqlSetting.GetSetting().userId);
            
            else if (rdoAllToday.IsChecked)
                lstInstallments.ItemsSource = await apiInstallment.GetAllTodayVM(sqlSetting.GetSetting().userId);

            else if (rdoAllUnReceivedToday.IsChecked)
                lstInstallments.ItemsSource = await apiInstallment.GetUnreceivedTodayVM(sqlSetting.GetSetting().userId);
            
            else if (rdoAllReceivedToday.IsChecked)
                lstInstallments.ItemsSource = await apiInstallment.GetReceivedTodayVM(sqlSetting.GetSetting().userId);

            lstInstallments.IsRefreshing = false;
        }

        async void lstInstallments_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            await Navigation.PushAsync(new InstallmentDetailsPage((e.SelectedItem as Models.Installment).id));
        }
    }
}
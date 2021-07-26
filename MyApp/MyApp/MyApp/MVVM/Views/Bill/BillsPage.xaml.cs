using MyApp.Helper;
using MyApp.Services.APIServices;
using MyApp.Services.SQLiteServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyApp.MVVM.Models;

using resx = MyApp.Resources.Languages;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyApp.MVVM.Views.Bill
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BillsPage : ContentPage
    {
        ClientServices apiClient;
        SQLiteClientServices sqlClient;

        SQLiteBillServices sqlBill;
        BillServices apiBill;

        SQLiteSettingServices sqlSetting;

        public BillsPage()
        {
            InitializeComponent();

            apiClient = new ClientServices();
            sqlClient = new SQLiteClientServices();

            apiBill = new BillServices();
            sqlBill = new SQLiteBillServices();

            sqlSetting = new SQLiteSettingServices();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            await FillLists();
        }


        async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddBillPage(null));
        }


        private async Task FillLists()
        {
            // fill clients picker.
            picClient.ItemsSource = sqlClient.GetAll();

            // fill bills list.
            var _lstBill = sqlBill.GetAll();

            if (_lstBill.Count == 0)
            {
                if (!EastariaHelper.IsOnline())
                {
                    await DisplayAlert("", resx.AppResource.pleaseOpenNetConn, resx.AppResource.ok);

                    return;
                }

                lstBills.IsRefreshing = true;

                // get bills from web server.
                List<Models.Bill> _lst = await apiBill.GetAll(sqlSetting.GetSetting().userId);

                sqlBill.UpdateAll(_lst);
                lstBills.ItemsSource = _lst;

                lstBills.IsRefreshing = false;
            }
            else
                lstBills.ItemsSource = _lstBill;
        }


        async void lstBill_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            await Navigation.PushAsync(new BillDetailsPage((e.SelectedItem as Models.Bill).id));
        }

        async void lstBills_Refreshing(object sender, EventArgs e)
        {
            if (!EastariaHelper.IsOnline())
            {
                await DisplayAlert("", resx.AppResource.pleaseOpenNetConn, resx.AppResource.ok);
                lstBills.IsRefreshing = false;
                return;
            }

            await Task.Delay(100);

            List<Models.Bill> lst = await apiBill.GetAll(sqlSetting.GetSetting().userId);

            sqlBill.UpdateAll(lst);
            lstBills.ItemsSource = lst;

            lstBills.IsRefreshing = false;
        }

        async void picClient_SelectedIndexChanged(object sender, EventArgs e)
        {
            var obj = picClient.SelectedItem as Models.Client;

            if (EastariaHelper.IsOnline())
                lstBills.ItemsSource = await apiBill.GetAll((picClient.SelectedItem as Models.Client).id);
            else
                lstBills.ItemsSource = sqlBill.GetAll((picClient.SelectedItem as Models.Client).id);
        }
    }
}
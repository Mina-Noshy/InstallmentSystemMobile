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

namespace MyApp.MVVM.Views.Client
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ClientsPage : ContentPage
    {
        ClientServices apiClient;
        SQLiteClientServices sqlClient;

        SQLiteSettingServices sqlSetting;

        public ClientsPage()
        {
            InitializeComponent();

            apiClient = new ClientServices();
            sqlClient = new SQLiteClientServices();

            sqlSetting = new SQLiteSettingServices();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            await FillLists();
        }


        async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddClientPage());
        }


        private async Task FillLists()
        {
            var lst = sqlClient.GetAll();

            if (lst.Count == 0)
            {
                if (!EastariaHelper.IsOnline())
                {
                    await DisplayAlert("", resx.AppResource.pleaseOpenNetConn, resx.AppResource.ok);
                    
                    return;
                }

                lstClient.IsRefreshing = true;
                List<Models.Client> _lst = await apiClient.GetAll(sqlSetting.GetSetting().userId);

                sqlClient.UpdateAll(_lst);
                lstClient.ItemsSource = _lst;

                lstClient.IsRefreshing = false;
            }
            else
                lstClient.ItemsSource = lst;
        }


        async void lstClient_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            await Navigation.PushAsync(new ClientDetailsPage((e.SelectedItem as Models.Client).id));
        }

        private void txtSearch_SearchButtonPressed(object sender, EventArgs e)
        {
            ApiSearch(txtSearch.Text.Trim());
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            SqlSearch(e.NewTextValue.Trim());
        }

        private void SqlSearch(string txt)
        {
            lstClient.IsRefreshing = true;
            lstClient.ItemsSource = sqlClient.GetAll(txt);
            lstClient.IsRefreshing = false;
        }

        async void ApiSearch(string txt)
        {
            lstClient.IsRefreshing = true;
            lstClient.ItemsSource = await apiClient.Search(sqlSetting.GetSetting().userId, txt);
            lstClient.IsRefreshing = false;
        }

        async void lstClient_Refreshing(object sender, EventArgs e)
        {
            if (!EastariaHelper.IsOnline())
            {
                await DisplayAlert("", resx.AppResource.pleaseOpenNetConn, resx.AppResource.ok);
                lstClient.IsRefreshing = false;
                return;
            }

            await Task.Delay(100);

            List<Models.Client> lst = await apiClient.GetAll(sqlSetting.GetSetting().userId);

            sqlClient.UpdateAll(lst);
            lstClient.ItemsSource = lst;

            lstClient.IsRefreshing = false;
        }

    }
}
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

namespace MyApp.MVVM.Views.Admin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UsersPage : ContentPage
    {
        SQLiteUserServices sqlUser;
        AccountServices apiAccount;

        public UsersPage()
        {
            InitializeComponent();

            sqlUser = new SQLiteUserServices();
            apiAccount = new AccountServices();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            await FillLists();
        }



        private async Task FillLists()
        {
            var lst = sqlUser.GetAll();

            if (lst.Count == 0)
            {
                if (!EastariaHelper.IsOnline())
                {
                    await DisplayAlert("", resx.AppResource.pleaseOpenNetConn, resx.AppResource.ok);

                    return;
                }

                lstUsers.IsRefreshing = true;
                List<Models.User> _lst = await apiAccount.GetAllUsers();

                sqlUser.UpdateAll(_lst);
                lstUsers.ItemsSource = _lst;

                lstUsers.IsRefreshing = false;
            }
            else
                lstUsers.ItemsSource = lst;
        }


        async void lstUsers_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            await Navigation.PushAsync(new UserDetailsPage((e.SelectedItem as Models.User).userId));
        }

        private void txtSearch_SearchButtonPressed(object sender, EventArgs e)
        {
            SqlSearch(txtSearch.Text.Trim());
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            SqlSearch(e.NewTextValue.Trim());
        }

        private void SqlSearch(string txt)
        {
            lstUsers.IsRefreshing = true;
            lstUsers.ItemsSource = sqlUser.GetAll(txt);
            lstUsers.IsRefreshing = false;
        }

        async void lstUsers_Refreshing(object sender, EventArgs e)
        {
            if (!EastariaHelper.IsOnline())
            {
                await DisplayAlert("", resx.AppResource.pleaseOpenNetConn, resx.AppResource.ok);
                lstUsers.IsRefreshing = false;
                return;
            }

            await Task.Delay(100);

            List<Models.User> lst = await apiAccount.GetAllUsers();

            sqlUser.UpdateAll(lst);
            lstUsers.ItemsSource = lst;
            lstUsers.IsRefreshing = false;
        }

    }
}
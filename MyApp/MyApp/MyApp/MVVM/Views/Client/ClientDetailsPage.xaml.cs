using MyApp.Helper;
using MyApp.MVVM.ViewModels;
using MyApp.Services.APIServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using resx = MyApp.Resources.Languages;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MyApp.MVVM.Views.Bill;

namespace MyApp.MVVM.Views.Client
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ClientDetailsPage : ContentPage
    {
        int _clientId;

        ClientServices apiClient;

        public ClientDetailsPage(int clientId)
        {
            InitializeComponent();

            _clientId = clientId;
            apiClient = new ClientServices();
        }


        protected async override void OnAppearing()
        {
            base.OnAppearing();

            await GetApi();
        }


        async Task GetApi()
        {
            if (EastariaHelper.IsOnline())
            {
                var _details = await apiClient.GetClient(_clientId);

                this.BindingContext = _details;

                indicator.IsRunning = false;
                return;
            }

            await DisplayAlert("", resx.AppResource.pleaseOpenNetConn, resx.AppResource.ok);
            await Task.Delay(3000);
            await GetApi();

        }


        async void btnDelete_Clicked(object sender, EventArgs e)
        {
            var delete = await DisplayActionSheet(resx.AppResource.sureToDelete, resx.AppResource.cancel, "", resx.AppResource.yes, resx.AppResource.no);

            if (delete != resx.AppResource.yes)
                return;

            indicator.IsRunning = true;

            bool result = await apiClient.DeleteClient(_clientId);

            if (result)
            {
                await DisplayAlert("", resx.AppResource.client + " " + resx.AppResource.deletedSuccessfully, resx.AppResource.ok);

                indicator.IsRunning = false;

                await Navigation.PopAsync();
            }

            else
            {
                await DisplayAlert("", resx.AppResource.somethingWrong, resx.AppResource.ok);
                indicator.IsRunning = false;
            }
        }

        async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditClientPage(_clientId));
        }

        async void btnBills_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ClientBillsPage(_clientId));
        }

        async void btnAddBill_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddBillPage(_clientId));
        }
    }
}
using MyApp.Services.APIServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using resx = MyApp.Resources.Languages;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MyApp.Helper;

namespace MyApp.MVVM.Views.Bill
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BillDetailsPage : ContentPage
    {
        int _billId;
        BillServices apiBill;

        public BillDetailsPage(int billId)
        {
            InitializeComponent();

            _billId = billId;
            apiBill = new BillServices();
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
                var _details = await apiBill.GetBill(_billId);

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

            bool result = await apiBill.DeleteBill(_billId);

            if (result)
            {
                await DisplayAlert("", resx.AppResource.bill + " " + resx.AppResource.deletedSuccessfully, resx.AppResource.ok);

                indicator.IsRunning = false;

                await Navigation.PopAsync();
            }

            else
            {
                await DisplayAlert("", resx.AppResource.somethingWrong, resx.AppResource.ok);
                indicator.IsRunning = false;
            }
        }

        async void btnInstallments_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new BillInstallmentsPage(_billId));
        }
    }
}
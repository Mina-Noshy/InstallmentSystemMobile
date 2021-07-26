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
using MyApp.MVVM.Models.StandardModels;
using MyApp.Services.SQLiteServices;

namespace MyApp.MVVM.Views.Bill
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddBillPage : ContentPage
    {
        int? _clientId;
        BillServices apiBill;
        SQLiteClientServices sqlClient;
        public AddBillPage(int? clientId)
        {
            InitializeComponent();

            apiBill = new BillServices();
            sqlClient = new SQLiteClientServices();

            if (clientId is null)
            {
                stkClient.IsVisible = true;
                picClient.ItemsSource = sqlClient.GetAll();
            }
            else
            {
                stkClient.IsVisible = false;
                _clientId = clientId;
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            picDelayFineType.ItemsSource = picInstallmentType.ItemsSource = new List<string> 
            { resx.AppResource.yearly, resx.AppResource.monthly, resx.AppResource.daily };
        }

        async void btnAdd_Clicked(object sender, EventArgs e)
        {

            if(_clientId is null)
            {
                // =========> client validation
                if (picClient.SelectedIndex == -1)
                {
                    await DisplayAlert("", resx.AppResource.clientName + " " + resx.AppResource.required, resx.AppResource.ok);
                    return;
                }
                else
                    _clientId = (picClient.SelectedItem as Models.Client).id;
            }

            // =========> total original validation
            if (int.Parse(txtOriginalAmount.Text) <= 0)
            {
                await DisplayAlert("", resx.AppResource.totalAmount + " " + resx.AppResource.required, resx.AppResource.ok);
                return;
            }

            // =========> percentage validation
            if (int.Parse(txtPercentage.Text) <= 0 || int.Parse(txtPercentage.Text)> 100)
            {
                await DisplayAlert("", resx.AppResource.percentage + " " + resx.AppResource.required, resx.AppResource.ok);
                return;
            }

            // =========> amount paid validation
            if (int.Parse(txtAmountPaid.Text) <= 0)
            {
                await DisplayAlert("", resx.AppResource.amountPaid + " " + resx.AppResource.required, resx.AppResource.ok);
                return;
            }

            // =========> installment count validation
            if (int.Parse(txtInstallmentCount.Text) <= 0)
            {
                await DisplayAlert("", resx.AppResource.installmentCount + " " + resx.AppResource.required, resx.AppResource.ok);
                return;
            }

            // =========> delay fine validation
            if (int.Parse(txtDelayFine.Text) <= 0)
            {
                await DisplayAlert("", resx.AppResource.delayFine + " " + resx.AppResource.required, resx.AppResource.ok);
                return;
            }

            // =========> delay fine type validation
            if (picDelayFineType.SelectedIndex == -1)
            {
                await DisplayAlert("", resx.AppResource.delayFineType + " " + resx.AppResource.required, resx.AppResource.ok);
                return;
            }

            // =========> installment type validation
            if (picInstallmentType.SelectedIndex == -1)
            {
                await DisplayAlert("", resx.AppResource.installmentType + " " + resx.AppResource.required, resx.AppResource.ok);
                return;
            }

            indicator.IsRunning = true;

            bool result = await apiBill.AddBill(new StandardBill
            {
                id = 0,
                clientId = (int)_clientId,
                percentage = double.Parse(txtPercentage.Text),
                originalAmount = double.Parse(txtOriginalAmount.Text),
                amountPaid = double.Parse(txtAmountPaid.Text),
                restAmount = double.Parse(txtRestAmount.Text),
                totalAmount = double.Parse(txtRestAmount.Text) + double.Parse(txtRestAmount.Text) * double.Parse(txtPercentage.Text) / 100,
                installmentCount = short.Parse(txtInstallmentCount.Text),
                delayFine = double.Parse(txtDelayFine.Text),
                description = txtDescription.Text,
                billDate = picDate.Date,

                delayFineType = picDelayFineType.SelectedIndex == 0? InstallmentTypes.سنوى :
                picDelayFineType.SelectedIndex == 1 ? InstallmentTypes.شهرى : InstallmentTypes.يومى,

                installmentType = picInstallmentType.SelectedIndex == 0 ? InstallmentTypes.سنوى :
                picInstallmentType.SelectedIndex == 1 ? InstallmentTypes.شهرى : InstallmentTypes.يومى
            });

            if (result)
            {
                await DisplayAlert("", resx.AppResource.bill + " " + resx.AppResource.addedSuccessfully, resx.AppResource.ok);
              
                indicator.IsRunning = false;

                await Navigation.PopAsync();
            }

            else
            {
                await DisplayAlert("", resx.AppResource.somethingWrong, resx.AppResource.ok);
                indicator.IsRunning = false;
            }

        }

        async void btnCancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private void txts_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                txtRestAmount.Text = (int.Parse(txtOriginalAmount.Text) - int.Parse(txtAmountPaid.Text)).ToString();
            }
            catch { }

            try
            {
                txtTotalAmount.Text = (double.Parse(txtRestAmount.Text) + ( 
                                      double.Parse(txtRestAmount.Text) * 
                                      double.Parse(txtPercentage.Text) / 100
                                      )).ToString("0");
            }
            catch { }
        }
    }
}
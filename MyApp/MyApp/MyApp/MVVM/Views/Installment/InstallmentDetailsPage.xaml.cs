using MyApp.Helper;
using MyApp.Services.APIServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using resx = MyApp.Resources.Languages;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MyApp.MVVM.Models.StandardModels;

namespace MyApp.MVVM.Views.Installment
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InstallmentDetailsPage : ContentPage
    {
        InstallmentServices apiInstallment;
        int _installmentId;
        public InstallmentDetailsPage(int installmentId)
        {
            InitializeComponent();
            apiInstallment = new InstallmentServices();

            _installmentId = installmentId;
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
                var _details = await apiInstallment.GetInstallmentVM(_installmentId);

                this.BindingContext = _details;

                int days = GetDays(_details.dueDate);
                lblDaysOfDelay.Text = days.ToString();

                int totalDelayFine = 0;


                if (_details.delayFineType == InstallmentTypes.سنوى)
                {
                    totalDelayFine = (int)(days / 365 * _details.delayFine);
                    lblTotalDelayFine.Text = $"{days}/365x{_details.delayFine}={totalDelayFine}";
                }

                else if (_details.delayFineType == InstallmentTypes.شهرى)
                {
                    totalDelayFine = (int)(days / 30 * _details.delayFine);
                    lblTotalDelayFine.Text = $"{days}/30{_details.delayFine}={totalDelayFine}";
                }

                else
                {
                    totalDelayFine = (int)(days * _details.delayFine);
                    lblTotalDelayFine.Text = $"{days}x{_details.delayFine}={totalDelayFine}";
                }

                lblTotalAmount.Text = ((int)(_details.amountValue + totalDelayFine)).ToString();

                indicator.IsRunning = false;
                return;
            }

            await DisplayAlert("", resx.AppResource.pleaseOpenNetConn, resx.AppResource.ok);
            await Task.Delay(3000);
            await GetApi();

        }

        async void btnBack_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        async void btnReceive_Clicked(object sender, EventArgs e)
        {
            indicator.IsRunning = true;

            var result = await apiInstallment.SwitchInstallmentState(_installmentId);

            if (result)
                await GetApi();
            else
                await DisplayAlert("", resx.AppResource.somethingWrong, resx.AppResource.ok);

            indicator.IsRunning = false;
        }

        private int GetDays(DateTime dueDate)
        {
            if (DateTime.UtcNow < dueDate)
                return 0;

            TimeSpan ts = DateTime.UtcNow - dueDate;
            return (int)ts.TotalDays;
        }
    }
}
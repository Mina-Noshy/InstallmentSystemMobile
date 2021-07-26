using MyApp.MVVM.Views.Installment;
using MyApp.Services.APIServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyApp.MVVM.Views.Bill
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BillInstallmentsPage : ContentPage
    {
        InstallmentServices apiInstallment;
        int _billId;
        public BillInstallmentsPage(int billId)
        {
            InitializeComponent();

            apiInstallment = new InstallmentServices();
            _billId = billId;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            lstInstallmentss.ItemsSource = await apiInstallment.GetBillInstallmentsVM(_billId);
        }

        async void lstInstallmentss_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            await Navigation.PushAsync(new InstallmentDetailsPage((e.SelectedItem as Models.Installment).id));
        }
    }
}
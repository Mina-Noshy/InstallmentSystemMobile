using MyApp.MVVM.Views.Bill;
using MyApp.Services.SQLiteServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyApp.MVVM.Views.Client
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ClientBillsPage : ContentPage
    {
        SQLiteBillServices sqlBill;
        public ClientBillsPage(int clientId)
        {
            InitializeComponent();

            sqlBill = new SQLiteBillServices();

            lstBills.ItemsSource = sqlBill.GetAll(clientId);
        }

        async void lstBills_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            await Navigation.PushAsync(new BillDetailsPage((e.SelectedItem as Models.Bill).id));
        }
    }
}
using MyApp.MVVM.Views.Account;
using MyApp.MVVM.Views.Admin;
using MyApp.MVVM.Views.Bill;
using MyApp.MVVM.Views.Client;
using MyApp.MVVM.Views.Installment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyApp.MVVM.Views.Tabbed
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainTabbedPage : TabbedPage
    {
        public MainTabbedPage(bool isAdmin)
        {
            InitializeComponent();

            this.Children.Add(new ClientsPage { IconImageSource = "ic_fas_dollar_sign" });
            this.Children.Add(new BillsPage { IconImageSource = "ic_fas_plus_circle" });
            this.Children.Add(new InstallmentsPage { IconImageSource = "ic_fas_home" });
            this.Children.Add(new AccountPage { IconImageSource = "ic_fas_home" });
            if (isAdmin)
                this.Children.Add(new UsersPage { IconImageSource = "ic_fas_profile" });

        }

        override protected void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();

            if (this.CurrentPage.Title == "Week")
            {
                MessagingCenter.Send<object>(this, "Week");
            }
        }

    }
}
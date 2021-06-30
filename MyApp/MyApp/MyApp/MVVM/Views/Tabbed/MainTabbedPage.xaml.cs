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
        public MainTabbedPage()
        {
            InitializeComponent();

            //this.Children.Add(new HomePage { IconImageSource = "ic_fas_home" });
            //this.Children.Add(new PricesPage { IconImageSource = "ic_fas_dollar_sign" });
            //this.Children.Add(new AddOfferPage { IconImageSource = "ic_fas_plus_circle" });
            //this.Children.Add(new MarketsPage { IconImageSource = "ic_fas_home" });
            //if (isAuthorized)
            //    this.Children.Add(new AccountPage { IconImageSource = "ic_fas_profile" });

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
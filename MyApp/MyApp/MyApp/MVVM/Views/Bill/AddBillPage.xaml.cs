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
    public partial class AddBillPage : ContentPage
    {
        public AddBillPage()
        {
            InitializeComponent();
        }
    }
}
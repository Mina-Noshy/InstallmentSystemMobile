//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

[assembly: global::Xamarin.Forms.Xaml.XamlResourceIdAttribute("MyApp.MVVM.Views.Client.ClientBillsPage.xaml", "MVVM/Views/Client/ClientBillsPage.xaml", typeof(global::MyApp.MVVM.Views.Client.ClientBillsPage))]

namespace MyApp.MVVM.Views.Client {
    
    
    [global::Xamarin.Forms.Xaml.XamlFilePathAttribute("MVVM\\Views\\Client\\ClientBillsPage.xaml")]
    public partial class ClientBillsPage : global::Xamarin.Forms.ContentPage {
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.ListView lstBills;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private void InitializeComponent() {
            global::Xamarin.Forms.Xaml.Extensions.LoadFromXaml(this, typeof(ClientBillsPage));
            lstBills = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.ListView>(this, "lstBills");
        }
    }
}
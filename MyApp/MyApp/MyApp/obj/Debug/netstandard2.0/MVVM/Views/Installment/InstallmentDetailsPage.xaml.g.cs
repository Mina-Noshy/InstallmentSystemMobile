//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

[assembly: global::Xamarin.Forms.Xaml.XamlResourceIdAttribute("MyApp.MVVM.Views.Installment.InstallmentDetailsPage.xaml", "MVVM/Views/Installment/InstallmentDetailsPage.xaml", typeof(global::MyApp.MVVM.Views.Installment.InstallmentDetailsPage))]

namespace MyApp.MVVM.Views.Installment {
    
    
    [global::Xamarin.Forms.Xaml.XamlFilePathAttribute("MVVM\\Views\\Installment\\InstallmentDetailsPage.xaml")]
    public partial class InstallmentDetailsPage : global::Xamarin.Forms.ContentPage {
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.ActivityIndicator indicator;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.Label lblDaysOfDelay;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.Label lblTotalDelayFine;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.Label lblTotalAmount;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::MyApp.Controls.button_md btnReceive;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::MyApp.Controls.button_md btnBack;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private void InitializeComponent() {
            global::Xamarin.Forms.Xaml.Extensions.LoadFromXaml(this, typeof(InstallmentDetailsPage));
            indicator = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.ActivityIndicator>(this, "indicator");
            lblDaysOfDelay = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.Label>(this, "lblDaysOfDelay");
            lblTotalDelayFine = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.Label>(this, "lblTotalDelayFine");
            lblTotalAmount = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.Label>(this, "lblTotalAmount");
            btnReceive = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::MyApp.Controls.button_md>(this, "btnReceive");
            btnBack = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::MyApp.Controls.button_md>(this, "btnBack");
        }
    }
}

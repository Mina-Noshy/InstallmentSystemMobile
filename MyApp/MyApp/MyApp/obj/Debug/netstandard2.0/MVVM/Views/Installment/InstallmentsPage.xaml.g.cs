//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

[assembly: global::Xamarin.Forms.Xaml.XamlResourceIdAttribute("MyApp.MVVM.Views.Installment.InstallmentsPage.xaml", "MVVM/Views/Installment/InstallmentsPage.xaml", typeof(global::MyApp.MVVM.Views.Installment.InstallmentsPage))]

namespace MyApp.MVVM.Views.Installment {
    
    
    [global::Xamarin.Forms.Xaml.XamlFilePathAttribute("MVVM\\Views\\Installment\\InstallmentsPage.xaml")]
    public partial class InstallmentsPage : global::Xamarin.Forms.ContentPage {
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.RadioButton rdoAllUnReceived;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.RadioButton rdoAllToday;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.RadioButton rdoAllUnReceivedToday;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.RadioButton rdoAllReceivedToday;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.RadioButton rdoAllInDay;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.DatePicker picDay;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::MyApp.Controls.button_sm btnSearch;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.ListView lstInstallments;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private void InitializeComponent() {
            global::Xamarin.Forms.Xaml.Extensions.LoadFromXaml(this, typeof(InstallmentsPage));
            rdoAllUnReceived = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.RadioButton>(this, "rdoAllUnReceived");
            rdoAllToday = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.RadioButton>(this, "rdoAllToday");
            rdoAllUnReceivedToday = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.RadioButton>(this, "rdoAllUnReceivedToday");
            rdoAllReceivedToday = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.RadioButton>(this, "rdoAllReceivedToday");
            rdoAllInDay = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.RadioButton>(this, "rdoAllInDay");
            picDay = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.DatePicker>(this, "picDay");
            btnSearch = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::MyApp.Controls.button_sm>(this, "btnSearch");
            lstInstallments = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.ListView>(this, "lstInstallments");
        }
    }
}

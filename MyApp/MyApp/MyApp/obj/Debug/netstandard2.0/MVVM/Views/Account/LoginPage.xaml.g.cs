//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

[assembly: global::Xamarin.Forms.Xaml.XamlResourceIdAttribute("MyApp.MVVM.Views.Account.LoginPage.xaml", "MVVM/Views/Account/LoginPage.xaml", typeof(global::MyApp.MVVM.Views.Account.LoginPage))]

namespace MyApp.MVVM.Views.Account {
    
    
    [global::Xamarin.Forms.Xaml.XamlFilePathAttribute("MVVM\\Views\\Account\\LoginPage.xaml")]
    public partial class LoginPage : global::Xamarin.Forms.ContentPage {
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.ActivityIndicator indicator;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.Label lblLogin;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.Switch swhLogin;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::MyApp.Controls.entry txtUserName;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::MyApp.Controls.entry txtFullName;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::MyApp.Controls.entry txtPassword;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::MyApp.Controls.entry txtConfirmPassword;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::MyApp.Controls.button_lg btnLogin;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.Button btnStart;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private void InitializeComponent() {
            global::Xamarin.Forms.Xaml.Extensions.LoadFromXaml(this, typeof(LoginPage));
            indicator = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.ActivityIndicator>(this, "indicator");
            lblLogin = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.Label>(this, "lblLogin");
            swhLogin = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.Switch>(this, "swhLogin");
            txtUserName = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::MyApp.Controls.entry>(this, "txtUserName");
            txtFullName = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::MyApp.Controls.entry>(this, "txtFullName");
            txtPassword = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::MyApp.Controls.entry>(this, "txtPassword");
            txtConfirmPassword = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::MyApp.Controls.entry>(this, "txtConfirmPassword");
            btnLogin = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::MyApp.Controls.button_lg>(this, "btnLogin");
            btnStart = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.Button>(this, "btnStart");
        }
    }
}
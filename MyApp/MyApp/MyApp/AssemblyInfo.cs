using Android.App;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

// use traffic to send and receive request from api
[assembly: Application(UsesCleartextTraffic = true)]

// to check network connection
[assembly: UsesPermission(Android.Manifest.Permission.AccessNetworkState)]
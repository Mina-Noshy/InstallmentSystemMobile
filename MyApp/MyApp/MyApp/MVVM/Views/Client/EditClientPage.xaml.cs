using MyApp.Helper;
using MyApp.Services.APIServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using resx = MyApp.Resources.Languages;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MyApp.MVVM.ViewModels;
using MyApp.MVVM.Models.StandardModels;
using MyApp.Services.SQLiteServices;

namespace MyApp.MVVM.Views.Client
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditClientPage : ContentPage
    {
        int _clientId;
        ClientVM _client;

        ClientServices apiClient;
        SQLiteSettingServices sqlSetting;

        public EditClientPage(int clientId)
        {
            InitializeComponent();
            _clientId = clientId;

            apiClient = new ClientServices();
            sqlSetting = new SQLiteSettingServices();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            await GetApi();
        }


        async Task GetApi()
        {
            if (EastariaHelper.IsOnline())
            {
                _client = await apiClient.GetClient(_clientId);

                this.BindingContext = _client;

                indicator.IsRunning = false;
                return;
            }

            await DisplayAlert("", resx.AppResource.pleaseOpenNetConn, resx.AppResource.ok);
            await Task.Delay(3000);
            await GetApi();

        }

        async void txtEmail_Unfocused(object sender, FocusEventArgs e)
        {
            // =========> email validation
            if (!string.IsNullOrEmpty(txtEmail.Text.Trim()))
            {
                if (!EastariaHelper.IsEmail(txtEmail.Text.Trim()))
                {
                    await DisplayAlert("", resx.AppResource.email + " " + resx.AppResource.notCorrect, resx.AppResource.ok);
                    txtEmail.Focus();
                    return;
                }
            }
        }

        async void btnUpdate_Clicked(object sender, EventArgs e)
        {
            // =========> owner name validation
            if (string.IsNullOrEmpty(txtName.Text))
            {
                await DisplayAlert("", resx.AppResource.clientName + " " + resx.AppResource.required, resx.AppResource.ok);
                return;
            }

            if (txtName.Text.Trim().Length < 6)
            {
                await DisplayAlert("", resx.AppResource.client + " " + resx.AppResource.nameSoShort, resx.AppResource.ok);
                return;
            }

            // =========> address validation
            if (string.IsNullOrEmpty(txtAddress.Text))
            {
                await DisplayAlert("", resx.AppResource.address + " " + resx.AppResource.required, resx.AppResource.ok);
                return;
            }

            if (txtAddress.Text.Trim().Length < 6)
            {
                await DisplayAlert("", resx.AppResource.addressSoShort, resx.AppResource.ok);
                return;
            }

            // =========> mobile validation
            if (string.IsNullOrEmpty(txtMobile1.Text))
            {
                await DisplayAlert("", resx.AppResource.mobile + " " + resx.AppResource.required, resx.AppResource.ok);
                return;
            }

            if (txtMobile1.Text.Trim().Length < 10)
            {
                await DisplayAlert("", resx.AppResource.phoneSoShort, resx.AppResource.ok);
                return;
            }

            // =========> connection validation
            if (!EastariaHelper.IsOnline())
            {
                await DisplayAlert("", resx.AppResource.pleaseOpenNetConn, resx.AppResource.ok);
                return;
            }

            // question to continue deleting
            var delete = await DisplayActionSheet(resx.AppResource.sureToUpdate, resx.AppResource.cancel, "", resx.AppResource.yes, resx.AppResource.no);

            if (delete != resx.AppResource.yes)
                return;

            indicator.IsRunning = true;

            bool result = await apiClient.UpdateClient(new StandardClient
            {
                id = _clientId,
                userId = sqlSetting.GetSetting().userId,
                name = txtName.Text,
                addressDetails = txtAddress.Text,
                phone_1 = txtMobile1.Text,
                phone_2 = txtMobile2.Text,
                phone_3 = txtMobile3.Text,
                email = txtEmail.Text,
                fax = txtFax.Text
            });

            if (result)
            {
                await DisplayAlert("", resx.AppResource.client + " " + resx.AppResource.updatedSuccessfully, resx.AppResource.ok);

                indicator.IsRunning = false;

                await Navigation.PopToRootAsync();
            }

            else
            {
                await DisplayAlert("", resx.AppResource.somethingWrong, resx.AppResource.ok);
                indicator.IsRunning = false;
            }
        }

        async void btnCancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }


    }
}
using MyApp.MVVM.Models.StandardModels;
using MyApp.MVVM.ViewModels;
using MyApp.Services.APIServices;
using MyApp.Services.SQLiteServices;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace MyApp.Helper
{
    public static class EastariaHelper
    {
        // =============> properties <=============
        public static Uri BaseUri = new Uri("http://eastaria.com/api/");
        public static string Lang { get; set; } = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;


        private static AccountServices apiAccount = new AccountServices();
        public static SQLiteSettingServices settingServices = new SQLiteSettingServices();


        // =============> lists <=============




        // =============> methous <=============
        public static bool IsOnline() =>
            Connectivity.NetworkAccess == NetworkAccess.None ? false :
            Connectivity.NetworkAccess == NetworkAccess.Unknown ? false : true;

        public static string GetEmailFormatting(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                if (addr.Address == email)
                    return email;
                else
                    return email += "@mobile.com";
            }
            catch
            {
                return email += "@mobile.com";
            }

        }

        public static bool IsEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                if (addr.Address == email)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }

        }

        public async static Task<AuthenticationVM> GetToken(string userName = "", string password = "")
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                userName = settingServices.GetSetting().userName;
                password = settingServices.GetSetting().password;
            }

            AuthenticationVM _authModel = await apiAccount.GetToken(new LoginVM
            {
                userName = GetEmailFormatting(userName),
                password = password
            });

            if (!_authModel.isAuthenticated)
                return _authModel;

            // save user info in sqlite database
            var _settings = settingServices.GetSetting();

            _settings.userId = _authModel.userId;

            _settings.firstName = _authModel.firstName;
            _settings.lastName = _authModel.lastName;
            _settings.stopAt = _authModel.stopAt;

            _settings.isAuthenticated = _authModel.isAuthenticated;
            _settings.token = _authModel.token;
            _settings.refreshToken = _authModel.refreshToken;
            _settings.refreshTokenExpiration = _authModel.refreshTokenExpiration;
            _settings.email = _authModel.email;
            _settings.userName = _authModel.userName;
            _settings.password = password;
            _settings.settingsUpdatedAt = DateTime.UtcNow;
            if (_authModel.roles != null)
                if (_authModel.roles.Count > 0)
                    foreach (string rol in _authModel.roles)
                        _settings.roles += rol + " ";

            settingServices.Update(_settings);

            return _authModel;
        }

        public static List<string> GetInstallmentTypes()
        {
            if (Lang == "ar")
                return Enum.GetValues(typeof(InstallmentTypes)).Cast<string>().ToList();
            else
                return Enum.GetValues(typeof(InstallmentTypesEN)).Cast<string>().ToList();
        }
    }
}

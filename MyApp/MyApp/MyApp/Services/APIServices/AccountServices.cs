using MyApp.Helper;
using MyApp.MVVM.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Services.APIServices
{
    public interface IAccountServices
    {
        Task<ResponseVM> Register(RegisterVM model);
        Task<AuthenticationVM> GetToken(LoginVM model);
        Task<bool> IsEmailAvailable(string email);
    }

    public class AccountServices : IAccountServices
    {
        HttpClient httpClient = new HttpClient(new Xamarin.Android.Net.AndroidClientHandler());
        public AccountServices()
        {
            httpClient.BaseAddress = EastariaHelper.BaseUri;
        }

        public async Task<AuthenticationVM> GetToken(LoginVM model)
        {
            var json = JsonConvert.SerializeObject(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("Account/GetToken/token", content);

            var stringObj = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<AuthenticationVM>(stringObj);
        }

        public async Task<bool> IsEmailAvailable(string email)
        {
            var response = await httpClient.PostAsync($"Account/IsEmailAvailable/is-available/{email}", null);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }

            return false;
        }

        public async Task<ResponseVM> Register(RegisterVM model)
        {

            var json = JsonConvert.SerializeObject(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("Account/Register/register", content);

            if (response.IsSuccessStatusCode)
            {
                var contentReturned = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ResponseVM>(contentReturned);
            }

            return new ResponseVM
            {
                status = "error",
                message = "can not connect to the server"
            };

        }
    }
}

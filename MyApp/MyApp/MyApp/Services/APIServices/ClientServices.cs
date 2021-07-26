using MyApp.Helper;
using MyApp.MVVM.Models;
using MyApp.MVVM.Models.StandardModels;
using MyApp.MVVM.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Services.APIServices
{
    public interface IClientServices
    {
        Task<List<Client>> GetAll(string userId);
        Task<List<Client>> Search(string userId, string txt);
        Task<ClientVM> GetClient(int clientId);
        Task<bool> AddClient(StandardClient client);
        Task<bool> DeleteClient(int clientId);
        Task<bool> UpdateClient(StandardClient client);
    }

    public class ClientServices : IClientServices
    {
        HttpClient httpClient;
        public ClientServices()
        {
            httpClient = new HttpClient(new Xamarin.Android.Net.AndroidClientHandler())
            {
                BaseAddress = EastariaHelper.BaseUri
            };

            httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer",
                EastariaHelper.settingServices.GetSetting().token);
        }

        public async Task<bool> AddClient(StandardClient client)
        {
            var json = JsonConvert.SerializeObject(client);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("Clients/PostClient", content);

            if (response.IsSuccessStatusCode)
                return true;

            return false;
        }

        public async Task<List<Client>> GetAll(string userId)
        {

            List<Client> lst = new List<Client>();

            httpClient.DefaultRequestHeaders.Add("lang", EastariaHelper.Lang);

            var response = await httpClient.GetAsync($"Clients/GetClientsVM/{userId}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                lst = JsonConvert.DeserializeObject<List<Client>>(content);
                return lst;
            }
            return lst;
        }

        public async Task<ClientVM> GetClient(int clientId)
        {
            var response = await httpClient.GetAsync($"Clients/GetClientVM/{clientId}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ClientVM>(content);
            }

            return new ClientVM();
        }

        public async Task<List<Client>> Search(string userId, string txt)
        {
            List<Client> lst = new List<Client>();

            httpClient.DefaultRequestHeaders.Add("lang", EastariaHelper.Lang);

            var response = await httpClient.GetAsync($"Clients/SearchVM/{userId}/{txt}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                lst = JsonConvert.DeserializeObject<List<Client>>(content);
                return lst;
            }
            return lst;
        }

        public async Task<bool> DeleteClient(int clientId)
        {
            var response = await httpClient.DeleteAsync($"Clients/DeleteClient/{clientId}");

            if (response.IsSuccessStatusCode)
                return true;

            return false;
        }

        public async Task<bool> UpdateClient(StandardClient client)
        {
            var json = JsonConvert.SerializeObject(client);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PutAsync($"Clients/PutClient/{client.id}", content);

            if (response.IsSuccessStatusCode)
                return true;

            return false;
        }
    }
}

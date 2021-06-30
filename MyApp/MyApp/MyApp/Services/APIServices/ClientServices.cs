using MyApp.Helper;
using MyApp.MVVM.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Services.APIServices
{
    public interface IClientServices
    {
        Task<List<Client>> GetAll(int userId);
        Task<List<Client>> Search(int userId, string txt);
        Task<Client> GetClient(int clientId);
        Task<bool> AddClient(Client client);
        Task<bool> DeleteClient(int clientId);
    }

    public class ClientServices : IClientServices
    {
        HttpClient httpClient = new HttpClient(new Xamarin.Android.Net.AndroidClientHandler());
        public ClientServices()
        {
            httpClient.BaseAddress = EastariaHelper.BaseUri;
        }

        public async Task<bool> AddClient(Client client)
        {
            var json = JsonConvert.SerializeObject(client);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("Markets/PostMarket", content);

            if (response.IsSuccessStatusCode)
                return true;

            return false;
        }

        public async Task<List<Client>> GetAll(int userId)
        {

            List<Client> lst = new List<Client>();

            httpClient.DefaultRequestHeaders.Add("lang", EastariaHelper.Lang);

            var response = await httpClient.GetAsync($"Markets/GetMarketsVM/{userId}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                lst = JsonConvert.DeserializeObject<List<Client>>(content);
                return lst;
            }
            return lst;
        }

        public async Task<Client> GetClient(int clientId)
        {
            var response = await httpClient.GetAsync($"Markets/GetMarket/{clientId}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Client>(content);
            }

            return new Client();
        }

        public async Task<List<Client>> Search(int userId, string txt)
        {
            List<Client> lst = new List<Client>();

            httpClient.DefaultRequestHeaders.Add("lang", EastariaHelper.Lang);

            var response = await httpClient.GetAsync($"Markets/SearchVM/{userId}/{txt}");
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
            //var json = JsonConvert.SerializeObject(model);
            //var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.DeleteAsync($"Markets/PostMarket/{clientId}");

            if (response.IsSuccessStatusCode)
                return true;

            return false;
        }


    }
}

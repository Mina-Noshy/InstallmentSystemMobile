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
    public interface IBillServices
    {
        Task<List<Bill>> GetAll(string userId);
        Task<List<Bill>> GetAll(int clientId);

        Task<BillVM> GetBill(int billId);
        Task<bool> AddBill(StandardBill bill);
        Task<bool> DeleteBill(int billId);
    }

    public class BillServices : IBillServices
    {
        HttpClient httpClient;
        public BillServices()
        {
            httpClient = new HttpClient(new Xamarin.Android.Net.AndroidClientHandler())
            {
                BaseAddress = EastariaHelper.BaseUri
            };

            httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer",
                EastariaHelper.settingServices.GetSetting().token);
        }

        public async Task<bool> AddBill(StandardBill bill)
        {
            var json = JsonConvert.SerializeObject(bill);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("Bills/PostBill", content);

            if (response.IsSuccessStatusCode)
                return true;

            return false;
        }

        public async Task<List<Bill>> GetAll(string userId)
        {

            List<Bill> lst = new List<Bill>();

            httpClient.DefaultRequestHeaders.Add("lang", EastariaHelper.Lang);

            var response = await httpClient.GetAsync($"Bills/GetUserBillsVM/{userId}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                lst = JsonConvert.DeserializeObject<List<Bill>>(content);
                return lst;
            }
            return lst;
        }

        public async Task<BillVM> GetBill(int billId)
        {
            var response = await httpClient.GetAsync($"Bills/GetBillVM/{billId}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<BillVM>(content);
            }

            return new BillVM();
        }

        public async Task<List<Bill>> GetAll(int clientId)
        {
            List<Bill> lst = new List<Bill>();

            httpClient.DefaultRequestHeaders.Add("lang", EastariaHelper.Lang);

            var response = await httpClient.GetAsync($"Bills/GetClientBillsVM/{clientId}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                lst = JsonConvert.DeserializeObject<List<Bill>>(content);
                return lst;
            }
            return lst;
        }

        public async Task<bool> DeleteBill(int billId)
        {
            var response = await httpClient.DeleteAsync($"Bills/DeleteBill/{billId}");

            if (response.IsSuccessStatusCode)
                return true;

            return false;
        }

    }
}

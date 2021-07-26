using MyApp.Helper;
using MyApp.MVVM.Models;
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
    public interface IInstallmentServices
    {
        Task<List<Installment>> GetBillInstallmentsVM(int billId);
        Task<List<Installment>> GetAllUnreceivedVM(string userId);
        Task<List<Installment>> GetAllTodayVM(string userId);
        Task<List<Installment>> GetReceivedTodayVM(string userId);
        Task<List<Installment>> GetUnreceivedTodayVM(string userId);
        Task<List<Installment>> GetByDayVM(string userId, string dateTime);

        Task<InstallmentVM> GetInstallmentVM(int id);
        Task<bool> SwitchInstallmentState(int id);

    }

    public class InstallmentServices : IInstallmentServices
    {
        HttpClient httpClient;
        public InstallmentServices()
        {
            httpClient = new HttpClient(new Xamarin.Android.Net.AndroidClientHandler())
            {
                BaseAddress = EastariaHelper.BaseUri
            };

            httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer",
                EastariaHelper.settingServices.GetSetting().token);
        }

        public async Task<List<Installment>> GetAllTodayVM(string userId)
        {
            List<Installment> lst = new List<Installment>();

            httpClient.DefaultRequestHeaders.Add("lang", EastariaHelper.Lang);

            var response = await httpClient.GetAsync($"Installments/GetAllTodayVM/{userId}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                lst = JsonConvert.DeserializeObject<List<Installment>>(content);
                return lst;
            }
            return lst;
        }

        public async Task<List<Installment>> GetAllUnreceivedVM(string userId)
        {
            List<Installment> lst = new List<Installment>();

            httpClient.DefaultRequestHeaders.Add("lang", EastariaHelper.Lang);

            var response = await httpClient.GetAsync($"Installments/GetAllUnreceivedVM/{userId}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                lst = JsonConvert.DeserializeObject<List<Installment>>(content);
                return lst;
            }
            return lst;
        }

        public async Task<List<Installment>> GetBillInstallmentsVM(int billId)
        {
            List<Installment> lst = new List<Installment>();

            httpClient.DefaultRequestHeaders.Add("lang", EastariaHelper.Lang);

            var response = await httpClient.GetAsync($"Installments/GetBillInstallmentsVM/{billId}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                lst = JsonConvert.DeserializeObject<List<Installment>>(content);
                return lst;
            }
            return lst;
        }

        public async Task<List<Installment>> GetByDayVM(string userId, string dateTime)
        {
            List<Installment> lst = new List<Installment>();

            httpClient.DefaultRequestHeaders.Add("lang", EastariaHelper.Lang);

            var response = await httpClient.GetAsync($"Installments/GetByDayVM/{userId}/{dateTime}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                lst = JsonConvert.DeserializeObject<List<Installment>>(content);
                return lst;
            }
            return lst;
        }

        public async Task<List<Installment>> GetReceivedTodayVM(string userId)
        {
            List<Installment> lst = new List<Installment>();

            httpClient.DefaultRequestHeaders.Add("lang", EastariaHelper.Lang);

            var response = await httpClient.GetAsync($"Installments/GetReceivedTodayVM/{userId}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                lst = JsonConvert.DeserializeObject<List<Installment>>(content);
                return lst;
            }
            return lst;
        }

        public async Task<List<Installment>> GetUnreceivedTodayVM(string userId)
        {
            List<Installment> lst = new List<Installment>();

            httpClient.DefaultRequestHeaders.Add("lang", EastariaHelper.Lang);

            var response = await httpClient.GetAsync($"Installments/GetUnreceivedTodayVM/{userId}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                lst = JsonConvert.DeserializeObject<List<Installment>>(content);
                return lst;
            }
            return lst;
        }

        public async Task<InstallmentVM> GetInstallmentVM(int id)
        {
            InstallmentVM install = new InstallmentVM();
            httpClient.DefaultRequestHeaders.Add("lang", EastariaHelper.Lang);

            var response = await httpClient.GetAsync($"Installments/GetInstallmentVM/{id}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                install = JsonConvert.DeserializeObject<InstallmentVM>(content);
                return install;
            }
            return install;
        }

        public async Task<bool> SwitchInstallmentState(int id)
        {
            var response = await httpClient.PostAsync($"Installments/SwitchInstallmentState/{id}", null);

            if (response.IsSuccessStatusCode)
                return true;

            return false;
        }

    }
}

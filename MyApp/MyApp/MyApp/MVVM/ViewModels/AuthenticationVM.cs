using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.MVVM.ViewModels
{
    public class AuthenticationVM
    {
        public string userId { get; set; }
        public string message { get; set; }
        public bool isAuthenticated { get; set; } = false;
        public string userName { get; set; }
        public string email { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public List<string> roles { get; set; }
        public string token { get; set; }
        public string refreshToken { get; set; }
        public DateTime refreshTokenExpiration { get; set; }

        public int cityId { get; set; }
        public int countryId { get; set; }
        public int userTypeId { get; set; }
        public string address { get; set; }
    }
}

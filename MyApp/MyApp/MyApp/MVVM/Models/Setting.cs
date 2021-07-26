using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.MVVM.Models
{
    public class Setting
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public string userId { get; set; } = null;
        public string address { get; set; } = null;
        public string userName { get; set; } = null;
        public string email { get; set; } = null;
        public string password { get; set; }
        public string phone { get; set; } = null;
        public string token { get; set; } = null;
        public bool isAuthenticated { get; set; }
        public string firstName { get; set; } = null;
        public string lastName { get; set; } = null;
        public DateTime? stopAt { get; set; } = null;
        public string roles { get; set; } = null;
        public string refreshToken { get; set; } = null;
        public DateTime? refreshTokenExpiration { get; set; } = null;

        public DateTime? settingsUpdatedAt { get; set; } = null;

        public bool isSettingsUpdated()
        {
            return settingsUpdatedAt.Value.AddDays(3) > DateTime.UtcNow;
        }

        public bool isTokenExpired()
        {
            return refreshTokenExpiration.Value < DateTime.UtcNow.AddDays(3);
        }
    }
}

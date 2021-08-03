using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.MVVM.ViewModels
{
    public class UserVM
    {
        public string userId { get; set; }
        public bool isAuthenticated { get; set; }
        public string userName { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public DateTime stopAt { get; set; }
        public List<string> roles { get; set; }
    }
}

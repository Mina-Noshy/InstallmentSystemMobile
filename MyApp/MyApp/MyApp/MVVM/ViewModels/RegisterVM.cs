using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.MVVM.ViewModels
{
    public class RegisterVM
    {
        public int cityId { get; set; }
        public int userTypeId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string userName { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }
        public string password { get; set; }

        // for just pass value to registration page
        public string fullName { get; set; }
    }
}

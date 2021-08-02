using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.MVVM.ViewModels
{
    public class UpdateUserPasswordVM
    {
        public string userId { get; set; }
        public string currentPassword { get; set; }
        public string newPassword { get; set; }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyApp.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class frame : Frame
    {
        public frame()
        {
            InitializeComponent();
        }
    }
}
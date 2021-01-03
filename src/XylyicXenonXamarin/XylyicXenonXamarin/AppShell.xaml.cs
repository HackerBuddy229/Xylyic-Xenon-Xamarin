using System;
using System.Collections.Generic;
using Xamarin.Forms;
using XylyicXenonXamarin.ViewModels;
using XylyicXenonXamarin.Views;

namespace XylyicXenonXamarin
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.Widget;
using Xamarin.Forms;
using XylyicXenonXamarin.interfaces;
using Application = Android.App.Application;

namespace XylyicXenonXamarin.Droid.services
{
    public class ToastService : IToastService
    {
        public void CreateToast(string message) =>
            Toast.MakeText(Application.Context, message, ToastLength.Short)?.Show();
        
    }
}

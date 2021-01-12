using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XylyicXenonXamarin.ViewModels;

namespace XylyicXenonXamarin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdvancedSettingsPage : ContentPage
    {
        public AdvancedSettingsPage(AdvancedSettingsPageViewModel vw)
        {
            BindingContext = vw;
            InitializeComponent();
        }

        private void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            (BindingContext as AdvancedSettingsPageViewModel)?.CopyHistoryCommand.Execute(e);
        }
    }
}
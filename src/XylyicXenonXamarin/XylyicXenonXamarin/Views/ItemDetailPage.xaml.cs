using System.ComponentModel;
using Xamarin.Forms;
using XylyicXenonXamarin.ViewModels;

namespace XylyicXenonXamarin.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}
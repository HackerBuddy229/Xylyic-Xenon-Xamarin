using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using XylyicXenonXamarin.Annotations;
using XylyicXenonXamarin.interfaces;

namespace XylyicXenonXamarin.ViewModels
{
    public class AdvancedSettingsPageViewModel : INotifyPropertyChanged
    {
        private readonly IToastService _toastService;
        private bool _displayPrefix = true;
        private string _prefix = string.Empty;

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public AdvancedSettingsPageViewModel(IToastService toastService)
        {
            _toastService = toastService;
            CopyHistoryCommand = new Command(CopyToClipboard);
        }

        public bool DisplayPrefix
        {
            get => _displayPrefix;
            set
            {
                _displayPrefix = value;
                OnPropertyChanged(nameof(DisplayPrefix));
            }
        }

        public string Prefix
        {
            get => _prefix;
            set
            {
                _prefix = value;
                OnPropertyChanged(nameof(Prefix));
            }
        }

        private async void CopyToClipboard(object obj)
        {
            var eventArgs = obj as ItemTappedEventArgs;
            var item = eventArgs?.Item;
            var name = item as string;

            await Clipboard.SetTextAsync(name);
            _toastService.CreateToast("Project name copied!");
        }

        public IList<string> History { get; set; } = new List<string>();
        public bool ShowHistory => History.Any();


        public Command CopyHistoryCommand { get; }
    }
}

using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using XylyicXenonXamarin.Annotations;
using XylyicXenonXamarin.interfaces;
using XylyicXenonXamarin.services;

namespace XylyicXenonXamarin.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private readonly IProjectNameBuilderService _projectNameBuilderService;
        private readonly IToastService _toastService;

        private readonly string _aboutMessage = "Welcome... \n" +
                                                "This app allows you to generate project names for use with your\n" +
                                                "various projects.\n" +
                                                "Just press the \'We can do better\' button \n" +
                                                "to get started." +
                                                "\n \n \n" +
                                                "This app was made by: \n" +
                                                "Rasmus Bengtsson\n" +
                                                "The credit for the picture goes to: \n" +
                                                "Jonny James - Unsplash\n" +
                                                $"Beryllium Apps\u00a9 - {DateTime.UtcNow.Year}";

        public MainPageViewModel(IProjectNameBuilderService projectNameBuilderService, IToastService toastService)
        {
            _projectNameBuilderService = projectNameBuilderService;
            _toastService = toastService;

            RefreshCommand = new Command( async () => await RefreshProjectName());

            ShowInfoCommand = new Command(async () 
                => await Application.Current.MainPage.DisplayAlert("About", _aboutMessage, "Ok"));

            CopyNameCommand = new Command(async () =>
            {
                await Clipboard.SetTextAsync(ProjectName);
                _toastService.CreateToast("Project name copied!");
            });

            NavigateToAdvancedCommand = new Command(async () =>
            {
                await Application.Current.MainPage.Navigation.PushModalAsync(new AdvancedSettingsPage());
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async Task RefreshProjectName()
        {
            var name = await _projectNameBuilderService.GetProjectName();
            ProjectName = name;
        }

        private string _projectName = string.Empty;

        public string ProjectName
        {
            get => _projectName;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    return;
                _projectName = value;

                OnPropertyChanged(nameof(ProjectName));
            }
        }

        public Command RefreshCommand { get; }
        public Command NavigateToAdvancedCommand { get; }
        public Command ShowInfoCommand { get; }
        public Command CopyNameCommand { get; }
    }
}

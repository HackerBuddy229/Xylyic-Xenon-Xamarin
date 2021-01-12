using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using XylyicXenonXamarin.Annotations;
using XylyicXenonXamarin.interfaces;
using XylyicXenonXamarin.models;
using XylyicXenonXamarin.services;

namespace XylyicXenonXamarin.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private readonly IProjectNameBuilderService _projectNameBuilderService;
        private readonly IToastService _toastService;

        private AdvancedSettingsPageViewModel settingsPageViewModel;
        private readonly IList<string> _history = new List<string>();

        private readonly string _aboutMessage = "Welcome... \n" +
                                                "This app allows you to generate project names for use with your\n" +
                                                "various projects.\n" +
                                                "Just press the \'We can do better\' button \n" +
                                                "to get started." +
                                                "\n \n \n" +
                                                "This app was made by: \n" +
                                                "Rasmus Bengtsson\n" +
                                                "The credit for the Background Image goes to: \n" +
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
                var viewModel = settingsPageViewModel ?? new AdvancedSettingsPageViewModel(_toastService);
                viewModel.History = _history;
                await Application.Current.MainPage.Navigation.PushAsync(new AdvancedSettingsPage(viewModel));
                settingsPageViewModel = viewModel;
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
            var settingsViewModel = settingsPageViewModel ?? new AdvancedSettingsPageViewModel(_toastService);

            //TODO: switch instead of null coalesing
            var name = await _projectNameBuilderService.GetProjectName(new NameOptions
            {
                Prefix = string.IsNullOrWhiteSpace(settingsViewModel.Prefix) ? "Project" : settingsViewModel.Prefix,
                UsePrefix = settingsViewModel.DisplayPrefix
            });
            ProjectName = name;
            AddToHistory(name);
        }

        private void AddToHistory(string name)
        {
            _history.Add(name);
            if (_history.Count > 10)
                _history.RemoveAt(0);
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

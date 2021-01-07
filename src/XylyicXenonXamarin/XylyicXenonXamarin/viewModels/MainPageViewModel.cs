using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using XylyicXenonXamarin.Annotations;
using XylyicXenonXamarin.services;

namespace XylyicXenonXamarin.viewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private readonly IProjectNameBuilderService _projectNameBuilderService;

        public MainPageViewModel(IProjectNameBuilderService projectNameBuilderService)
        {
            _projectNameBuilderService = projectNameBuilderService;
            RefreshCommand = new Command( async () => await RefreshProjectName());
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
    }
}

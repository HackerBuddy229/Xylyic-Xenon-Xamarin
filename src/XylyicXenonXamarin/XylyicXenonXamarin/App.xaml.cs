using System;
using Autofac;
using Autofac.Core;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XylyicXenonXamarin.services;
using XylyicXenonXamarin.viewModels;

namespace XylyicXenonXamarin
{
    public partial class App : Application
    {
        

        public App()
        {
            InitializeComponent();

            var container = SetupContainer();

            var mainPage = new MainPage {BindingContext = container.Resolve<MainPageViewModel>()};
            MainPage = new NavigationPage(mainPage);
        }

        private IContainer SetupContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<MainPageViewModel>()
                .AsSelf()
                .InstancePerLifetimeScope();

            builder.RegisterType<FileStorageService>()
                .As<IFileStorageService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ProjectNameBuilderService>()
                .As<IProjectNameBuilderService>()
                .InstancePerLifetimeScope();

            return builder.Build();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

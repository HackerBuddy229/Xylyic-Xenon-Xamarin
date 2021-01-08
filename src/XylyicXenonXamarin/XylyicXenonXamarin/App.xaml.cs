using System;
using System.Threading.Tasks;
using Autofac;
using Autofac.Core;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XylyicXenonXamarin.interfaces;
using XylyicXenonXamarin.services;
using XylyicXenonXamarin.ViewModels;

namespace XylyicXenonXamarin
{
    public partial class App : Application
    {
        

        public App()
        {
            InitializeComponent();

            var container = SetupContainer();

            var mainPage = new MainPage(container.Resolve<MainPageViewModel>());
            Task.Run(async () =>
            {
                if (mainPage.BindingContext is MainPageViewModel wm)
                    await wm.RefreshProjectName();
            });

            //if (mainPage.BindingContext is MainPageViewModel wm)
            //    wm.RefreshProjectName().ConfigureAwait(false).GetAwaiter().GetResult();


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

            builder.Register(c => DependencyService.Resolve<IToastService>())
                    .As<IToastService>()
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

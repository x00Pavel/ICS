using System;
using System.Globalization;
using System.Threading;
using System.Windows;
using FestivalApp.App.Extensions;
using FestivalApp.App.Factories;
using FestivalApp.App.Services;
using FestivalApp.App.ViewModels;
using FestivalApp.BL.Facades;
using FestivalApp.BL.Mappers;
using FestivalApp.BL.Models;
using FestivalApp.DAL;
using FestivalApp.DAL.Entities;
using FestivalApp.DAL.Factories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace FestivalApp.App
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost _host;

        public App()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");

            _host = Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration(ConfigureAppConfiguration)
                .ConfigureServices((context, services) => { ConfigureServices(context.Configuration, services); })
                .Build();
        }

        private static void ConfigureAppConfiguration(HostBuilderContext context, IConfigurationBuilder builder)
        {
            builder.AddJsonFile(@"appsettings.json", false, true);
        }

        private static void ConfigureServices(IConfiguration configuration,
            IServiceCollection services)
        {
            services.AddSingleton<MainWindow>();

            services.AddSingleton<IEntityFactory, EntityFactory>();

            services.AddSingleton<IMapper<BandEntity, BandListModel, BandDetailModel>, BandMapper>();
            services.AddSingleton<IMapper<StageEntity, StageListModel, StageDetailModel>, StageMapper>();
            services.AddSingleton<IMapper<PerformanceEntity, PerformanceListModel, PerformanceDetailModel>, PerformanceMapper>();

            services.AddSingleton<RepositoryBase<BandEntity>>();
            services.AddSingleton<RepositoryBase<StageEntity>>();
            services.AddSingleton<RepositoryBase<PerformanceEntity>>();

            services.AddSingleton<BandFacade>();
            services.AddSingleton<StageFacade>();
            services.AddSingleton<PerformanceFacade>();

            services.AddSingleton<IMediator, Mediator>();

            services.AddSingleton<MainViewModel>();

            services.AddSingleton<IBandListViewModel, BandsViewModel>();
            services.AddSingleton<IStageListViewModel, StagesViewModel>();
            services.AddSingleton<IPerformanceListViewModel, PerformancesViewModel>();

            services.AddFactory<IBandDetailViewModel, BandDetailViewModel>();
            services.AddFactory<IStageDetailViewModel, StageDetailViewModel>();
            services.AddFactory<IPerformanceDetailViewModel, PerformanceDetailViewModel>();
            services.AddFactory<IPerformanceNewViewModel, PerformanceNewViewModel>();

            services.AddSingleton<FestivalDbContext>(provider =>
                new SqlServerDbContextFactory(configuration.GetConnectionString("DefaultConnection")).CreateDbContext()
            );

        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await _host.StartAsync();

#if DEBUG
            var dbContext = _host.Services.GetRequiredService<FestivalDbContext>();
            await dbContext.Database.MigrateAsync();

#endif

            var mainWindow = _host.Services.GetRequiredService<MainWindow>();
            mainWindow.Show();

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            using (_host)
            {
                await _host.StopAsync(TimeSpan.FromSeconds(5));
            }

            base.OnExit(e);
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Shapes;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.Extensions.DependencyInjection;
using NlogTest.Interfaces;
using NlogTest.Services;
using NLog.Extensions.Logging;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace NlogTest
{
    public partial class App : Application
    {
        private IServiceProvider _serviceProvider;
        private MainWindow? _mainWindow;
        public App()
        {
            this.InitializeComponent();

            ServiceCollection services = new ServiceCollection();

            services.AddSingleton<IConsole>(
                implementationFactory: static _ => new DefaultConsole
                {
                    // クラスのフィールドを初期化してDIさせる
                    IsEnabled = true,
                }
            );

            services.AddSingleton<IGreetingService, DefaultGreetingService>();
            services.AddSingleton<FarewellService>();
            services.AddTransient<MainWindow>();

            IConfigurationRoot config = new ConfigurationBuilder()
                                                .SetBasePath(System.IO.Path.Combine(AppContext.BaseDirectory, "Settings"))
                                                .AddJsonFile("nlog.json", optional: true, reloadOnChange: true)
                                                .Build();
            services.AddLogging(builder =>
            {
                builder.ClearProviders();
                builder.AddNLog(config);
            });

            _serviceProvider  = services.BuildServiceProvider();
        }

        /// <summary>
        /// Invoked when the application is launched.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            _mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            _mainWindow.Activate();
        }
    }
}

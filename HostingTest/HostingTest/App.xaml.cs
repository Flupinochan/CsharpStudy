using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using HostingTest.Interfaces;
using HostingTest.Services;
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
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace HostingTest
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();

            HostApplicationBuilder builder = Host.CreateApplicationBuilder();

            builder.Services.AddTransient<IExampleTransientService, ExampleTransientService>();
            builder.Services.AddScoped<IExampleScopedService, ExampleScopedService>();
            builder.Services.AddSingleton<IExampleSingletonService, ExampleSingletonService>();
            builder.Services.AddTransient<ServiceLifetimeReporter>();

            using IHost host = builder.Build();

            ExemplifyServiceLifetime(host.Services, "Lifetime 1");
            ExemplifyServiceLifetime(host.Services, "Lifetime 2");

        }

        /// <summary>
        /// Invoked when the application is launched.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            m_window = new MainWindow();
            m_window.Activate();
        }

        private Window? m_window;

        static void ExemplifyServiceLifetime(IServiceProvider hostProvider, string lifetime)
        {
            using IServiceScope serviceScope = hostProvider.CreateScope();
            IServiceProvider provider = serviceScope.ServiceProvider;
            ServiceLifetimeReporter logger = provider.GetRequiredService<ServiceLifetimeReporter>();
            logger.ReportServiceLifetimeDetails(
                $"{lifetime}: Call 1 to provider.GetRequiredService<ServiceLifetimeReporter>()");

            Console.WriteLine("...");

            logger = provider.GetRequiredService<ServiceLifetimeReporter>();
            logger.ReportServiceLifetimeDetails(
                $"{lifetime}: Call 2 to provider.GetRequiredService<ServiceLifetimeReporter>()");

            Console.WriteLine();
        }
    }
}

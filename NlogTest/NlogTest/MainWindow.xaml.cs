using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Microsoft.Extensions.Logging;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using NlogTest.Interfaces;
using NlogTest.Services;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace NlogTest
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        private IGreetingService _greetingService;
        private FarewellService _farewellService;
        private ILogger<MainWindow> _logger;

        public MainWindow(IGreetingService greetingService, FarewellService farewellService, ILogger<MainWindow> logger)
        {
            this.InitializeComponent();
            _greetingService = greetingService;
            _farewellService = farewellService;
            _logger = logger;
        }

        public void myButton_Click(object sender, RoutedEventArgs e)
        {
            myButton.Content = "Clicked";
            _greetingService.Greet("Hello");
            _farewellService.SayGoodbye("GoodBye");
            _logger.LogInformation("Nlog InformationèoóÕ");
        }
    }
}

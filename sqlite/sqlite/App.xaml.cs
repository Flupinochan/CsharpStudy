using System.Configuration;
using System.IO;
using System.Windows;

namespace sqlite;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App:Application
{

    private static String DatabaseName = ConfigurationManager.AppSettings["DB_NAME"]!;
    private static String FolderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
    public static String DatabasePath = Path.Combine(FolderPath, DatabaseName);
}


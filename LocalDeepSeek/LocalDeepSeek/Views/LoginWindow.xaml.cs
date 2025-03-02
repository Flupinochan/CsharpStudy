using LocalDeepSeek.Models;
using LocalDeepSeek.Utils;
using System.Windows;

namespace LocalDeepSeek.Views;

/// <summary>
/// LoginWindow.xaml の相互作用ロジック
/// </summary>
public partial class LoginWindow : Window
{
    public static Int32 UserId;

    public LoginWindow()
    {
        InitializeComponent();
    }

    private void LoginButton_Click(Object sender, RoutedEventArgs e)
    {
        // ユーザ名の取得
        String userName = this.UsernameTextBox.Text;

        // パスワードを取得
        String password = this.PasswordTextBox.Text;
        String hashPassword = StringManagement.GetHashPassword(password);

        // ユーザを新規作成する場合
        if (this.NewUserToggleButton.IsChecked == true)
        {
            UserId = SqliteManagement.CreateUser(userName, hashPassword);
        }
        // 既存ユーザの認証処理をする場合
        else
        {
            UserId = SqliteManagement.ValidateUserLogin(userName, hashPassword);
        }

        // 認証失敗した場合
        if (UserId == -1)
        {
            MessageBox.Show(this, "認証に失敗しました", "エラー", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }else if (UserId == -2)
        {
            MessageBox.Show(this, "すでに同一ユーザが存在します", "エラー", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        // Mainwindowの表示
        MainWindow mainWindow = new MainWindow()
        {
            Owner = this,
            WindowStartupLocation = WindowStartupLocation.CenterOwner
        };
        this.Hide();
        mainWindow.ShowDialog();
        this.Show();
    }
}

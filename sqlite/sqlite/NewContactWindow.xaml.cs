using sqlite.Classes;
using SQLite;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace sqlite;

/// <summary>
/// NewContactWindow.xaml の相互作用ロジック
/// </summary>
public partial class NewContactWindow:Window
{
    public NewContactWindow()
    {
        this.InitializeComponent();
    }

    // 新規データ挿入
    private void Button_Click(Object sender, RoutedEventArgs e)
    {
        Contact contact = new Contact()
        {
            Name = this.nameTextBox.Text,
            Email = this.emailTextBox.Text,
            Phone = this.phoneTextBox.Text,
        };

        // SQLite接続
        using(SQLiteConnection sqliteConnection = new SQLiteConnection(App.DatabasePath))
        {
            // トランザクション開始
            sqliteConnection.BeginTransaction();

            try
            {
                // データクラス型のテーブルを作成 (既にある場合は何もしない)
                sqliteConnection.CreateTable<Contact>();

                // データクラスを挿入
                sqliteConnection.Insert(contact);

                // トランザクション終了 (コミット)
                sqliteConnection.Commit();
            }
            catch(Exception ex)
            {
                // トランザクション終了 (ロールバック)
                sqliteConnection.Rollback();
                Debug.WriteLine(ex);
            }
        }
        this.DialogResult = true;
    }





}

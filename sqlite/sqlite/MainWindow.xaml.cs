using sqlite.Classes;
using SQLite;
using System.Windows;
using System.Windows.Controls;

namespace sqlite;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow:Window
{
    private List<Contact> _contacts;

    public MainWindow()
    {
        this.InitializeComponent();
        this._contacts = new List<Contact>();
        ReadDatabase();
    }

    // ウィンドウ表示
    private void Button_Click(Object sender, RoutedEventArgs e)
    {
        NewContactWindow newContactWindow = new NewContactWindow()
        {
            Owner = this,
            WindowStartupLocation = WindowStartupLocation.CenterOwner,
        };
        newContactWindow.ShowDialog();

        ReadDatabase();
    }


    // データベース読み込み
    private void ReadDatabase()
    {
        using (SQLiteConnection sqliteConnection = new SQLiteConnection(App.DatabasePath))
        {
            sqliteConnection.BeginTransaction();

            try
            {
                sqliteConnection.CreateTable<Contact>();
                this._contacts = sqliteConnection.Table<Contact>().ToList().OrderBy(contact => contact.Name).ToList();

                sqliteConnection.Commit();
            }
            catch
            {
                sqliteConnection.Rollback();
            }
        }

        if (this._contacts != null)
        {
            //this.ContactsListView.Items.Clear();
            //foreach(Contact contact in contacts)
            //{
            //    this.ContactsListView.Items.Add(new ListViewItem()
            //    {
            //        Content = contact
            //    });
            //}

            // Listはバインド可能
            this.ContactsListView.ItemsSource = this._contacts;
        }
    }

    // フィルタ
    private void FilterTextBox_TextChanged(Object sender, TextChangedEventArgs e)
    {
        if (sender is TextBox filterTextBox)
        {
            List<Contact> filterdContacts = this._contacts.Where(contact => contact.Name.ToLower().Contains(filterTextBox.Text.ToLower())).ToList();
            this.ContactsListView.ItemsSource = filterdContacts;
        }
    }

    // ListViewItemクリック時のEvent
    private void ContactsListView_SelectionChanged(Object sender, SelectionChangedEventArgs e)
    {
        Contact selectedContact = (Contact)this.ContactsListView.SelectedItem;
        if(selectedContact is null) return;
        ContactUpdateWindow contactUpdateWindow = new ContactUpdateWindow(selectedContact)
        {
            Owner = this,
            WindowStartupLocation = WindowStartupLocation.CenterScreen
        };
        contactUpdateWindow.ShowDialog();
        ReadDatabase();
    }
}
using sqlite.Classes;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace sqlite
{
    /// <summary>
    /// ContactUpdateWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class ContactUpdateWindow : Window
    {
        private Contact _contact;
        public ContactUpdateWindow(Contact contact)
        {
            InitializeComponent();
            this._contact = contact;
            this.nameTextBox.Text = _contact.Name;
            this.emailTextBox.Text = _contact.Email;
            this.phoneTextBox.Text = _contact.Phone;
        }



        private void UpdateButton_Click(Object sender, RoutedEventArgs e)
        {
            this._contact.Name = this.nameTextBox.Text;
            this._contact.Email = this.emailTextBox.Text;
            this._contact.Phone = this.phoneTextBox.Text;
            using(SQLiteConnection sqliteConnection = new SQLiteConnection(App.DatabasePath))
            {
                sqliteConnection.BeginTransaction();
                try
                {
                    sqliteConnection.Update(this._contact);
                    sqliteConnection.Commit();
                }
                catch
                {
                    sqliteConnection.Rollback();
                }
            }

            this.DialogResult = true;
        }

        private void DeleteButton_Click(Object sender, RoutedEventArgs e)
        {
            using(SQLiteConnection sqliteConnection = new SQLiteConnection(App.DatabasePath))
            {
                sqliteConnection.BeginTransaction();
                try
                {
                    sqliteConnection.Delete(this._contact);
                    sqliteConnection.Commit();
                }
                catch
                {
                    sqliteConnection.Rollback();
                }
            }

            this.DialogResult = true;
        }
    }
}

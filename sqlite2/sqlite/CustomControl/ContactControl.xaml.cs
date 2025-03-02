using sqlite.Classes;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace sqlite.CustomControl
{
    /// <summary>
    /// ContactControl.xaml の相互作用ロジック
    /// </summary>
    /// 

    // ユーザコントロールは、setterで値を渡す
    public partial class ContactControl : UserControl
    {
        //private Contact _contact;
        //// valueは、set(contact) のcontactが入る
        //public Contact Contact
        //{
        //    get { return _contact; }
        //    set
        //    {
        //        _contact = value;
        //        this.NameTextBlock.Text = _contact.Name;
        //        this.EmailTextBlock.Text = _contact.Email;
        //        this.PhoneTextBlock.Text = _contact.Phone;
        //    }
        //}

        // 以下の機能はデータバインディングを提供する

        // propdb タブ補完で自動生成
        public Contact Contact
        {
            get { return (Contact)GetValue(ContactProperty); }
            set { SetValue(ContactProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Contact.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ContactProperty =
            DependencyProperty.Register("Contact", typeof(Contact), typeof(ContactControl), new PropertyMetadata(new Contact() { Name = "name", Email = "email", Phone = "phone" }, SetText));
                                                                                                                  // SetTextを推奨事項からメソッド生成

        private static void SetText(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ContactControl contactControl = d as ContactControl;
            if (contactControl is null) return;
            contactControl.NameTextBlock.Text = (e.NewValue as Contact).Name;
            contactControl.EmailTextBlock.Text = (e.NewValue as Contact).Email;
            contactControl.PhoneTextBlock.Text = (e.NewValue as Contact).Phone;
        }

        public ContactControl()
        {
            InitializeComponent();
        }
    }
}

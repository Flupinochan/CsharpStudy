using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PasswordManagement
{
    public partial class RegisterPasswordForm : Form
    {
        public PasswordInfo passwordInfo { get; set; }

        public RegisterPasswordForm(List<string> categoryList)
        {
            // 各コントロールの初期化
            InitializeComponent();
            // パスワード情報格納クラスの初期化
            passwordInfo = new PasswordInfo();
            // カテゴリを初期化
            CategoryBomboBox.Items.AddRange(categoryList.ToArray());
        }

        // 登録ボタンが押されたときの処理
        private void RegisterButton_Click(object sender, EventArgs e)
        {
            // パスワード情報を取得し、クラスに格納
            passwordInfo.Category = CategoryBomboBox.Text;
            passwordInfo.ItemName = ItemNameTextBox.Text;
            passwordInfo.LoginId = LoginIdTextBox.Text;
            passwordInfo.Email = EmailTextBox.Text;
            passwordInfo.Password = PasswordTextBox.Text;
            passwordInfo.Other = OtherTextBox.Text;

            // このFormを閉じる
            this.DialogResult = DialogResult.OK; // .ShowDialog()の返り値となる
            this.Close();
        }

        // キャンセルボタンが押されたときの処理
        private void CancelButton_Click(object sender, EventArgs e)
        {
            // このFormを閉じる
            this.DialogResult = DialogResult.Cancel; // .ShowDialog()の返り値となる
            this.Close();
        }
    }
}

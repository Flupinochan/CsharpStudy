using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PasswordManagement
{
    public partial class MainForm : Form
    {
        public List<PasswordInfo> PasswordInfoList = new List<PasswordInfo>();

        public MainForm()
        {
            InitializeComponent();
        }

        // パスワード一覧を更新する
        private void UpdatePasswordList()
        {
            PasswordListBox.Items.Clear();
            foreach (PasswordInfo passwordInfo in PasswordInfoList)
            {
                // カテゴリに一致するもの または 「すべて」の場合に表示
                if ((CategoryComboBox.Text == passwordInfo.Category) || (CategoryComboBox.Text == "すべて"))
                {
                    PasswordListBox.Items.Add(passwordInfo);
                }
            }
        }

        // カテゴリを更新する
        private void UpdateCategory()
        {
            string selectedCategoryName = CategoryComboBox.Text;

            CategoryComboBox.Items.Clear();
            CategoryComboBox.Items.Add("すべて");
            foreach (PasswordInfo passwordInfo in PasswordInfoList)
            {
                // カテゴリの重複は避ける
                if (!CategoryComboBox.Items.Contains(passwordInfo.Category))
                {
                    CategoryComboBox.Items.Add(passwordInfo.Category);
                }
            }

            // 更新前に選択されていたカテゴリを選択しておく (削除されてなければ、「すべて」を選択
            int index = CategoryComboBox.FindStringExact(selectedCategoryName);
            if (index != -1)
            {
                CategoryComboBox.SelectedIndex = index;
            }
            else
            {
                CategoryComboBox.Text = "すべて";
            }
        }

        // パスワード登録ボタンを押したときの処理
        private void RegisterPasswordButton_Click(object sender, EventArgs e)
        {
            // パスワード登録Formを起動
            List<string> categoryList = new List<string>();
            foreach (string category in CategoryComboBox.Items)
            { 
                categoryList.Add(category);
            }

            RegisterPasswordForm registerPasswordForm = new RegisterPasswordForm(categoryList);

            // DialogResult.OK(登録ボタン)が押されたときに、パスワード情報を取得
            DialogResult dialogResult = registerPasswordForm.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                PasswordInfoList.Add(registerPasswordForm.passwordInfo);
                
                UpdateCategory();
                UpdatePasswordList();
                CategoryComboBox.Text = registerPasswordForm.passwordInfo.Category;
                clearGroupBox();
            }
        }

        // パスワード編集ボタンを押したときの処理
        private void PasswordEditButton_Click(object sender, EventArgs e)
        {
            // 各TextBoxへ入力可能にする
            if ((PasswordListBox.SelectedIndex != -1) && (PasswordEditButton.Text == "パスワードの編集"))
            {
                LoginIdTextBox.ReadOnly = false;
                LoginIdTextBox.BorderStyle = BorderStyle.Fixed3D;
                EmailTextBox.ReadOnly = false;
                EmailTextBox.BorderStyle = BorderStyle.Fixed3D;
                PasswordTextBox.ReadOnly = false;
                PasswordTextBox.BorderStyle = BorderStyle.Fixed3D;
                OtherTextBox.ReadOnly = false;
                OtherTextBox.BorderStyle = BorderStyle.Fixed3D;

                PasswordEditButton.Text = "編集完了";
                PasswordListBox.Enabled = false;
                CategoryComboBox.Enabled = false;
                RegisterPasswordButton.Enabled = false;
            }
            else
            {
                // パスワードを更新する
                // ※(PasswordInfo)PasswordListBox.SelectedItemは、List<PasswordInfo> PasswordInfoList の要素をbind(参照)している
                // 　そのため、ComboBoxのpasswordInfoを更新するとPasswordInfoListも更新される
                PasswordInfo passwordInfo = (PasswordInfo)PasswordListBox.SelectedItem;
                passwordInfo.LoginId = LoginIdTextBox.Text;
                passwordInfo.Email = EmailTextBox.Text;
                passwordInfo.Password = PasswordTextBox.Text;
                passwordInfo.Other = OtherTextBox.Text;

                LoginIdTextBox.ReadOnly = true;
                LoginIdTextBox.BorderStyle = BorderStyle.None;
                EmailTextBox.ReadOnly = true;
                EmailTextBox.BorderStyle = BorderStyle.None;
                PasswordTextBox.ReadOnly = true;
                PasswordTextBox.BorderStyle = BorderStyle.None;
                OtherTextBox.ReadOnly = true;
                OtherTextBox.BorderStyle = BorderStyle.None;

                PasswordEditButton.Text = "パスワードの編集";
                PasswordListBox.Enabled = true;
                CategoryComboBox.Enabled = true;
                RegisterPasswordButton.Enabled = true;
            }

        }

        // カテゴリを選択したときの処理
        private void CategoryComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatePasswordList();
            clearGroupBox();
        }

        // パスワード一覧を選択したときの処理(選択したパスワード情報を表示)
        private void PasswordListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Listに.Add()しているPasswordInfoが取得される
            PasswordInfo passwordInfo = (PasswordInfo)PasswordListBox.SelectedItem; // ただのObject型なのでキャストするとよい
            if (passwordInfo != null)
            {
                LoginIdTextBox.Text = passwordInfo.LoginId;
                EmailTextBox.Text = passwordInfo.Email;
                PasswordTextBox.Text = passwordInfo.Password;
                OtherTextBox.Text = passwordInfo.Other;
            }
        }

        // ファイル(保存)クリック時 List<PasswordInfo>をシリアライズして、XML形式のファイルで保存
        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog.ShowDialog(); // ファイル保存用Explorerを表示
        }

        // ファイル(読み込み)クリック時 XML形式のファイルをデシリアライズして、List<PasswordInfo>で読み込む
        private void ReadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog.ShowDialog(); // ファイル読み込み用Explorerを表示
        }

        // ファイル保存用Explorer 保存ボタンクリック時
        private void saveFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            XDocument xmlDoc = new XDocument();
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<PasswordInfo>));
            using (XmlWriter xmlWriter = XmlWriter.Create(xmlDoc.CreateWriter()))
            {
                xmlSerializer.Serialize(xmlWriter, PasswordInfoList);
            }

            string saveFileName = saveFileDialog.FileName;
            using (StreamWriter streamWriter = new StreamWriter(saveFileName))
            {
                streamWriter.Write(xmlDoc.ToString());
            }
        }

        // ファイル読み込み用Explorer 開くボタンクリック時
        private void openFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<PasswordInfo>));
            string openFileName = openFileDialog.FileName;
            using (StreamReader streamReader = new StreamReader(openFileName))
            {
                PasswordInfoList = (List<PasswordInfo>)serializer.Deserialize(streamReader);
            }

            UpdateCategory();
            UpdatePasswordList();
            clearGroupBox();
        }

        // パスワード一覧でクリックしたときの処理 (パスワード削除処理)
        private void PasswordListBox_MouseUp(object sender, MouseEventArgs e)
        {
            // クリックしたListBoxのItemのインデックス(上のItemから0, 1, 2...)を取得
            int itemIndex = PasswordListBox.IndexFromPoint(e.Location);
            // 右クリックでItemを選択
            PasswordListBox.SelectedIndex = itemIndex;
            // 右クリック時のみ処理
            if (e.Button == MouseButtons.Right)
            {
                // クリックの座標位置を※PointToScreenで変換して取得
                Point point = PasswordListBox.PointToScreen(e.Location);
                // ListBoxのItem上の場合のみ
                if ((itemIndex >= 0) && (itemIndex < PasswordListBox.Items.Count))
                {
                    // ContextMenuStripを表示
                    RClickContextMenuStrip.Show(point);
                }
              
            }

        }

        // 削除ボタンを押したときの処理
        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 削除確認MessageBox　(thisを指定することでForm画面の中央に表示されるが、マルチモニターだと上手くいかない)
            DialogResult dialogResult = MessageBox.Show(this, "本当に削除しますか?", "確認", MessageBoxButtons.OKCancel);
            // OKボタンが押されたとき
            if (dialogResult == DialogResult.OK)
            {
                // 選択されているパスワード情報を取得し、削除
                PasswordInfo passwordInfo = (PasswordInfo)PasswordListBox.SelectedItem;
                PasswordInfoList.Remove(passwordInfo);

                UpdateCategory();
                UpdatePasswordList();
            }
        }

        // パスワード情報をクリア
        private void clearGroupBox()
        {
            LoginIdTextBox.Text = "";
            EmailTextBox.Text = "";
            PasswordTextBox.Text = "";
            OtherTextBox.Text = "";
        }
    }
}

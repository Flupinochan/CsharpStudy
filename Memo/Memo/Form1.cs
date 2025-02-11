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

namespace Memo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        // ファイルの保存確認
        private bool IsSaved()
        {
            // 変更されていなく保存が不要/保存済みの場合
            if (!NotepadTextBox.Modified)
            {
                NotepadTextBox.Text = "";
                this.Text = "無題";
                NotepadTextBox.Modified = false;
                return false;
            }
            // ファイルを保存していない場合の保存確認メッセージ
            DialogResult result = MessageBox.Show("ファイルを保存しますか", "ファイルの保存確認",
               MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                // さらにExplorerで保存確認
                return SaveFileDialog.ShowDialog() == DialogResult.OK;
            }
            return true;
        }


        // 新規作成クリック時
        private void CreateFile_Click(object sender, EventArgs e)
        {
            if (IsSaved())
            {
                NotepadTextBox.Text = "";
                this.Text = "無題";
                NotepadTextBox.Modified = false;
            }
        }

        // 読み込みクリック時
        private void ReadFile_Click(object sender, EventArgs e)
        {
            // Explorerを開く
            IsSaved();
            OpenFileDialog.ShowDialog();
        }

        // 保存クリック時
        private void SaveFile_Click(object sender, EventArgs e)
        {
            // Explorerを開く
            SaveFileDialog.ShowDialog();
        }

        // Explorerで開くボタンが押されたときの処理
        private void OpenFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            // TextBoxにファイル読み込み
            string filePath = OpenFileDialog.FileName;
            string fileName = Path.GetFileName(filePath);
            this.Text = fileName;
            using (StreamReader streamReader = new StreamReader(filePath, false))
            {
                NotepadTextBox.Text = streamReader.ReadToEnd();
                NotepadTextBox.Modified = false;
            }

        }

        // ExplorerでOkボタンが押されたときの処理
        private void SaveFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            // ファイルを保存
            NotepadTextBox.Modified = false;
            string filePath = SaveFileDialog.FileName;
            string fileName = Path.GetFileName(filePath);
            this.Text = fileName;
            using (StreamWriter streamWriter = new StreamWriter(filePath, false))
            {
                streamWriter.Write(NotepadTextBox.Text);
            }
        }

        // TextBoxの内容が更新されて、まだ保存されていないことを示す「ファイル名 *」
        private void NotepadTextBox_ModifiedChanged(object sender, EventArgs e)
        {
            if (NotepadTextBox.Modified == true)
            {
                this.Text += " *";
            }

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            IsSaved();
        }
    }
}

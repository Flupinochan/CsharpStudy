namespace Memo
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.NotepadTextBox = new System.Windows.Forms.TextBox();
            this.NotepadMenuStrip = new System.Windows.Forms.MenuStrip();
            this.ファイルToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CreateFile = new System.Windows.Forms.ToolStripMenuItem();
            this.ReadFile = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveFile = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.SaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.NotepadMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // NotepadTextBox
            // 
            this.NotepadTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NotepadTextBox.Location = new System.Drawing.Point(0, 24);
            this.NotepadTextBox.Multiline = true;
            this.NotepadTextBox.Name = "NotepadTextBox";
            this.NotepadTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.NotepadTextBox.Size = new System.Drawing.Size(800, 426);
            this.NotepadTextBox.TabIndex = 0;
            this.NotepadTextBox.WordWrap = false;
            this.NotepadTextBox.ModifiedChanged += new System.EventHandler(this.NotepadTextBox_ModifiedChanged);
            // 
            // NotepadMenuStrip
            // 
            this.NotepadMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ファイルToolStripMenuItem});
            this.NotepadMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.NotepadMenuStrip.Name = "NotepadMenuStrip";
            this.NotepadMenuStrip.Size = new System.Drawing.Size(800, 24);
            this.NotepadMenuStrip.TabIndex = 1;
            this.NotepadMenuStrip.Text = "menuStrip1";
            // 
            // ファイルToolStripMenuItem
            // 
            this.ファイルToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CreateFile,
            this.ReadFile,
            this.SaveFile});
            this.ファイルToolStripMenuItem.Name = "ファイルToolStripMenuItem";
            this.ファイルToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.ファイルToolStripMenuItem.Text = "ファイル";
            // 
            // CreateFile
            // 
            this.CreateFile.Name = "CreateFile";
            this.CreateFile.Size = new System.Drawing.Size(180, 22);
            this.CreateFile.Text = "新規作成";
            this.CreateFile.Click += new System.EventHandler(this.CreateFile_Click);
            // 
            // ReadFile
            // 
            this.ReadFile.Name = "ReadFile";
            this.ReadFile.Size = new System.Drawing.Size(180, 22);
            this.ReadFile.Text = "読み込み";
            this.ReadFile.Click += new System.EventHandler(this.ReadFile_Click);
            // 
            // SaveFile
            // 
            this.SaveFile.Name = "SaveFile";
            this.SaveFile.Size = new System.Drawing.Size(180, 22);
            this.SaveFile.Text = "保存";
            this.SaveFile.Click += new System.EventHandler(this.SaveFile_Click);
            // 
            // OpenFileDialog
            // 
            this.OpenFileDialog.Filter = "テキストファイル|*.txt|すべてのファイル|*.*";
            this.OpenFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.OpenFileDialog_FileOk);
            // 
            // SaveFileDialog
            // 
            this.SaveFileDialog.Filter = "テキストファイル|*.txt|すべてのファイル|*.*";
            this.SaveFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.SaveFileDialog_FileOk);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.NotepadTextBox);
            this.Controls.Add(this.NotepadMenuStrip);
            this.MainMenuStrip = this.NotepadMenuStrip;
            this.Name = "Form1";
            this.Text = "無題";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.NotepadMenuStrip.ResumeLayout(false);
            this.NotepadMenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox NotepadTextBox;
        private System.Windows.Forms.MenuStrip NotepadMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem ファイルToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CreateFile;
        private System.Windows.Forms.ToolStripMenuItem ReadFile;
        private System.Windows.Forms.ToolStripMenuItem SaveFile;
        private System.Windows.Forms.OpenFileDialog OpenFileDialog;
        private System.Windows.Forms.SaveFileDialog SaveFileDialog;
    }
}


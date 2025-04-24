namespace PasswordManagement
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            this.RegisterPasswordButton = new System.Windows.Forms.Button();
            this.CategoryComboBox = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.PasswordListBox = new System.Windows.Forms.ListBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ファイルToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.読み込みToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.保存ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ファイルToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ReadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.RClickContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.DeleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pushButtonCustomControl1 = new PasswordManagement.CustomControl.PushButtonCustomControl();
            this.groupBox5 = new PasswordManagement.GroupBoxCustomControl();
            this.label4 = new System.Windows.Forms.Label();
            this.OtherTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.PasswordTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.EmailTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.LoginIdTextBox = new System.Windows.Forms.TextBox();
            this.PasswordEditButton = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.RClickContextMenuStrip.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // RegisterPasswordButton
            // 
            this.RegisterPasswordButton.Location = new System.Drawing.Point(38, 36);
            this.RegisterPasswordButton.Name = "RegisterPasswordButton";
            this.RegisterPasswordButton.Size = new System.Drawing.Size(121, 39);
            this.RegisterPasswordButton.TabIndex = 0;
            this.RegisterPasswordButton.Text = "パスワードの登録";
            this.RegisterPasswordButton.UseVisualStyleBackColor = true;
            this.RegisterPasswordButton.Click += new System.EventHandler(this.RegisterPasswordButton_Click);
            // 
            // CategoryComboBox
            // 
            this.CategoryComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CategoryComboBox.FormattingEnabled = true;
            this.CategoryComboBox.Location = new System.Drawing.Point(38, 114);
            this.CategoryComboBox.Name = "CategoryComboBox";
            this.CategoryComboBox.Size = new System.Drawing.Size(121, 20);
            this.CategoryComboBox.TabIndex = 14;
            this.CategoryComboBox.SelectedIndexChanged += new System.EventHandler(this.CategoryComboBox_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(36, 99);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(51, 12);
            this.label6.TabIndex = 15;
            this.label6.Text = "■カテゴリ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(37, 169);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 12);
            this.label5.TabIndex = 17;
            this.label5.Text = "■パスワード一覧";
            // 
            // PasswordListBox
            // 
            this.PasswordListBox.FormattingEnabled = true;
            this.PasswordListBox.ItemHeight = 12;
            this.PasswordListBox.Location = new System.Drawing.Point(39, 184);
            this.PasswordListBox.Name = "PasswordListBox";
            this.PasswordListBox.Size = new System.Drawing.Size(120, 268);
            this.PasswordListBox.TabIndex = 16;
            this.PasswordListBox.SelectedIndexChanged += new System.EventHandler(this.PasswordListBox_SelectedIndexChanged);
            this.PasswordListBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PasswordListBox_MouseUp);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ファイルToolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(507, 24);
            this.menuStrip1.TabIndex = 18;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // ファイルToolStripMenuItem1
            // 
            this.ファイルToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.読み込みToolStripMenuItem,
            this.保存ToolStripMenuItem});
            this.ファイルToolStripMenuItem1.Name = "ファイルToolStripMenuItem1";
            this.ファイルToolStripMenuItem1.Size = new System.Drawing.Size(53, 20);
            this.ファイルToolStripMenuItem1.Text = "ファイル";
            // 
            // 読み込みToolStripMenuItem
            // 
            this.読み込みToolStripMenuItem.Name = "読み込みToolStripMenuItem";
            this.読み込みToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            this.読み込みToolStripMenuItem.Text = "読み込み";
            this.読み込みToolStripMenuItem.Click += new System.EventHandler(this.ReadToolStripMenuItem_Click);
            // 
            // 保存ToolStripMenuItem
            // 
            this.保存ToolStripMenuItem.Name = "保存ToolStripMenuItem";
            this.保存ToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            this.保存ToolStripMenuItem.Text = "保存";
            this.保存ToolStripMenuItem.Click += new System.EventHandler(this.SaveToolStripMenuItem_Click);
            // 
            // ファイルToolStripMenuItem
            // 
            this.ファイルToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ReadToolStripMenuItem,
            this.SaveToolStripMenuItem});
            this.ファイルToolStripMenuItem.Name = "ファイルToolStripMenuItem";
            this.ファイルToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.ファイルToolStripMenuItem.Text = "ファイル";
            // 
            // ReadToolStripMenuItem
            // 
            this.ReadToolStripMenuItem.Name = "ReadToolStripMenuItem";
            this.ReadToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            this.ReadToolStripMenuItem.Text = "読み込み";
            this.ReadToolStripMenuItem.Click += new System.EventHandler(this.ReadToolStripMenuItem_Click);
            // 
            // SaveToolStripMenuItem
            // 
            this.SaveToolStripMenuItem.Name = "SaveToolStripMenuItem";
            this.SaveToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            this.SaveToolStripMenuItem.Text = "保存";
            this.SaveToolStripMenuItem.Click += new System.EventHandler(this.SaveToolStripMenuItem_Click);
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.Filter = "XMLファイル|*.xml|すべてのファイル|*.*";
            this.saveFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog_FileOk);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            this.openFileDialog.Filter = "XMLファイル|*.xml|すべてのファイル|*.*";
            this.openFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog_FileOk);
            // 
            // RClickContextMenuStrip
            // 
            this.RClickContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DeleteToolStripMenuItem});
            this.RClickContextMenuStrip.Name = "RClickContextMenuStrip";
            this.RClickContextMenuStrip.Size = new System.Drawing.Size(99, 26);
            // 
            // DeleteToolStripMenuItem
            // 
            this.DeleteToolStripMenuItem.Name = "DeleteToolStripMenuItem";
            this.DeleteToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.DeleteToolStripMenuItem.Text = "削除";
            this.DeleteToolStripMenuItem.Click += new System.EventHandler(this.DeleteToolStripMenuItem_Click);
            // 
            // pushButtonCustomControl1
            // 
            this.pushButtonCustomControl1.Location = new System.Drawing.Point(193, 81);
            this.pushButtonCustomControl1.Name = "pushButtonCustomControl1";
            this.pushButtonCustomControl1.Size = new System.Drawing.Size(30, 30);
            this.pushButtonCustomControl1.TabIndex = 19;
            this.pushButtonCustomControl1.Text = "test";
            this.pushButtonCustomControl1.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label4);
            this.groupBox5.Controls.Add(this.OtherTextBox);
            this.groupBox5.Controls.Add(this.label3);
            this.groupBox5.Controls.Add(this.PasswordTextBox);
            this.groupBox5.Controls.Add(this.label2);
            this.groupBox5.Controls.Add(this.EmailTextBox);
            this.groupBox5.Controls.Add(this.label1);
            this.groupBox5.Controls.Add(this.LoginIdTextBox);
            this.groupBox5.Controls.Add(this.PasswordEditButton);
            this.groupBox5.Location = new System.Drawing.Point(253, 36);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(238, 416);
            this.groupBox5.TabIndex = 12;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "該当なし";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 270);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 12);
            this.label4.TabIndex = 17;
            this.label4.Text = "【その他】";
            // 
            // OtherTextBox
            // 
            this.OtherTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.OtherTextBox.Location = new System.Drawing.Point(20, 285);
            this.OtherTextBox.Name = "OtherTextBox";
            this.OtherTextBox.ReadOnly = true;
            this.OtherTextBox.Size = new System.Drawing.Size(195, 12);
            this.OtherTextBox.TabIndex = 16;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 201);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 12);
            this.label3.TabIndex = 15;
            this.label3.Text = "【パスワード】";
            // 
            // PasswordTextBox
            // 
            this.PasswordTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.PasswordTextBox.Location = new System.Drawing.Point(20, 216);
            this.PasswordTextBox.Name = "PasswordTextBox";
            this.PasswordTextBox.ReadOnly = true;
            this.PasswordTextBox.Size = new System.Drawing.Size(195, 12);
            this.PasswordTextBox.TabIndex = 14;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 132);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 12);
            this.label2.TabIndex = 13;
            this.label2.Text = "【メールアドレス】";
            // 
            // EmailTextBox
            // 
            this.EmailTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.EmailTextBox.Location = new System.Drawing.Point(20, 147);
            this.EmailTextBox.Name = "EmailTextBox";
            this.EmailTextBox.ReadOnly = true;
            this.EmailTextBox.Size = new System.Drawing.Size(195, 12);
            this.EmailTextBox.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 12);
            this.label1.TabIndex = 10;
            this.label1.Text = "【ログインID】";
            // 
            // LoginIdTextBox
            // 
            this.LoginIdTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.LoginIdTextBox.Location = new System.Drawing.Point(20, 78);
            this.LoginIdTextBox.Name = "LoginIdTextBox";
            this.LoginIdTextBox.ReadOnly = true;
            this.LoginIdTextBox.Size = new System.Drawing.Size(195, 12);
            this.LoginIdTextBox.TabIndex = 9;
            // 
            // PasswordEditButton
            // 
            this.PasswordEditButton.Location = new System.Drawing.Point(20, 356);
            this.PasswordEditButton.Name = "PasswordEditButton";
            this.PasswordEditButton.Size = new System.Drawing.Size(195, 39);
            this.PasswordEditButton.TabIndex = 1;
            this.PasswordEditButton.Text = "パスワードの編集";
            this.PasswordEditButton.UseVisualStyleBackColor = true;
            this.PasswordEditButton.Click += new System.EventHandler(this.PasswordEditButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(507, 516);
            this.Controls.Add(this.pushButtonCustomControl1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.PasswordListBox);
            this.Controls.Add(this.CategoryComboBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.RegisterPasswordButton);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "パスワード管理ツール";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.RClickContextMenuStrip.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button RegisterPasswordButton;
        private System.Windows.Forms.Button PasswordEditButton;
        private GroupBoxCustomControl groupBox5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox OtherTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox PasswordTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox EmailTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox LoginIdTextBox;
        private System.Windows.Forms.ComboBox CategoryComboBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ListBox PasswordListBox;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ファイルToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ReadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SaveToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.ContextMenuStrip RClickContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem DeleteToolStripMenuItem;
        private CustomControl.PushButtonCustomControl pushButtonCustomControl1;
        private System.Windows.Forms.ToolStripMenuItem ファイルToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 読み込みToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 保存ToolStripMenuItem;
    }
}


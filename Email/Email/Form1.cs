using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Email
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, string lParam);
        private const int EM_SETCUEBANNER = 0x1501; // プレースホルダーテキストを設定するメッセージ

        public Form1()
        {
            InitializeComponent();
            textBox1.HandleCreated += (s, e) =>
            {
                SendMessage(textBox1.Handle, EM_SETCUEBANNER, (IntPtr)1, "sample@gmail.com");
            };
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Hello World", "TEST", MessageBoxButtons.OK);
        }
    }
}

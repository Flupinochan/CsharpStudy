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
    public partial class GroupBoxCustomControl : GroupBox
    {
        public GroupBoxCustomControl()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            // base.OnPaint(pe);
            Graphics graphics = pe.Graphics; // 絵画を行う機能
            Pen pen = new Pen(Brushes.BlueViolet, 3); // 線を絵画する機能

            int margin = 8;
            graphics.DrawRectangle(pen, margin, margin, Width -(margin * 2), Height -(margin * 2)); // 四角形を絵画

            SizeF textSize = graphics.MeasureString(Text, Font);
            SolidBrush solidBrushBackGround = new SolidBrush(BackColor); // 背景を書く機能
            graphics.FillRectangle(solidBrushBackGround, 15.0f, 0.0f, textSize.Width, textSize.Height);

            SolidBrush solidBrushText = new SolidBrush(ForeColor); // 文字を書く機能
            graphics.DrawString(Text, Font, solidBrushText, 15.0f, 0.0f); // TextやFontは、プロパティウィンドウと連携するようにしている

            // 各機能の破棄
            pen.Dispose();
            solidBrushBackGround.Dispose();
            solidBrushText.Dispose();
        }
    }
}

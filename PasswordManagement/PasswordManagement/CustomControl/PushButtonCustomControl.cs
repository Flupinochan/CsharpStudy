using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PasswordManagement.CustomControl
{
    public partial class PushButtonCustomControl : Button
    {
        public PushButtonCustomControl()
        {
            InitializeComponent();
            // MouseUp += メソッド名();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            // base.OnPaint(pe);

            // PushButton.icoを取得
            Icon icon = Properties.Resources.PushButton;

            // コントロールのサイズをIconのサイズにする
            this.Size = new Size(icon.Width, icon.Height);

            // コントロールの中央にIconを絵画する
            int x = (this.Width - icon.Width) / 2;
            int y = (this.Height - icon.Height) / 2;
            pe.Graphics.DrawIcon(icon, x, y); // Imageの場合は.DrawImage()

            using (Brush textBrush = new SolidBrush(Color.Black))
            {
                // テキストの位置を中央に設定
                SizeF textSize = pe.Graphics.MeasureString(this.Text, this.Font);
                int textX = (this.Width - (int)textSize.Width) / 2;
                int textY = (this.Height - (int)textSize.Height) / 2;

                // テキストを描画
                pe.Graphics.DrawString(this.Text, this.Font, textBrush, textX, textY);
            }



            //Rectangle rcSrc, rcDst;
            //// ※bitmapはドット絵で絵画すること
            //Bitmap bitmap = Properties.Resources.PushButton.ToBitmap(); // Resoucesフォルダの画像情報を取得
            //rcDst = new Rectangle(0, 0, bitmap.Height, bitmap.Width);
            //rcSrc = new Rectangle(0, 0, Height, Width);
            //graphcs.DrawImage(bitmap, rcDst, rcSrc, GraphicsUnit.Pixel); // 画像を描画
        }

        // OnMouseUpメソッドオーバーライドすることで、MouseUpイベントの処理を追加可能
        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            base.OnMouseUp(mevent);
            // クリック後の処理
            // MessageBox.Show("MouseUp event triggered!");
            Location = new Point(Location.X - 1, Location.Y - 1);
        }

        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            base.OnMouseUp(mevent);
            // クリック後の処理
            Location = new Point(Location.X + 1, Location.Y + 1);
        }
    }
}

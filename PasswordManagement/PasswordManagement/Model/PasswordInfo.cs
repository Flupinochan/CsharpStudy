using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManagement
{
    // パスワード情報を格納するクラス
    public class PasswordInfo
    {
        public string Category { get; set; } = "";
        public string ItemName { get; set; } = "";
        public string LoginId { get; set; } = "";
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
        public string Other { get; set; } = "";

        // パスワード一覧(ListBox)の表示内容をItemNameにするためにオーバーライド
        public override string ToString()
        {
            return this.ItemName;
        }
    }
}

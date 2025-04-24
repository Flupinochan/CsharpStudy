using System;
using System.ComponentModel;
using SQLite;

namespace LocalDeepSeek.Models
{
    public class Message : INotifyPropertyChanged
    {
        [PrimaryKey, AutoIncrement, NotNull, Column("MessageId")]
        public int MessageId { get; set; }

        [NotNull, Column("ChatHistoryId")]
        public int ChatHistoryId { get; set; }

        [NotNull, Column("UserId")]
        public int UserId { get; set; }

        // 1ならHuman 0ならAI
        [NotNull, Column("IsHuman")]
        public int IsHuman { get; set; }

        // contentはストリーミングデータにより、UIをリアルタイムで変更する
        private string _content;

        [NotNull, Column("Content")]
        public string Content
        {
            get => _content;
            set
            {
                if(_content != value)
                {
                    _content = value;
                    OnPropertyChanged(nameof(Content));
                }
            }
        }

        [NotNull, Column("CreatedAt")]
        public string CreatedAt { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public override string ToString()
        {
            return $"MessageId: {MessageId}, ChatHistoryId: {ChatHistoryId}, UserId: {UserId}, " +
                   $"IsHuman: {(IsHuman == 1 ? "Human" : "AI")}, Content: {Content}, CreatedAt: {CreatedAt}";
        }
    }
}

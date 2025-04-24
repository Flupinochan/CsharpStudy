using SQLite;

namespace LocalDeepSeek.Models;

[Table("ChatHistory")]
public class ChatHistory
{
    [PrimaryKey, AutoIncrement, NotNull, Column("ChatHistoryId")]
    public Int32 ChatHistoryId { get; set; }

    [NotNull, Column("UserId")]
    public Int32 UserId { get; set; }

    [NotNull, MaxLength(50), Column("Title")]
    public String Title { get; set; }

    [NotNull, Column("CreatedAt")]
    public String CreatedAt { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

    public override String ToString()
    {
        return $"ChatHistoryId: {ChatHistoryId}, UserId: {UserId}, Title: {Title}, CreatedAt: {CreatedAt}";
    }
}
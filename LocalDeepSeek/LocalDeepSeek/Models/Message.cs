using SQLite;

namespace LocalDeepSeek.Models;

public class Message
{
    [PrimaryKey, AutoIncrement, NotNull, Column("MessageId")]
    public Int32 MessageId { get; set; }

    [NotNull, Column("ChatHistoryId")]
    public Int32 ChatHistoryId { get; set; }

    [NotNull, Column("UserId")]
    public Int32 UserId { get; set; }

    // 1ならHuman 0ならAI
    [NotNull, Column("IsHuman")]
    public Int32 IsHuman { get; set; }

    [NotNull, Column("Content")]
    public String Content { get; set; }

    [NotNull, Column("CreatedAt")]
    public String CreatedAt { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

    public override String ToString()
    {
        return $"MessageId: {MessageId}, ChatHistoryId: {ChatHistoryId}, UserId: {UserId}, " +
               $"IsHuman: {(IsHuman == 1 ? "Human" : "AI")}, Content: {Content}, CreatedAt: {CreatedAt}";
    }
}
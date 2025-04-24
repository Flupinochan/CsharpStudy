using SQLite;

namespace LocalDeepSeek.Models;

[Table("User")]
public class User
{
    [PrimaryKey, AutoIncrement, NotNull, Column("UserId")]
    public Int32 UserId { get; set; }

    [Unique, NotNull, MaxLength(50), Column("UserName")]
    public String UserName { get; set; }

    [NotNull, Column("HashPassword")]
    public String HashPassword { get; set; }

    [NotNull, Column("CreatedAt")]
    public String CreatedAt { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

    public override String ToString()
    {
        return $"UserId: {UserId}, UserName: {UserName}, HashPassword: {HashPassword}, CreatedAt: {CreatedAt}";
    }
}
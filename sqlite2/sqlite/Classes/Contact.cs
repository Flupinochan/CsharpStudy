using SQLite;

namespace sqlite.Classes;

[Table("Contact")]
public class Contact
{
    [PrimaryKey, AutoIncrement, Unique]
    public Int32 Id { get; set; }
    [MaxLength(50)]
    public String Name { get; set; }
    public String Email { get; set; }
    public String Phone { get; set; }
    // [Ignore] firstName + lastNameのようなDBに保存しなくても既存のカラムから計算して
    // 生成できるデータの場合はDBに保存しないフィールドとして指定可能

    // コンストラクタは不要

    public override String ToString(){
        return $"{this.Name} - {this.Email} - {this.Phone}";
    }

}

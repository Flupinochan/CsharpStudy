using System.Text.Json;
using System.Text.Json.Serialization;

namespace WoT.Utils;

public class ErrorResponseData
{
    [JsonPropertyName("status")]
    public String? Status { get; set; }

    [JsonPropertyName("error")]
    public TestErrorDetails? Error { get; set; }

    public override String ToString() => JsonSerializer.Serialize(this);

}


public class ErrorDetails
{
    [JsonPropertyName("field")]
    public String? Field { get; set; }

    [JsonPropertyName("message")]
    public String? Message { get; set; }

    [JsonPropertyName("code")]
    public Int32? Code { get; set; }

    [JsonPropertyName("value")]
    public String? Value { get; set; }

    public override String ToString() => JsonSerializer.Serialize(this);
}

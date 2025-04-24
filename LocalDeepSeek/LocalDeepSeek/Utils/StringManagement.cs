using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LocalDeepSeek.Utils;

public class StringManagement
{
    /// <summary>
    /// 生成AIのレスポンス文字列を整形
    /// </summary>
    /// <param name="input">生成AIのレスポンス文字列</param>
    /// <returns></returns>
    public static String RemoveThinkTags(String input)
    {
        // <think>文字列</think> を削除
        String patternThinkTags = @"<think>.*?</think>";
        String cleanedInput = Regex.Replace(input, patternThinkTags, String.Empty, RegexOptions.Singleline);

        // 最初の空行 を削除
        String patternEmptyLines = @"^\s*[\r\n]+";
        cleanedInput = Regex.Replace(cleanedInput, patternEmptyLines, String.Empty);

        return cleanedInput;
    }


    /// <summary>
    /// ハッシュ文字列を返却
    /// </summary>
    /// <param name="input">パスワード</param>
    /// <returns></returns>
    public static String GetHashPassword(String input)
    {
        using SHA256 sha256 = SHA256.Create();
        Byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
        return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
    }
}

using System.Reflection;
using System.Text.Json;

namespace EnvConfigTest;

public static class JsonOptionsProvider
{
    public static readonly JsonSerializerOptions Options = new JsonSerializerOptions { WriteIndented = true };
}

class Program
{
    /// <summary>
    /// エントリーポイント
    /// </summary>
    static void Main()
    {
        try
        {
            // 埋め込みリソースからの設定を取得
            Person embeddingConfig = GetEmbeddingResourceData();
            Console.WriteLine();
            // 外部ファイルからの設定を取得
            Person? overrideConfig = GetOverrideData();
            Console.WriteLine();
            // 最終的な設定を取得
            Person finalConfig = overrideConfig ?? embeddingConfig;
            Console.WriteLine("最終的に使用される設定:");
            Console.WriteLine(finalConfig);
        }
        catch(JsonException ex)
        {
            Console.WriteLine($"JSONデシリアライズエラー: {ex}");
        }
        catch(Exception ex)
        {
            Console.WriteLine($"予期しないエラー: {ex}");
        }
    }


    /// <summary>
    /// 埋め込みリソースからデフォルト設定を取得する
    /// </summary>
    private static Person GetEmbeddingResourceData()
    {
        String[] embeddingResourceNames = Assembly.GetExecutingAssembly().GetManifestResourceNames();
        Console.WriteLine($"埋め込みリソース名一覧: {String.Join(Environment.NewLine, embeddingResourceNames)}");

        String envResourceName = "EnvConfigTest.env.json";
        Stream resourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(envResourceName)
            ?? throw new InvalidOperationException($"埋め込みリソース {envResourceName} が見つかりません");

        using(resourceStream)
        {
            Person defaultConfig = JsonSerializer.Deserialize<Person>(resourceStream, JsonOptionsProvider.Options)
                ?? throw new InvalidOperationException("埋め込みリソースのデータのデシリアライズに失敗しました");

            Console.WriteLine("埋め込みリソースから取得したデータ:");
            Console.WriteLine(defaultConfig);
            return defaultConfig;
        }
    }


    /// <summary>
    /// 外部ファイルから設定を取得する
    /// </summary>
    private static Person? GetOverrideData()
    {
        String exeFolderPath = AppDomain.CurrentDomain.BaseDirectory;
        Console.WriteLine($"exeフォルダのパス: {exeFolderPath}");

        String envPath = Path.Combine(exeFolderPath, "env.json");
        Console.WriteLine($"env.jsonのパス: {envPath}");

        if(File.Exists(envPath))
        {
            Stream overrideStream = File.OpenRead(envPath);
            using(overrideStream)
            {
                Person overrideConfig = JsonSerializer.Deserialize<Person>(overrideStream, JsonOptionsProvider.Options)
                    ?? throw new InvalidOperationException("外部ファイルのデータのデシリアライズに失敗しました");

                Console.WriteLine("外部ファイルから取得したデータ:");
                Console.WriteLine(overrideConfig);
                return overrideConfig;
            }
        }
        else
        {
            Console.WriteLine($"外部ファイル {envPath} が見つかりません");
            return null;
        }
    }
}


class Person
{
    public String Name { get; set; } = String.Empty;
    public Int32 Age { get; set; } = -1;
    public String Email { get; set; } = String.Empty;

    public override String ToString() => JsonSerializer.Serialize(this, JsonOptionsProvider.Options);
}
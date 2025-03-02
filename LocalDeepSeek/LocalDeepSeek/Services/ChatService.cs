using LocalDeepSeek.Models;
using LocalDeepSeek.Utils;
using Microsoft.Extensions.AI;
using System.Diagnostics;
using System.Windows.Controls;

namespace LocalDeepSeek.Services;

/// <summary>
/// 生成AIリクエスト処理
/// </summary>
public class ChatService
{
    private static String _endpoint = "http://localhost:11434/";
    private static IChatClient ChatClient = new OllamaChatClient(_endpoint, modelId: App.ModelId);

    /// <summary>
    /// 非ストリーミング出力
    /// </summary>
    /// <param name="input">ユーザの入力</param>
    public static async Task<String> Chat(String input)
    {
        String strResponse = "応答がありません";
        try
        {
            ChatResponse chatResponse = await ChatClient.GetResponseAsync(input);
            strResponse = chatResponse.ToString();
            strResponse = StringManagement.RemoveThinkTags(strResponse);
        }
        catch(Exception ex)
        {
            App.Logger.ERROR($"生成AI出力エラー: {ex}");
        }
        return strResponse;
    }


    /// <summary>
    /// 非ストリーミング出力 (チャット履歴付き)
    /// </summary>
    /// <param name="input">ユーザの入力</param>
    public static async Task<String> ChatWithHistory(String input, List<Message> messageList)
    {
        String strResponse = "応答がありません";
        try
        {
            List<ChatMessage> conversation = [];
            foreach (Message message in messageList)
            {
                ChatRole chatRole = message.IsHuman == 1 ? ChatRole.User : ChatRole.Assistant;
                conversation.Add(new(chatRole, message.Content));
            }
            conversation.Add(new(ChatRole.User, input));

            ChatResponse chatResponse = await ChatClient.GetResponseAsync(conversation);
            strResponse = chatResponse.ToString();
            strResponse = StringManagement.RemoveThinkTags(strResponse);
        }
        catch(Exception ex)
        {
            App.Logger.ERROR($"生成AI出力エラー: {ex}");
        }
        return strResponse;
    }


    /// <summary>
    /// ストリーミング出力
    /// </summary>
    /// <param name="input">ユーザの入力</param>
    /// <param name="textBox">出力するTextBox</param>
    public static async Task Streaming(String input, TextBox textBox)
    {
        String strResponse = "応答がありません";
        try
        {
            await foreach(ChatResponseUpdate update in ChatClient.GetStreamingResponseAsync(input))
            {
                strResponse = update.ToString();
                strResponse = StringManagement.RemoveThinkTags(strResponse);
                textBox.Dispatcher.Invoke(() =>
                {
                    textBox.Text += strResponse;
                });
            }
        }catch (Exception ex)
        {
            App.Logger.ERROR($"生成AI出力エラー: {ex}");
        }
    }



}
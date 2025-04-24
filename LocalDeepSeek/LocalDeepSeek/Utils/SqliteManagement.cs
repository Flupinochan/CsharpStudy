using LocalDeepSeek.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalDeepSeek.Utils;

/// <summary>
/// SQLite操作関連
/// </summary>
class SqliteManagement
{
    /// <summary>
    /// テーブル作成
    /// </summary>
    public static void CreateTable()
    {
        // SQLite接続
        using (SQLiteConnection connection = new SQLiteConnection(App.DatabasePath))
        {
            // トランザクション開始
            connection.BeginTransaction();
            try
            {
                // データクラス型のテーブルを作成 (すでにある場合は何もしない)
                connection.CreateTable<User>();
                connection.CreateTable<Message>();
                connection.CreateTable<ChatHistory>();

                // コミット
                connection.Commit();
                App.Logger.INFO("データベース作成成功");
            }
            catch (Exception ex)
            {
                // ロールバック
                connection.Rollback();
                App.Logger.ERROR($"テーブル作成失敗: {ex}");
            }
        }
    }


    /// <summary>
    /// 新規ユーザ作成
    /// </summary>
    /// </summary>
    /// <param name="userName">ユーザ名</param>
    /// <param name="hashPassword">ハッシュ化したパスワード</param>
    /// <returns>作成したユーザのID</returns>
    public static Int32 CreateUser(String userName, String hashPassword)
    {
        Int32 userId = -1;
        // SQLite接続
        using(SQLiteConnection connection = new SQLiteConnection(App.DatabasePath))
        {
            // トランザクション開始
            connection.BeginTransaction();
            try
            {
                // ユーザ作成 (AutoIncrementしたuserIdが戻り値)
                User user = new User()
                {
                    UserName = userName,
                    HashPassword = hashPassword,
                };
                connection.Insert(user);
                userId = connection.ExecuteScalar<Int32>("SELECT last_insert_rowid()");

                // コミット
                connection.Commit();
                App.Logger.INFO($"新規ユーザ作成成功 UserId: {userId} UserName: {user.UserName}");
            }catch (SQLiteException ex)
            {
                if (ex.ToString().Contains("UNIQUE constraint failed: User.UserName")){
                    userId = -2;
                }
            }
            catch(Exception ex)
            {
                // ロールバック
                connection.Rollback();
                App.Logger.ERROR($"新規ユーザ作成失敗: {ex}");
            }
        }
        return userId;
    }


    /// <summary>
    /// 認証処理
    /// </summary>
    /// <param name="userName">ユーザ名</param>
    /// <param name="hashPassword">パスワード</param>
    /// <returns>成功したらUserIdを返し、失敗したら-1を返す</returns>
    public static Int32 ValidateUserLogin(String userName, String hashPassword)
    {
        // SQLite接続
        using(SQLiteConnection connection = new SQLiteConnection(App.DatabasePath))
        {
            // トランザクション開始
            connection.BeginTransaction();
            try
            {
                // ユーザ検索
                User user = connection.Table<User>()
                                      .FirstOrDefault(u => u.UserName == userName && u.HashPassword == hashPassword);

                // コミット
                connection.Commit();

                if(user is not null)
                {
                    App.Logger.INFO($"{userName} ユーザ ユーザID: {user.UserId}が見つかりました");
                    return user.UserId;
                }
            }
            catch(Exception ex)
            {
                // ロールバック
                connection.Rollback();
                App.Logger.ERROR($"ユーザ検索に失敗しました: {ex}");
            }
        }
        App.Logger.INFO($"{userName} ユーザが見つかりませんでした");
        return -1;
    }


    /// <summary>
    /// MessageデータクラスのListを取得
    /// </summary>
    /// <returns>MessageデータクラスのList</returns>
    public static List<Message> GetMessageList(Int32 userId, Int32 chatHistoryId)
    {
        List<Message> messageList = [];
        // SQLite接続
        using(SQLiteConnection connection = new SQLiteConnection(App.DatabasePath))
        {
            // トランザクション開始
            connection.BeginTransaction();
            try
            {
                messageList = connection.Table<Message>()
                                        .Where(message => message.UserId == userId && message.ChatHistoryId == chatHistoryId)
                                        .OrderBy(message => message.MessageId)
                                        .ToList();
                // コミット
                connection.Commit();
                App.Logger.INFO($"MessageのListを取得: {String.Join(", ", messageList)}");
            }
            catch(Exception ex)
            {
                // ロールバック
                connection.Rollback();
                App.Logger.ERROR($"MessageのListの取得に失敗しました: {ex}");
            }
        }
        return messageList;
    }


    /// <summary>
    /// ChatHistoryデータクラスのListを取得
    /// </summary>
    /// <returns>ChatHistoryデータクラスのList</returns>
    public static List<ChatHistory> GetChatHistoryList(Int32 userId)
    {
        List<ChatHistory> chatHistoryList = [];
        // SQLite接続
        using(SQLiteConnection connection = new SQLiteConnection(App.DatabasePath))
        {
            // トランザクション開始
            connection.BeginTransaction();
            try
            {
                chatHistoryList = connection.Table<ChatHistory>()
                                            .Where(message => message.UserId == userId)
                                            .OrderByDescending(chatHistory => chatHistory.ChatHistoryId)
                                            .ToList();
                // コミット
                connection.Commit();
                App.Logger.INFO($"ChatHistoryのListを取得: {String.Join(", ", chatHistoryList)}");
            }
            catch(Exception ex)
            {
                // ロールバック
                connection.Rollback();
                App.Logger.ERROR($"ChatHistoryのListを取得に失敗しました: {ex}");
            }
        }
        return chatHistoryList;
    }


    /// <summary>
    /// ChatHistoryデータクラスを挿入
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="Title"></param>
    /// <returns></returns>
    public static Int32 InsertHistory(Int32 userId, String Title)
    {
        // SQLite接続
        using(SQLiteConnection connection = new SQLiteConnection(App.DatabasePath))
        {
            // トランザクション開始
            connection.BeginTransaction();
            try
            {
                ChatHistory chatHistory = new ChatHistory()
                {
                    UserId = userId,
                    Title = Title,
                };

                // ChatHistoryデータクラスを挿入
                connection.Insert(chatHistory);
                Int32 chatHistoryId = connection.ExecuteScalar<Int32>("SELECT last_insert_rowid()");
                App.Logger.DEBUG($"作成されたChatHistoryId: {chatHistoryId}");

                // コミット
                connection.Commit();
                App.Logger.INFO($"ChatHistoryの挿入成功");
                return chatHistoryId;
            }
            catch(Exception ex)
            {
                // ロールバック
                connection.Rollback();
                App.Logger.ERROR($"ChatHistoryの挿入失敗: {ex}");
            }
            return -1;
        }
    }


    /// <summary>
    /// Messageデータクラスを挿入
    /// </summary>
    /// <param name="chatHistoryId"></param>
    /// <param name="userId"></param>
    /// <param name="isHuman"></param>
    /// <param name="content"></param>
    public static void InsertMessage(Message message)
    {
        // SQLite接続
        using(SQLiteConnection connection = new SQLiteConnection(App.DatabasePath))
        {
            // トランザクション開始
            connection.BeginTransaction();
            try
            {
                // Messageデータクラスを挿入
                connection.Insert(message);

                // コミット
                connection.Commit();
                App.Logger.INFO($"Messageの挿入成功");
            }
            catch(Exception ex)
            {
                // ロールバック
                connection.Rollback();
                App.Logger.ERROR($"Messageの挿入失敗: {ex}");
            }
        }
    }


    /// <summary>
    /// ChatHistoryおよび関連づくMessageの削除
    /// </summary>
    /// <param name="chatHistoryId"></param>
    /// <param name="userId"></param>
    public static void DeleteChatHistory(Int32 chatHistoryId, Int32 userId)
    {
        // SQLite接続
        using(SQLiteConnection connection = new SQLiteConnection(App.DatabasePath))
        {
            // トランザクション開始
            connection.BeginTransaction();
            try
            {
                // ChatHistoryの削除
                Int32 deletedChatHistoryRowsCount = connection.Table<ChatHistory>()
                                                              .Where(c => c.ChatHistoryId == chatHistoryId && c.UserId == userId)
                                                              .Delete();

                // ChatHistoryに関連づくMessageを削除 (ORMではForeignKeyやCASCADEで自動削除ができないため)
                Int32 deletedMessageRowsCount = connection.Table<Message>()
                                                          .Where(c => c.ChatHistoryId == chatHistoryId && c.UserId == userId)
                                                          .Delete();

                // コミット
                connection.Commit();
                App.Logger.INFO($"ChatHistoryの削除件数: {deletedChatHistoryRowsCount}、Messageの削除件数: {deletedMessageRowsCount}");
            }
            catch(Exception ex)
            {
                // ロールバック
                connection.Rollback();
                App.Logger.ERROR($"ChatHistoryの削除に失敗しました: {ex}");
            }
        }
    }

}

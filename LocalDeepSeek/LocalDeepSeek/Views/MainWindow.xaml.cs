using LocalDeepSeek.Models;
using LocalDeepSeek.Services;
using LocalDeepSeek.Utils;
using Microsoft.Extensions.AI;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace LocalDeepSeek.Views;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private List<Message> _messageList;
    private List<ChatHistory> _chatHistoryList;
    private Int32 _selectedChatHistoryId;

    public MainWindow()
    {
        InitializeComponent();
        _messageList = new List<Message>();
        _chatHistoryList = new List<ChatHistory>();
        _selectedChatHistoryId = -1;
        UpdateMessages();
        UpdateChatHistories();
    }




    /// <summary>
    /// ChatHistory(ListViewItem)選択時のEventトリガー、MessageListを更新
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void HistoryListView_SelectionChanged(Object sender, SelectionChangedEventArgs e)
    {
        if(sender is ListView listView && listView.SelectedItem is ChatHistory chatHistory)
        {
            _selectedChatHistoryId = chatHistory.ChatHistoryId;
            UpdateMessages();
        }
    }


    /// <summary>
    /// TextBoxフォーカス時のEventトリガー、MessageListを更新
    /// ※ListViewItemのTextBoxを選択してもSelectionChangedはトリガーされないため
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void TextBox_GotFocus(Object sender, RoutedEventArgs e)
    {
        // TextBoxが選択されたときに、選択されたアイテムのChatHistoryIdを取得
        if(sender is TextBox textBox)
        {
            // TextBoxのListViewItemを取得
            Object dataContext = textBox.DataContext;

            // ListViewItemからChatHistoryIdを取得
            if(dataContext is ChatHistory chatHistory)
            {
                // TextBoxのChatHistoryを選択
                HistoryListView.SelectedItem = chatHistory;

                _selectedChatHistoryId = chatHistory.ChatHistoryId;
                UpdateMessages();
            }
        }
    }


    /// <summary>
    /// Deleteボタンクリック時のEventトリガー
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void DeleteButton_Click(Object sender, RoutedEventArgs e)
    {
        ChatHistory chatHistory = (ChatHistory)this.HistoryListView.SelectedItem;
        Int32 chatHistoryId = chatHistory.ChatHistoryId;
        SqliteManagement.DeleteChatHistory(chatHistoryId, LoginWindow.UserId);
        this._selectedChatHistoryId = -1;
        UpdateChatHistories();
        UpdateMessages();
    }


    /// <summary>
    /// Newボタンクリック時のEventトリガー
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void NewButton_Click(Object sender, RoutedEventArgs e)
    {
        string titleNow = DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss");
        _selectedChatHistoryId = SqliteManagement.InsertHistory(LoginWindow.UserId, titleNow);
        UpdateChatHistories();
        UpdateMessages();
    }


    /// <summary>
    /// 送信ボタンクリック時のEventトリガー
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void ChatButton_Click(Object sender, RoutedEventArgs e)
    {
        // UI更新
        this.ChatButton.IsEnabled = false;
        this.ChatButton.Background = (SolidColorBrush)Application.Current.Resources["CustomGrayColor"];

        // ChatHistoryが選択されていない場合は、ChatHistoryを作成
        if (_selectedChatHistoryId == -1)
        {
            string titleNow = DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss");
            _selectedChatHistoryId = SqliteManagement.InsertHistory(LoginWindow.UserId, titleNow);
            UpdateChatHistories();
        }

        // HumanのMessageを挿入
        String input = this.UserInputTextBox.Text;
        SqliteManagement.InsertMessage(_selectedChatHistoryId, LoginWindow.UserId, 1, input);
        UpdateMessages();
        this.UserInputTextBox.Text = String.Empty;

        // 生成AIのMessageを挿入
        // String response = await ChatService.Chat(input);
        String response = await ChatService.ChatWithHistory(input, _messageList);
        SqliteManagement.InsertMessage(_selectedChatHistoryId, LoginWindow.UserId, 0, response);
        UpdateMessages();

        // UI更新
        this.ChatButton.Background = (SolidColorBrush)Application.Current.Resources["CustomGreenColor"];
        this.ChatButton.IsEnabled = true;
    }


    /// <summary>
    /// MessageListViewのバインド更新
    /// </summary>
    private void UpdateMessages()
    {
        this._messageList = SqliteManagement.GetMessageList(LoginWindow.UserId, _selectedChatHistoryId);
        this.MessageListView.ItemsSource = this._messageList;
    }


    /// <summary>
    /// ChatHistoryListViewのバインド更新
    /// </summary>
    private void UpdateChatHistories()
    {
        this._chatHistoryList = SqliteManagement.GetChatHistoryList(LoginWindow.UserId);
        this.HistoryListView.ItemsSource = this._chatHistoryList;
    }


    /// <summary>
    /// Logoutボタンクリック時のEventトリガー
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void LogoutButton_Click(Object sender, RoutedEventArgs e)
    {
        this.Close();
    }
}
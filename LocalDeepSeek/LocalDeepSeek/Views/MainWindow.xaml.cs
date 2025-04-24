using LocalDeepSeek.Models;
using LocalDeepSeek.Services;
using LocalDeepSeek.Utils;
using Microsoft.Extensions.AI;
using System.Collections.ObjectModel;
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
    private MessageViewModel _messageViewModel;
    private List<ChatHistory> _chatHistoryList;
    private Int32 _selectedChatHistoryId;

    public MainWindow()
    {
        InitializeComponent();
        _messageList = new List<Message>();
        _chatHistoryList = new List<ChatHistory>();
        _selectedChatHistoryId = -1;
        UpdateMessages();
        _messageViewModel = new MessageViewModel();
        _messageViewModel.Messages = new ObservableCollection<Message>(_messageList);
        this.MessageListView.ItemsSource = _messageViewModel.Messages;
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

        // ViewModelバインドに切り替え
        _messageViewModel.Messages = new ObservableCollection<Message>(_messageList);
        this.MessageListView.ItemsSource = _messageViewModel.Messages;

        // HumanのMessageをViewModelに追加
        String input = this.UserInputTextBox.Text;
        Message humanMessage = new Message()
        {
            ChatHistoryId = _selectedChatHistoryId,
            UserId = LoginWindow.UserId,
            IsHuman = 1,
            Content = input,
        };
        _messageViewModel.Messages.Add(humanMessage);

        this.UserInputTextBox.Text = String.Empty;
        
        // DB挿入 (ユーザメッセージ)
        SqliteManagement.InsertMessage(humanMessage);

        // AIのMessageをViewModelに追加
        Message aiMessage = new Message()
        {
            ChatHistoryId = _selectedChatHistoryId,
            UserId = LoginWindow.UserId,
            IsHuman = 0,
            Content = String.Empty,
        };
        _messageViewModel.Messages.Add(aiMessage);
        await ChatService.StreamingWithHistory(input, _messageViewModel);

        // DB挿入 (AIメッセージ)
        SqliteManagement.InsertMessage(aiMessage);

        // DBから反映
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
        // Messageバインドに切り替え
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
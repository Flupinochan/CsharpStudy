using Microsoft.WindowsAPICodePack.Dialogs;
using System.IO;
using System.Windows;

namespace FrameResizer;

public partial class MainWindow : Window
{
    // ソースフォルダ名
    private String _sourceFolderPath = String.Empty;
    // 出力フォルダ名 (デフォルトはソースフォルダ名と同じ)
    private String _outputFolderPath = String.Empty;
    // 画像ファイル名
    private List<String> _selectedImageNameList = new List<String>();

    public MainWindow()
    {
        InitializeComponent();
    }


    /// <summary>
    /// File選択Button処理 フォルダ名およびファイル名を取得
    /// </summary>
    private void SelectFileButton_Click(Object sender, RoutedEventArgs e)
    {
        CommonOpenFileDialog fileDialog = new CommonOpenFileDialog
        {
            IsFolderPicker = false, // ファイル選択モード
            Multiselect = true,     // 複数選択を許可
            Title = "ファイルを選択してください ※複数選択可",
        };
        fileDialog.Filters.Add(new CommonFileDialogFilter("Images", "*.jpg;*.jpeg;*.png"));
        fileDialog.Filters.Add(new CommonFileDialogFilter("All", "*.*"));

        CommonFileDialogResult fileDialogResult = fileDialog.ShowDialog();
        if(fileDialogResult == CommonFileDialogResult.Cancel || fileDialogResult == CommonFileDialogResult.None) return;
        if(fileDialog.FileNames is null) return;

        _sourceFolderPath = Path.GetDirectoryName(fileDialog.FileNames.First())!;
        _outputFolderPath = Path.GetDirectoryName(fileDialog.FileNames.First())!;
        _selectedImageNameList = fileDialog.FileNames.Select(filePath => Path.GetFileName(filePath)).ToList();

        this.SelectedFileListBox.ItemsSource = _selectedImageNameList;
        this.OutputFolderTextBox.Text = _outputFolderPath;
    }

    /// <summary>
    /// Folder選択Button フォルダ名およびファイル名を取得
    /// </summary>
    private void SelectFolderButton_Click(Object sender, RoutedEventArgs e)
    {
        CommonOpenFileDialog folderDialog = new CommonOpenFileDialog
        {
            IsFolderPicker = true,
            Multiselect = false,
            Title = "フォルダを選択してください"
        };

        CommonFileDialogResult fileDialogResult = folderDialog.ShowDialog();
        if(fileDialogResult == CommonFileDialogResult.Cancel || fileDialogResult == CommonFileDialogResult.None) return;
        if(folderDialog.FileNames is null) return;

        String[] imageExtensions = { ".jpg", ".jpeg", ".png" };
        String[] selectedFilesPath = Directory.GetFiles(folderDialog.FileName)
                                              .Where(filePath => imageExtensions.Contains(Path.GetExtension(filePath)))
                                              .ToArray();
        _sourceFolderPath = folderDialog.FileName;
        _outputFolderPath = folderDialog.FileName;
        _selectedImageNameList = selectedFilesPath.Select(filePath => Path.GetFileName(filePath)).ToList();

        this.SelectedFileListBox.ItemsSource = _selectedImageNameList;
        this.OutputFolderTextBox.Text = _outputFolderPath;
    }

    /// <summary>
    /// Width RadioButton
    /// </summary>
    private void WidthRadioButton_Checked(Object sender, RoutedEventArgs e)
    {
        if (this.WidthDecimalUpDown is null || this.HeightDecimalUpDown is null) return;
        this.WidthDecimalUpDown.IsEnabled = true;
        this.HeightDecimalUpDown.IsEnabled = false;
        this.HeightDecimalUpDown.Value = 0;
    }

    /// <summary>
    /// Height RadioButton
    /// </summary>
    private void HeightRadioButton_Checked(Object sender, RoutedEventArgs e)
    {
        if (this.WidthDecimalUpDown is null || this.HeightDecimalUpDown is null) return;
        this.HeightDecimalUpDown.IsEnabled = true;
        this.WidthDecimalUpDown.IsEnabled = false;
        this.WidthDecimalUpDown.Value = 0;
    }
}
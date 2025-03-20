using FrameResizer.Utils;
using Microsoft.WindowsAPICodePack.Dialogs;
using SixLabors.ImageSharp;
using System.IO;
using System.Windows;
using MaterialDesignThemes.Wpf;

namespace FrameResizer;

public partial class MainWindow : Window
{
    // ソースフォルダ名
    private String _sourceFolderPath = String.Empty;
    // 出力フォルダ名 (デフォルトはソースフォルダ名と同じ)
    private String _outputFolderPath = String.Empty;
    // 画像ファイル名
    private List<String> _selectedImageNameList = new List<String>();
    // Width
    private Int32 _outputWidth = -1;
    // Height
    private Int32 _outputHeight = -1;
    // Border Color
    Color _borderColor = Color.Black;
    // Border Size
    private Int32  _borderSize = -1;

    public MainWindow()
    {
        InitializeComponent();
    }


    /// <summary>
    /// SelectFileButton フォルダ名およびファイル名を取得
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
    /// SelectFolderButton フォルダ名およびファイル名を取得
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
        if(fileDialogResult is CommonFileDialogResult.Cancel or CommonFileDialogResult.None) return;
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
    /// SelectOutputFolderButton 出力フォルダ名を取得
    /// </summary>
    private void SelectOutputFolderButton_Click(Object sender, RoutedEventArgs e)
    {
        CommonOpenFileDialog folderDialog = new CommonOpenFileDialog
        {
            IsFolderPicker = true,
            Multiselect = false,
            Title = "出力フォルダを選択してください"
        };

        CommonFileDialogResult fileDialogResult = folderDialog.ShowDialog();
        if(fileDialogResult is CommonFileDialogResult.Cancel or CommonFileDialogResult.None) return;
        if(folderDialog.FileNames is null) return;

        _outputFolderPath = folderDialog.FileName;
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
        this.HeightDecimalUpDown.Value = 1;
    }

    /// <summary>
    /// Height RadioButton
    /// </summary>
    private void HeightRadioButton_Checked(Object sender, RoutedEventArgs e)
    {
        if (this.WidthDecimalUpDown is null || this.HeightDecimalUpDown is null) return;
        this.HeightDecimalUpDown.IsEnabled = true;
        this.WidthDecimalUpDown.IsEnabled = false;
        this.WidthDecimalUpDown.Value = 1;
    }


    /// <summary>
    /// 実行 Button
    /// </summary>
    async private void ExecuteButton_Click(Object sender, RoutedEventArgs e)
    {
        // Button無効化/ローディング開始
        this.ExecuteButton.Content = "処理中";
        ButtonProgressAssist.SetIsIndeterminate(this.ExecuteButton, true);
        ButtonProgressAssist.SetIsIndicatorVisible(this.ExecuteButton, true);
        this.ExecuteButton.IsEnabled = false;

        await Task.Delay(1000);

        // Width/Heightの取得
        if(this.WidthRadioButton.IsChecked == true)
        {
            _outputWidth = Convert.ToInt32(this.WidthDecimalUpDown.Value);
            _outputHeight = 0;
        }
        else
        {
            _outputHeight = Convert.ToInt32(this.HeightDecimalUpDown.Value);
            _outputWidth = 0;
        }

        // 出力フォルダ名の取得
        _outputFolderPath = this.OutputFolderTextBox.Text;

        // BorderColorの取得
        _borderColor = Color.ParseHex(this.BorderColorTextBox.Text);

        // BorderSizeの取得
        _borderSize = Convert.ToInt32(this.BorderSizeDecimalUpDown.Value);

        // 画像リサイズ実行
        List<String> sourceImagePaths = new List<String>();
        List<String> outputImagePaths = new List<String>();
        foreach(String imageName in _selectedImageNameList)
        {
            // ソースイメージの絶対パス
            String sourceImagePath = Path.Combine(_sourceFolderPath, imageName);
            sourceImagePaths.Add(sourceImagePath);
            // 出力イメージの絶対パス
            String outputImagePath = Path.Combine(_outputFolderPath, imageName);
            outputImagePaths.Add(outputImagePath);
        }

        ParallelLoopResult parallelLoopResult = Parallel.For(0, sourceImagePaths.Count, i =>
        {
            String sourceImagePath = sourceImagePaths[i];
            String outputImagePath = outputImagePaths[i];
            CustomiseImage.Convert(sourceImagePath, outputImagePath,
                                    _outputHeight, _outputWidth,
                                    _borderSize, _borderColor);
        });

        if(parallelLoopResult.IsCompleted)
        {
            Console.WriteLine("全処理が正常に完了しました");
        }

        // Button有効化/ローディング終了
        this.ExecuteButton.Content = "実行";
        ButtonProgressAssist.SetIsIndeterminate(this.ExecuteButton, false);
        ButtonProgressAssist.SetIsIndicatorVisible(this.ExecuteButton, false);
        this.ExecuteButton.IsEnabled = true;

        this.FinishedDialog.IsOpen = true;
    }

    /// <summary>
    /// Dialogを閉じるButton
    /// </summary>
    private void CloseDialogButton_Click(Object sender, RoutedEventArgs e)
    {
        this.FinishedDialog.IsOpen = false;
    }
}
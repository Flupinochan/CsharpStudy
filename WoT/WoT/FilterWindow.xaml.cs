using System.Windows;
using WoT.Utils;

namespace WoT;

/// <summary>
/// FilterWindow.xaml の相互作用ロジック
/// </summary>
public partial class FilterWindow:Window
{
    // static変数にするとClose()されても残る
    private static String _nation = String.Empty;
    private static String _tier = String.Empty;
    private static String _name = String.Empty;
    private static String _battle = String.Empty;
    private static String _winRate = String.Empty;
    private static String _badge = String.Empty;

    public FilterWindow()
    {
        this.InitializeComponent();
        this.nationTextBox.Text = _nation;
        this.tierTextBox.Text = _tier;
        this.nameTextBox.Text = _name;
        this.battleTextBox.Text = _battle;
        this.winRateTextBox.Text = _winRate;
        this.badgeTextBox.Text = _badge;
    }

    /// <summary>
    /// キャンセルボタン
    /// </summary>
    private void cancelButton_Click(Object sender, RoutedEventArgs e)
    {
        this.Close();
    }

    /// <summary>
    /// OKボタン (フィルター処理)
    /// </summary>
    private void okButton_Click(Object sender, RoutedEventArgs e)
    {
        _nation = this.nationTextBox.Text;
        _tier = this.tierTextBox.Text;
        _name = this.nameTextBox.Text;
        _battle = this.battleTextBox.Text;
        _winRate = this.winRateTextBox.Text;
        _badge = this.badgeTextBox.Text;

        Int32? tierInt = null;
        if(Int32.TryParse(_tier, out Int32 tierValue))
        {
            tierInt = tierValue;
        }

        Int32? battleInt = null;
        if(Int32.TryParse(_battle, out Int32 battleValue))
        {
            battleInt = battleValue;
        }

        Double? winRateDouble = null;
        if(Double.TryParse(_winRate, out Double winRateValue))
        {
            winRateDouble = winRateValue;
        }

        Int32? badgeInt = null;
        if(Int32.TryParse(_badge, out Int32 badgeValue))
        {
            badgeInt = badgeValue;
        }

        // フィルター処理
        List<PlayerVehicleSummary> filteredList = MainWindow._observablePlayerVehicleSummary.Where(delegate (PlayerVehicleSummary vehicle)
        {
            // それぞれANDマッチ判定
            Boolean nameMatches = String.IsNullOrEmpty(_name) || vehicle.Name?.Contains(_name, StringComparison.OrdinalIgnoreCase) == true;
            Boolean nationMatches = String.IsNullOrEmpty(_nation) || vehicle.Nation?.Contains(_nation, StringComparison.OrdinalIgnoreCase) == true;
            Boolean tierMatches = !tierInt.HasValue || vehicle.Tier == tierInt;
            Boolean battleMatches = !battleInt.HasValue || vehicle.Battles >= battleInt;
            Boolean winRateMatches = !winRateDouble.HasValue || vehicle.WinRate >= winRateDouble;
            Boolean badgeMatches = !badgeInt.HasValue || vehicle.MarkOfMastery == badgeInt;

            return nameMatches && nationMatches && tierMatches && battleMatches && winRateMatches && badgeMatches;
        }).ToList();

        // フィルター結果を代入
        MainWindow._filterObservablePlayerVehicleSummary.Clear();
        foreach(PlayerVehicleSummary vehicle in filteredList)
        {
            MainWindow._filterObservablePlayerVehicleSummary.Add(vehicle);
        }


        this.Close();
    }
}

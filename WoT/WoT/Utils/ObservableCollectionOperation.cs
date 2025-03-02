using System.Collections.ObjectModel;

namespace WoT.Utils;

class ObservableCollectionOperation
{
    // 指定したページのObservableCollectionを返す関数
    public ObservableCollection<PlayerVehicleSummary> Paginate(
        ObservableCollection<PlayerVehicleSummary> filteredList,
        Int32 pageSize,
        Int32 currentPage)
    {
        // コピーを作成
        List<PlayerVehicleSummary> copyList = new List<PlayerVehicleSummary>(filteredList);

        Int32 totalItems = copyList.Count;
        Int32 totalPages = (Int32)Math.Ceiling((Double)totalItems / pageSize);

        // 現在のページに対応するアイテムを取得
        List<PlayerVehicleSummary> itemsToDisplay = copyList
            .Skip((currentPage - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        return new ObservableCollection<PlayerVehicleSummary>(itemsToDisplay);
    }

}

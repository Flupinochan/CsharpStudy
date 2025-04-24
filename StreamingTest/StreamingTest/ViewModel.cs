using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace StreamingTest;

using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;

public class ViewModel
{
    public ObservableCollection<MyData> Items { get; set; }

    public ViewModel()
    {
        Items = new ObservableCollection<MyData>
        {
            new MyData { Name = "Item1", Value = 10 },
            new MyData { Name = "Item2", Value = 20 },
            new MyData { Name = "Item3", Value = 30 }
        };

        // ストリーミングデータのシミュレーション
        Task.Run(UpdateDataAsync);
    }

    private async Task UpdateDataAsync()
    {
        Random rnd = new Random();
        while(true)
        {
            await Task.Delay(1000); // 1秒ごとにデータ更新

            // どれか1つのアイテムの値をランダムに変更
            int index = rnd.Next(Items.Count);
            Items[index].Value = rnd.Next(0, 100);
        }
    }
}

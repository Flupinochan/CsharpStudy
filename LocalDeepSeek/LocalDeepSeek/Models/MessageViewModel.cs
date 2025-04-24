using LocalDeepSeek.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

public class MessageViewModel
{
    public ObservableCollection<Message> Messages { get; set; }

    public MessageViewModel()
    {
        Messages = new ObservableCollection<Message>();
    }
}

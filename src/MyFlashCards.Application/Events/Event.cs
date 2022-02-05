using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyFlashCards.Application.Events;

public abstract class Event<TData> : EventBase
{
    [NotMapped]
    public TData Data {
        get => JsonConvert.DeserializeObject<TData>(DataString)!;
        set => DataString = JsonConvert.SerializeObject(value);
    }
}

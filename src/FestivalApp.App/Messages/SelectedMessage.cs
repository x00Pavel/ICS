using FestivalApp.BL.Models;

namespace FestivalApp.App.Messages
{
    public class SelectedMessage<T> : Message<T>
        where T : IModel
    {
    }
}

using FestivalApp.BL.Models;

namespace FestivalApp.App.Messages
{
    public class UpdateMessage<T> : Message<T>
        where T : IModel
    {
    }
}

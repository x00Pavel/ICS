using FestivalApp.BL.Models;

namespace FestivalApp.App.Messages
{
    public class DeleteMessage<T> : Message<T> where T : IModel
    {
    }
}

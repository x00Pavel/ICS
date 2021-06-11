using FestivalApp.BL.Models;

namespace FestivalApp.App.Messages
{
    public class NewEntityMessage<T> : Message<T> where T : IModel
    {

    }
}

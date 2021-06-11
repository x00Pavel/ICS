namespace FestivalApp.App.Factories
{
    public interface IFactory<out T>
    {
        T Create();
    }
}


namespace InteractionSystem.Interfaces
{
    public interface IDisplayable<T> : IActivable
    {
        void Display(T obj);
    }
}

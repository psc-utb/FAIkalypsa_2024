
namespace InteractionSystem.Interfaces
{
    public interface IInformationElement<T> : IActivable
    {
        void SetInformation(T info);
    }
}

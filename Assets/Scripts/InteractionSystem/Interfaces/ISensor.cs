
namespace InteractionSystem.Interfaces
{
    public interface ISensor<T>
    {
        T SensedObject { get; }
        T Sense();
    }
}

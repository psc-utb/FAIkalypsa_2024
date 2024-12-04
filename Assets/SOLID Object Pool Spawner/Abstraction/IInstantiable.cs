namespace SOLID_Object_Pool
{
    public interface IInstantiable<T>
    {
        T Instantiate();
    }
}

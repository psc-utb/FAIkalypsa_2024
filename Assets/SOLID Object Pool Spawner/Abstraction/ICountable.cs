namespace SOLID_Object_Pool
{
    public interface ICountable
    {
        /// <summary>
        /// The total amount of items currently in the object.
        /// </summary>
        int Count { get; }
    }
}

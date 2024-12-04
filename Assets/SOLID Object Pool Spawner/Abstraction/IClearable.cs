namespace SOLID_Object_Pool
{
    public interface IClearable
    {
        /// <summary>
        /// Removes all items. If the object contains a destroy callback then it will be called for each item that is in the object.
        /// </summary>
        void Clear();
    }
}

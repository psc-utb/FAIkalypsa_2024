namespace SOLID_Object_Pool
{
    public interface IObjectPool<T>
    {
        /// <summary>
        /// Get an instance from the pool. If the pool is empty then a null is returned.
        /// </summary>
        /// <returns>Instance from the pool or null.</returns>
        T GetObject();

        /// <summary>
        /// Returns the instance back to the pool.
        /// </summary>
        /// <param name="releasedObject"></param>
        void ReleaseObject(T releasedObject);
    }
}

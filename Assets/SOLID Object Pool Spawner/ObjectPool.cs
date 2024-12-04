using System.Collections.Generic;

namespace SOLID_Object_Pool
{
    public class ObjectPool<T> : IObjectPool<T>, ICountable where T : class
    {
        //This list is used to keep track of all the spawned objects' position
        //to disable them when they are behing the deactivation point.
        protected Queue<T> objectPool;

        public int Count => objectPool.Count;


        //Create objects before the game starts to make the ready to be reused.
        public ObjectPool(IInstantiable<T> poolObjectInstantiable, int numberOfObjects)
        {
            objectPool = numberOfObjects > 0 ? new Queue<T>(numberOfObjects) : new Queue<T>();

            for (int i = 0; i < numberOfObjects; i++)
            {
                T obj = poolObjectInstantiable.Instantiate();
                objectPool.Enqueue(obj);
            }
        }

        public T GetObject()
        {
            if (objectPool.Count > 0)
            {
                T objecttoSpawn = objectPool.Dequeue();

                return objecttoSpawn;
            }
            return null;
        }

        public void ReleaseObject(T disabledObject)
        {
            objectPool.Enqueue(disabledObject);
        }
    }
}
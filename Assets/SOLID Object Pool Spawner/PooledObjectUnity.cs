using SOLID_Object_Pool;
using UnityEngine;

namespace SOLID_Object_Pool_Spawner
{
    public class PooledObjectUnity : IPooledObject<GameObject>
    {
        private GameObject pooledObject;
        public GameObject Object => pooledObject;

        private GameObject parentObject;


        public PooledObjectUnity(GameObject obj) : this(obj, null)
        {
        }

        public PooledObjectUnity(GameObject obj, GameObject parentObject)
        {
            pooledObject = obj;
            this.parentObject = parentObject;
        }

        public virtual void Initialize()
        {
            Object.SetActive(false);
            if (parentObject)
            {
                Object.transform.parent = parentObject.transform;
            }
        }
    }
}

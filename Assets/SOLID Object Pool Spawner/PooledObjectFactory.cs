using SOLID_Object_Pool;
using UnityEngine;

namespace SOLID_Object_Pool_Spawner
{
    [System.Serializable]
    public class PooledObjectFactory : IComposition<GameObject>, IInstantiable<PooledObjectUnity>
    {
        [SerializeField]
        private GameObject pooledObject;
        public GameObject Object => pooledObject;


        [Header("Parent Game Object -> Spawn Point (Optional)")]
        [Tooltip("Set it if you want to use a parent Game Object of spawned object.")]
        [SerializeField]
        private GameObject parentObject;
        public GameObject ParentObject => parentObject;


        PooledObjectUnity IInstantiable<PooledObjectUnity>.Instantiate()
        {
            GameObject obj = GameObject.Instantiate(Object);
            PooledObjectUnity objPooled = new PooledObjectUnity(obj, ParentObject);
            objPooled.Initialize();
            return objPooled;
        }
    }
}

using System.Collections.Generic;
using SOLID_Object_Pool;
using UnityEngine;

namespace SOLID_Object_Pool_Spawner
{
    public class ObjectPoolSpawner : Spawner
    {
        [Header("Spawn properties")]
        [SerializeField]
        private int count;
        public int Count => count;

        private IObjectPool<PooledObjectUnity> objectPool;

        private readonly Dictionary<GameObject, PooledObjectUnity> spawnedObjects = new Dictionary<GameObject, PooledObjectUnity>();

        protected new void Awake()
        {
            base.Awake();
        }

        //Create objects before the game starts to make the ready to be reused.
        protected override void Initialize()
        {
            base.Initialize();
            objectPool = new ObjectPool<PooledObjectUnity>(spawnedObject, count);
        }

        public override void SpawnObject()
        {
            PooledObjectUnity objPooled = objectPool.GetObject();
            GameObject obj = objPooled?.Object ?? null;

            SpawnObject(obj);

            if (objPooled != null)
            {
                spawnedObjects[obj] = objPooled;
            }
        }

        public override void DeSpawn(GameObject obj, bool setActive)
        {
            base.DeSpawn(obj, setActive);

            if (spawnedObjects.ContainsKey(obj))
            {
                objectPool.ReleaseObject(spawnedObjects[obj]);
                spawnedObjects.Remove(obj);
            }
        }
    }
}
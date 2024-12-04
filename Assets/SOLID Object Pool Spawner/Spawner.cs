using SOLID_Object_Pool;
using UnityEngine;

namespace SOLID_Object_Pool_Spawner
{
    public class Spawner : MonoBehaviour
    {
        [Tooltip("Assign a reference which implements ISpawnTriggerable interface (optional).")]
        [SerializeField]
        private GameObject spawnTriggerable;


        [Header("Spawned object")]
        [SerializeField]
        protected PooledObjectFactory spawnedObject;


        [Header("First spawned using external class or system")]
        [SerializeField]
        protected bool spawnedExternallyFirst = false;


        [SerializeField]
        private float spawnChance = 1;
        [SerializeField]
        private Transform positionPoint;
        [SerializeField]
        private Vector3 customPosition;
        [SerializeField]
        private Vector3 randPosFactor;
        [SerializeField]
        private Vector3 rotation;
        [SerializeField]
        private Vector3 randRotFactor;


        protected void Awake()
        {
            Initialize();

            //spawn trigger initialization of the gameobject
            if (spawnTriggerable != null)
            {
                ISpawnTriggerable spawnEventableObj = spawnTriggerable.GetComponent<ISpawnTriggerable>();
                if (spawnEventableObj != null)
                {
                    spawnEventableObj.SpawnTrigger += this.SpawnObject;
                }
            }
        }


        //Create objects before the game starts to make the ready to be reused.
        protected virtual void Initialize()
        {
            //can be used, e.g., for player respawn when the player where inserted to the scene by a designer (i.e. not by a code)
            if (spawnedExternallyFirst == true && spawnedObject != null)
            {
                //connect method with the despawn event if no count is set but there is a pool object
                IDespawnable<GameObject> despawnableObj = spawnedObject.Object.GetComponent<IDespawnable<GameObject>>();
                if (despawnableObj != null)
                {
                    despawnableObj.Despawn += this.DeSpawn;
                }
            }
        }


        public virtual void SpawnObject()
        {
            GameObject obj = spawnedObject.Object;
            SpawnObject(obj, customPosition, randPosFactor, rotation, randRotFactor, spawnChance);
        }

        public virtual void SpawnObject(GameObject obj)
        {
            SpawnObject(obj, customPosition, randPosFactor, rotation, randRotFactor, spawnChance);
        }

        //Manages the spawning of objects based on the input parameter values of object properties.
        protected void SpawnObject(GameObject obj, Vector3 position, Vector3 randPosFactor, Vector3 rotation, Vector3 randRotFactor, float spawnChance)
        {
            //Generate random value and spawn based on it.
            if (Random.value <= spawnChance)
            {
                if (obj != null)
                {

                    Vector3 randomPos = new Vector3(Random.Range(-randPosFactor.x, randPosFactor.x),
                                                 Random.Range(-randPosFactor.y, randPosFactor.y),
                                                 Random.Range(-randPosFactor.z, randPosFactor.z));

                    Vector3 randomRot = new Vector3(Random.Range(-randRotFactor.x, randRotFactor.x),
                                                 Random.Range(-randRotFactor.y, randRotFactor.y),
                                                 Random.Range(-randRotFactor.z, randRotFactor.z));

                    obj.transform.localRotation = Quaternion.Euler(rotation + randomRot);


                    //connect method with the despawn event
                    IDespawnable<GameObject> despawnableObj = obj.GetComponent<IDespawnable<GameObject>>();
                    if (despawnableObj != null)
                    {
                        despawnableObj.Despawn += this.DeSpawn;
                    }


                    //set position of the spawned object
                    if (positionPoint)
                    {
                        obj.transform.position = positionPoint.transform.position + randomPos;
                    }
                    else if (spawnedObject.ParentObject)
                    {
                        obj.transform.localPosition = position + randomPos;
                    }
                    else
                    {
                        obj.transform.position = position + randomPos;
                    }


                    //reinitialization of all game object's scripts (just prior to activation of game object)
                    IReinitializable[] reinitializableObjs = obj.GetComponents<IReinitializable>();
                    foreach (var reinitializableObj in reinitializableObjs)
                    {
                        if (reinitializableObj != null)
                        {
                            reinitializableObj.Reinitializate();
                        }
                    }


                    //activate game object after reinitialization
                    obj.SetActive(true);
                }
                else
                {
                    Debug.LogWarning("No GameObject was spawned! (Pool with objects is probably empty)");
                }
            }
        }


        public void DeSpawn(GameObject obj)
        {
            DeSpawn(obj, false);
        }

        public virtual void DeSpawn(GameObject obj, bool setActive)
        {
            //disconnect method with the despawn event
            IDespawnable<GameObject> despawnableObj = obj.GetComponent<IDespawnable<GameObject>>();
            if (despawnableObj != null)
            {
                despawnableObj.Despawn -= this.DeSpawn;
            }
            obj.SetActive(setActive);
        }
    }
}
using System.Collections.Generic;
using UnityEngine;


namespace HolyCow.FactoryGame
{
    /// <summary>
    /// Manages object pooling for a specific GameObject type.
    /// </summary>
    public class ObjectPoolManager : MonoGenericSingletone<ObjectPoolManager>
    {
        [SerializeField] private GameObject objectToPool; // The prefab to create and pool
        [SerializeField] private int poolSize; // The initial size of the object pool

        private List<GameObject> objectPool = new List<GameObject>(); // The list of pooled objects

        private void Start()
        {
            InitializeObjectPool();
        }

        /// <summary>
        /// Initializes the object pool by instantiating and deactivating objects.
        /// </summary>
        private void InitializeObjectPool()
        {
            for (int i = 0; i < poolSize; i++)
            {
                GameObject obj = Instantiate(objectToPool);
                obj.SetActive(false);
                objectPool.Add(obj);
            }
        }

        /// <summary>
        /// Retrieves a pooled object, activates it, and returns it. Returns null if no objects are available.
        /// </summary>
        /// <returns>The pooled object, or null if none are available.</returns>
        public GameObject GetPooledObject()
        {
            foreach (GameObject obj in objectPool)
            {
                if (!obj.activeInHierarchy)
                {
                    obj.SetActive(true);
                    return obj;
                }
            }
            return null;
        }
    }
}
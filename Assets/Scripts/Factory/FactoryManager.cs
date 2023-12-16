using System.Collections.Generic;
using UnityEngine;


namespace HolyCow.FactoryGame
{
    /// <summary>
    /// Manages a list of active factories and their gem production.
    /// </summary>
    public class FactoryManager : MonoGenericSingletone<FactoryManager>
    {
        [SerializeField] private List<Factory> factories; // List of active factories
        [SerializeField] private float productionTimer = 0f; // Timer for gem production

  
        // Add a factory to the list of active factories.
        public void AddFactory(Factory factory)
        {
            factories.Add(factory);
        }

        // Get the list of active factories.
        public List<Factory> GetActiveFactories()
        {
            return factories;
        }

        private void Update()
        {
            if (factories.Count <= 0)
                return;

            UpdateFactoriesGems();
        }


        // Update gem production for all active factories.
        public void UpdateFactoriesGems()
        {
            productionTimer += Time.deltaTime;
            if (productionTimer >= 1.0f)
            {
                foreach (Factory factory in factories)
                {
                    factory.ProduceGems();
                }
                productionTimer -= 1.0f;
            }
        }
    }
}

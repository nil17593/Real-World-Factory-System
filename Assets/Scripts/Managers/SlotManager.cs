using UnityEngine;
using System;
using System.Collections.Generic;



    [Serializable]
    public class FactoryData
    {
        public int level;
        public float productionRate;
    }

    [Serializable]
    public class FactoryDataWrapper
    {
        public List<FactoryData> factoryDataList;
    }


namespace HolyCow.FactoryGame
{
    /// <summary>
    /// Manages the creation, loading, and saving of factory objects within a grid layout.
    /// </summary>
    public class SlotManager : MonoBehaviour
    {
        [SerializeField] private Factory factoryPrefab;
        [SerializeField] private Transform gridLayout; // Reference to your grid layout
        [SerializeField] private int factoryMadeGemCost;
        [SerializeField] private int maxFactoryCount = 100;

        #region 
        private int currentFactoryCount;
        private const string currentFactoryCountPlayerPrefsKey = "ActiveFactories";
        #endregion

        private void Start()
        {
            LoadFactories();

            if (PlayerPrefs.HasKey(currentFactoryCountPlayerPrefsKey))
            {
                currentFactoryCount = PlayerPrefs.GetInt(currentFactoryCountPlayerPrefsKey);
            }
        }

        // Creates a new factory if enough gems are available and adds it to the grid layout.
        public void BuildFactory()
        {
            if (!GameManager.Instance.TrySpendGems(factoryMadeGemCost))
            {
                UIManager.Instance.ShowMessage("Don't have enough Gems", false);
                return;
            }
            else if (currentFactoryCount < maxFactoryCount)
            {
                Factory newFactory = Instantiate(factoryPrefab, gridLayout);
                FactoryManager.Instance.AddFactory(newFactory);
                currentFactoryCount += 1;
                PlayerPrefs.SetInt(currentFactoryCountPlayerPrefsKey, currentFactoryCount);
            }
        }

        private void OnApplicationQuit()
        {
            SaveFactories();
        }

        // Save the data of factories to a JSON file.
        private void SaveFactories()
        {
            FactoryDataWrapper wrapper = new FactoryDataWrapper
            {
                factoryDataList = new List<FactoryData>()
            };

            List<Factory> factories = FactoryManager.Instance.GetActiveFactories();

            foreach (Factory factory in factories)
            {
                FactoryData factoryData = new FactoryData
                {
                    level = factory.Level,
                    productionRate = factory.ProductionRate,
                };
                wrapper.factoryDataList.Add(factoryData);
            }

            JsonManager.Instance.SaveJson(wrapper); // Use the JsonManager to save the data
        }

        // Loads the data of factories from a JSON file and creates them in the grid layout.
        private void LoadFactories()
        {
            FactoryDataWrapper wrapper = JsonManager.Instance.LoadJson<FactoryDataWrapper>(); // Use the JsonManager to load the data

            if (wrapper != null)
            {
                List<FactoryData> factoryDataList = wrapper.factoryDataList;

                foreach (FactoryData factoryData in factoryDataList)
                {
                    CreateFactory(factoryData);
                }
            }
            else
            {
                Factory initialFactory = Instantiate(factoryPrefab, gridLayout);
                FactoryManager.Instance.AddFactory(initialFactory);
                currentFactoryCount += 1;
                PlayerPrefs.SetInt(currentFactoryCountPlayerPrefsKey, currentFactoryCount);
            }
        }

        // Creates a factory based on the provided factory data and adds it to the grid layout.
        private void CreateFactory(FactoryData factoryData)
        {
            Factory newFactory = Instantiate(factoryPrefab, gridLayout);
            newFactory.SetLevel(factoryData.level);
            newFactory.SetProductionRate(factoryData.productionRate);
            newFactory.Init();
            FactoryManager.Instance.AddFactory(newFactory);
        }
    }
}
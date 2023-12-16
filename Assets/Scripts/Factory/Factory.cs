using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace HolyCow.FactoryGame
{
    /// <summary>
    /// Represents a factory that produces gems over time and can be upgraded.
    /// </summary>
    public class Factory : MonoBehaviour, IFactory
    {
        [Header("Factory Settings")]
        [SerializeField] private int factoryID;
        [SerializeField] private int level = 1;
        [SerializeField] private float productionRate = 0.0000001f;
        [SerializeField] private int maxUpgradeLevel = 3;
        [SerializeField] private int upgradeCost = 20;
        [Space]
        [Header("UI references")]
        [SerializeField] private TextMeshProUGUI levelText;
        [SerializeField] private Button upgradeButton;

        #region private fields
        private int totalGemsProduced = 0;
        private float lastClosureTime;
        #endregion


        // Initializes the factory and its UI elements.
        private void Start()
        {
            //Init();
            // Initialize the UI elements
            if (levelText != null)
                levelText.text = "Level " + level;

            if (upgradeButton != null)
                upgradeButton.onClick.AddListener(UpgradeFactory);
        }


        // Initializes the factory, updates the gems produced during closure, and adds remaining gems.
        public void Init()
        {
            // Load the last closure time from PlayerPrefs
            if (PlayerPrefs.HasKey("LastClosureTime"))
            {
                lastClosureTime = PlayerPrefs.GetFloat("LastClosureTime", Time.realtimeSinceStartup);

                // Calculate the time difference since the last closure
                float timeDifference = Time.time - lastClosureTime;
                int gemsProducedDuringClosure = Mathf.FloorToInt(timeDifference * ProductionRate);

                // Subtract gems produced during closure from the total
                totalGemsProduced -= gemsProducedDuringClosure;
                Debug.Log(totalGemsProduced);
                // Add the remaining gems to the GameManager
                GameManager.Instance.AddGems(totalGemsProduced);
            }
        }


        // Produces gems and adds them to the GameManager while playing the gem addition animation.
        public void ProduceGems()
        {
            int gemsProduced = Mathf.FloorToInt(ProductionRate);
            totalGemsProduced += gemsProduced;
            UIManager.Instance.PlayAddGemsAnimation(this.transform);
            GameManager.Instance.AddGems(gemsProduced);
        }

        public int GetCurrentLevel()
        {
            return level;
        }

        // Upgrades the factory to a higher level if possible.
        public void UpgradeFactory()
        {
            if (!GameManager.Instance.TrySpendGems(upgradeCost))
            {
                UIManager.Instance.ShowMessage("Dont Have Enough Gems", false);
                return;
            }
            if (level < maxUpgradeLevel)
            {
                level++; // Increase the factory's level
                ProductionRate = level; // Update production rate based on the level
                UIManager.Instance.ShowMessage("Level upgraded", true);
                if (levelText != null)
                    levelText.text = "Level " + level;
            }
            else
            {
                UIManager.Instance.ShowMessage("Already upgraded to max level", false);
            }
        }


        // Stores the last closure time when the application is closed.
        private void OnApplicationQuit()
        {
            // Store the current time in PlayerPrefs when the game is closed
            PlayerPrefs.SetFloat("LastClosureTime", Time.realtimeSinceStartup);
            PlayerPrefs.Save();
        }

        private void OnDestroy()
        {
            // Store the current time in PlayerPrefs when the game is closed
            PlayerPrefs.SetFloat("LastClosureTime", Time.realtimeSinceStartup);
            PlayerPrefs.Save();
        }

        // Getter methods for private fields
        #region Getter methods
        public int FactoryID
        {
            get { return factoryID; }
        }

        public int Level
        {
            get { return level; }
            private set { level = value; }
        }

        public float ProductionRate
        {
            get { return productionRate; }
            private set { productionRate = value; }
        }

        public float LastClosureTime
        {
            get { return lastClosureTime; }
            private set { lastClosureTime = value; }
        }

        public int MaxUpgradeLevel
        {
            get { return maxUpgradeLevel; }
        }

        public int UpgradeCost
        {
            get { return upgradeCost; }
            private set { upgradeCost = value; }
        }
        #endregion

        // Setter methods for private fields
        #region Setter methods
        public void SetFactoryID(int id)
        {
            factoryID = id;
        }

        public void SetLevel(int newLevel)
        {
            if (newLevel >= 1)
            {
                level = newLevel;
            }
        }

        public void SetProductionRate(float rate)
        {
            if (rate >= 1.0f)
            {
                productionRate = rate;
            }
        }

        public void SetLastClosureTime(float time)
        {
            lastClosureTime = time;
        }

        public void SetUpgradeCost(int cost)
        {
            if (cost >= 0)
            {
                upgradeCost = cost;
            }
        }
        #endregion
    }
}
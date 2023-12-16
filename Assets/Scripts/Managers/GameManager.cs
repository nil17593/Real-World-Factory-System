using UnityEngine;
using TMPro;
using System;

namespace HolyCow.FactoryGame
{
    /// <summary>
    /// Manages the game's gem currency and provides functionality to add and spend gems.
    /// </summary>
    public class GameManager : MonoGenericSingletone<GameManager>
    {
        [SerializeField] private TextMeshProUGUI gemsCountText; // Reference to the UI text displaying the gem count
        [SerializeField] private int totalGems = 0; // The total number of gems

        private const string GemsPlayerPrefsKey = "Gems";

        // Gets the current total gem count.
        public int TotalGems
        {
            get { return totalGems; }
        }

        private void Start()
        {
            LoadGems();           
        }

        //loads the toatal gems count at the start
        private void LoadGems()
        {
            if (PlayerPrefs.HasKey(GemsPlayerPrefsKey))
            {
                totalGems = PlayerPrefs.GetInt(GemsPlayerPrefsKey);
                UpdateGemsText(totalGems);
            }
        }

        // Adds the specified number of gems to the player's total and updates the UI.
        public void AddGems(int gems)
        {
            totalGems += gems;
            PlayerPrefs.SetInt(GemsPlayerPrefsKey, totalGems);
            PlayerPrefs.Save(); // Make sure to save immediately
            UpdateGemsText(totalGems);
        }


        // Updates the gem count displayed on the UI.
        void UpdateGemsText(int gems)
        {
            if (gemsCountText != null)
            {
                if (gems >= 1000000)
                {
                    gemsCountText.text = (gems / 1000000f).ToString("F1") + "M";
                }
                else if (gems >= 1000)
                {
                    gemsCountText.text = (gems / 1000f).ToString("F1") + "K";
                }
                else
                {
                    gemsCountText.text = gems.ToString();
                }
            }
        }

        // Tries to spend gems for an upgrade and updates the UI if successful.
        public bool TrySpendGems(int upgradeCost)
        {
            Debug.Log(totalGems);
            if (totalGems > upgradeCost)
            {
                totalGems -= upgradeCost;
                UpdateGemsText(totalGems);
                return true;
            }
            return false;
        }

        //triggered when quit button clicked
        public void OnAppliCationQuitButtonClick()
        {
            Application.Quit();
        }
    }
}
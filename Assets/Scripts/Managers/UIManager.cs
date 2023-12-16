using UnityEngine;
using TMPro;
using System.Collections;
using DG.Tweening;


namespace HolyCow.FactoryGame
{
    /// <summary>
    /// Singleton class responsible for managing in-game UI elements and animations.
    /// </summary>
    public class UIManager : MonoGenericSingletone<UIManager>
    {
        [Header("UI references")]
        [Tooltip("add inactive message text named messageText")]
        [SerializeField] private TextMeshProUGUI messageText; // Reference to the in-game message text
        [SerializeField] private RectTransform gemsContainer; // Reference to the container for gem animations
        [SerializeField] private GameObject flyingGemsPrefab; // Reference to the flying gems prefab

        protected override void Awake()
        {
            base.Awake();
        }


        // Coroutine for displaying in-game messages with optional success color.
        public IEnumerator ShowMessageCoroutine(string _message, bool success)
        {
            if (success)
            {
                messageText.color = Color.green;
            }
            else
            {
                messageText.color = Color.red;
            }
            messageText.gameObject.SetActive(true);
            messageText.text = _message;
            yield return new WaitForSeconds(1f);
            messageText.gameObject.SetActive(false);
        }


        // Display an in-game message with optional success color.
        public void ShowMessage(string _message, bool success)
        {
            StartCoroutine(ShowMessageCoroutine(_message, success));
        }


        //Play an animation for adding gems that move to a specified position.
        public void PlayAddGemsAnimation(Transform pos)
        {
            GameObject flyingGems = ObjectPoolManager.Instance.GetPooledObject();
            flyingGems.transform.position = pos.position;
            flyingGems.transform.SetParent(gemsContainer);
            flyingGems.transform.DOLocalMoveY(flyingGems.transform.localPosition.y + 15f, .5f)
               .OnComplete(() => flyingGems.SetActive(false)).SetEase(Ease.Linear);
        }
    }
}

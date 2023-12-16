using UnityEngine;


namespace HolyCow.FactoryGame
{
    /// <summary>
    ///Generic singletone class 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MonoGenericSingletone<T> : MonoBehaviour where T : MonoGenericSingletone<T>
    {
        private static T instance;

        public static T Instance { get { return instance; } }

        protected virtual void Awake()
        {
            if (instance == null)
            {
                instance = (T)this;
                //DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }
}
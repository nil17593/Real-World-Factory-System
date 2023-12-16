using UnityEngine;
using System.IO;


namespace HolyCow.FactoryGame
{
    /// <summary>
    /// Manages loading and saving JSON data to/from a specified file.
    /// </summary>
    public class JsonManager : MonoGenericSingletone<JsonManager>
    {
        [SerializeField] private string jsonFilePath = "factoryData.json"; // The file path for JSON data

        protected override void Awake()
        {
            base.Awake();
            // Ensure that the JSON file path is valid
            jsonFilePath = Application.persistentDataPath + "/factoryData.json";
            if (string.IsNullOrEmpty(jsonFilePath))
            {
                Debug.LogError("JSON file path is not set.");
                enabled = false; // Disable the script
            }
        }

        /// <summary>
        /// Loads JSON data from the specified file and deserializes it to an object of type T.
        /// </summary>
        /// <typeparam name="T">The type of object to deserialize the JSON data into.</typeparam>
        /// <returns>The deserialized object, or the default value of T if the file doesn't exist.</returns>
        public T LoadJson<T>()
        {
            if (File.Exists(jsonFilePath))
            {
                string json = File.ReadAllText(jsonFilePath);
                return JsonUtility.FromJson<T>(json);
            }
            else
            {
                return default(T);
            }
        }

        /// <summary>
        /// Serializes an object of type T to JSON and saves it to the specified file.
        /// </summary>
        /// <typeparam name="T">The type of object to serialize to JSON.</typeparam>
        /// <param name="data">The object to be serialized and saved.</param>
        public void SaveJson<T>(T data)
        {
            string json = JsonUtility.ToJson(data);
            File.WriteAllText(jsonFilePath, json);
        }
    }
}

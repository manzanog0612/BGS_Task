using UnityEngine;

namespace BGS_Task.Gameplay.Common.File
{
    public class FileHandler
    {
        public static void Save<T>(T data, string path)
        {
            string json = JsonUtility.ToJson(data);
            System.IO.File.WriteAllText(path, json);
        }

        public static T Load<T>(string path)
        {
            string json = System.IO.File.ReadAllText(path);
            return JsonUtility.FromJson<T>(json);
        }

        public static bool Exists(string path)
        {
            return System.IO.File.Exists(path);
        }
    }
}

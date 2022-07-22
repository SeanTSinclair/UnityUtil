using UnityEngine;

namespace ChacoUtil.Data
{
    public class Singleton<T> : MonoBehaviour where T : Component
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    GameObject singletonInstance = new GameObject();
                    singletonInstance.name = typeof(T).Name;
                    singletonInstance.hideFlags = HideFlags.HideAndDontSave;
                    _instance = singletonInstance.AddComponent<T>();
                }
                return _instance;
            }
        }

        private void OnDestroy()
        {
            if (_instance == this)
            {
                _instance = null;
            }
        }
    }
}
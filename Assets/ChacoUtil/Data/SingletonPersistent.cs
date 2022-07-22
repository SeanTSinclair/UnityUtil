using UnityEngine;
using UnityEngine.SceneManagement;

namespace ChacoUtil.Data
{
    public class SingletonPersistent<T> : MonoBehaviour where T : Component
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    Scene activeScene = SceneManager.GetActiveScene();
                    SceneManager.SetActiveScene(SceneManager.GetSceneByName("Managers"));
                    GameObject singletonInstance = new GameObject();
                    singletonInstance.name = typeof(T).Name;
                    singletonInstance.hideFlags = HideFlags.HideAndDontSave;
                    _instance = singletonInstance.AddComponent<T>();
                    SceneManager.SetActiveScene(activeScene);
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
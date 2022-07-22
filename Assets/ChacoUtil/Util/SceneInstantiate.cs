using ChacoUtil.Data;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace ChacoUtil.Util
{
    public class SceneInstantiate : Singleton<SceneInstantiate>
    {
        [SerializeField] private Object persistentScene;

        private void Awake()
        {
            if (!SceneManager.GetSceneByName(persistentScene.name).isLoaded)
            {
                SceneManager.LoadSceneAsync(persistentScene.name, LoadSceneMode.Additive);
            }
        }
        
        [ContextMenu("Load Scene 1")]
        public void LoadScene1()
        {
            SceneManager.UnloadSceneAsync("SceneTwo");
            SceneManager.LoadSceneAsync("SceneOne", LoadSceneMode.Additive);
        }
    
        [ContextMenu("Load Scene 2")]
        public void LoadScene2()
        {
            SceneManager.UnloadSceneAsync("SceneOne");
            SceneManager.LoadSceneAsync("SceneTwo", LoadSceneMode.Additive);
        }
    
    }
}

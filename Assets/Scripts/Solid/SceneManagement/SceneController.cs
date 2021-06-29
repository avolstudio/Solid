using Solid.Attributes;
using Solid.View;
using UnityEngine;

namespace Solid.SceneManagement
{
    [SceneName("your scene name in build settings")]
    public abstract class SceneController: MonoBehaviour
    {
        protected object[] _parameters;

        protected UIManager UIManager;
        
        protected Canvas sceneCanvas;
        
        private void Awake()
        {
            SceneManager.TryPullParameters(GetType(),out _parameters);

            CreateUIManager();
            
            OnSceneAwake(_parameters);
        }

        private void CreateUIManager()
        {
            if (sceneCanvas == null)
            {
                Debug.Log("sceneCanvas is not set.UIManager will not be created");
                return;
            }
            
            UIManager = new UIManager(sceneCanvas);
        }

        protected virtual void OnSceneAwake(params object [] parameters)
        {
        
        }
    }
}

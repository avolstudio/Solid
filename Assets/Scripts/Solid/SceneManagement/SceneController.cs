using Solid.Attributes;
using Solid.View;
using UnityEngine;

namespace Solid.SceneManagement
{
    [SceneName("your scene name in build settings")]
    public abstract class SceneController: MonoBehaviour
    {
        protected object[] Parameters;

        protected UIManager UIManager;
        
        [SerializeField] protected Canvas SceneCanvas;
        
        private void Awake()
        {
            SceneManager.TryPullParameters(GetType(),out Parameters);

            CreateUIManager();
            
            OnSceneAwake(Parameters);
        }

        private void CreateUIManager()
        {
            if (SceneCanvas == null)
            {
                Debug.Log("sceneCanvas is not set.UIManager will not be created");
                return;
            }
            
            UIManager = new UIManager(SceneCanvas);
        }

        protected virtual void OnSceneAwake(params object [] parameters)
        {
        
        }
    }
}

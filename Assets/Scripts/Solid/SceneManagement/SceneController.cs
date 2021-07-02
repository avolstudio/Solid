using Solid.Attributes;
using Solid.View;
using UnityEngine;

namespace Solid.SceneManagement
{
    [SceneName("your scene name in build settings")]
    public abstract class SceneController : MonoBehaviour
    {
        [SerializeField] protected Canvas SceneCanvas;
        protected object[] _parameters;

        protected UIManager UIManager;

        private void Awake()
        {
            SceneManager.TryPullParameters(GetType(), out _parameters);

            CreateUIManager();

            OnSceneAwake(_parameters);
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

        protected virtual void OnSceneAwake(params object[] parameters)
        {
        }
    }
}
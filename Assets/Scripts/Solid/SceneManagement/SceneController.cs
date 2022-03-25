using Solid.Attributes;
using Solid.Behaviours;
using Solid.UI;
using UnityEngine;

namespace Solid.SceneManagement
{
    [SceneName("your scene name in build settings")]
    public abstract class SceneController : SolidBehaviour
    {
        [SerializeField] protected SceneView _view;
        
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
            if (_view.Canvas == null)
            {
                Debug.Log("sceneCanvas is not set.UIManager will not be created");
                return;
            }

            UIManager = new UIManager(_view.Canvas);
        }

        protected virtual void OnSceneAwake(params object[] parameters)
        {
        }
    }
}

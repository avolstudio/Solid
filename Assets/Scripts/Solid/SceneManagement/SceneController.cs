using UnityEngine;

namespace Solid.SceneManagement
{
    [SceneName("your scene name in build settings")]
    public abstract class SceneController: MonoBehaviour
    {
        protected object[] _parameters;
        private void Awake()
        {
            Debug.Log(SceneManager.TryPullParameters(GetType(),out _parameters));
        
            OnSceneAwake(_parameters);
        }

        protected virtual void OnSceneAwake(params object [] parameters)
        {
        
        }
    }
}

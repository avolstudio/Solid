using System;
using System.Collections.Generic;
using System.Linq;
using Solid.Attributes;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

/*Copyright (c) Created by Oleksii Volovich 2021*/
namespace Solid.SceneManagement
{
    public static class SceneManager
    {
        private static readonly Dictionary<Type, object[]> _scenesParameters = new Dictionary<Type, object[]>();

        public static bool TryPullParameters(Type controllerType, out object[] parameters)
        {
            return _scenesParameters.TryGetValue(controllerType, out parameters);
        }

        public static void LoadAsync<TSceneController>(LoadSceneMode mode = LoadSceneMode.Single,
            params object[] parameters) where TSceneController : SceneController
        {
            var name = PushParameters<TSceneController>(parameters);

            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(name, mode);
        }

        public static TSceneController Load<TSceneController>(LoadSceneMode mode = LoadSceneMode.Single,
            params object[] parameters) where TSceneController : SceneController
        {
            var name = PushParameters<TSceneController>(parameters);

            UnityEngine.SceneManagement.SceneManager.LoadScene(name, mode);

            return Object.FindObjectOfType<TSceneController>();
        }

        private static string PushParameters<TSceneController>(object[] parameters)
            where TSceneController : SceneController
        {
            var sceneAttribute =
                (SceneName) typeof(TSceneController).GetCustomAttributes(true).First(att => att is SceneName);

            if (sceneAttribute == null)
                throw new Exception("SceneName attribute not specified");

            _scenesParameters.Add(typeof(TSceneController), parameters);

            return sceneAttribute.Name;
        }
    }
}
using System;
using UnityEngine;

/*Copyright (c) Created by Oleksii Volovich 2021*/

namespace Solid.Core
{
    public abstract class SolidBehaviour : MonoBehaviour
    {
        private static object[] _parameters;
        
        public int ID { get; }

        private void Awake()
        {
            try
            {
                OnAwake(_parameters);
            }
            catch (Exception e)
            {
                Debug.Log("Initialization failed due to " + e);
            }
        }

        protected virtual void OnAwake(params object[] parameters)
        {
        }

        public static SolidBehaviour Add(Type component, GameObject container, params object[] parameters)
        {
            _parameters = parameters;

            return (SolidBehaviour) container.AddComponent(component);
        }

        public static TSolidComponent Add<TSolidComponent>(GameObject container, params object[] parameters)
            where TSolidComponent : SolidBehaviour
        {
            _parameters = parameters;

            return (TSolidComponent) container.AddComponent(typeof(TSolidComponent));
        }

        public static TSolidComponent Instantiate<TSolidComponent>(TSolidComponent obj, params object[] parameters)
            where TSolidComponent : SolidBehaviour
        {
            _parameters = parameters;

            return GameObject.Instantiate(obj);
        }

        public static TSolidComponent Instantiate<TSolidComponent>(TSolidComponent obj, Transform parent,
            params object[] parameters) where TSolidComponent : SolidBehaviour
        {
            _parameters = parameters;

            return GameObject.Instantiate(obj, parent);
        }

        public static TSolidComponent Instantiate<TSolidComponent>(TSolidComponent obj, Vector3 position,
            Quaternion rotation, params object[] parameters) where TSolidComponent : SolidBehaviour
        {
            _parameters = parameters;

            return GameObject.Instantiate(obj, position, rotation);
        }
    }
}
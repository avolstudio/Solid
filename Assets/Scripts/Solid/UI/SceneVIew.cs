using Solid.Core;
using UnityEngine;

/*Copyright (c) Created by Oleksii Volovich 2021*/
namespace Solid.UI
{
    [RequireComponent(typeof(Canvas))]
    public class SceneView:SolidBehaviour
    {
        [SerializeField] private Canvas _canvas;

        protected override void OnAwake(params object[] parameters)
        {
            _canvas = GetComponent<Canvas>();
        }

        public Canvas Canvas => _canvas;
    }
}
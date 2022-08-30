using System.Collections.Generic;
using Solid.Core;
using UnityEngine;

/*Copyright (c) Created by Oleksii Volovich 2021*/
namespace Solid.UI
{
    public sealed class UIManager
    {
        private readonly Canvas _canvas;

        private Stack<PopUp> _popUps;

        public UIManager(Canvas canvas)
        {
            _canvas = canvas;
            _popUps = new Stack<PopUp>();
        }

        public InstantiateOperation<PopUpInfo,TPopUp> ShowPopUp<TPopUp>(bool waitForClose = true, params object[] parameters)
            where TPopUp : PopUp
        {
            var operation = Operation.CreateFromPrefab<PopUpInfo,TPopUp>(target:_canvas.gameObject, waitForClose, false, parameters: parameters);

            _popUps.Push(operation.Prefab);
            
            return operation;
        }
    }
}
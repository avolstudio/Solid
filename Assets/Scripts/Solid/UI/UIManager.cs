using System.Collections.Generic;
using UnityEngine;

namespace Solid.UI
{
    public sealed class UIManager
    {
        private readonly Canvas _canvas;
        private readonly Stack<Operation> _openedPopUps = new Stack<Operation>();

        public UIManager(Canvas canvas)
        {
            _canvas = canvas;
        }

        public void CloseAll()
        {
            for (var i = 0; i < _openedPopUps.Count; i++) _openedPopUps.Pop().Terminate(true);
        }

        public void CloseTop()
        {
            _openedPopUps.Pop().Terminate(true);
        }


        public InstantiatedOperation<TPopUp> ShowPopUp<TPopUp>(bool waitForClose = false, params object[] parameters)
            where TPopUp : PopUp
        {
            var operation = Operation.CreateFromPrefab<TPopUp>(target:_canvas.gameObject, waitForClose, false, parameters: parameters);

            _openedPopUps.Push(operation);

            return operation;
        }
    }
}
using System.Collections.Generic;
using UnityEngine;

namespace Solid.View
{
    public sealed class UIManager
    {
        private Stack<Operation> _openedPopUps = new Stack<Operation>();
    
        private Canvas _canvas;

        public UIManager(Canvas canvas)
        {
            _canvas = canvas;
        }

        public void CloseAll()
        {
            for (int i = 0; i < _openedPopUps.Count; i++)
            {
                _openedPopUps.Pop().Terminate(true);
            }
        }

        public void CloseTop()
        {
            _openedPopUps.Pop().Terminate(true);
        }
    
    
        public InstantiatedOperation<TPopUp> ShowPopUp<TPopUp>(bool waitForClose = false,params object[] parameters) where TPopUp: PopUp
        {
            var operation = Operation.CreateFromPrefab<TPopUp>(_canvas.gameObject,waitForClose,false, parameters);
            
            _openedPopUps.Push(operation);

            return operation;
        }
        
    }
}

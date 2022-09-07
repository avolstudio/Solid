using System.Collections.Generic;
using Solid.Core;
using UnityEngine;

/*Copyright (c) Created by Oleksii Volovich 2021*/
namespace Solid.UI
{
    public sealed class UIManager
    {
        private readonly Canvas _canvas;

        private List<PopUp> _popUps;

        public UIManager(Canvas canvas)
        {
            _canvas = canvas;
            _popUps = new List<PopUp>();
        }

        public InstantiateOperation<PopUpInfo,TPopUp> ShowPopUp<TPopUp>(bool waitForClose = true, params object[] parameters)
            where TPopUp : PopUp
        {
            var operation = Operation.CreateFromPrefab<PopUpInfo,TPopUp>(target:_canvas.gameObject, waitForClose, false, parameters: parameters);

            _popUps.Add(operation.Prefab);
            
            return operation;
        }

        public void ClosePopUp<TPopUp>()
        {
            var target =  _popUps.Find(popup => popup is TPopUp);

            if (target!= null)
            {
                target.CloseAndDestroy();
            }
        } 
        public void ClosePopUp<TPopUp>(TPopUp popUp)
        {
            var target =  _popUps.Find(popup => Equals(popup, popUp));

            if (target != null)
            {
                target.CloseAndDestroy();
            }
        }

        public void CloseAllPopUps()
        {
            for (int i = 0; i < _popUps.Count; i++)
            {
                _popUps[i].CloseAndDestroy();
            }
        }
    }
}
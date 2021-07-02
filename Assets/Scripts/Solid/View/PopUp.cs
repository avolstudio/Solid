using System;
using Solid.Attributes;
using Solid.Behaviours;
using UnityEngine;
using UnityEngine.UI;

namespace Solid.View
{
    [ResourcePath("override path here")]
    public abstract class PopUp : Awaitable
    {
        [SerializeField] protected Canvas _canvas;

        [SerializeField] private Button _closeButton;

        public Button CloseButton => _closeButton;

        private void OnEnable()
        {
            try
            {
                OnShow();
            }
            catch (Exception e)
            {
                Debug.LogError("popUp showing failed with exception: " + e);
                throw;
            }
        }

        protected abstract void OnShow();
    }
}
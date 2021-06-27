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
        
        public Button CloseButton;

        private void OnEnable()
        {
            try
            {
                OnShow();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }

        protected abstract void OnShow();
    }
}

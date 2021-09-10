using System;
using System.Threading.Tasks;
using Solid.Attributes;
using Solid.Behaviours;
using UnityEngine;
using UnityEngine.UI;

namespace Solid.UI
{
    [ResourcePath("override path here")]
    public abstract class PopUp : Awaitable
    {
        [SerializeField] protected Canvas _canvas;

        [SerializeField] private Button _closeButton;

        public Button CloseButton => _closeButton;

        private  void Start()
        {
            _closeButton?.onClick.AddListener(Close);
        }

        protected void OnDestroy()
        {
            SetComplete(true);
            
            _closeButton?.onClick.RemoveListener(Close);
        }

        private async void OnEnable()
        {
            try
            {
                 await OnShow();
            }
            catch (Exception e)
            {
                Debug.LogError("popUp showing failed with exception: " + e);
                throw;
            }
        }

        protected async virtual Task OnShow()
        {
        }

        protected virtual async void Close()
        {
            Destroy(gameObject);
        }
    }
}
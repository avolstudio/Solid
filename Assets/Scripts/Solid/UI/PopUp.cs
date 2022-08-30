using System;
using Solid.Attributes;
using Solid.Core;
using Solid.Model;
using UnityEngine;
using UnityEngine.UI;

/*Copyright (c) Created by Oleksii Volovich 2021*/
namespace Solid.UI
{
    [ResourcePath("override path here")]
    public abstract class PopUp : Awaitable<PopUpInfo>
    {
        [SerializeField] protected Canvas _canvas;

        [SerializeField] protected Button _closeButton;

        public Button CloseButton => _closeButton;

        protected override void OnAwake(params object[] parameters)
        {
            Result = null;

            if (_closeButton != null)
            {
                _closeButton.onClick.AddListener(() => SetCompleteAndDestroy());
            }
        }

        protected void SetCompleteAndDestroy(bool isComplete = true)
        {
            SetComplete(isComplete);
                
            Destroy(gameObject);
        }
    }

    public class PopUpInfo:IModel<PopUpInfo>
    {
        private string _info;
        
        public string Info
        {
            get
            {
                return _info;
            }
            set
            {
                _info = value;
                
                ModelChanged?.Invoke(this);
            }
        }
        
        public bool isDirty { get; }
        public event Action<PopUpInfo> ModelChanged;
    }
}
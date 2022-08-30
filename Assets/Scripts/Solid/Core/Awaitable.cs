using System;
using UnityEngine;

/*Copyright (c) Created by Oleksii Volovich 2021*/
namespace Solid.Core
{
    public abstract class Awaitable<TResult> : SolidBehaviour
    {
        public TResult Result { get; protected set; }
        public event Action<TResult> Success;
        public event Action<TResult> Error;
        
        private bool _finishedWithSuccess;

        private bool _isCompleted;
        
        private async void Start() 
        {
            try
            {
                OnStart();
            }
            catch (Exception e)
            {
                Debug.LogError("Execution failed due to "+ e);
            }
        }

        public bool IsCompleted
        {
            get => _isCompleted;

            private set
            {
                _isCompleted = value;

                if (!_isCompleted)
                    return;

                if (_finishedWithSuccess)
                    Success?.Invoke(Result);
                else
                    Error?.Invoke(Result);

                OnFinish(_finishedWithSuccess);

                Destroy(this);
            }
        }

        public void Terminate(bool result)
        {
            SetComplete(result);
        }

        protected void SetComplete(bool finishedWithSuccess = true)
        {
            _finishedWithSuccess = finishedWithSuccess;

            IsCompleted = true;
        }
        
        protected abstract void OnStart();
        
        protected virtual void OnFinish(bool finishedWithSuccess)
        {
        }
    }
}
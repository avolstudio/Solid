using System;

/*Copyright (c) Created by Oleksii Volovich 2021*/
namespace Solid.Behaviours
{
    public abstract class Awaitable : SolidBehaviour
    {
        public event Action Finish;
        public event Action Error;

        private bool _isCompleted;
    
        private bool _finishedWithSuccess;
        public bool IsCompleted 
        {
            get => _isCompleted;
        
            private set
            {
                _isCompleted = value;

                if (!_isCompleted) 
                    return;
            
                if (_finishedWithSuccess)
                {
                    Finish?.Invoke();
                }
                else
                {
                    Error?.Invoke();
                }

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
    

        protected virtual void OnFinish(bool finishedWithSuccess)
        {
        
        }
    }
    
    public abstract class Awaitable<TResult> : Awaitable
    {
        public TResult Result { get; protected set; } 
    }
}

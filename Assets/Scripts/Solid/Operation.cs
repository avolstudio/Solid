using System;
using System.Runtime.CompilerServices;
using Solid.Behaviours;
using UnityEngine;
using Object = UnityEngine.Object;


/*Copyright (c) Created by Oleksii Volovich 2021*/
namespace Solid
{
    public class Operation : INotifyCompletion 
    {
        public bool DestroyContainerAfterExecution { get; }
        public bool LockThread { get; }
        public OperationStatus Status { get; protected set; }
        public object[] Parameters { get; }
        public GameObject Container { get;  }
        protected Awaitable _awaitableComponent { get; set; }
        public void GetResult() {
            if (!IsCompleted)
            {
                return;
            }
        }
        public bool IsCompleted => _awaitableComponent.IsCompleted;

        private Action _finishHandler, _errorHandler;

        private Action _continuation;

        private readonly Type _awaitableType;
    
        protected Operation(Type awaitableType,GameObject target = null,bool lockThread = true,bool destroyContainerAfterExecution = true,params object[] parameters)
        {
            _awaitableType = awaitableType;
            
            Container = target != null ? target :new GameObject();

            LockThread = lockThread;
        
            DestroyContainerAfterExecution = destroyContainerAfterExecution;

            Parameters = parameters;

            Status = OperationStatus.CreatedNotRunning;
        }

        private protected Operation()
        {
            
        }
        
        public void AddOnFinishHandler(Action handler)
        {
            _finishHandler += handler;
        } 
        public void RemoveOnFinishHandler(Action handler)
        {
            _finishHandler -= handler;
        } 
        public void AddOnErrorHandler(Action handler)
        {
            _errorHandler += handler;
        } 
        public void RemoveOnErrorHandler(Action handler)
        {
            _errorHandler -= handler;
        }

        public void Terminate(bool result,string message)
        {
            _awaitableComponent.Terminate(result);
        }

        public virtual void Run()
        {
            if (Status == OperationStatus.Running)
            {
                Debug.Log("Already running");
            
                return ;
            }
        
            _awaitableComponent = (Awaitable)SolidBehaviour.Add(_awaitableType,Container, Parameters);
        
            Status = OperationStatus.Running;
        }

        private void OnOperationFinished()
        {
            Status = OperationStatus.Finished;
        
            _finishHandler?.Invoke();

            DestroyAndContinue();
        }

        private void OnOperationError()
        {
            Status = OperationStatus.Error;
        
            _errorHandler?.Invoke();
            
            DestroyAndContinue();
        }

        private void DestroyAndContinue()
        {
            if (DestroyContainerAfterExecution) Object.Destroy(Container);
        
            _continuation?.Invoke();
        }
    
        void INotifyCompletion.OnCompleted(Action continuation)
        {
            _awaitableComponent.Error += OnOperationError;

            _awaitableComponent.Finished += OnOperationFinished;

            if (LockThread)
            {
                _continuation = continuation;
            }
            else
            {
                continuation?.Invoke();
            }
        }
        
        public Operation GetAwaiter () 
        {
            Run();

            return this;
        }

        public static Operation Create<TAwaitable>(GameObject target = null, bool lockThread = true,
            bool destroyContainerAfterExecution = true, params object[] parameters) where TAwaitable : Awaitable
        {
            return new Operation(typeof(TAwaitable),target,lockThread,destroyContainerAfterExecution,parameters);
        } 
        public static Operation<TResult> Create<TAwaitable,TResult>(GameObject target = null, bool lockThread = true,
            bool destroyContainerAfterExecution = true, params object[] parameters) where TAwaitable : Awaitable<TResult>
        {
            return new Operation<TResult>(typeof(TAwaitable),target,lockThread,destroyContainerAfterExecution,parameters);
        }
    }
    
    public class Operation<TResult> : Operation 
    {
        protected internal Operation(Type awaitableType, GameObject target = null, bool lockThread = true, bool destroyContainerAfterExecution = true, params object[] parameters) : base(awaitableType, target, lockThread, destroyContainerAfterExecution, parameters)
        {
        }

        public new TResult GetResult() => ((Awaitable<TResult>) _awaitableComponent).Result;
    }

    public enum OperationStatus
    {
        CreatedNotRunning,
        Running,
        Finished,
        Error
    }
}
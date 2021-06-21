using System;
using System.Runtime.CompilerServices;
using Solid.Behaviours;
using UnityEngine;
using Object = UnityEngine.Object;


/*Copyright (c) Created by Oleksii Volovich 2021*/
namespace Solid
{
    public class SolidOperation : INotifyCompletion 
    {
        public bool DestroyContainerAfterExecution { get; }
        public bool LockThread { get; }
        public OperationStatus Status { get; protected set; }
        public object[] Parameters { get; }
        public GameObject Container { get;  } 
        public AwaitableBehaviour Operation { get; protected set; }
        public void GetResult() {}
        public bool IsCompleted => Operation.IsCompleted;

        private Action _finishHandler, _errorHandler;

        private Action _continuation;

        private Type _operationType;
    
        public SolidOperation(Type operation,GameObject target = null,bool lockThread = true,bool destroyContainerAfterExecution = true,string namePostfix = "",params object[] parameters)
        {
            _operationType = operation;
            
            Container = target != null ? target :new GameObject();

            LockThread = lockThread;
        
            DestroyContainerAfterExecution = destroyContainerAfterExecution;
        
            Container.name = $"{operation.Name} + { namePostfix}";
        
            Parameters = parameters;
        
            Operation = default;

            Status = OperationStatus.CreatedNotRunning;
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
            Operation.Terminate(result);
        }

        public virtual void Run()
        {
            if (Status == OperationStatus.Running)
            {
                Debug.Log("Already running");
            
                return ;
            }
        
            Operation = (AwaitableBehaviour)SolidBehaviour.Add(_operationType,Container, Parameters);
        
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
            Operation.Error += OnOperationError;

            Operation.Finished += OnOperationFinished;

            if (LockThread)
            {
                _continuation = continuation;
            }
            else
            {
                continuation?.Invoke();
            }
        }
        
        public  SolidOperation GetAwaiter () 
        {
            Run();

            return this;
        }
    }
    
    public class SolidOperation<TOperation,TResult> : SolidOperation where TOperation:AwaitableBehaviour<TResult>
    {
        public TResult GetResult()
        {
            return ((TOperation) Operation).Result;
        }

        public SolidOperation(GameObject target = null,bool lockThread = true,bool destroyContainerAfterExecution = true,string namePostfix = "",params object[] parameters) : base(typeof(TOperation),target,lockThread,destroyContainerAfterExecution,namePostfix,parameters)
        {
        }
        
        public override void Run()
        {
            if (Status == OperationStatus.Running)
            {
                Debug.Log("Already running");
            
                return ;
            }
        
            Operation = SolidBehaviour.Add<TOperation>(Container, Parameters);
        
            Status = OperationStatus.Running;
        }
        
        public SolidOperation<TOperation,TResult> GetAwaiter ()
        {
            Run();

            return this;
        }
        
    }

    public enum OperationStatus
    {
        CreatedNotRunning,
        Running,
        Finished,
        Error
    }
}
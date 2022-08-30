using System;
using System.Runtime.CompilerServices;
using Solid.Examples;
using UnityEngine;
using Object = UnityEngine.Object;


/*Copyright (c) Created by Oleksii Volovich 2021*/
namespace Solid.Core
{
    public class Operation<TResult> :Operation,INotifyCompletion
    {
        public bool destroyContainerAfterExecution { get; protected set; } = true;
        public bool waitForFinish { get; protected set; }
        public OperationStatus status { get; protected set; }
        public bool IsCompleted => awaitable.IsCompleted;
        public GameObject container { get; protected set; }
        public Awaitable<TResult> awaitable { get; protected set; }
        protected object[] parameters { get; set; }

        private readonly Type _awaitableType;

        private Action _continuation;

        private Action<TResult> _finishHandler;
        
        private Action<TResult> _errorHandler;

        public Operation(Type awaitableType, GameObject target = null, bool waitForFinish = true,
            bool destroyContainerAfterExecution = true, params object[] parameters)
        {
            status = OperationStatus.CreatedNotRunning;
            
            _awaitableType = awaitableType;

            container = target != null ? target : new GameObject(awaitableType.Name);

            this.waitForFinish = waitForFinish;

            this.destroyContainerAfterExecution = destroyContainerAfterExecution;

            this.parameters = parameters;
        }

        protected Operation()
        {
            
        }
        
        public void OnCompleted(Action continuation)
        {
            awaitable.Error += OnOperationError;

            awaitable.Success += OnOperationSuccess;

            if (waitForFinish)
                _continuation = continuation;
            else
                continuation?.Invoke();
        }

        public TResult GetResult()
        {
            return awaitable.Result;
        }
        
        public Operation<TResult> GetAwaiter()
        {
            Run();

            return this;
        }

        public Operation<TResult> AddOnFinishHandler(Action<TResult> handler)
        {
            _finishHandler += handler;
            
            return this;
        }

        public void RemoveOnFinishHandler(Action<TResult> handler)
        {
            _finishHandler -= handler;
        }

        public Operation<TResult> AddOnErrorHandler(Action<TResult> handler)
        {
            _errorHandler += handler;

            return this;
        }

        public void RemoveOnErrorHandler(Action<TResult> handler)
        {
            _errorHandler -= handler;
        }

        public void Terminate(bool result)
        {
            awaitable.Terminate(result);
        }
        
        protected bool IsRunning()
        {
            return status == OperationStatus.Running;
        }
        
        protected virtual void Run()
        {
            if (IsRunning())
                return;

            awaitable = (Awaitable<TResult>) SolidBehaviour.Add(_awaitableType, container, parameters);

            status = OperationStatus.Running;
        }

        private void OnOperationSuccess(TResult result)
        {
            status = OperationStatus.FinishedWithSuccess;

            _finishHandler?.Invoke(result);

            DestroyAndContinue();
        }

        private void OnOperationError(TResult result)
        {
            status = OperationStatus.Error;

            _errorHandler?.Invoke(result);

            DestroyAndContinue();
        }

        private void DestroyAndContinue()
        {
            if (destroyContainerAfterExecution) 
                Object.Destroy(container);

            _continuation?.Invoke();
        }
    }

    public abstract class Operation
    {
        protected Operation()
        {
            
        }
        public static Operation<TResult> Run<TAwaitable,TResult>(GameObject target = null, bool waitForFinish = true,
            bool destroyContainerAfterExecution = true, params object[] parameters) where TAwaitable: Awaitable<TResult>
        {
            return new Operation<TResult>(typeof(TAwaitable), target, waitForFinish, destroyContainerAfterExecution, parameters);
        } 
        public static Operation<float> Timer(float time,GameObject target = null, bool waitForFinish = true,
            bool destroyContainerAfterExecution = true)
        {
            return Run<LerpFloat,float>(target, waitForFinish, destroyContainerAfterExecution,
                new object[] {0f, 1f, time});
        }

        public static InstantiateOperation<TResult,TPrefab> CreateFromPrefab<TResult,TPrefab>(GameObject target = null,
            bool waitForFinish = true,
            bool destroyContainerAfterExecution = true, params object[] parameters) where TPrefab : Awaitable<TResult>
        {
            return new InstantiateOperation<TResult,TPrefab>(target, waitForFinish, destroyContainerAfterExecution, parameters);
        }
    }
}
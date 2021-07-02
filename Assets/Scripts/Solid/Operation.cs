using System;
using System.Linq;
using System.Runtime.CompilerServices;
using Solid.Attributes;
using Solid.Behaviours;
using UnityEngine;
using Object = UnityEngine.Object;


/*Copyright (c) Created by Oleksii Volovich 2021*/
namespace Solid
{
    public class Operation : INotifyCompletion
    {
        protected Type _awaitableType;

        private Action _continuation;

        private Action _finishHandler, _errorHandler;

        protected Operation(Type awaitableType, GameObject target = null, bool lockThread = true,
            bool destroyContainerAfterExecution = true, params object[] parameters)
        {
            _awaitableType = awaitableType;

            Container = target != null ? target : new GameObject();

            LockThread = lockThread;

            DestroyContainerAfterExecution = destroyContainerAfterExecution;

            Parameters = parameters;

            Status = OperationStatus.CreatedNotRunning;
        }

        private protected Operation()
        {
        }

        public bool DestroyContainerAfterExecution { get; protected set; }
        public bool LockThread { get; protected set; }
        public OperationStatus Status { get; protected set; }
        public bool IsCompleted => Awaitable.IsCompleted;
        public GameObject Container { get; protected set; }
        protected object[] Parameters { get; set; }
        public Awaitable Awaitable { get; protected set; }

        void INotifyCompletion.OnCompleted(Action continuation)
        {
            Awaitable.Error += OnOperationError;

            Awaitable.Finish += OnOperationFinished;

            if (LockThread)
                _continuation = continuation;
            else
                continuation?.Invoke();
        }

        public void GetResult()
        {
        }


        public Operation GetAwaiter()
        {
            Run();

            return this;
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

        public void Terminate(bool result)
        {
            Awaitable.Terminate(result);
        }


        protected bool IsRunning()
        {
            return Status == OperationStatus.Running;
        }


        protected virtual void Run()
        {
            if (IsRunning())
                return;

            Awaitable = (Awaitable) SolidBehaviour.Add(_awaitableType, Container, Parameters);

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

        public static Operation Create<TAwaitable>(GameObject target = null, bool lockThread = true,
            bool destroyContainerAfterExecution = true, params object[] parameters) where TAwaitable : Awaitable
        {
            return new Operation(typeof(TAwaitable), target, lockThread, destroyContainerAfterExecution, parameters);
        }

        public static Operation<TResult> Create<TAwaitable, TResult>(GameObject target = null, bool lockThread = true,
            bool destroyContainerAfterExecution = true, params object[] parameters)
            where TAwaitable : Awaitable<TResult>
        {
            return new Operation<TResult>(typeof(TAwaitable), target, lockThread, destroyContainerAfterExecution,
                parameters);
        }

        public static InstantiatedOperation<TPrefab> CreateFromPrefab<TPrefab>(GameObject target = null,
            bool lockThread = true,
            bool destroyContainerAfterExecution = true, params object[] parameters) where TPrefab : Awaitable
        {
            return new InstantiatedOperation<TPrefab>(target, lockThread, destroyContainerAfterExecution, parameters);
        }
    }


    public class InstantiatedOperation<TPrefab> : Operation where TPrefab : Awaitable
    {
        protected internal InstantiatedOperation(GameObject parent = null, bool lockThread = true,
            bool destroyContainerAfterExecution = true, params object[] parameters)
        {
            Container = parent;

            LockThread = lockThread;

            DestroyContainerAfterExecution = destroyContainerAfterExecution;

            Parameters = parameters;

            Status = OperationStatus.CreatedNotRunning;
        }

        protected InstantiatedOperation()
        {
        }

        protected override void Run()
        {
            if (IsRunning())
                return;

            var path = GetPath();

            var prefab = Resources.Load<TPrefab>(path);

            Awaitable = SolidBehaviour.Instantiate(prefab, Container, Parameters);

            Status = OperationStatus.Running;
        }

        private string GetPath()
        {
            var pathAtt = (ResourcePath) typeof(TPrefab).GetCustomAttributes(true)
                .First(attribute => attribute is ResourcePath);

            if (pathAtt == null)
                throw new Exception("Path was attribute not specified");

            return pathAtt.Path;
        }
    }

    public class Operation<TResult> : Operation
    {
        protected internal Operation(Type awaitableType, GameObject target = null, bool lockThread = true,
            bool destroyContainerAfterExecution = true, params object[] parameters) : base(awaitableType, target,
            lockThread, destroyContainerAfterExecution, parameters)
        {
        }

        public new TResult GetResult()
        {
            return ((Awaitable<TResult>) Awaitable).Result;
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
using System;
using System.Linq;
using Solid.Attributes;
using UnityEngine;

/*Copyright (c) Created by Oleksii Volovich 2021*/
namespace Solid.Core
{
    public class InstantiateOperation<TResult,TPrefab> : Operation<TResult> where TPrefab : Awaitable<TResult>
    {
        public TPrefab Prefab => awaitable as TPrefab;
        
        public InstantiateOperation(GameObject parent = null, bool waitForFinish = true,
            bool destroyContainerAfterExecution = true, params object[] parameters)
        {
            container = parent;

            base.waitForFinish = waitForFinish;

            base.destroyContainerAfterExecution = destroyContainerAfterExecution;

            base.parameters = parameters;

            status = OperationStatus.CreatedNotRunning;
        }

        protected InstantiateOperation()
        {
        }

        protected override void Run()
        {
            if (IsRunning())
                return;

            var path = GetPath();

            var prefab = Resources.Load<TPrefab>(path);

            awaitable = SolidBehaviour.Instantiate(prefab, container.transform, parameters);

            status = OperationStatus.Running;
        }

        private string GetPath()
        {
            var attributes = typeof(TPrefab).GetCustomAttributes(false);

            if (attributes == null || attributes.Length == 0)
            {
                throw new Exception("No custom attributes applied to this type. Add ResourcePath att and try again");
            }

            var pathAtt = (ResourcePath) attributes.First(att => att is ResourcePath);
            
            if (pathAtt == null)
                throw new Exception("Path was attribute not specified");

            return pathAtt.Path;
        }
    }
}

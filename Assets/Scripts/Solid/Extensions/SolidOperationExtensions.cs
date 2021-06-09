using Solid.Behaviours;

/*Copyright (c) Created by Oleksii Volovich 2021*/
namespace Solid.Extensions
{
    public static class SolidOperationExtensions
    {
        public static SolidOperation<TOperation,TResult> GetAwaiter<TOperation,TResult>(this SolidOperation<TOperation,TResult> scheduledOperation) where TOperation:AwaitableBehaviour<TResult>
        {
            scheduledOperation.Run();

            return scheduledOperation;
        }
    }
}

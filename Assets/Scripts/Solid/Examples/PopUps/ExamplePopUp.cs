using Solid.Attributes;
using Solid.Core;
using Solid.UI;
using UnityEngine;

namespace Solid.Examples.PopUps
{
    [ResourcePath("PopUps/ExamplePopUp")]
    public class ExamplePopUp : PopUp
    {
        [SerializeField] private float showTime = 5f;
        protected override void OnAwake(params object[] parameters)
        {
            base.OnAwake(parameters);
            
            if (parameters.Length == 0) return;
            
            showTime = (float) parameters[0];
        }

        protected override async void OnStart()
        {
            await Operation.Timer(showTime,target:gameObject,destroyContainerAfterExecution:false);
            
            CloseAndDestroy();
        }
    }
}
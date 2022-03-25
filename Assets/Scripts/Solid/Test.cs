using Solid;
using Solid.Behaviours;
using UnityEngine;

public class Test : MonoBehaviour
{
    private async void Start()
    {
        
        Debug.Log("start thread blocking operation. Duration  - 3 second");
        await Operation.Create<LerpFloat>(parameters: new object[] {0f, 10f, 3f});
        Debug.Log("finishs thread blocking operation.");

        var operation = Operation.Create<LerpFloat, float>(lockThread: false, parameters: new object[] {0f, 10f, 3f});

        operation.AddOnFinishHandler(() => Debug.Log("finish non blocking operation"));

        await operation;

        Debug.Log("this operation will not block execution while awaiting for result");

        Debug.Log("so you can process result later");
    }
}
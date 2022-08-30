using Solid;
using Solid.Core;
using Solid.Examples;
using UnityEngine;

public class Test : MonoBehaviour
{
    private async void Start()
    {
        
        Debug.Log("start thread blocking operation. Duration  - 3 second");
        await Operation.Run<LerpFloat,float>(parameters: new object[] {0f, 10f, 3f});
        Debug.Log("finishs thread blocking operation.");

        var operation = Operation.Run<LerpFloat, float>(waitForFinish: false, parameters: new object[] {0f, 10f, 3f});

        operation.AddOnFinishHandler((x) => Debug.Log("finish non blocking operation"));

        await operation;

        Debug.Log("this operation will not block execution while awaiting for result");

        Debug.Log("so you can process result later");
    }
}
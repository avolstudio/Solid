using Solid;
using Solid.Behaviours;
using UnityEngine;

public class Test : MonoBehaviour
{
    private async void Start()
    {
        Debug.Log("Begin execution");

        await new SolidOperation(typeof(LerpFloat),parameters: new object[]{0f,10f,3f});
        
        await new SolidOperation<LerpFloat, float>(parameters: new object[]{0f,10f,3f});
        Debug.Log("continue after 3 seconds");
        
        await new SolidOperation<LerpFloat, float>(parameters: new object[]{0f,10f,3f});
        Debug.Log("continue after 3 seconds");
        
        var operation =  new  SolidOperation<LerpFloat,float>(lockThread:false,parameters: new object[]{0f,10f,3f});
        
        operation.AddOnFinishHandler( () => Debug.Log("finish non blocking operation"));
        
        await operation;
        
        Debug.Log("this operation will not block execution while awaiting for result");
        
        Debug.Log("so you can process result later");
    }


}

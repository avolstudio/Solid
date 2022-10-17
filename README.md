# Solid
Lightweight framework which provide awaitable and parameterized MonoBehaviour, and Model-View pair

Look at the script named Test. Create new scene, add Test to any GameObject and run. Test have next lines in Start method:

    
    Debug.Log("Begin execution");
    
    await new SolidOperation<LerpFloat, float>(parameters: new object[]{0f,10f,3f});
    Debug.Log("continue after 3 seconds");
    
    await new SolidOperation<LerpFloat, float>(parameters: new object[]{0f,10f,3f});
    Debug.Log("continue after 3 seconds");
    
    var operation =  new  SolidOperation<LerpFloat,float>(lockThread:false,parameters: new object[]{0f,10f,3f});
    
    operation.AddOnFinishHandler( () => Debug.Log("finish non blocking operation"));
    
    await operation;
    
    Debug.Log("this operation will not block execution while awaiting for result");
    
    Debug.Log("so you can process result later");
    
    
All this code runs in main thread but provide you async/await approach.
Hope it will be helpful for your projects!

For any questions contact me on avolstudio@gmail.com

# Solid
Lightweight framework which provide awaitable and parameterized MonoBehaviour, and Model-View pair.
Load Demo scene and run

    
        Debug.Log("Run and wait for timer");
        await Operation.Timer(3f, gameObject, true, false); // shortcut for running LerpFloat operation
        
        Debug.Log("Show popUp and wait for close");
        await _uiManager.ShowPopUp<ExamplePopUp>();

        Debug.Log("Play audio file and wait for finish");
        await Operation.Run<PlayAudio,bool>(parameters:_clip);
        
        Debug.Log("Make get request and wait for result");
        var responseMessage = await Operation.Run<HTTPGetRequest,HttpResponseMessage>(parameters:"https://en.wikipedia.org/wiki/Main_Page");
        
        Debug.Log("result " + responseMessage);
    
    
All this code runs in main thread but provide you async/await approach.
Hope it will be helpful for your projects!

For any questions contact me on avolstudio@gmail.com

using System.Net.Http;
using Solid.Core;
using Solid.Examples;
using Solid.Examples.PopUps;
using Solid.UI;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private AudioClip _clip;
    
    private UIManager _uiManager;
    
    private void Awake()
    {
        var canvas = FindObjectOfType<Canvas>();

        _uiManager = new UIManager(canvas);
    }

    private async void Start()
    {
        Debug.Log("Run and wait for timer");
        await Operation.Timer(3f, gameObject, true, false); // shortcut for running LerpFloat operation
        
        Debug.Log("Show popUp and wait for close");
        await _uiManager.ShowPopUp<ExamplePopUp>();

        Debug.Log("Play audio file and wait for finish");
        await Operation.Run<PlayAudio,bool>(parameters:_clip);
        
        Debug.Log("Make get request and wait for result");
        var responseMessage = await Operation.Run<HTTPGetRequest,HttpResponseMessage>(parameters:"https://en.wikipedia.org/wiki/Main_Page");
        
        Debug.Log("result " + responseMessage);
    }
}
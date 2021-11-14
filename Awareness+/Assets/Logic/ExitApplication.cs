using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ExitApplication : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void closeApplication()
    {
        StartCoroutine(closeApplicationExecute());
    }

    public IEnumerator closeApplicationExecute()
    {
        UnityWebRequest www = UnityWebRequest.Get("https://hololenscommunicationserviceawareness.azurewebsites.net/api/hololens-communication-service/disconnect-hololens");
        UnityWebRequest www2 = UnityWebRequest.Get("https://awarenessmonitorscomunication.azurewebsites.net/disconnect");
        www.timeout = 1;
        yield return www2.SendWebRequest();
        if (www.result != UnityWebRequest.Result.Success)
        {
            
            Debug.Log(www.error);
        }
        yield return www.SendWebRequest();
        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        Application.Quit();
    }
}


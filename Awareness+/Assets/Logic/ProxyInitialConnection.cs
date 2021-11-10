using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ProxyInitialConnection : MonoBehaviour
{

    bool connected = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EstablishConnection()
    {
        StartCoroutine(CheckStatus());
    }


    public IEnumerator CheckStatus()
    {
        if (connected)
        {
            UnityWebRequest www = UnityWebRequest.Get("https://hololenscommunicationserviceawareness.azurewebsites.net/api/hololens-communication-service/disconnect-hololens");
            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            connected = false;
        }
        else
        {
            UnityWebRequest www = UnityWebRequest.Get("https://hololenscommunicationserviceawareness.azurewebsites.net/api/hololens-communication-service/connect-hololens");
            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            connected = true;

        }
        
        
    }
}

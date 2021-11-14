using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class DisconnectMonitor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void disconnectMonitor()
    {
        StartCoroutine(disconnectMonitorFunction());
    }


    public IEnumerator disconnectMonitorFunction()
    {
        UnityWebRequest www = UnityWebRequest.Get("https://awarenessmonitorscomunication.azurewebsites.net/disconnect");
        www.timeout = 1;
        yield return www.SendWebRequest();
        if (www.result != UnityWebRequest.Result.Success)
        {
            SceneManager.LoadScene("NoConnectionToMonitor");
            Debug.Log(www.error);
        }

    }
    }

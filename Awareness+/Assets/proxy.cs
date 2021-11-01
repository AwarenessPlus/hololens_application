using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class proxy : MonoBehaviour
{
    // Start is called before the first frame update

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Status()
    {
        StartCoroutine(CheckStatus());
    }


    public IEnumerator CheckStatus() {
        UnityWebRequest www = UnityWebRequest.Get("https://hololenscommunicationserviceawareness.azurewebsites.net/api/hololens-communication-service/obtain-procedure-data");
        yield return www.SendWebRequest();
        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            String data = www.downloadHandler.text.ToString();
            int idProcedure = Convert.ToInt32(data);
            if (idProcedure == 0)
            {
                SceneManager.LoadScene("PrincipalMenu");
            }
            else {
                SceneManager.LoadScene("FirstScene");
               
            }

            

        }
    }
}

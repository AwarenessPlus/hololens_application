using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;



public class GetVitalSigns : MonoBehaviour
{
    public  Text myText;
    public Image myImage;
    public GameObject warningImage;
    private static string baseUrl = "https://awarenessmonitorscomunication.azurewebsites.net/";

     float secondsCounter=0;
    float secondsToCount=0.1F;
    string functionName= "GetTextureSaturation";


    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        secondsCounter += Time.deltaTime;
      if (secondsCounter >= secondsToCount)
      {
         secondsCounter=0;
         StartCoroutine(functionName);
      }


    }

    public void setSaturation() {
        functionName = "GetTextureSaturation";
    }
    public void setFrecuency()
    {
        functionName = "GetTextureCardiactFrecuency";
    }
    public void setPreasure()
    {
        functionName = "GetTextureNonInvasiveBloodPreasure";
    }




    IEnumerator GetTextureSaturation() {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(baseUrl + "saturation");
        www.timeout = 5;
        yield return www.SendWebRequest();
        if (www.result != UnityWebRequest.Result.Success) {
            Debug.Log(www.error);
            warningImage.SetActive(true);
        }
        else {
            warningImage.SetActive(false);
            Texture2D myTexture2D = (Texture2D)((DownloadHandlerTexture)www.downloadHandler).texture;
            Sprite mySprite = Sprite.Create(myTexture2D, new Rect(0.0f, 0.0f, myTexture2D.width, myTexture2D.height), new Vector2(0.5f, 0.5f), 100.0f);
            myImage.sprite = mySprite;
        }   
    }

    IEnumerator GetTextureCardiactFrecuency()
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(baseUrl + "cardiac-frecuency");
        www.timeout = 5;
        yield return www.SendWebRequest();
        if (www.result != UnityWebRequest.Result.Success)
        {
            warningImage.SetActive(true);
            Debug.Log(www.error);
        }
        else
        {
            warningImage.SetActive(false);
            Texture2D myTexture2D = (Texture2D)((DownloadHandlerTexture)www.downloadHandler).texture;
            Sprite mySprite = Sprite.Create(myTexture2D, new Rect(0.0f, 0.0f, myTexture2D.width, myTexture2D.height), new Vector2(0.5f, 0.5f), 100.0f);
            myImage.sprite = mySprite;
        }
    }

    IEnumerator GetTextureNonInvasiveBloodPreasure()
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(baseUrl + "non-invasive-blood-presure");
        www.timeout = 5;
        yield return www.SendWebRequest();
        if (www.result != UnityWebRequest.Result.Success)
        {
            warningImage.SetActive(true);
            Debug.Log(www.error);
        }
        else
        {
            warningImage.SetActive(false);
            Texture2D myTexture2D = (Texture2D)((DownloadHandlerTexture)www.downloadHandler).texture;
            Sprite mySprite = Sprite.Create(myTexture2D, new Rect(0.0f, 0.0f, myTexture2D.width, myTexture2D.height), new Vector2(0.5f, 0.5f), 100.0f);
            myImage.sprite = mySprite;
        }
    }

}

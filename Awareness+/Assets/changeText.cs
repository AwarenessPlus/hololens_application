using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Linq;
using UnityEngine.Windows.WebCam;
using System.IO;

public class changeText : MonoBehaviour
{
    public  Text myText;
    public Image myImage;
    int textNumber =0;
    int counter=0;
     float secondsCounter=0;
    float secondsToCount=0.25F;
    string functionName= "GetTextureSaturation";
    static readonly float MaxRecordingTime = 5.0f;

    VideoCapture m_VideoCapture = null;
    float m_stopRecordingTimer = float.MaxValue;



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

        if (m_VideoCapture == null || !m_VideoCapture.IsRecording)
        {
            return;
        }

        if (Time.time > m_stopRecordingTimer)
        {
            string[] lines =
        {
            "First line", "Second line", "Third line"
        };

            File.WriteAllLines(@"U: \\Users\marlo\Downloads\DummyFile.txt", lines);
            m_VideoCapture.StopRecordingAsync(OnStoppedRecordingVideo);
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
        UnityWebRequest www = UnityWebRequestTexture.GetTexture("http://10.195.23.82/AwarenessPlus/MonitorService/saturation");
        yield return www.SendWebRequest();
        if (www.result != UnityWebRequest.Result.Success) {
            Debug.Log(www.error);
        }
        else {
            Texture2D myTexture2D = (Texture2D)((DownloadHandlerTexture)www.downloadHandler).texture;
            Sprite mySprite = Sprite.Create(myTexture2D, new Rect(0.0f, 0.0f, myTexture2D.width, myTexture2D.height), new Vector2(0.5f, 0.5f), 100.0f);
            myImage.sprite = mySprite;
        }   
    }

    IEnumerator GetTextureCardiactFrecuency()
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture("http://10.195.23.82/AwarenessPlus/MonitorService/Cardiac-frecunecy");
        yield return www.SendWebRequest();
        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Texture2D myTexture2D = (Texture2D)((DownloadHandlerTexture)www.downloadHandler).texture;
            Sprite mySprite = Sprite.Create(myTexture2D, new Rect(0.0f, 0.0f, myTexture2D.width, myTexture2D.height), new Vector2(0.5f, 0.5f), 100.0f);
            myImage.sprite = mySprite;
        }
    }

    IEnumerator GetTextureNonInvasiveBloodPreasure()
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture("http://10.195.23.82/AwarenessPlus/MonitorService/non-invasive-blood-presure");
        yield return www.SendWebRequest();
        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Texture2D myTexture2D = (Texture2D)((DownloadHandlerTexture)www.downloadHandler).texture;
            Sprite mySprite = Sprite.Create(myTexture2D, new Rect(0.0f, 0.0f, myTexture2D.width, myTexture2D.height), new Vector2(0.5f, 0.5f), 100.0f);
            myImage.sprite = mySprite;
        }
    }

    void StartVideoCaptureTest()
    {

        Resolution cameraResolution = VideoCapture.SupportedResolutions.OrderByDescending((res) => res.width * res.height).First();
        Debug.Log(cameraResolution);

        float cameraFramerate = VideoCapture.GetSupportedFrameRatesForResolution(cameraResolution).OrderByDescending((fps) => fps).First();
        Debug.Log(cameraFramerate);

        VideoCapture.CreateAsync(false, delegate (VideoCapture videoCapture)
        {
            if (videoCapture != null)
            {
                m_VideoCapture = videoCapture;
                Debug.Log("Created VideoCapture Instance!");

                CameraParameters cameraParameters = new CameraParameters();
                cameraParameters.hologramOpacity = 0.0f;
                cameraParameters.frameRate = cameraFramerate;
                cameraParameters.cameraResolutionWidth = cameraResolution.width;
                cameraParameters.cameraResolutionHeight = cameraResolution.height;
                cameraParameters.pixelFormat = CapturePixelFormat.BGRA32;

                m_VideoCapture.StartVideoModeAsync(cameraParameters,
                                                   VideoCapture.AudioState.ApplicationAndMicAudio,
                                                   OnStartedVideoCaptureMode);
            }
            else
            {
                Debug.LogError("Failed to create VideoCapture Instance!");
            }
        });
    }

    void OnStartedVideoCaptureMode(VideoCapture.VideoCaptureResult result)
    {
        Debug.Log("Started Video Capture Mode!");
        string timeStamp = Time.time.ToString().Replace(".", "").Replace(":", "");
        string filename = string.Format("TestVideo_{0}.mp4", timeStamp);
        string filepath = System.IO.Path.Combine("U: /Users/marlo/Videos/", filename);
        filepath = filepath.Replace("/", @"\");
        m_VideoCapture.StartRecordingAsync(@"U: \\Users\marlo\Downloads\TestVideo.mp4", OnStartedRecordingVideo);

        string timeStamp2 = Time.time.ToString().Replace(".", "").Replace(":", "");
        string filename2 = string.Format("WriteLines.txt", timeStamp);
        string filepath2 = System.IO.Path.Combine("U: /Users/marlo/Videos/", filename);
        filepath2 = filepath2.Replace("/", @"\");

        string[] lines =
        {
            "First line", "Second line", "Third line" 
        };

        File.WriteAllLines(@"U: \\Users\marlo\Downloads\DummyFile.txt", lines);
    }

    void OnStoppedVideoCaptureMode(VideoCapture.VideoCaptureResult result)
    {
        Debug.Log("Stopped Video Capture Mode!");
    }

    void OnStartedRecordingVideo(VideoCapture.VideoCaptureResult result)
    {
        Debug.Log("Started Recording Video!");
        m_stopRecordingTimer = Time.time + MaxRecordingTime;
    }

    void OnStoppedRecordingVideo(VideoCapture.VideoCaptureResult result)
    {
        Debug.Log("Stopped Recording Video!");
        m_VideoCapture.StopVideoModeAsync(OnStoppedVideoCaptureMode);
    }

}

using System.Collections;
using System.IO;
using System.Net;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class PictureManager : MonoBehaviour
{
    public static PictureManager instance = null;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void pictureClick()
    {
        StartCoroutine(request());
 
    }

    public IEnumerator request()
    {
        string json = getJsonRequest(getImage64());
        UnityWebRequest www = UnityWebRequest.Post("https://vision.googleapis.com/v1/images:annotate?key=AIzaSyB3CYJqXrWOnMqvzrgawkvR44dX4Z5iAF4", json);
        byte[] bytes = Encoding.UTF8.GetBytes(json);
        UploadHandlerRaw uH = new UploadHandlerRaw(bytes);
        uH.contentType = "application/json";
        www.uploadHandler = uH;
        {
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
            }
        }
    }

    public string getImage64()
    {
        string path = "Assets/Level/3.jpg";
        byte[] fileData =  File.ReadAllBytes(path);
        return System.Convert.ToBase64String(fileData);
    }

    public string getJsonRequest(string imageBase64)
    {
        return "{ \"requests\":[{\"image\":{\"content\":\"" + imageBase64 + "\"},\"features\":[{\"type\":\"DOCUMENT_TEXT_DETECTION\",\"maxResults\":1}]}]}";
    }

}


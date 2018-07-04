using System.Collections;
using System.IO;
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
        request();
 
    }

    public void request()
    {
        JsonRequest jsonRequest = new JsonRequest(getImage64(), new Features("LABEL_DETECTION", 1));
        string json = JsonUtility.ToJson(jsonRequest);
        UnityWebRequest www = UnityWebRequest.Post("https://vision.googleapis.com/v1/images:annotate?key=AIzaSyB3CYJqXrWOnMqvzrgawkvR44dX4Z5iAF4", json);
        {
            //yield return www.SendWebRequest();
            UnityWebRequestAsyncOperation teste = www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                Debug.Log(teste.);
            }
        }
    }

    public string getImage64()
    {
        string path = "Assets/Level/3.jpg";
        byte[] fileData =  File.ReadAllBytes(path);
        return System.Convert.ToBase64String(fileData);
    }



    //    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(String.Format("http://api.openweathermap.org/data/2.5/weather?id={0}&APPID={1}", CityId, API_KEY));
    //    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
    //    StreamReader reader = new StreamReader(response.GetResponseStream());
    //    string jsonResponse = reader.ReadToEnd();
    //    WeatherInfo info = JsonUtility.FromJson<WeatherInfo>(jsonResponse);
    //    return info;
}


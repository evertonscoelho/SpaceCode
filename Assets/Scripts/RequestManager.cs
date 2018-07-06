using System.Collections;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public static class RequestManager{

    public static IEnumerator request()
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
                if (www.responseCode == 200)
                {
                    JsonResponse jsonResponse = JsonUtility.FromJson<JsonResponse>(www.downloadHandler.text);
                    string response = jsonResponse.responses[0].textAnnotations[0].description;
                                        
                }
            }
        }
    }

    private static string getImage64()
    {
        string path = "Assets/Level/3.jpg";
        byte[] fileData = File.ReadAllBytes(path);
        return System.Convert.ToBase64String(fileData);
    }

    private static string getJsonRequest(string imageBase64)
    {
        return "{ \"requests\":[{\"image\":{\"content\":\"" + imageBase64 + "\"},\"features\":[{\"type\":\"DOCUMENT_TEXT_DETECTION\",\"maxResults\":1}]}]}";
    }

}

[System.Serializable]
public class JsonResponse
{
    public Response[] responses;
}

[System.Serializable]
public class Response
{
    public TextAnnotations[] textAnnotations;
    public FullTextAnnotation fullTextAnnotation;
}

[System.Serializable]
public class TextAnnotations
{
    public string locale;
    public string description;
    public Object boundingPoly; 
}

[System.Serializable]
public class FullTextAnnotation
{
    Object[] pages;
}


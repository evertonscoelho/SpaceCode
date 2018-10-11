using System;
using System.Collections;
using System.Text;
using UnityEngine.Networking;

public class RequestManager{

    public static IEnumerator Request(byte[] bytes)
    {
        string json = getJsonRequest(getImage64(bytes));
        UnityWebRequest www = UnityWebRequest.Post("http://ec2-18-219-28-2.us-east-2.compute.amazonaws.com", json);
        byte[] bytesRequest = Encoding.UTF8.GetBytes(json);
        UploadHandlerRaw uH = new UploadHandlerRaw(bytesRequest);
        uH.contentType = "application/json";
        www.uploadHandler = uH;
        {
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError || www.responseCode != 200)
            {
                RecognizeCommandManager.response(Messages.ERRO_SERVIDOR + www.responseCode + " " + www.error  ,  true);
            }
            else
            {
                RecognizeCommandManager.response(www.downloadHandler.text, false);
            }
        }
    }

    private static string getImage64(byte[] data)
    {
        return System.Convert.ToBase64String(data);
    }

    private static string getJsonRequest(string imageBase64)
    {
        return "{\"image\": \"" + imageBase64 + "\"}";
    }
}
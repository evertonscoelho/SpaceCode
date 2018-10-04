using System;

public class RequestManager{

    public static String request(byte[] bytes)
    {
        //return "UNKNOW";
        //return "A,Left,Right,Down,NEXT,B,Right,Up,Left,Up,Down,NEXT,C,B,B,NEXT,A";
        return "A,UP,UP,UP,B,NEXT,B,UP,LEFT,Left,Up,Down,NEXT,C,B,B";
        /*
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
                    Debug.Log(www.downloadHandler.text);
                    Debug.Log(response);
                    convertToCommand(response);                    
                }
            }
        }
        */
    }

    

    private static string getImage64(byte[] data)
    {
        return System.Convert.ToBase64String(data);
    }

    private static string getJsonRequest(string imageBase64)
    {
        return "{ \"requests\":[{\"image\":{\"content\":\"" + imageBase64 + "\"},\"features\":[{\"type\":\"DOCUMENT_TEXT_DETECTION\",\"maxResults\":1}]}]}";
    }

}
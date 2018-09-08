using System;
using System.Collections;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class RequestManager{

    public String request()
    {
        return "A,Left,Right,Down,NEXT,B,Right,Up,Left,Up,Down,NEXT,C,B,B";
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

    private static void convertToCommand(string response)
    {
        /*
            switch (response)
            {
                case "PIS":
                    return Up;
                case "VRO":
                    return Up;
                case "CIR":
                    return Up;
                case "BRA":
                    return Down;
                case "LI":
                    return Down;
                case "LA":
                    return Left;
                case "PO":
                    return Left;
                case "DO":
                    return Right;
                case "MO":
                    return Right;
                case "RA":
                    return F1;
                case "CA":
                    return F2;
                case "TO":
                    return F3;
                default:
                    return null;
            }
            
        }
        */
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
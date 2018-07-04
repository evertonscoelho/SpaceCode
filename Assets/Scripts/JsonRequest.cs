using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class JsonRequest
{
    public string image;
    public Features features;

    public JsonRequest(string image, Features features)  
    {
            this.image = image;
            this.features = features;
    }
}

[System.Serializable]
public class Features
{
    public string data;
    public int maxResults;

    public Features(string data, int maxResults)
    {
        this.data = data;
        this.maxResults = maxResults;
    }
}

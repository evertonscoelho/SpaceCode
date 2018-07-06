using UnityEngine;

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
        StartCoroutine(RequestManager.request()); 
    }
}


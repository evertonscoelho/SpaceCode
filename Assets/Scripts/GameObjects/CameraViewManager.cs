using UnityEngine;
using UnityEngine.UI;

public class CameraViewManager : UnityEngine.MonoBehaviour
{
    public Button takePicture;
    public GameObject loadingText;

    void Start () {
        deactivate();
        RecognizeCommandManager.instance.setCameraViewManager(this);
    }

    public void active()
    {
        gameObject.SetActive(true);
        loadingText.SetActive(false);
        takePicture.interactable = true;
    }

    public void deactivate()
    {
        gameObject.SetActive(false);
    }

    public void loading()
    {
        loadingText.SetActive(true);
        takePicture.interactable = false;
    }
}

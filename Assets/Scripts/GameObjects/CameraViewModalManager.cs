using UnityEngine;
using UnityEngine.UI;

public class CameraViewModalManager : UnityEngine.MonoBehaviour
{
    public Button takePicture;
    public Text loadingText;

    void Start () {
        deactivate();
        RecognizeCommandManager.instance.setCameraViewManager(this);
        loadingText.text = Messages.LABEL_CARREGANDO;
    }

    public void active()
    {
        gameObject.SetActive(true);
        loadingText.gameObject.SetActive(false);
        takePicture.interactable = true;
    }

    public void deactivate()
    {
        gameObject.SetActive(false);
    }

    public void loading()
    {
        loadingText.gameObject.SetActive(true);
        takePicture.interactable = false;
    }
}

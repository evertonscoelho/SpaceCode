using UnityEngine;
using UnityEngine.UI;

public class LoaderLevel : MonoBehaviour
{
    public Button picture;
    public Button back;
    public Button help;
    public Button sound;

    void Awake()
    {
       GameManager.instance.setupSceneLevel(this);
    }
    
    public void deactivateButtons()
    {
        picture.interactable = false;
        back.interactable = false;
        help.interactable = false;
        sound.interactable = false;
    }
}
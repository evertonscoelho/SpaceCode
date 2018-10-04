using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class LoaderLevel : MonoBehaviour
{
    public GameObject picture;
    public GameObject back;
    public GameObject help;
    public GameObject sound;

    void Awake()
    {
       GameManager.instance.setupSceneLevel(this);
    }

    public void activateButtons()
    {
        picture.GetComponent<Button>().SetEnabled(true);
        back.GetComponent<Button>().SetEnabled(true);
        help.GetComponent<Button>().SetEnabled(true);
        sound.GetComponent<Button>().SetEnabled(true);
    }

    public void deactivateButtons()
    {
        picture.GetComponent<Button>().SetEnabled(false);
        back.GetComponent<Button>().SetEnabled(false);
        help.GetComponent<Button>().SetEnabled(false);
        sound.GetComponent<Button>().SetEnabled(false);
    }
}
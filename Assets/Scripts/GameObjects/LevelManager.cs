using UnityEngine;
using UnityEngine.UI;

public class LevelManager: MonoBehaviour
{
    public Button picture, back, help, sound;
    public Text TextCommands;

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

    public void setTextCommands(string commands)
    {
        TextCommands.text = commands;
    }
}
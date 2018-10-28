using UnityEngine;
using UnityEngine.UI;

public class LevelManager: MonoBehaviour
{
    public Button picture, back, help, sound;
    public Text TextCommands, TextTitleCommands, TextLoading;

    void Start()
    {
        GameManager.instance.setupSceneLevel(this);
        TextTitleCommands.text = Messages.TITULO_COMANDOS_RESTANTES;
        TextLoading.text = Messages.CARREGANDO;
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
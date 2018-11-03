using UnityEngine;
using UnityEngine.UI;

public class LevelManager: MonoBehaviour
{
    public Button picture, back, help, sound;
    public Text TextCommands, TextTitleCommands, TextLoading, TextTitleCommandsUse, TextCommandsUse;

    void Start()
    {
        GameManager.instance.setupSceneLevel(this);
        TextTitleCommands.text = Messages.LABEL_COMANDOS_RESTANTES;
        TextTitleCommandsUse.text = Messages.LABEL_COMANDOS_USO;
        TextLoading.text = Messages.LABEL_CARREGANDO;
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

    public void setTextCommandsUse(int commands)
    {
        TextCommandsUse.text = commands.ToString();
    }
}
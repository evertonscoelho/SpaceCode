using UnityEngine;
using UnityEngine.UI;

public class LevelManager: MonoBehaviour
{
    public Button picture, back, help, sound;
    public Text TextCommands, TextLoading, TextTitle, TextTitleBoardCommand;

    void Start()
    {
        GameManager.instance.setupSceneLevel(this);
        TextLoading.text = Messages.LABEL_CARREGANDO;
        TextTitleBoardCommand.text = Messages.TITULO_BOARD_COMANDOS;
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

    public void setTitle(string level)
    {
        TextTitle.text = string.Format(Messages.TITULO_TELA_FASES, level);
    }
}
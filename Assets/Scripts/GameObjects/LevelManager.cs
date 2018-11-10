using UnityEngine;
using UnityEngine.UI;

public class LevelManager: MonoBehaviour
{
    public Button picture, help, sound;
    public Text TextCommands, TextLoading, TextTitle, TextTitleBoardCommand;

    void Start()
    {
        GameManager gameManager = GameManager.instance;
        gameManager.setupSceneLevel(this);
        TextLoading.text = gameManager.messages.getLabelCarregando();
        TextTitleBoardCommand.text = gameManager.messages.getTituloBoardComandos();
    }
    
    public void deactivateButtons()
    {
        picture.interactable = false;
        help.interactable = false;
        sound.interactable = false;
    }

    public void setTextCommands(string commands)
    {
        TextCommands.text = commands;
    }

    public void setTitle(string level)
    {
        TextTitle.text = string.Format(GameManager.instance.messages.getTituloTelaFases(), level);
    }
}
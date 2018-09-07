using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.Experimental.UIElements;

public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;

    private BoardManager boardScript;
    private String levelId;

    public BoardCommandManager boardComamandManager;
    private ModalPanel modalPanelHelp;
    private ModalPanel modalPanelEndGame;
    private ModalPanel modalPanelCommands;
    private Function[] functions;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        boardScript = GetComponent<BoardManager>();

    }

    public void soundClick()
    {
        SoundManager.instance.soundClick();
    }

    public void pictureClick()
    {
        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.functions = PictureManager.instance.pictureClick();
        gameManager.modalPanelCommands.setCommands(gameManager.functions, gameManager.boardComamandManager);
        gameManager.modalPanelCommands.showModal(true, null);
    }

    public void functionsCorrect()
    {
        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.modalPanelCommands.showModal(false, null);
        gameManager.boardComamandManager.doCommands(gameManager.functions);
    }

    public void functionsWrong()
    {
        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.modalPanelCommands.showModal(false, null);
        gameManager.pictureClick();
    }

    public void loadLevel(String id)
    {
        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (id.Equals("-1")) {
            int newLevel = Int32.Parse(gameManager.levelId) + 1;
            gameManager.levelId = newLevel.ToString();
        }
        else
        {
            gameManager.levelId = id;
        }
        gameManager.loadScene(2);
    }

    public void setupSceneLevel()
    {
        boardScript.SetupScene(levelId);
        BoardCommandManager boardCommand = GameObject.Find("BoardCommand").GetComponent<BoardCommandManager>();
        boardCommand.initValues();
    }

    public void loadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public Boolean checkEndGameCommand()
    {
        StatusGame status = boardScript.checkEndGame(1, 0);
        if (status.Equals(StatusGame.DEFEAT))
        {
            doDefeat();
            return true;
        }
        return false;
    }

    public void clickHelpMainScene(Boolean active)
    {
        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.modalPanelHelp.showModal(active, null);
    }

    public void checkEndGameCollectable(Vector2 positionCollectable)
    {
        StatusGame status = boardScript.checkEndGame(0, 1);
        if (status.Equals(StatusGame.VICTORY))
        {
            StartCoroutine(doVictory(positionCollectable));
        }
    }

    public void doDefeat()
    {
        boardComamandManager.StopAllCoroutines();
        modalPanelEndGame.showModal(true, "Perdeu :/");
        modalPanelEndGame.interactableButtonNext(false);
    }

    private IEnumerator doVictory(Vector2 positionCollectable)
    {
        boardComamandManager.StopAllCoroutines();
        yield return StartCoroutine(boardComamandManager.terminateMovement(positionCollectable));
        modalPanelEndGame.showModal(true, "Ganhou :)");
        modalPanelEndGame.interactableButtonNext(true);
    }

    public void setModalPanelHelp(ModalPanel modalPanel)
    {
        this.modalPanelHelp = modalPanel;
    }

    public void setModalPanelEndGame(ModalPanel modalPanel)
    {
        this.modalPanelEndGame = modalPanel;
    }

    public void setModalPanelCommands(ModalPanel modalPanel)
    {
        this.modalPanelCommands = modalPanel;
    }
    
}
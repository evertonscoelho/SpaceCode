using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;

    private BoardManager boardScript;
    private LoaderLevel loaderLevel;
    private String levelId;

    public BoardCommandManager boardComamandManager;
    public ModalPanelManager ModalPanelManager;

    private List<Function> functions;

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
        PictureManager.instance.pictureClick();      
    }

    public void takePictureClick()
    {
        GameManager gameManager = GameManager.instance;
        try
        {
            gameManager.functions = PictureManager.instance.takePictureClick();
            gameManager.ModalPanelManager.setCommands(gameManager.functions, gameManager.boardComamandManager);
            gameManager.ModalPanelManager.activeModal(true, "", false, false, false, true);
            gameManager.ModalPanelManager.setTitleCommands(Messages.TITULO_PAINEL_COMANDOS);
        }
        catch (System.InvalidOperationException e)
        {
            gameManager.ModalPanelManager.activeModal(true, Messages.TITULO_PAINEL_ERRO, false, false, true, false);
            gameManager.ModalPanelManager.setDescriptionError(e.Message);
        }
    }

    public void functionsCorrect()
    {
        GameManager gameManager = GameManager.instance;
        gameManager.ModalPanelManager.deactiveModal();
        gameManager.boardComamandManager.doCommands(gameManager.functions);
        gameManager.loaderLevel.deactivateButtons();
    }

    public void functionsWrong()
    {
        GameManager gameManager = GameManager.instance;
        gameManager.ModalPanelManager.deactiveModal();
        gameManager.pictureClick();
    }

    public void loadLevel(String id)
    {
        GameManager gameManager = GameManager.instance;
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

    public void setupSceneLevel(LoaderLevel loaderLevel)
    {
        this.loaderLevel = loaderLevel;
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

    public void clickHelp(Boolean mainScene)
    {
        GameManager gameManager = GameManager.instance;
        gameManager.ModalPanelManager.activeModal(true, Messages.TITULO_PAINEL_AJUDA, true, false, false, false);
        if (mainScene)
        {
            gameManager.ModalPanelManager.setDescriptionHelp(Messages.DESCRICAO_AJUDA_SOBRE_JOGO);
        }
        else
        {
            gameManager.ModalPanelManager.setDescriptionHelp(Messages.DESCRICAO_AJUDA_SOBRE_FASE);
        }
    }

    public void clickCloseModal()
    {
        GameManager.instance.ModalPanelManager.deactiveModal();
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
        ModalPanelManager.activeModal(true, Messages.TITULO_PAINEL_FIM_JOGO_DERROTA, false, true, false, false);
        ModalPanelManager.interactableButtonNext(false);
    }

    private IEnumerator doVictory(Vector2 positionCollectable)
    {
        boardComamandManager.StopAllCoroutines();
        yield return StartCoroutine(boardComamandManager.terminateMovement(positionCollectable));
        ModalPanelManager.activeModal(true, Messages.TITULO_PAINEL_FIM_JOGO_VITORIA, false, true, false, false);
        ModalPanelManager.interactableButtonNext(true);
    }
}
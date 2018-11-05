using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;

    private BoardManager boardScript;
    private LevelManager levelManager;
    private String levelId;
    public int maxLevel;

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
        boardScript.initValues();
    }

    public void soundClick()
    {
        SoundManager.instance.soundClick();
    }

    public void pictureClick()
    {
        RecognizeCommandManager.instance.pictureClick();      
    }

    public void takePictureClick()
    {
        GameManager gameManager = GameManager.instance;
        Level level = gameManager.boardScript.getLevel();
        RecognizeCommandManager.instance.takePictureClick(level.maxCommandsUse);
    }

    public void recognizeCommand(List<Function> functions, int indexCircle, int indexStar, int indexTriangle)
    {
        this.functions = functions;
        ModalPanelManager.setCommands(functions, boardScript);
        ModalPanelManager.activeModal(true, "", false, false, false, true, false);
        ModalPanelManager.setTitleCommands(Messages.TITULO_PAINEL_COMANDOS);
        boardScript.setIndex(indexCircle, indexStar, indexTriangle);
    }

    public void showErro(string erro, bool buttonOkVisible, bool buttonTryAgainVisible)
    {
        ModalPanelManager.activeModal(true, Messages.TITULO_PAINEL_ERRO, false, false, true, false, false);
        ModalPanelManager.setDescriptionError(erro);
        ModalPanelManager.setVisibleButtonsErro(buttonOkVisible, buttonTryAgainVisible);
    }

    public void functionsCorrect()
    {
        GameManager gameManager = GameManager.instance;
        gameManager.ModalPanelManager.deactiveModal();
        gameManager.StartCoroutine(gameManager.boardScript.execute(gameManager.functions));
        gameManager.levelManager.deactivateButtons();
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

    public void setupSceneLevel(LevelManager levelManager)
    {
        this.levelManager = levelManager;
        this.boardScript.SetupScene(levelId);
        levelManager.setTextCommands(boardScript.getCommandsRemaining());
        levelManager.setTitle(levelId);
    }

    public void loadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public Boolean checkEndGameCommand()
    {
        StatusGame status = boardScript.checkEndGame(1, 0);
        levelManager.setTextCommands(boardScript.getCommandsRemaining());
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
        gameManager.ModalPanelManager.activeModal(true, Messages.TITULO_PAINEL_AJUDA, true, false, false, false, false);
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

    public void reloadLevel()
    {
        GameManager.instance.boardScript.StopAllCoroutines();
        SceneManager.LoadScene(2);
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
        boardScript.StopAllCoroutines();
        ModalPanelManager.activeModal(true, Messages.TITULO_PAINEL_FIM_JOGO_DERROTA, false, true, false, false, false);
        ModalPanelManager.interactableButtonNext(false);
    }

    private IEnumerator doVictory(Vector2 positionCollectable)
    {
        boardScript.StopAllCoroutines();
        yield return StartCoroutine(boardScript.terminateMovement(positionCollectable));

        int level = Int32.Parse(levelId);

        if (level < maxLevel)
        {
            ModalPanelManager.activeModal(true, Messages.TITULO_PAINEL_FIM_JOGO_VITORIA, false, true, false, false, false);
        }
        else
        {
            ModalPanelManager.activeModal(true, Messages.TITULO_PAINEL_FIM_JOGO_VITORIA, false, false, false, false, true);
            ModalPanelManager.setDescriptionLastLevel(Messages.MENSAGEM_ULTIMA_FASE);
        }
        int levelReached = PlayerPrefs.GetInt("levelReached", 1);
        if (level >= levelReached)
        {
            PlayerPrefs.SetInt("levelReached", level+1);
        }
        
        ModalPanelManager.interactableButtonNext(true);
    }
}
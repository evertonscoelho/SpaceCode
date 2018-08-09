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

    public void loadLevel(String id)
    {
        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.levelId = id;
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
        GameObject modalHelpMainScene = GameObject.Find("ModalPanel");
        if (active)
        {
            modalHelpMainScene.GetComponent<Transform>().SetAsLastSibling();
        }
        else
        {
            modalHelpMainScene.SetActive(false);
        }
    }

    public void checkEndGameCollectable(Vector2 positionCollectable)
    {
        StatusGame status = boardScript.checkEndGame(0, 1);
        if (status.Equals(StatusGame.VICTORY))
        {
            StartCoroutine(doVictory(positionCollectable));
        }
    }

    private void doDefeat()
    {
        boardComamandManager.StopAllCoroutines();
        print("Perdeu");
    }

    private IEnumerator doVictory(Vector2 positionCollectable)
    {
        boardComamandManager.StopAllCoroutines();
        yield return StartCoroutine(boardComamandManager.terminateMovement(positionCollectable));
        print("Ganhou");
    }
}
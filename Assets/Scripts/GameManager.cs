using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;


public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;             

    private BoardManager boardScript;                      
    private String levelId;

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
    
    public Boolean checkEndGame(int addCommand, int addCollectable)
    {
        StatusGame status = boardScript.checkEndGame(addCommand, addCollectable);
        if(status.Equals(StatusGame.VICTORY))
        {
            doVictory();
            return true;
        }else if (status.Equals(StatusGame.DEFEAT))
        {
            doDefeat();
            return true;
        }
        else
        {
            return false;
        }
    }

    private void doDefeat()
    {
        print("Perdeu");
    }

    private void doVictory()
    {
        print("Ganhou");
    }
}
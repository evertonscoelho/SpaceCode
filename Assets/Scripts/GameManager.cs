using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;


public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.

    private BoardManager boardScript;                       //Store a reference to our BoardManager which will set up the level.
    private String levelId;

    //Awake is always called before any Start functions
    void Awake()
    {
        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);

        //Get a component reference to the attached BoardManager script
        boardScript = GetComponent<BoardManager>();
        
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
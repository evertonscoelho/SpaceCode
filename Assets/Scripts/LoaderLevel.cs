using UnityEngine;
using System.Collections;


public class LoaderLevel : MonoBehaviour
{
    public GameObject gameManager;          //GameManager prefab to instantiate.

    void Awake()
    {
        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.setupSceneLevel();
    }
}
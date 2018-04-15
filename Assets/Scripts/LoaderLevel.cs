using UnityEngine;
using System.Collections;


public class LoaderLevel : MonoBehaviour
{
    void Awake()
    {
        GameManager.instance.setupSceneLevel();
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraViewManager : MonoBehaviour {

	void Start () {
        deactivate();
        PictureManager.instance.setCameraViewManager(this);
    }

    public void active()
    {
        gameObject.SetActive(true);
    }

    public void deactivate()
    {
        gameObject.SetActive(false);
    }
}

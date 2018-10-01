using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraViewManager : MonoBehaviour {

	void Start () {
        gameObject.GetComponent<RawImage>().enabled = false;
        PictureManager.instance.setCameraImage(gameObject.GetComponent<RawImage>());
    }
	
}

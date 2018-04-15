using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundButtonController : MonoBehaviour {

	void Start () {
        SoundManager.instance.soundInit(gameObject);
    }

}

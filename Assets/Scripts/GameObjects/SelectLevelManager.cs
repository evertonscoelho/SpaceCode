using UnityEngine;
using UnityEngine.UI;

public class SelectLevelManager : MonoBehaviour {

    public Button[] levelButtons;
    public Sprite enable;
    public Sprite disable;
    
    void Start () {
        int levelReached = PlayerPrefs.GetInt("levelReached", 1);
        for (int i = 0; i < levelButtons.Length; i++){
            if (i + 1 > levelReached)
            {
                levelButtons[i].enabled = false;
                levelButtons[i].GetComponent<Image>().sprite = disable;
            }
            else
            {
                levelButtons[i].interactable = true;
                levelButtons[i].GetComponent<Image>().sprite = enable;
            }
        }
	}
}

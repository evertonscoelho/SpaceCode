using UnityEngine;
using UnityEngine.UI;

public class SelectLevelManager : MonoBehaviour {

    public Button[] levelButtons;
    
    void Start () {
        int levelReached = PlayerPrefs.GetInt("levelReached", 1); 

        for (int i = 0; i < levelButtons.Length; i++){
            if (i + 1 > levelReached)
            {
                levelButtons[i].interactable = false;
            }
            else
            {
                levelButtons[i].interactable = true;
            }
        }
			}
	

}

using UnityEngine;
using UnityEngine.UI;

public class SelectLevelManager : MonoBehaviour {

    public Button[] levelButtons;
    public Text title;
    
    void Start () {
        int levelReached = PlayerPrefs.GetInt("levelReached", 1);
        levelReached = 9;
        for (int i = 0; i < levelButtons.Length; i++)
        { 
            if (i + 1 > levelReached)
            {
                levelButtons[i].enabled = false;
            }
            else
            {
                levelButtons[i].interactable = true;
            }
        }
        title.text = GameManager.instance.messages.getTituloTelaSelecao();
	}
}

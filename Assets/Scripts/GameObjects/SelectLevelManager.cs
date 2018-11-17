using UnityEngine;
using UnityEngine.UI;

public class SelectLevelManager : MonoBehaviour {

    public Button[] levelButtons;
    public Text title;
    
    void Start () {
        int levelReached = PlayerPrefs.GetInt("levelReached", 1);
        levelReached = 9;
        GameObject lockObject;
        for (int i = 0; i < levelButtons.Length; i++)
        {
            lockObject = levelButtons[i].transform.GetChild(1).gameObject;
            if (i + 1 > levelReached)
            {
                levelButtons[i].interactable = false;
                lockObject.SetActive(true);
            }
            else
            {
                levelButtons[i].interactable = true;
                lockObject.SetActive(false);
            }
        }
        title.text = GameManager.instance.messages.getTituloTelaSelecao();
	}
}

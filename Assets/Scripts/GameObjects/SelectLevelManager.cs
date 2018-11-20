using UnityEngine;
using UnityEngine.UI;

public class SelectLevelManager : MonoBehaviour {

    public Text title;
    public int maxLevel;
    private int page, levelReached;
    public Button nextPage, backPage;
    public GameObject buttonLevel;
    public GameObject panelButtonsLevel;

    void Start () {

        levelReached = PlayerPrefs.GetInt("levelReached", 1);
        page = 0;
        levelReached = 22;
        title.text = GameManager.instance.messages.getTituloTelaSelecao();
        backPage.interactable = false;
        nextPageInteractable();
        printPage();
        GameManager.instance.setSelectLevel(this);
    }

    private void printPage()
    {
        GameObject lockObject, button, textButton;
        int position = 0; 

        foreach (Transform child in panelButtonsLevel.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        for (int i = 1+(page*10); i <= (page+1)*10 && i <= maxLevel; i++)
        {
            button = Instantiate(buttonLevel) as GameObject;
            button.GetComponent<RectTransform>().SetParent(panelButtonsLevel.transform, false);
            if (position < 5) { 
                button.transform.localPosition = getPositionInstance(position, 0, button.GetComponent<RectTransform>().rect.width, button.GetComponent<RectTransform>().rect.height);
            }
            else
            {
                button.transform.localPosition = getPositionInstance(position-5, -1, button.GetComponent<RectTransform>().rect.width, button.GetComponent<RectTransform>().rect.height);
            }
            button.GetComponent<Button>().onClick.RemoveAllListeners();
            string temp = i.ToString();
            button.GetComponent<Button>().onClick.AddListener(delegate { GameManager.instance.loadLevel(temp);});
            textButton = button.transform.GetChild(0).gameObject;
            textButton.GetComponent<Text>().text = i.ToString();
            lockObject = button.transform.GetChild(1).gameObject;
            if (i > levelReached)
            {
                button.GetComponent<Button>().interactable = false;
                lockObject.SetActive(true);
            }
            else
            {
                button.GetComponent<Button>().interactable = true;
                lockObject.SetActive(false);
            }
            position++;
        }
    }

    private Vector3 getPositionInstance(float x, float y, float offsetX, float offsetY)
    {
        return new Vector3(x * offsetX + -220, y * offsetY + 150, 0f);
    }

    public void nextPageLevelClick()
    {
        page++;
        backPage.interactable = true;
        nextPageInteractable();
        printPage();
    }

    public void nextPageInteractable()
    {
        if(maxLevel > (page+1) * 10)
        {
            nextPage.interactable = true;
        }
        else
        {
            nextPage.interactable = false;
        }
    }

    public void backPageSelectLevelClick()
    {
        page--;
        nextPage.interactable = true;
        if(page == 0)
        {
            backPage.interactable = false;
        }
        printPage();
    }
}

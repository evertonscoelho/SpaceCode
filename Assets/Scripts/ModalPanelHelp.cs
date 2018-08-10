using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModalPanelHelp : MonoBehaviour {

    public Text question;
  
    public GameObject modalPanelObject;

    void Start()
    {
        GameManager.instance.setModalPanelHelp(this.GetComponent<ModalPanelHelp>());
        clickHelpMainScene(false);
    }

    public void clickHelpMainScene(System.Boolean active)
    {
        if (active)
        {
            ShowPainel();
        }
        else
        {
            ClosePanel();
        }
    }

    private void ShowPainel()
    {
        //this.question.text = "a";
        modalPanelObject.SetActive(true);
    }


    private void ClosePanel()
    {
        modalPanelObject.SetActive(false);
    }
}

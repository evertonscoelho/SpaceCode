using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModalPanel : MonoBehaviour {

    public Text question;
    public string tipoModal;

    public GameObject modalPanelObject;

    void Start()
    {
        if(tipoModal.Equals("ModalPanelHelp"))
        {
            GameManager.instance.setModalPanelHelp(this.GetComponent<ModalPanel>());
        }
        else if(tipoModal.Equals("ModalPanelEndGame"))
        {
            GameManager.instance.setModalPanelEndGame(this.GetComponent<ModalPanel>());
        }
        
        clickHelpMainScene(false, null);
    }

    public void clickHelpMainScene(Boolean active, string text)
    {
        if (active)
        {
            if(text != null)
            {
                question.text = text;
            }
            ShowPainel();
        }
        else
        {
            ClosePanel();
        }
    }

    private void ShowPainel()
    {
        modalPanelObject.SetActive(true);
    }

    private void ClosePanel()
    {
        modalPanelObject.SetActive(false);
    }
}

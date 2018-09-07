using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModalPanel : MonoBehaviour {

    public Text question;
    public Button buttonNext;
    public string tipoModal;

    public GameObject modalPanelObject;
    public GameObject boardCommand;

    private float offsetX;
    private float offsetY;

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
        else if (tipoModal.Equals("ModalPanelCommand"))
        {
            GameManager.instance.setModalPanelCommands(this.GetComponent<ModalPanel>());
        }


        showModal(false, null);
    }
    public void interactableButtonNext(Boolean interactable)
    {
        buttonNext.interactable = interactable;
    }

    public void showModal(Boolean active, string text)
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

    public void setCommands(Function[] functions, BoardCommandManager boardComamandManager)
    {
        Transform transformBoardCommand = boardCommand.transform;
        GameObject command = boardComamandManager.getObjectToInstantiate(functions[0].Commands[0]);
        offsetX = command.GetComponent<SpriteRenderer>().bounds.size.x;
        offsetY = command.GetComponent<SpriteRenderer>().bounds.size.y;

        for (int y = 0; y < functions.Length; y++)
        {
            for (int x = 0; x < functions[y].Commands.Length; x++)
            {
                GameObject toInstantiate = boardComamandManager.getObjectToInstantiate(functions[y].Commands[x]);
                GameObject instance = Instantiate(toInstantiate, transformBoardCommand.transform, true) as GameObject;
                instance.transform.localPosition = getPositionInstance(x - 3, (y * -1) + 1);
            }
        }
    }

    private Vector3 getPositionInstance(int x, float y)
    {
        return new Vector3(x * offsetX, y * offsetY, 0f);
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

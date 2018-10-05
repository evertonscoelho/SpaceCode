using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModalPanelManager : MonoBehaviour {

    public Text title, descriptionError, descriptionHelp, titleCommands, descriptionLastLevel;
    public GameObject panelHelp, panelEndGame, panelErrorCommand, panelCommands, panelLastLevel;
   
    public GameObject boardCommand;
    public Button buttonNext;

    private float offsetX, offsetY;

    void Start()
    {
        GameManager.instance.ModalPanelManager = this;
        deactiveModal();
    }

    public void interactableButtonNext(Boolean interactable)
    {
        buttonNext.interactable = interactable;
    }

    public void activeModal(Boolean active, string title, Boolean panelHelp, Boolean panelEndGame, Boolean panelErrorCommand, Boolean panelCommands, Boolean panelLastLevel)
    {
        if (active)
        {
            gameObject.SetActive(true);
        }
        this.title.text = title;
        this.panelHelp.SetActive(panelHelp);
        this.panelEndGame.SetActive(panelEndGame);
        this.panelErrorCommand.SetActive(panelErrorCommand);
        this.panelCommands.SetActive(panelCommands);
        this.panelLastLevel.SetActive(panelLastLevel);
    }

    public void setTitleCommands(string titleCommands)
    {
        this.titleCommands.text = titleCommands;
    }

    public void setDescriptionError(string description)
    {
        descriptionError.text = description;
    }

    public void setDescriptionHelp(string description)
    {
        descriptionHelp.text = description;
    }

    public void setDescriptionLastLevel(string description)
    {
        descriptionLastLevel.text = description;
    }

    public void deactiveModal()
    {
        gameObject.SetActive(false);
    }

    public void setCommands(List<Function> functions, BoardCommandManager boardComamandManager)
    {
        Transform transformBoardCommand = boardCommand.transform;
        GameObject command = boardComamandManager.getObjectToInstantiate(functions[0].Commands[0]);
        offsetX = command.GetComponent<SpriteRenderer>().bounds.size.x;
        offsetY = command.GetComponent<SpriteRenderer>().bounds.size.y;

        for (int y = 0; y < functions.Count; y++)
        {
            for (int x = 0; x < functions[y].Commands.Count; x++)
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
}

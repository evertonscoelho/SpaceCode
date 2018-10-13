using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModalPanelManager : MonoBehaviour {

    public Text title, descriptionError, descriptionHelp, titleCommands, descriptionLastLevel;
    public GameObject panelHelp, panelEndGame, panelErrorCommand, panelCommands, panelLastLevel;
   
    public GameObject boardCommand;
    public Button buttonNext;

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

    public void setCommands(List<Function> functions, BoardManager boardManager)
    {
        float width = 50, height = 50;
        Transform transformBoardCommand = boardCommand.transform;
        GameObject toInstantiate, commandObject;
        foreach (Transform child in transformBoardCommand)
        {
            GameObject.Destroy(child.gameObject);
        }

        for (int y = 0; y < functions.Count; y++)
        {
            for (int x = 0; x < functions[y].Commands.Count; x++)
            {
                toInstantiate = boardManager.getObjectToInstantiate(functions[y].Commands[x]);
                commandObject = new GameObject();
                Image image = commandObject.AddComponent<Image>();
                commandObject.GetComponent<RectTransform>().SetParent(transformBoardCommand.transform, false);
                commandObject.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
                image.sprite = toInstantiate.GetComponent<SpriteRenderer>().sprite;
                commandObject.transform.localPosition = getPositionInstance(x - 5, (y * -1.5f) + 1, width, height);
            }
        }
    }

    private Vector3 getPositionInstance(int x, float y, float offsetX, float offsetY)
    {
        return new Vector3(x * offsetX, y * offsetY, 0f);
    }
}

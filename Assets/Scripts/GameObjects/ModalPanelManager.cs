using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModalPanelManager : MonoBehaviour {

    public Text title, descriptionError, descriptionHelp, titleCommands, descriptionLastLevel, descriptionButtonFases, descriptionButtonTryAgain, descriptionButtonNext;
    public Text descriptionTryAgainError, descriptionOkError, descriptionButtonYes, descriptionButtonNoTryAgain, descriptionLevelPanelLastLevel;
    public GameObject panelHelp, panelEndGame, panelErrorCommand, panelCommands, panelLastLevel;
   
    public GameObject boardCommand;
    public Button buttonNext, buttonTryAgainError, buttonOkError;

    void Start()
    {
        GameManager.instance.ModalPanelManager = this;
        deactiveModal();
        descriptionButtonFases.text = Messages.BOTAO_FASES;
        descriptionButtonTryAgain.text = Messages.BOTAO_TENTAR_NOVAMENTE;
        descriptionButtonNext.text = Messages.BOTAO_PROXIMA_FASE;
        descriptionTryAgainError.text = Messages.BOTAO_TENTAR_NOVAMENTE;
        descriptionOkError.text = Messages.BOTAO_OK;
        descriptionButtonYes.text = Messages.BOTAO_SIM;
        descriptionButtonNoTryAgain.text = Messages.BOTAO_NAO_TENTAR_NOVAMENTE;
        descriptionLevelPanelLastLevel.text = Messages.BOTAO_FASES;
    }

    public void interactableButtonNext(Boolean interactable)
    {
        buttonNext.interactable = interactable;
    }

    public void setVisibleButtonsErro(bool buttonOkVisible, bool buttonTryAgainVisible)
    {
        buttonTryAgainError.gameObject.SetActive(buttonTryAgainVisible);
        buttonOkError.gameObject.SetActive(buttonOkVisible);


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
        Transform transformBoardCommand = boardCommand.transform;
        foreach (Transform child in transformBoardCommand)
        {
            GameObject.Destroy(child.gameObject);
        }

        for (int y = 0; y < functions.Count; y++)
        {
            int positionBoard = 0;
            for (int x = 0; x < functions[y].Commands.Count; x++)
            {
                printCommandOnBoard(functions[y].Commands[x], ref positionBoard, y, transformBoardCommand.transform, false, boardManager);
                if (EnumCommand.LOOP.Equals(functions[y].Commands[x].EnumCommand))
                {
                    for (int a = 0; a < functions[y].Commands[x].loop.Count; a++)
                    {
                        printCommandOnBoard(functions[y].Commands[x].loop[a], ref positionBoard, y, transformBoardCommand.transform, false, boardManager);
                    }
                    printCommandOnBoard(functions[y].Commands[x], ref positionBoard, y, transformBoardCommand.transform, true, boardManager);
                }

            }
        }
    }

    private void printCommandOnBoard(Command command, ref int positionX, int positionY, Transform transform, bool numberRepeat, BoardManager boardManager)
    {
        int width = 44;
        int height = 44;
        GameObject toInstantiate, commandObject;
        if (numberRepeat)
        {
            toInstantiate = boardManager.getNumberLoop(command.numRepeatLoop);
        }
        else
        {
            toInstantiate = boardManager.getObjectToInstantiate(command.EnumCommand);
        }
        commandObject = new GameObject();
        Image image = commandObject.AddComponent<Image>();
        commandObject.GetComponent<RectTransform>().SetParent(transform.transform, false);
        commandObject.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
        image.sprite = toInstantiate.GetComponent<Image>().sprite;
        commandObject.transform.localPosition = getPositionInstance(positionX - 5, (positionY * -1) + 1, width, height);
        positionX++;    
    }

    private Vector3 getPositionInstance(int x, float y, float offsetX, float offsetY)
    {
        return new Vector3(x * offsetX-5, y * offsetY-5, 0f);
    }
}

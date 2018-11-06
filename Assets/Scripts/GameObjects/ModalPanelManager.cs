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

    public void setCommands(List<Function> functions, GameManager gameManager)
    {
        gameManager.setCommands(functions, boardCommand.transform, 44, 44, -5, 1, true);
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

}

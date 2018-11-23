using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModalPanelManager : MonoBehaviour {

    public Text title, descriptionError, titleCommands, descriptionLastLevel, descriptionButtonFases, descriptionButtonTryAgain, descriptionButtonNext;
    public Text descriptionTryAgainError, descriptionOkError, descriptionButtonYes, descriptionButtonNoTryAgain, descriptionLevelPanelLastLevel, languageBR, languageUS;
    public GameObject panelEndGame, panelErrorCommand, panelCommands, panelLastLevel, panelLanguage, icLock;
   
    public GameObject boardCommand;
    public Button buttonNext, buttonTryAgainError, buttonOkError;

    public int Scene;

    void Start()
    {
        GameManager gameManager = GameManager.instance;
        gameManager.ModalPanelManager = this;
        deactiveModal();
        descriptionButtonFases.text = gameManager.messages.getBotaoFases();
        descriptionButtonTryAgain.text = gameManager.messages.getBotaoTentarNovamente();
        descriptionButtonNext.text = gameManager.messages.getBotaoProximaFase();
        descriptionTryAgainError.text = gameManager.messages.getBotaoTentarNovamente();
        descriptionOkError.text = gameManager.messages.getBotaoOk();
        descriptionButtonYes.text = gameManager.messages.getBotaoSim();
        descriptionButtonNoTryAgain.text = gameManager.messages.getNaoBotaoTentarNovamente();
        descriptionLevelPanelLastLevel.text = gameManager.messages.getBotaoFases();
        languageBR.text = gameManager.messages.getPortugues();
        languageUS.text = gameManager.messages.getIngles();
    }

    public void interactableButtonNext(Boolean interactable)
    {
        buttonNext.interactable = interactable;
        icLock.SetActive(!interactable);
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

    public void activeModal(string title, Boolean panelEndGame, Boolean panelErrorCommand, Boolean panelCommands, Boolean panelLastLevel, Boolean panelLanguage)
    {
        gameObject.SetActive(true);
        this.title.text = title;
        this.panelEndGame.SetActive(panelEndGame);
        this.panelErrorCommand.SetActive(panelErrorCommand);
        this.panelCommands.SetActive(panelCommands);
        this.panelLastLevel.SetActive(panelLastLevel);
        this.panelLanguage.SetActive(panelLanguage);
    }

    public void setTitleCommands(string titleCommands)
    {
        this.titleCommands.text = titleCommands;
    }

    public void setDescriptionError(string description)
    {
        descriptionError.text = description;
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

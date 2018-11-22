using UnityEngine;
using UnityEngine.UI;

public class ModalPanelHelpManager : MonoBehaviour {

    public Text title, descriptionHelp, descriptionMove1, descriptionMove2, descriptionMove3, descriptionLoop, descriptionRecursion, descriptionFunction;
    private GameManager gameManager;
    public GameObject panelHelp, panelHelpMove, panelHelpLoop, panelHelpRecursion, panelFunction;

    void Start () {
        gameManager = GameManager.instance;
        gameManager.ModalPanelHelpManager = this;
        deactiveModal();
        title.text = gameManager.messages.getTituloPainelAjuda();
    }

    public void deactiveModal()
    {
        gameObject.SetActive(false);
    }

    public void activeModal(int helpDifficult)
    {
        gameObject.SetActive(true);
        panelHelp.SetActive(false);
        panelHelpMove.SetActive(false);
        panelHelpLoop.SetActive(false);
        panelHelpRecursion.SetActive(false);
        panelFunction.SetActive(false);
        switch (helpDifficult)
        {
            case 0:
                descriptionHelp.text = gameManager.messages.getDescricaoAjudaSobreJogo();
                panelHelp.SetActive(true);
                break;
            case 1:
                descriptionMove1.text = gameManager.messages.getDescricaoAjudaMovimentosBasicos1();
                descriptionMove2.text = gameManager.messages.getDescricaoAjudaMovimentosBasicos2();
                descriptionMove3.text = gameManager.messages.getDescricaoAjudaMovimentosBasicos3();
                panelHelpMove.SetActive(true);
                break;
            case 2:
                panelHelp.SetActive(true);
                descriptionHelp.text = gameManager.messages.getDescricaoAjudaMaisColetaveis();
                break;
            case 3:
                panelFunction.SetActive(true);
                descriptionFunction.text = gameManager.messages.getDescricaoAjudaMaisFuncoes();
                break;
            case 4:
                panelHelpLoop.SetActive(true);
                descriptionLoop.text = gameManager.messages.getDescricaoAjudaRepeticao();
                break;
            case 5:
                panelHelpRecursion.SetActive(true);
                descriptionRecursion.text = gameManager.messages.getDescricaoAjudaRecursao();
                break;
        }
    }

}

using UnityEngine;
using UnityEngine.UI;

public class ModalPanelHelpManager : MonoBehaviour {

    public Text title, descriptionHelp, descriptionMove;
    private GameManager gameManager;
    public GameObject panelHelp, panelHelpMove;

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
        switch (helpDifficult)
        {
            case 0:
                descriptionHelp.text = gameManager.messages.getDescricaoAjudaSobreJogo();
                panelHelp.SetActive(true);
                break;
            case 1:
                descriptionMove.text = gameManager.messages.getDescricaoAjudaMovimentosBasicos();
                panelHelpMove.SetActive(true);
                break;
        }
    }

}

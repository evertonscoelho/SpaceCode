using UnityEngine;
using UnityEngine.UI;


public class MainManager : MonoBehaviour {

    public Text nameGame, buttonDownload;

    void Start() {
        GameManager gameManager = GameManager.instance; 
        nameGame.text = gameManager.messages.getNomeJogo();
        buttonDownload.text = gameManager.messages.getBotaoBaixarTela();
    }
}

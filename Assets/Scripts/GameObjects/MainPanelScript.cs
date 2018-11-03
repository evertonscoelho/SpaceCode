using UnityEngine;
using UnityEngine.UI;

public class MainPanelScript : MonoBehaviour {

    public Text nameGame; 

    void Start () {
        nameGame.text = Messages.NOME_JOGO;
    }
	
}

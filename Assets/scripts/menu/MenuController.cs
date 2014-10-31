using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour {
    public void startGame() {
        Application.LoadLevel("main"); 
    }

    public void showHowTo() {
        Application.LoadLevel("howTo");
    }
    
    public void backToMenu() {
        Application.LoadLevel("menu");
    }
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HideLabel : MonoBehaviour {
    Text textComp;
	
    void Start () {
        textComp = gameObject.GetComponent<Text>();
        textComp.enabled = false;
	}

    IEnumerator waitAndHide() {
        yield return new WaitForSeconds(3f);
        textComp.enabled = false;
    }

    public void show() {
        textComp.enabled = true;
        StartCoroutine("waitAndHide");
    }
}

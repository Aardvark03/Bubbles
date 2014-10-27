using UnityEngine;
using System.Collections;

public class SpawnerController : MonoBehaviour {
    public float spawnFrequency;
    public bool isActive;
    public GameObject ballPrefab;

    public Sprite passiveSprite;
    public Sprite activeSprite;

	void Start () {
        StartCoroutine("spawnBalls");	
    }

    public void toggle() {
        isActive = !isActive;
        
        SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();

        if (isActive) {
            renderer.sprite = activeSprite;
        } else {
            renderer.sprite = passiveSprite;
        }

    }

    IEnumerator spawnBalls() {
        while (true) {
            if (isActive) {
                Instantiate(ballPrefab, transform.position, Quaternion.identity);
            }
            yield return new WaitForSeconds(spawnFrequency);
        }
    }

    
}

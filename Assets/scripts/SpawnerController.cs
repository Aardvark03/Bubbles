using UnityEngine;
using System.Collections;

public class SpawnerController : MonoBehaviour {
    public float spawnFrequency;
    public bool isActive;
    public GameObject ballPrefab;

    public Sprite passiveSprite;
    public Sprite activeSprite;

    float spawnTimer;

    void Start() {
        spawnTimer = 0f; 
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

    void Update() {
        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0) {
                if (isActive) {
                    Instantiate(ballPrefab, transform.position, Quaternion.identity);
                    spawnTimer = spawnFrequency;
                } else {
                    spawnTimer = 0f;
                }
        }
    }
}

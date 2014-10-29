using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class GameController : MonoBehaviour {
    public Text gameOverText;
    public Text scoreText;

    int scoreRed;
    int scoreBlue;

    void Start() {
        BallController.OnGameOver += OnGameOver;

        scoreRed = 0;
        scoreBlue = 0;
    }

    void updateSpawnerArray(GameObject[] spawners) {
        int active = 0;

        foreach (GameObject spawner in spawners) {
            SpawnerController controller = spawner.GetComponent<SpawnerController>();

            if (controller.isActive) {
                active += 1;
            }
        }
        
        float frequency = 0.5f * active;

        foreach (GameObject spawner in spawners) {
            SpawnerController controller = spawner.GetComponent<SpawnerController>();
            controller.spawnFrequency = frequency;
        }

    }

    void updateSpawnerFrequencies() {
        GameObject[] blueSpawners = GameObject.FindGameObjectsWithTag("blueSpawner");
        GameObject[] redSpawners = GameObject.FindGameObjectsWithTag("redSpawner");

        updateSpawnerArray(blueSpawners);
        updateSpawnerArray(redSpawners);
        
    }

    void Update() {
        if (Input.GetMouseButtonUp(0)) {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null) {
                string tag = hit.collider.gameObject.tag;
                if (tag == "blueSpawner" || tag == "redSpawner") {
                    SpawnerController spawner = hit.collider.gameObject.GetComponent<SpawnerController>();
                    spawner.toggle();

                    updateSpawnerFrequencies();
                }
            }
        }
    }

    void reset() {
        GameObject[] blueSpawners = GameObject.FindGameObjectsWithTag("blueSpawner");
        GameObject[] redSpawners = GameObject.FindGameObjectsWithTag("redSpawner");
        
        GameObject[] spawners = new GameObject[blueSpawners.Length + redSpawners.Length];

        blueSpawners.CopyTo(spawners, 0);
        redSpawners.CopyTo(spawners, blueSpawners.Length);

        foreach (GameObject spawner in spawners) {
            SpawnerController controller = spawner.GetComponent<SpawnerController>();

            if (controller.isActive) {
                controller.toggle();
            }
        }
        
        GameObject[] balls = GameObject.FindGameObjectsWithTag("bubble");

        foreach (GameObject ball in balls) {
            Destroy(ball);
        }
    }

    void OnGameOver(string winner) {
        HideLabel hideScript = gameOverText.GetComponent<HideLabel>(); 
        gameOverText.text = winner + " wins!";
        hideScript.show();

        if (winner == "Blue") {
            scoreBlue += 1;
        } else {
            scoreRed += 1;
        }
        scoreText.text = String.Format("{0} : {1}", scoreBlue, scoreRed);

        reset();
    }
}

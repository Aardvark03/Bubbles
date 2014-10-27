using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
    void Start() {
        BallController.OnGameOver += OnGameOver;  
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
         foreach (Touch touch in Input.touches) {
            if (touch.phase == TouchPhase.Ended) {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(touch.position), Vector2.zero);

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
        Debug.Log("Game Over. " + winner + " wins!");
        Debug.Log("Restarting...");

        reset();
    }
}

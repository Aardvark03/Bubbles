using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour {
    public float speed;
    public int direction; 

    public delegate void GameOver(string winner);
    public static event GameOver OnGameOver;

    Rect worldBounds;
    float spriteHeight;

    void Start() {
        Camera cam = Camera.main;
        
        Vector3 upperRight = new Vector3(Screen.width, Screen.height, 0f);
        Vector3 lowerLeft = new Vector3(0f, 0f, 0f);

        Vector3 upperRightWorld = cam.ScreenToWorldPoint(upperRight);
        Vector3 lowerLeftWorld = cam.ScreenToWorldPoint(lowerLeft);

        worldBounds = new Rect(lowerLeftWorld.x, upperRightWorld.y,
                                 upperRightWorld.x - lowerLeftWorld.x,
                                 upperRightWorld.y - lowerLeftWorld.y);
        
        spriteHeight = renderer.bounds.size.y;
    }

    void checkBounds() {
        if (transform.position.y + spriteHeight * 0.5f < worldBounds.y - worldBounds.height ||
            transform.position.y - spriteHeight * 0.5f > worldBounds.y) {
           if (OnGameOver != null) {
                string winner;
                if (direction == -1) {
                    winner = "Red";
                } else {
                    winner = "Blue";
                }

                OnGameOver(winner);
           }
        }
    }

	void Update () {
        float dt = Time.deltaTime;
        float s = speed * dt;

        Vector2 pos = rigidbody2D.position;
        pos.y += direction * s;

        rigidbody2D.MovePosition(pos);

        checkBounds();
	
	}

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.tag == "bubble") {
            Destroy(gameObject);
            Destroy(collider.gameObject);
        }
    }
}

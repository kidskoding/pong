using UnityEngine;

public class BallScript : MonoBehaviour {
    public float speed = 10f;
    private Rigidbody2D rb;

    private Vector2 direction;
    private bool hasStarted = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(rb == null) {
            rb = GetComponent<Rigidbody2D>();
        }

        LaunchBall();
    }

    // Update is called once per frame
    void Update()
    {
        if(!hasStarted && Input.GetKeyDown(KeyCode.Space)) {
            LaunchBall();
        }
        if(hasStarted) {
            rb.linearVelocity = direction * speed;
        }
    }

    void LaunchBall() {
        if(!hasStarted) {
            direction = new Vector2(
                Random.Range(-1f, 1f), 
                Random.Range(-1f, 1f)
            ).normalized;

            hasStarted = true;
        }
    }
    
    void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.CompareTag("Wall")) {
            direction.y = -direction.y;
        }
        if(collision.gameObject.CompareTag("Paddle")) {
            direction.x = -direction.x;
        }
    }
}

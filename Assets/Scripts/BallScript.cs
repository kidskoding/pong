using UnityEngine;

public class BallScript : MonoBehaviour {
    public float speed = 10f;
    public float launchDelay = 2f;
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
        if(!hasStarted) {
            Vector3 screenCenter = new Vector3(
                Screen.width / 2,
                Screen.height / 2,
                0f
            );
            
            Vector3 sceneCenter = Camera.main.ScreenToWorldPoint(screenCenter);
            this.transform.position = new Vector3(sceneCenter.x, sceneCenter.y, 0f);
            Invoke("LaunchBall", 1.25f);
        }
        
        if(hasStarted) {
            rb.linearVelocity = direction * speed;
        }

        Vector3 screenPos = Camera.main.WorldToScreenPoint(this.transform.position);
        if(screenPos.x > Screen.width || screenPos.x < 0) {
          hasStarted = false;
        }
    }

    void LaunchBall() {
        if(!hasStarted) {
            float xSpeed = 1f;
            float val = Random.Range(-1f, 1f);
            if(val < 0) {
              xSpeed = -1f;
            }

            direction = new Vector2(
                xSpeed,
                1f
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

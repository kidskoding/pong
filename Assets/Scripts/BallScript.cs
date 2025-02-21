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

        ResetBall();
    }

    // Update is called once per frame
    void Update()
    {
        if(!hasStarted && Input.GetKeyDown(KeyCode.Space)) {
            ResetBall();
        }
        if(hasStarted) {
            rb.linearVelocity = direction * speed;
        }
    }

    void ResetBall() {
        if(!hasStarted) {
            direction = new Vector2(
                Random.Range(-1f, 1f), 
                Random.Range(-1f, 1f)
            ).normalized;
            
            hasStarted = true;
        }
    }
}

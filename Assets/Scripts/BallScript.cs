using UnityEngine;

public class BallScript : MonoBehaviour {
    public float speed = 10f;
    private Rigidbody2D rb;
    private Vector2 direction;
    private bool hasStarted = false;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        LaunchBall();
    }

    void Update() {
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
        var paddleAi = GameObject.FindWithTag("AIPaddle").GetComponent<PaddleAgentScript>();

        if(screenPos.x > Screen.width) {
            paddleAi.AddReward(-1.0f);
            hasStarted = false;
        } else if(screenPos.x < 0) {
            paddleAi.AddReward(1.0f);
            hasStarted = false;
        }
        Debug.Log(paddleAi.GetCumulativeReward());
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

        if(collision.gameObject.CompareTag("Paddle") || collision.gameObject.CompareTag("AIPaddle")) {
            if(collision.gameObject.CompareTag("AIPaddle")) {
                var paddleAi = collision.gameObject.GetComponent<PaddleAgentScript>();
                paddleAi.AddReward(+0.2f);
            }

            bool finalVectorPointsRight = direction.x < 0;
            direction.x = Mathf.Abs(direction.x);

            float dtheta = (transform.position.y - collision.gameObject.transform.position.y) / 1.5f;
            float thetaInitial = Mathf.Atan2(direction.y, direction.x);
            float thetaFinal = thetaInitial + dtheta;

            float maxBallAngle = Mathf.PI / 4;
            thetaFinal = Mathf.Clamp(thetaFinal, -maxBallAngle, maxBallAngle);

            direction.x = finalVectorPointsRight ? Mathf.Cos(thetaFinal) : -Mathf.Cos(thetaFinal);
            direction.y = Mathf.Sin(thetaFinal);
        }
    }
}

using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class PaddleAgentScript : Agent {
    public float speed = 5f;
    private GameObject ball;
    private Rigidbody2D ballRb;
    private Rigidbody2D paddleRb;    
    
    public override void Initialize() {
        ball = GameObject.Find("Ball");
        paddleRb = ball.GetComponent<Rigidbody2D>();
        ballRb = ball.GetComponent<Rigidbody2D>();
    }

    /// Collect the states of the paddle
    ///
    /// State Space
    ///   - paddle position
    ///   - ball position
    ///   - ball velocity (includes direction)
    public override void CollectObservations(VectorSensor sensor) {
        sensor.AddObservation(this.transform.position);
        sensor.AddObservation(ball.transform.position);
        sensor.AddObservation(ball.GetComponent<Rigidbody2D>().linearVelocity);
    }
    
    /// Control the paddle agent's action 
    ///
    /// Discrete Action Space
    ///   - Do nothing
    ///   - Move Up 
    ///   - Move Down
    ///
    /// Train the Paddle Agent AI based on its action, 
    /// where it gains or loses reward based on whether the appropriate action was done
    public override void OnActionReceived(ActionBuffers actions) {
        int action = actions.DiscreteActions[0];

        switch(action) {
            case 0:
                break;
            case 1:
                ballRb.linearVelocity = new Vector2(0, speed);
                break;
            case 2:
                ballRb.linearVelocity = new Vector2(0, -speed);
                break;
        }
    }

    /// Set environment state when the episode begins
    ///
    /// Typically would reset all states
    public override void OnEpisodeBegin() {
        transform.position = new Vector3(transform.position.x, 0, 0);
        ball.transform.position = Vector3.zero;
        ballRb.linearVelocity = Vector2.zero;

        float randomAngle = Random.Range(-45f, 45f);
        float direction = Random.Range(0, 2) == 0 ? -1f : 1f;
        ballRb.linearVelocity = new Vector2(5f * direction, 5f * Mathf.Tan(randomAngle * Mathf.Deg2Rad));
    }
}

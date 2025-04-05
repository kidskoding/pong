using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class PaddleAgentScript : Agent {
    public float speed = 5f;
    private GameObject ball;
    private Rigidbody2D ballRb;
    
    public bool debugMode = true;
    public float verticalBoundary = 4.5f;
    
    void Start() {
        Initialize();
    }
    
    public override void Initialize() {
        ball = GameObject.Find("Ball");
        if(ball != null) {
            ballRb = ball.GetComponent<Rigidbody2D>();
        } else {
            Debug.LogError("Ball not found. Make sure it's named 'Ball' in the scene.");
        }
    }
    
    // Update is called once per frame to show debug visuals
    void FixedUpdate() {
        RequestDecision();

        if(debugMode && ball != null) {
            Debug.DrawLine(transform.position, ball.transform.position, Color.red);
        }
    }
    
    public override void CollectObservations(VectorSensor sensor) {
        float normalizedPaddleY = transform.position.y / verticalBoundary;
        sensor.AddObservation(normalizedPaddleY);
        
        if(ball != null) {
            Vector3 ballRelativePos = ball.transform.position - transform.position;
            sensor.AddObservation(ballRelativePos.x / 10f);
            sensor.AddObservation(ballRelativePos.y / 10f);
            
            Vector2 ballVelocity = ballRb.linearVelocity;
            sensor.AddObservation(ballVelocity.x);
            sensor.AddObservation(ballVelocity.y);
        } else {
            sensor.AddObservation(0f);
            sensor.AddObservation(0f);
            sensor.AddObservation(0f);
            sensor.AddObservation(0f);
        }
    }
    
    public override void OnActionReceived(ActionBuffers actions) {
        int action = actions.DiscreteActions[0];
        Debug.LogWarning($"Agent chose action: {action}");
        
        Vector3 paddlePosition = transform.position;
        
        switch (action) {
            case 1:
                if (paddlePosition.y < verticalBoundary) {
                    paddlePosition.y += speed * Time.deltaTime;
                } else {
                    AddReward(-0.01f);
                }
                break;
            case 2:
                if (paddlePosition.y > -verticalBoundary) {
                    paddlePosition.y -= speed * Time.deltaTime;
                } else {
                    AddReward(-0.01f);
                }
                break;
        }
        
        transform.position = new Vector3(
            paddlePosition.x, 
            Mathf.Clamp(paddlePosition.y, -verticalBoundary, verticalBoundary), 
            paddlePosition.z
          );
    
        if(ball != null) {
            float distanceToBallY = Mathf.Abs(transform.position.y - ball.transform.position.y);
            AddReward(-distanceToBallY * 0.001f);
        }
    }
    
    public override void OnEpisodeBegin() {
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
    }
}

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
    void Update() {
        if(debugMode) {
            Debug.Log($"Action: Last action taken, Reward: {GetCumulativeReward()}");
            
            if(ball != null) {
                Debug.DrawLine(transform.position, ball.transform.position, Color.red);
            }
        }
    }
    
    public override void CollectObservations(VectorSensor sensor) {
        float normalizedPaddleY = transform.position.y / verticalBoundary;
        sensor.AddObservation(normalizedPaddleY);
        
        if(ball != null) {
            Vector3 ballRelativePos = ball.transform.position - transform.position;
            sensor.AddObservation(ballRelativePos.x / 10f);
            sensor.AddObservation(ballRelativePos.y / 10f);
            
            Vector2 ballVelocity = ballRb.linearVelocity.normalized;
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
        
        switch(action) {
            case 0:
                Debug.Log("0 is chosen");
                break;
            case 1:
                Debug.Log("1 is chosen");
                paddlePosition.y += speed * Time.deltaTime;
                break;
            case 2:
                Debug.Log("2 is chosen");
                paddlePosition.y -= speed * Time.deltaTime;
                break;
        }
        
        paddlePosition.y = Mathf.Clamp(paddlePosition.y, -verticalBoundary, verticalBoundary);
        transform.position = paddlePosition;
    }
    
    /* public override void Heuristic(in ActionBuffers actionsOut) {
        var discreteActions = actionsOut.DiscreteActions;
        
        if(Input.GetKey(KeyCode.UpArrow)) {
            discreteActions[0] = 1;
        } else if(Input.GetKey(KeyCode.DownArrow)) {
            discreteActions[0] = 2;
        } else {
            discreteActions[0] = 0;
        }
    } */
    
    public override void OnEpisodeBegin() {
        if(ball != null) {
            Debug.Log("Episode beginning. Resetting agent state.");
        }
    }
}

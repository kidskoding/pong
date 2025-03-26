using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class PaddleAgentScript : Agent {
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
      
    }

    // Update is called once per frame
    void Update() {
      
    }

    /// Collect the states of the paddle
    ///
    /// State Space
    ///   - paddle position
    ///   - ball position
    ///   - ball velocity (includes direction)
    public override void CollectObservations(VectorSensor sensor) {
        base.CollectObservations(sensor);
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
        base.OnActionReceived(actions);
    }

    /// Set environment state when the episode begins
    ///
    /// Typically would reset all states
    public override void OnEpisodeBegin() {
        base.OnEpisodeBegin();
    }
}

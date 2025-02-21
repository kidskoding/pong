using UnityEngine;

public class PaddleScript : MonoBehaviour
{
    public float speed = 10f;
    public float boundaryY = 4.0f;
    
    public float moveInput;

    public KeyCode upKey = KeyCode.W;
    public KeyCode downKey = KeyCode.S;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float move = 0;

        if(Input.GetKey(upKey)) {
            move = 1;
        } else if(Input.GetKey(downKey)) {
            move = -1;
        }

        transform.Translate(Vector3.up * move * speed * Time.deltaTime);

        float clampedY = Mathf.Clamp(transform.position.y, -boundaryY, boundaryY);
        transform.position = new Vector3(
            transform.position.x, 
            clampedY, 
            transform.position.z
        );
    }
}
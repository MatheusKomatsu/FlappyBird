using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody thisRigidBody;
    public float jumpPower = 0.5f;
    public float jumpInterval = 0.5f;
    private float jumpCooldown = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        thisRigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {   
        bool isGameActive = GameManager.Instance.IsGameActive(); 
        // Update Cooldown
        jumpCooldown -= Time.deltaTime;
        bool canJump = jumpCooldown <= 0 && isGameActive;

        // Jump!
        if (canJump){
            bool jumpInput = Input.GetKey(KeyCode.Space);
            if (jumpInput){
                Jump();
            }
        }
        // Toggle gravity
        thisRigidBody.useGravity = isGameActive;
        
    }
    // Both function are used to detect collision, but the first applies to the obstacles, and the second, to the score giver marker 
    // which is only a trigger
    void OnCollisionEnter(Collision other){
        OnCustomCollisionEnter(other.gameObject); 
    }
     void OnTriggerEnter(Collider other){
        OnCustomCollisionEnter(other.gameObject);
    }

     private void OnCustomCollisionEnter(GameObject other){
        // Detect collision
        bool isSensor = other.CompareTag("Sensor");
        if (isSensor){
            // Score increases in  1!
            GameManager.Instance.score++;
            Debug.Log("Score: " + GameManager.Instance.score);
        }else {
            // Game Over!!
            GameManager.Instance.EndGame();
        }
        
        
    }
    private void Jump(){
        // Reset Cooldown
        jumpCooldown = jumpInterval;
        // Reset velocity (only the impulse is important)
        thisRigidBody.velocity = Vector3.zero;
        // Apply Force
        thisRigidBody.AddForce(new Vector3(0,jumpPower,0),ForceMode.Impulse);
    } 
}

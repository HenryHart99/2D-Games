using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private bool JumpedKeyWasPressed;
    private float horizontalInput;
    private Rigidbody rigidbodyComponent;
    [SerializeField] private Transform groundCheckTransform;
    [SerializeField] private LayerMask playerMask;
    private int superJumpsRemaining;



    // Start is called before the first frame update
    void Start()
    {
        rigidbodyComponent = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
      
        // Check if space key is press down
        if (Input.GetKeyDown(KeyCode.Space))
        {
            JumpedKeyWasPressed = true;
        }

        horizontalInput = Input.GetAxis("Horizontal");
       

    }

    // FixedUpdate is called once every physics update

    private void FixedUpdate()
    {

        rigidbodyComponent.velocity = new Vector3(horizontalInput, rigidbodyComponent.velocity.y, rigidbodyComponent.velocity.z);

        if (Physics.OverlapSphere(groundCheckTransform.position, 0.1f, playerMask).Length == 0) 
        {
            return;
        }




        if (JumpedKeyWasPressed)
        {
            float JumpPower = 8f;

            if (superJumpsRemaining > 0)
            {
                JumpPower *= 1;
                superJumpsRemaining++;
            }

            rigidbodyComponent.AddForce(Vector3.up * JumpPower, ForceMode.VelocityChange);

            JumpedKeyWasPressed = false;
        }    
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            Destroy(other.gameObject);
            superJumpsRemaining++;
        }
    }

    
}

 

   
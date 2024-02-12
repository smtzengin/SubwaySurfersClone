using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private CharacterController controller;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float gravityValue = -9.81f;
    [SerializeField] private float jumpHeight = 2f;
    private Vector3 playerVelocity;
    [SerializeField] private bool isGrounded;

    [SerializeField] private LayerMask obstacleLayer;
    [SerializeField] private float rayLength = 2f;
    [SerializeField] private float sidestepDistance = 2f;

    private void Awake()
    {        
        anim = GetComponent<Animator>() ?? anim;    
        controller = GetComponent<CharacterController>() ?? controller;
    }

    private void Update()
    {        
        isGrounded = controller.isGrounded;
        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }       

        Vector3 move = transform.forward * speed;
        controller.Move(move * Time.deltaTime);

        
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -2f * gravityValue);
            anim.SetTrigger("isJumping");
        }

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (!Physics.Raycast(transform.position, transform.right, sidestepDistance, obstacleLayer))
            {                
                controller.Move(transform.right * sidestepDistance);
            }
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (!Physics.Raycast(transform.position, -transform.right, sidestepDistance, obstacleLayer))
            {                
                controller.Move(-transform.right * sidestepDistance);
            }
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
        anim.SetBool("isRunning", true);
    }

    private void OnDrawGizmos()
    {        
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.right * rayLength);
        Gizmos.DrawLine(transform.position, transform.position - transform.right * rayLength);
    }
    

}

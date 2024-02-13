using UnityEngine;
using System;
using System.Collections;

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
    [SerializeField] private float laneChangeSpeed = 10f;
    [SerializeField] private int currentLane = 0; // 0: sol , 1: orta , 2: sağ
    private readonly float[] lanePositions = new float[] { -7, 0, 7 };

    [SerializeField] private float reboundForce = 5f; 
    [SerializeField] private bool isDizzy = false;
    private void Awake()
    {
        anim = GetComponent<Animator>() ?? anim;
        controller = GetComponent<CharacterController>() ?? controller;
    }

    private void Update()
    {
        if (!GameManager.Instance.IsGameStart && !GameManager.Instance.IsGameOver) return;
        if (isDizzy)
            return;
        else
        {
            Vector3 move = transform.forward * speed;
            controller.Move(move * Time.deltaTime);

            Vector3 targetPosition = new Vector3(lanePositions[currentLane], transform.position.y, transform.position.z);
            Vector3 direction = (targetPosition - transform.position).normalized;
            float distanceToTarget = Vector3.Distance(transform.position, targetPosition);

            if (distanceToTarget > 0.1f)
            {
                controller.Move(laneChangeSpeed * Time.deltaTime * direction);
            }

            playerVelocity.y += gravityValue * Time.deltaTime;
            controller.Move(playerVelocity * Time.deltaTime);
            anim.SetBool("isRunning", true);

            isGrounded = controller.isGrounded;
            if (isGrounded && playerVelocity.y < 0)
            {
                playerVelocity.y = 0f;
            }

            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                anim.SetTrigger("isSlide");
            }

            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                playerVelocity.y += Mathf.Sqrt(jumpHeight * -2f * gravityValue);
                anim.SetTrigger("isJumping");
            }

            if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && currentLane < lanePositions.Length - 1)
            {
                currentLane++;
            }
            else if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && currentLane > 0)
            {
                currentLane--;
            }
        }        
        
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Obstacle"))
        {
            GameManager.Instance.TakeDamage(1);
            Vector3 reboundDirection = (transform.position - hit.point).normalized;
            reboundDirection.y = 0; 
           
            playerVelocity = Vector3.zero;

            StartCoroutine(ReboundPlayer(reboundDirection, reboundForce));
            anim.SetTrigger("isDizzy");
            isDizzy = true;
            Invoke("ResetDizzyState", 1.5f); 
        }

        if(hit.gameObject.CompareTag("Gold"))
        {
            Destroy(hit.gameObject);
            GameManager.Instance.IncreaseGold();         
            
        }
    }
    
    private IEnumerator ReboundPlayer(Vector3 direction, float force)
    {
        float elapsedTime = 0;
        float duration = 1.5f; 

        while (elapsedTime < duration)
        {
            controller.Move(force * Time.deltaTime * direction);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    void ResetDizzyState()
    {
        isDizzy = false;
        
    }
    public void StartSlide()
    {
        Debug.Log("Start Animation Event Triggered");
        controller.height = 0.25f;
        controller.center = new Vector3(0f, .13f, 0.03f);
    }

    public void ResetSlide()
    {
        Debug.Log("Animation Event Triggered");
        controller.height = 0.48f;
        controller.center = new Vector3(0f, .25f, 0.03f);
    }
   
}
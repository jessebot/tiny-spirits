using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEditor;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 0;
    public Vector3 jump;
    public float jumpForce = 2.0f;
    public bool isGrounded;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public GameObject beginTextObject;
 
    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;
    private float movementZ;
    
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        jump = new Vector3(0.0f, 2.0f, 0.0f);

        // for the collectable count
        count = 0;
        SetCountText();

        // begin and end text
        winTextObject.SetActive(false);
    }
    void FixedUpdate()
    {
        movementZ = 0.0f;
        Vector3 movement = new Vector3(movementX, movementZ, movementY);
        rb.AddForce(movement * moveSpeed);
    }

    // moving left and right and back and forth
    void OnMove(InputValue movementValue)
    {
        // on PC, Mac, Linux
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;

        // animations
        animator.SetBool("IsWalking", true);
        animator.SetBool("IsIdle", false);
        beginTextObject.SetActive(false);
    }

    // jumping
    void OnJump()
    {
        if(isGrounded)
        {
            rb.AddForce(jump * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    void OnCollisionStay(Collision other) {
        isGrounded = true;
        animator.SetBool("IsWalking", false);
        animator.SetBool("IsIdle", true);
    }

    void OnCollisionExit(Collision other) {
        isGrounded = false;
    }

    // for collecting things
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;

            SetCountText();
        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        // hardcoded shiny
        if(count >= 10)
        {
            winTextObject.SetActive(true);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

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

    void OnCollisionStay(Collision other) {
        isGrounded = true;
        animator.SetBool("IsWalking", false);
        animator.SetBool("IsIdle", true);
    }

    void OnCollisionExit(Collision other) {
        isGrounded = false;
    }
  
    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;

        // animations
        animator.SetBool("IsWalking", true);
        animator.SetBool("IsIdle", false);
        beginTextObject.SetActive(false);

    }

    void OnJump()
    {
        if(isGrounded)
        {
            rb.AddForce(jump * jumpForce, ForceMode.Impulse);
            isGrounded = false;
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

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        rb.AddForce(movement * moveSpeed);
    }

    // for collecting things
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;

            SetCountText();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public Vector3 jump;
    public float jumpForce = 2.0f;
    public bool isGrounded;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
 
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
        winTextObject.SetActive(false);
    }

    void OnCollisionStay(Collision other) {
        isGrounded = true;
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
    }

    void OnJump(InputValue jumpValue)
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
        if(count >= 12)
        {
            winTextObject.SetActive(true);
        }
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        rb.AddForce(movement * speed);
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

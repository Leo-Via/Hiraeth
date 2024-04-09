using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    AudioManager audioManager;

    private void Awake()
    {
        GameObject audioObject = GameObject.FindGameObjectWithTag("Audio");
        if (audioObject != null)
        {
            audioManager = audioObject.GetComponent<AudioManager>();
            if (audioManager == null)
            {
                Debug.LogError("AudioManager component not found on object with tag 'Audio'");
            }
        }
        else
        {
            Debug.LogError("No GameObject with tag 'Audio' found in the scene");
        }
    }

    public float moveSpeed = 5f; // Adjust this value to change the movement speed
    public float jumpForce = 10f; // Adjust this value to change the jump force

    const float groundCheckRadius = 0.2f;

    bool isGrounded = false;

    private Rigidbody2D rb; // Reference to the Rigidbody2D component
    private SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer component
    private Animator animator; // Reference to the Animator component

    [SerializeField] Transform groundCheckCollider;
    [SerializeField] LayerMask groundLayer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component attached to the player
        spriteRenderer = GetComponent<SpriteRenderer>(); // Get the SpriteRenderer component attached to the player
        animator = GetComponent<Animator>(); // Get the Animator component attached to the player
    }

    void Update()
    {
        // Get the horizontal input axis (left/right)
        float horizontalInput = Input.GetAxis("Horizontal");
        Debug.Log("Horizontal Input: " + horizontalInput);

        // Move the player horizontally
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);

        // Flip the player's sprite if moving left
        if (horizontalInput < 0)
        {
            spriteRenderer.flipX = true;
        }
        // Flip the player's sprite if moving right
        else if (horizontalInput > 0)
        {
            spriteRenderer.flipX = false;
        }

        GroundCheck();

        // Check for jump input
        if (Input.GetButtonDown("Jump"))
        {
            GroundCheck();
            // Jump if the player is grounded
            if (isGrounded)
            {
                rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);

                // Set 'IsJumping' parameter to true to transition to jump animation
                animator.SetBool("IsJumping", true);

                // Audio for jumping
                audioManager.PlaySFX(audioManager.Jump);
            }
        }

        // Check if the player has landed after falling
        if (rb.velocity.y <= 0 && isGrounded)
        {
            // Reset the 'IsJumping' parameter in the Animator
            animator.SetBool("IsJumping", false);
        }

        // Set the yVelocity in the animator 
        animator.SetFloat("yVelocity", rb.velocity.y);

        // Set the 'Speed' parameter in the Animator based on movement speed
        animator.SetFloat("Speed", Mathf.Abs(horizontalInput));

        // Check if the player initiates an attack action
        if (Input.GetButtonDown("Fire1")) // Example: "Fire1" could be the attack button
        {
            // Trigger the attack animation in the Animator
            animator.SetTrigger("Attack");
        }

    }

    void GroundCheck()
    {
        isGrounded = false;
        // Check if the GroundCheckObject is colliding with other
        // 2D Colliders that are in the "Ground" Layer
        // If yes (isGrounded true) else (isGrounded false)
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckCollider.position, groundCheckRadius, groundLayer);
        if (colliders.Length > 0)
            isGrounded = true;

        Debug.Log("Is Grounded: " + isGrounded);
        // As long as we are grounded the "IsJumping" bool
        // in animator is disabled 
        animator.SetBool("IsJumping", !isGrounded);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Adjust this value to change the movement speed
    public float jumpForce = 10f; // Adjust this value to change the jump force

    private Rigidbody2D rb; // Reference to the Rigidbody2D component
    private SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer component
    private Animator animator; // Reference to the Animator component

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

        // Set the 'Speed' parameter in the Animator based on movement speed
        animator.SetFloat("Speed", Mathf.Abs(horizontalInput));

        // Check if the player is grounded (you can implement your own ground check logic here)
        bool isGrounded = IsGrounded();

        // Jump if the player is grounded and the jump button is pressed
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);

            // Trigger the 'Jump' animation in the Animator
            animator.SetTrigger("Jump");
        }

        // Check if the player initiates an attack action
        if (Input.GetButtonDown("Fire1")) // Example: "Fire1" could be the attack button
        {
            // Trigger the attack animation in the Animator
            animator.SetTrigger("Attack");
        }
    }

    void FixedUpdate()
    {
        // Check if the player is falling
        if (rb.velocity.y < 0 && !IsGrounded())
        {
            // Trigger the fall animation
            animator.SetBool("IsFalling", true);
        }

        // Check if the player has landed after falling
        if (rb.velocity.y <= 0 && IsGrounded())
        {
            // Reset the 'IsFalling' parameter in the Animator
            animator.SetBool("IsFalling", false);

            // Check if the player is moving horizontally
            float horizontalInput = Input.GetAxis("Horizontal");
            if (Mathf.Abs(horizontalInput) > 0.1f)
            {
                // Set the 'Speed' parameter in the Animator based on movement speed
                animator.SetFloat("Speed", Mathf.Abs(horizontalInput));
            }
            else
            {
                // Reset the 'Speed' parameter in the Animator
                animator.SetFloat("Speed", 0f);
            }
        }
    }

    private bool IsGrounded()
    {
        // Implement your own ground check logic here
        // For simplicity, we'll use a raycast below the player to check for ground
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.1f);
        return hit.collider != null;
    }
}
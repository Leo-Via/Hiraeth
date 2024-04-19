using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    private float initialBounce = 14.5f;
    private float bounceMultiplier = 1.425f; // Multiplier for increasing bounce height on subsequent interactions
    private int interactions = 0; // Number of times the player has interacted with the JumpPad
    private int maxInteractions = 2; // Maximum number of interactions allowed

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();

            // Calculate the bounce force based on the number of interactions
            float currentBounce = initialBounce * Mathf.Pow(bounceMultiplier, interactions);

            // Apply the calculated bounce force
            playerRb.AddForce(Vector2.up * currentBounce, ForceMode2D.Impulse);

            interactions++;

            // Check if the maximum number of interactions has been reached
            if (interactions == maxInteractions)
            {
                // Optionally, disable the JumpPad or adjust its behavior
                interactions = 0;
            }
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallAndUp : MonoBehaviour
{

    Vector2 initialPosition;
    Rigidbody2D rb;

    [SerializeField] float fallDelay, respawnTime;

    private void Start()
    {
        // Store the initial position of the platform
        initialPosition = transform.position;

        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine("Fall");
        }

    }

    IEnumerator Fall()
    {
        yield return new WaitForSeconds(fallDelay);
        rb.bodyType = RigidbodyType2D.Dynamic;
        yield return new WaitForSeconds(respawnTime);
        Reset();
    }


    private void Reset()
    {

        rb.bodyType = RigidbodyType2D.Static;
        transform.position = initialPosition;
    }
}

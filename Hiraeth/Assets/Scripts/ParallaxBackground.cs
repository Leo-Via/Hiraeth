using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    public float parallaxSpeed = 0.02f; // Adjust this value to control the speed of parallax scrolling

    private Transform cameraTransform;
    private Vector3 lastCameraPosition;

    private float textureUnitSizeX;

    private void Start()
    {
        cameraTransform = Camera.main.transform;
        lastCameraPosition = cameraTransform.position;

        // Get the width of the background sprite
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        textureUnitSizeX = spriteRenderer.bounds.size.x;
    }

    private void FixedUpdate()
    {
        // Calculate the amount of movement based on the camera's movement
        float deltaX = cameraTransform.position.x - lastCameraPosition.x;
        Vector3 backgroundPosition = transform.position + Vector3.right * deltaX * parallaxSpeed;

        // If the background has moved enough to create a looping effect, reset its position
        if (Mathf.Abs(cameraTransform.position.x - backgroundPosition.x) >= textureUnitSizeX)
        {
            float offsetPositionX = (cameraTransform.position.x - backgroundPosition.x) % textureUnitSizeX;
            backgroundPosition = new Vector3(cameraTransform.position.x + offsetPositionX, transform.position.y, transform.position.z);
        }

        // Update the background's position
        transform.position = backgroundPosition;

        // Update the camera's last position
        lastCameraPosition = cameraTransform.position;
    }
}
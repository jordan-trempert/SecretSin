using UnityEngine;

public class SpriteBounce : MonoBehaviour
{
    public float bounceAmplitude = 0.5f; // Height of the bounce
    public float bounceFrequency = 1f; // Speed of the bounce

    private Vector3 startPosition; // Initial position of the sprite

    void Start()
    {
        startPosition = transform.position; // Store the starting position
    }

    void Update()
    {
        // Calculate the new Y position based on a sine wave
        float newY = startPosition.y + Mathf.Sin(Time.time * bounceFrequency) * bounceAmplitude;

        // Set the sprite's position with the new Y value
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);
    }
}

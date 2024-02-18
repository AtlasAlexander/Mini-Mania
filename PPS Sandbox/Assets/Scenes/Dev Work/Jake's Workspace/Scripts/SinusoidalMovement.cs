using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinusoidalMovement : MonoBehaviour
{
    public float verticalMoveDistance = 1.0f; // Vertical distance to move up and down
    public float verticalMoveSpeed = 1.0f; // Speed of vertical movement

    public float horizontalMoveDistance = 1.0f; // Horizontal distance to move left and right
    public float horizontalMoveSpeed = 1.0f; // Speed of horizontal movement

    private Vector3 startPos; // Starting position of the object
    private float timer = 0.0f; // Timer for movement

    void Start()
    {
        startPos = transform.position; // Set the starting position
    }

    void Update()
    {
        // Calculate the new vertical position based on sine wave
        float newVerticalY = startPos.y + Mathf.Sin(timer * verticalMoveSpeed) * verticalMoveDistance;
        // Calculate the new horizontal position based on sine wave
        float newHorizontalX = startPos.x + Mathf.Cos(timer * horizontalMoveSpeed) * horizontalMoveDistance;

        // Update the object's position
        transform.position = new Vector3(newHorizontalX, newVerticalY, transform.position.z);

        // Increment the timer
        timer += Time.deltaTime;
    }
}

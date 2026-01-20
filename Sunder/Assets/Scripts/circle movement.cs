using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class circlemovement : MonoBehaviour
{
    public float angularSpeed = 1f; // Speed of rotation in radians per second
    public float circleRadius = 1f; // Radius of the circle
    private Vector2 fixedPoint; // The center of the circle
    private float currentAngle;

    void Start()
    {
        // Set the current position as the center point (or set a specific center)
        fixedPoint = transform.position;
    }

    void Update()
    {
        // Increase the angle over time
        currentAngle += angularSpeed * Time.deltaTime;

        // Calculate the new position using sine and cosine
        float x = Mathf.Cos(currentAngle) * circleRadius;
        float y = Mathf.Sin(currentAngle) * circleRadius;
        Vector2 offset = new Vector2(x, y);

        // Update the object's position
        transform.position = fixedPoint + offset;
    }
}

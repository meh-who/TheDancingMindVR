using UnityEngine;

public class CircleMovement : MonoBehaviour
{
    public bool startMoving = false; // Trigger to start moving
    public float radius = 5f; // Radius of the circle
    public float speed = 1f; // Speed of movement
    private Vector3 centerPosition; // Center position of the circle
    private float angle = 0f; // Current angle of rotation around the circle

    private void Start()
    {
        // Get the center position of the circle (assumes the circle is centered at the object's initial position)
        centerPosition = transform.position;
    }

    private void Update()
    {
        // Only move if the trigger is set to true
        if (startMoving)
        {
            // Update the angle of rotation around the circle based on the current time and speed
            angle += speed * Time.deltaTime;

            // Calculate the new position based on the current angle and radius
            Vector3 newPosition = centerPosition + new Vector3(Mathf.Cos(angle), 0f, Mathf.Sin(angle)) * radius;

            // Move the object to the new position
            transform.position = newPosition;
        }
    }

    // Utility method to visualize the circle in the editor (not necessary for the movement script to work)
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
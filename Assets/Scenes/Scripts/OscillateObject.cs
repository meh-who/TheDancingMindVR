using UnityEngine;

public class OscillateObject : MonoBehaviour
{
    public float speed = 2f;
    public float amplitude = 1f;
    public bool startMoving = false;

    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        if (startMoving)
        {
            float yOffset = Mathf.Sin(Time.time * speed) * amplitude;
            transform.position = initialPosition + new Vector3(0f, yOffset, 0f);
        }
    }

    public void TriggerMove()
    {
        startMoving = !   startMoving;
    }
}

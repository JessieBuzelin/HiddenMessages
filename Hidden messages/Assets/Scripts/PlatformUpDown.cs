using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float moveDistance = 2f; // Total distance to move
    public float moveSpeed = 2f; // Speed of the movement

    private Vector3 startPosition;
    private Vector3 targetPosition;
    private float journeyLength;
    private float startTime;

    private void Start()
    {
        startPosition = transform.position; // Record the starting position
        targetPosition = startPosition + Vector3.up * moveDistance; // Set target position
        journeyLength = Vector3.Distance(startPosition, targetPosition);
        startTime = Time.time;
    }

    private void Update()
    {
        float distanceCovered = (Time.time - startTime) * moveSpeed; // Calculate how far to move
        float fractionOfJourney = distanceCovered / journeyLength; // Calculate the fraction of the journey

        // Move the platform up and down
        if (fractionOfJourney <= 1)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, fractionOfJourney);
        }
        else
        {
            // Swap start and target positions to move back down
            Vector3 temp = startPosition;
            startPosition = targetPosition;
            targetPosition = temp;
            startTime = Time.time; // Reset start time for the next journey
        }
    }
}

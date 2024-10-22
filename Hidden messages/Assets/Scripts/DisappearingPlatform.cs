using UnityEngine;
using System.Collections;

public class PlatformRespawner : MonoBehaviour
{
    public float destroyTime = 2f; // Time before the platform is destroyed
    public float respawnTime = 3f; // Time before the platform respawns

    private Vector3 initialPosition;
    private bool isActive = true; // Track if the platform is active

    private void Start()
    {
        // Store the initial position of the platform
        initialPosition = transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collider is tagged as "Player"
        if (collision.gameObject.CompareTag("Player") && isActive)
        {
            StartCoroutine(RespawnPlatform());
        }
    }

    private IEnumerator RespawnPlatform()
    {
        isActive = false; // Prevent multiple triggers

        // Wait for the destroy time
        yield return new WaitForSeconds(destroyTime);
        // Disable the platform
        gameObject.SetActive(false);

        // Wait for the respawn time
        yield return new WaitForSeconds(respawnTime);
        // Respawn the platform
        Respawn();

        isActive = true; // Reset the state
    }

    private void Respawn()
    {
        // Reset the platform position and enable it
        transform.position = initialPosition;
        gameObject.SetActive(true);
    }
}

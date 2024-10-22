using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int health = 3; // Player's health

    public void Restart()
    {
        // Get the current scene
        Scene currentScene = SceneManager.GetActiveScene();
        // Reload the current scene
        SceneManager.LoadScene(currentScene.name);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Player takes damage. Current health: " + health);

        // Add logic to handle player death if health <= 0
        if (health <= 0)
        {
            Debug.Log("Player has died.");
            Restart();
            // Handle player death (e.g., restart the game, show game over screen)
        }
    }
}

using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health = 3; // Enemy's health

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Enemy takes damage. Current health: " + health);

        // Handle enemy death if health <= 0
        if (health <= 0)
        {
            Debug.Log("Enemy has died.");
            // Handle enemy death (e.g., disable, destroy, or play animation)
            Destroy(gameObject); // Example: destroy the enemy GameObject
        }
    }
}

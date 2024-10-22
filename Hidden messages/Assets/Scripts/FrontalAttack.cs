using UnityEngine;

public class FrontalConeAttack : MonoBehaviour
{
    public float attackRange = 5f; // Maximum distance of the attack
    public float coneAngle = 45f; // Angle of the attack cone
    public int damage = 3; // Damage dealt to enemies
    public GameObject attackEffectPrefab; // Reference to the particle effect prefab

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) // Use space to trigger the attack
        {
            PerformAttack();
            Debug.Log("Frontal!");
        }
    }

    private void PerformAttack()
    {
        // Get all colliders in the attack range
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRange);
        InstantiateAttackEffect(transform.position);
        foreach (Collider collider in hitColliders)
        {
            // Check if the collider has the "Enemy" tag and is within the attack cone
            if (collider.CompareTag("Enemy") && IsInAttackCone(collider.transform))
            {
                // Apply damage to the enemy
                EnemyHealth enemyHealth = collider.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(damage); // Apply damage
                    Debug.Log("Hit " + collider.name + " for " + damage + " damage!");

                }
            }
        }
    }

    private bool IsInAttackCone(Transform target)
    {
        // Calculate direction to the target
        Vector3 directionToTarget = (target.position - transform.position).normalized;

        // Calculate the forward direction of the attacker
        Vector3 forward = transform.forward;

        // Check the angle between the forward direction and the direction to the target
        float angle = Vector3.Angle(forward, directionToTarget);

        // Check if the target is within the cone's angle
        return angle <= coneAngle / 2f;
    }

    private void InstantiateAttackEffect(Vector3 position)
    {
        if (attackEffectPrefab != null)
        {
            // Instantiate the particle effect at the specified position
            GameObject effect = Instantiate(attackEffectPrefab, position, Quaternion.identity);
            Destroy(effect, 2f); // Destroy the effect after 2 seconds (adjust as needed)
        }
    }

    private void OnDrawGizmos()
    {
        // Draw the attack range and cone in the editor for visualization
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange); // Attack range

        Vector3 leftBoundary = Quaternion.Euler(0, -coneAngle / 2f, 0) * transform.forward * attackRange;
        Vector3 rightBoundary = Quaternion.Euler(0, coneAngle / 2f, 0) * transform.forward * attackRange;

        Gizmos.DrawLine(transform.position, transform.position + leftBoundary);
        Gizmos.DrawLine(transform.position, transform.position + rightBoundary);
    }
}

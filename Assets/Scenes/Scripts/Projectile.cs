using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 1;
    public Vector2 direction = Vector2.right;
    public GameObject shooter;

    private bool hasHit = false;

    void Update()
    {
        // Move the projectile forward
        transform.Translate(direction.normalized * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (hasHit) return; // Already hit something, ignore further actions
        if (other.gameObject == shooter) return; // Don't hit myself

        // Look for PlayerHealth on the parent of what was hit
        PlayerHealth target = other.GetComponentInParent<PlayerHealth>();

        if (target != null)
        {
            hasHit = true;
            target.TakeDamage(damage);
            Destroy(gameObject);
        }
        else if (!other.isTrigger)
        {
            // Destroy if it hits a wall or non-trigger
            Destroy(gameObject);
        }
    }
}

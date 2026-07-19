using UnityEngine;

/// <summary>
/// Simple back-and-forth patrol enemy.
/// Damages the player on contact. Turns red when near player.
/// </summary>
public class EnemyPatrol : MonoBehaviour
{
    [Header("Patrol")]
    public float patrolDistance = 6f;
    public float speed = 2f;

    [Header("Combat")]
    public float damageInterval = 1f;
    public int damage = 1;

    Vector3 startPos;
    Vector3 targetPos;
    bool movingForward = true;
    float damageCooldown = 0f;
    Renderer rend;
    Color baseColor = new Color(0.7f, 0.1f, 0.1f);

    void Start()
    {
        startPos = transform.position;
        targetPos = startPos + transform.forward * patrolDistance;
        rend = GetComponent<Renderer>();
        if (rend != null)
            rend.material.color = baseColor;
    }

    void Update()
    {
        if (GameManager.Instance != null && !GameManager.Instance.IsGameActive()) return;

        // Patrol movement
        Vector3 dest = movingForward ? targetPos : startPos;
        transform.position = Vector3.MoveTowards(transform.position, dest, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, dest) < 0.1f)
            movingForward = !movingForward;

        // Face movement direction
        Vector3 dir = (dest - transform.position);
        if (dir.magnitude > 0.01f)
            transform.forward = dir.normalized;

        if (damageCooldown > 0f) damageCooldown -= Time.deltaTime;
    }

    void OnCollisionStay(Collision col)
    {
        if (!col.gameObject.CompareTag("Player")) return;
        if (damageCooldown > 0f) return;
        damageCooldown = damageInterval;
        GameManager.Instance?.TakeDamage(damage);
    }

    void OnCollisionEnter(Collision col)
    {
        if (!col.gameObject.CompareTag("Player")) return;
        damageCooldown = damageInterval;
        GameManager.Instance?.TakeDamage(damage);
    }
}

using UnityEngine;

/// <summary>
/// Spike trap on the floor. Pulses up and down.
/// Damages the player when the spikes are raised.
/// </summary>
public class SpikeTrap : MonoBehaviour
{
    public float cycleTime = 2f;
    public float raisedHeight = 0.6f;
    public int damage = 1;

    Vector3 lowPos;
    Vector3 highPos;
    float timer = 0f;
    bool isRaised = false;
    float damageCooldown = 0f;

    void Start()
    {
        lowPos = transform.position;
        highPos = lowPos + Vector3.up * raisedHeight;
    }

    void Update()
    {
        if (GameManager.Instance != null && !GameManager.Instance.IsGameActive()) return;

        timer += Time.deltaTime;
        float t = Mathf.PingPong(timer / cycleTime, 1f);
        transform.position = Vector3.Lerp(lowPos, highPos, t);
        isRaised = t > 0.5f;

        if (damageCooldown > 0f) damageCooldown -= Time.deltaTime;
    }

    void OnTriggerStay(Collider col)
    {
        if (!isRaised) return;
        if (!col.CompareTag("Player")) return;
        if (damageCooldown > 0f) return;
        damageCooldown = 1.5f;
        GameManager.Instance?.TakeDamage(damage);
    }
}

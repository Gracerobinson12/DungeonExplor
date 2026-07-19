using UnityEngine;

/// <summary>
/// Health pickup — restores 1 health when walked over.
/// Rotates slowly for visibility.
/// </summary>
public class HealthPickup : MonoBehaviour
{
    public float rotateSpeed = 60f;
    public float collectRadius = 1.2f;
    public int healAmount = 1;

    Transform player;

    void Start()
    {
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null) player = p.transform;
    }

    void Update()
    {
        transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
        if (player == null) return;
        if (Vector3.Distance(transform.position, player.position) < collectRadius)
        {
            GameManager.Instance?.HealPlayer(healAmount);
            Destroy(gameObject);
        }
    }
}

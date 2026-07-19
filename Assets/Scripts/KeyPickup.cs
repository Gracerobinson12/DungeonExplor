using UnityEngine;

/// <summary>
/// Collectible key. Rotates and bobs slowly.
/// Auto-collects when player walks close.
/// </summary>
public class KeyPickup : MonoBehaviour
{
    public float rotateSpeed = 90f;
    public float bobSpeed = 1.5f;
    public float bobHeight = 0.2f;
    public float collectRadius = 1.5f;

    Transform player;
    Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null) player = p.transform;
    }

    void Update()
    {
        transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
        transform.position = startPos + Vector3.up * Mathf.Sin(Time.time * bobSpeed) * bobHeight;

        if (player == null) return;
        if (Vector3.Distance(transform.position, player.position) < collectRadius)
        {
            GameManager.Instance?.CollectKey();
            Destroy(gameObject);
        }
    }
}

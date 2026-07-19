using UnityEngine;

/// <summary>
/// A locked door that requires the player to have collected
/// at least one key to open. Separate from the final ExitDoor.
/// Shows prompt when player is near.
/// </summary>
public class LockedDoor : MonoBehaviour
{
    public int keysRequired = 2;
    public float openSpeed = 2f;
    public float interactDistance = 2.5f;

    bool isOpen = false;
    Vector3 closedPos;
    Vector3 openPos;
    Transform player;

    void Start()
    {
        closedPos = transform.position;
        openPos = closedPos + Vector3.up * 4f;
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null) player = p.transform;
    }

    void Update()
    {
        if (isOpen)
        {
            transform.position = Vector3.MoveTowards(
                transform.position, openPos, openSpeed * Time.deltaTime);
            return;
        }

        if (player == null || GameManager.Instance == null) return;

        float dist = Vector3.Distance(transform.position, player.position);
        if (dist < interactDistance)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                // Try to open
                isOpen = true;
                GameManager.Instance.SetInteractionPrompt("Door unlocked!");
            }
            else
            {
                GameManager.Instance.SetInteractionPrompt(
                    "[E] Unlock door (needs " + keysRequired + " keys)");
            }
        }
        else
        {
            GameManager.Instance.SetInteractionPrompt("");
        }
    }
}

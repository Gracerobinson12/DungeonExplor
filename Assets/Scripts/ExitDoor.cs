using UnityEngine;

/// <summary>
/// The dungeon exit door. Opens when ALL keys are collected.
/// Triggers win condition when player enters.
/// </summary>
public class ExitDoor : MonoBehaviour
{
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
        }

        if (!isOpen && GameManager.Instance != null && GameManager.Instance.AllKeysCollected())
        {
            isOpen = true;
        }

        if (player == null || GameManager.Instance == null) return;

        float dist = Vector3.Distance(transform.position, player.position);
        if (dist < interactDistance)
        {
            if (!isOpen)
                GameManager.Instance.SetInteractionPrompt("Collect all keys to open the exit!");
            else
                GameManager.Instance.SetInteractionPrompt("You escaped! Press [E] to win!");

            if (isOpen && Input.GetKeyDown(KeyCode.E))
                GameManager.Instance.TriggerWin();
        }
        else
        {
            GameManager.Instance.SetInteractionPrompt("");
        }
    }
}

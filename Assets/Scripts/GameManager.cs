using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

/// <summary>
/// Central game state for Checkpoint 2:
/// - Key collection tracking
/// - Health / damage system
/// - Countdown timer
/// - Win / lose conditions with UI feedback
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Game Settings")]
    public int totalKeys = 5;
    public float gameDuration = 120f;
    public int maxHealth = 3;

    [Header("UI")]
    public TextMeshProUGUI keyCountText;
    public TextMeshProUGUI objectiveText;
    public TextMeshProUGUI interactionPromptText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI timerText;
    public GameObject winPanel;
    public GameObject losePanel;

    [Header("Audio")]
    public AudioSource musicSource;
    public AudioSource sfxSource;
    public AudioClip keyPickupClip;
    public AudioClip hurtClip;
    public AudioClip winClip;
    public AudioClip loseClip;
    public AudioClip healthPickupClip;

    int keysCollected = 0;
    int currentHealth;
    float timeRemaining;
    bool gameOver = false;
    bool gameWon = false;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        currentHealth = maxHealth;
        timeRemaining = gameDuration;
        keysCollected = 0;
        gameOver = false;
        gameWon = false;

        if (winPanel != null) winPanel.SetActive(false);
        if (losePanel != null) losePanel.SetActive(false);

        if (musicSource != null) musicSource.Play();

        UpdateAllUI();
        SetObjectiveText("Find all " + totalKeys + " keys and escape!");
    }

    void Update()
    {
        if (gameOver || gameWon) return;

        timeRemaining -= Time.deltaTime;
        if (timeRemaining <= 0f)
        {
            timeRemaining = 0f;
            TriggerLose("Time's up!");
        }

        UpdateTimerUI();
    }

    // ---- Key system ----
    public void CollectKey()
    {
        if (gameOver || gameWon) return;
        keysCollected++;
        PlaySFX(keyPickupClip);
        UpdateKeyUI();

        if (AllKeysCollected())
            SetObjectiveText("All keys collected! Find the exit!");
        else
            SetObjectiveText("Keys: " + keysCollected + "/" + totalKeys + " — keep exploring!");
    }

    public bool AllKeysCollected() => keysCollected >= totalKeys;

    // ---- Health system ----
    public void TakeDamage(int amount = 1)
    {
        if (gameOver || gameWon) return;
        currentHealth -= amount;
        PlaySFX(hurtClip);
        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            TriggerLose("You died!");
        }
    }

    public void HealPlayer(int amount = 1)
    {
        if (gameOver || gameWon) return;
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        PlaySFX(healthPickupClip);
        UpdateHealthUI();
    }

    // ---- Win / Lose ----
    public void TriggerWin()
    {
        if (gameOver || gameWon) return;
        gameWon = true;
        if (musicSource != null) musicSource.Stop();
        PlaySFX(winClip);
        if (winPanel != null) winPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void TriggerLose(string reason = "")
    {
        if (gameOver || gameWon) return;
        gameOver = true;
        if (musicSource != null) musicSource.Stop();
        PlaySFX(loseClip);
        if (losePanel != null) losePanel.SetActive(true);
        SetObjectiveText(reason);
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // ---- UI helpers ----
    void UpdateAllUI()
    {
        UpdateKeyUI();
        UpdateHealthUI();
        UpdateTimerUI();
    }

    void UpdateKeyUI()
    {
        if (keyCountText != null)
            keyCountText.text = "Keys: " + keysCollected + " / " + totalKeys;
    }

    void UpdateHealthUI()
    {
        if (healthText != null)
        {
            string hearts = "";
            for (int i = 0; i < maxHealth; i++)
                hearts += (i < currentHealth) ? "♥ " : "♡ ";
            healthText.text = hearts.Trim();
        }
    }

    void UpdateTimerUI()
    {
        if (timerText != null)
        {
            int secs = Mathf.CeilToInt(timeRemaining);
            timerText.text = "Time: " + secs + "s";
            timerText.color = timeRemaining < 20f ? Color.red : Color.white;
        }
    }

    public void SetObjectiveText(string msg)
    {
        if (objectiveText != null) objectiveText.text = msg;
    }

    public void SetInteractionPrompt(string msg)
    {
        if (interactionPromptText != null) interactionPromptText.text = msg;
    }

    void PlaySFX(AudioClip clip)
    {
        if (sfxSource != null && clip != null)
            sfxSource.PlayOneShot(clip);
    }

    public bool IsGameActive() => !gameOver && !gameWon;
}

using UnityEngine;
using TMPro; // For UI elements like health bar

public class PlayerBehaviour : MonoBehaviour
{
    public int maxHealth = 100;
    public float currentHealth;
    public HealthBar healthbar;
    public AudioClip damageSound; // Assign in Inspector
    private AudioSource audioSource; // Reference to the AudioSource component for sound effects

    private float damageTimer = 0f;
    public float damageInterval = 1f; // seconds between damage ticks

    public Vector3 respawnPoint; // Assign this in the Inspector

    int points = 0;
    CoinBehaviour currentCoin;
    public TMP_Text scoreText; // Assign in Inspector

    private DoorBehaviour currentDoor;
    bool canInteract = false;

    void Start()
    {
        currentHealth = maxHealth;  // Initialize health bar with max health
        healthbar.SetSlider(currentHealth);
        Debug.Log("Player initialized with max health: " + maxHealth);
    }

    void Awake()
    {
        audioSource = GetComponent<AudioSource>(); // Get the AudioSource component
        if (audioSource == null)
        {
            Debug.LogWarning("AudioSource component not found on PlayerBehaviour.");
        }
    }

    public void UpdateScoreUI()
    {
        scoreText.text = "Score: " + points;
    }

    public void ModifyScore(CoinBehaviour currentCoin)
    {
        points += currentCoin.coinValue;
        UpdateScoreUI();
        Debug.Log("Score" + points);
    }


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Collectible"))
        {
            currentCoin = collision.gameObject.GetComponent<CoinBehaviour>();
            if (currentCoin != null)
            {
                currentCoin.Collect(this);
            }
        }

        else if (collision.gameObject.CompareTag("healArea"))
        {
            HealBox healArea = collision.gameObject.GetComponent<HealBox>();
            if (healArea != null)
            {
                currentHealth += healArea.healAmount;
                if (currentHealth > maxHealth)
                    currentHealth = maxHealth; // Ensure health does not exceed max health
                healthbar.SetSlider(currentHealth); // Update health bar
                Debug.Log("Player healed! Current health: " + currentHealth);
                healArea.PlayHealSound(); // Play healing sound
                Destroy(healArea.gameObject); // Destroy the heal box after use
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("damageArea"))
        {
            DamageArea damageArea = other.gameObject.GetComponent<DamageArea>();
            if (damageArea != null)
            {
                currentHealth -= damageArea.damageAmount;
                if (damageSound != null && audioSource != null)
                {
                    audioSource.PlayOneShot(damageSound);
                }
                if (currentHealth < 0)
                    currentHealth = 0; // Ensure health does not go below zero
                healthbar.SetSlider(currentHealth);
                Debug.Log("Player damaged! Current health: " + currentHealth);
                if (currentHealth == 0)
                {
                    Die();  // Call the Die method if health reaches zero
                }
            }
        }

        else if (other.gameObject.CompareTag("Door"))
        {
            DoorBehaviour door = other.gameObject.GetComponent<DoorBehaviour>();
            if (door != null)
            {
                currentDoor = other.GetComponent<DoorBehaviour>();
                canInteract = true; // Allow interaction with the door
                Debug.Log("Player can interact with the door.");
            }
        }

    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Door"))
        {
            DoorBehaviour door = other.gameObject.GetComponent<DoorBehaviour>();
            if (door != null && currentDoor == door)
            {
                door.Close(); // Close the door when exiting
                currentDoor = null;
                canInteract = false; // Disable interaction with the door
                Debug.Log("Player exited door interaction.");
            }
        }

        else if (other.gameObject.CompareTag("damageArea"))
        {
            damageTimer = 0f; // Reset damage timer when exiting the damage area
            Debug.Log("Player exited damage area.");
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("damageArea"))
        {
            damageTimer += Time.deltaTime;
            if (damageTimer >= damageInterval)
            {
                DamageArea damageArea = other.gameObject.GetComponent<DamageArea>();
                if (damageArea != null)
                {
                    currentHealth -= damageArea.damageAmount;
                    if (damageSound != null && audioSource != null)
                    {
                        audioSource.PlayOneShot(damageSound);
                    }
                    if (currentHealth < 0)
                        currentHealth = 0;
                    healthbar.SetSlider(currentHealth);
                    Debug.Log("Player taking damage! Current health: " + currentHealth);
                    if (currentHealth == 0)
                    {
                        Die();
                    }
                }
                damageTimer = 0f; // Reset timer after applying damage
            }
        }
    }

    public void OnInteract()
    {
        if (canInteract)
        {
            if (currentDoor != null)
            {
                Debug.Log("Interacting with door.");
                currentDoor.Interact();
            }
        }
    }

    void Die()
    {
        Debug.Log("Player has died.");
        currentHealth = maxHealth; // Reset health to max
        healthbar.SetSlider(currentHealth); // Update health bar
        transform.position = respawnPoint; // Respawn at the set respawn point
    }

    public void SetRespawnPoint(Vector3 newPoint)
    {
        respawnPoint = newPoint;
        Debug.Log("Respawn point set to: " + respawnPoint);
    }

}

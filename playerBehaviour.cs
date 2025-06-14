using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthbar;

    int points = 0;
    CoinBehaviour currentCoin;

    void Start()
    {
        currentHealth = maxHealth;
        // Initialize health bar with max health
        healthbar.SetSlider(currentHealth);
        Debug.Log("Player initialized with max health: " + maxHealth);
    }
    public void ModifyScore(CoinBehaviour currentCoin)
    {
        points += currentCoin.coinValue;
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
                healthbar.SetSlider(currentHealth); // Update health bar
                Debug.Log("Player healed! Current health: " + currentHealth);
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
                healthbar.SetSlider(currentHealth);
                Debug.Log("Player damaged! Current health: " + currentHealth);
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


}
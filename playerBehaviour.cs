using UnityEngine;

public class playerBehaviour : MonoBehaviour
{
    int points = 0;
    int maxHealth = 100;
    coinBehaviour currentCoin; 

    public void ModifyScore()
    {
        points += currentCoin.coinValue;
        Debug.Log("Score" + points);
    }

    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Collectible"))
        {
            currentCoin = collision.gameObject.GetComponent<coinBehaviour>();
            if (currentCoin != null)
            {
                currentCoin.Collect(this);
            }
        }   
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("damageArea"))
        {
            damageArea damageArea = other.gameObject.GetComponent<damageArea>();
            if (damageArea != null)
            {
                currentHealth -= damageArea.damageAmount;
                Debug.Log("Player damaged! Current health: " + currentHealth);
            }
        }
    } 
}
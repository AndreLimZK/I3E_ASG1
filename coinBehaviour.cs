using UnityEngine;

public class coinBehaviour : MonoBehaviour
{
    public int coinValue = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Collect(playerBehaviour player)
    {
        // Add coin collection logic here
        player.ModifyScore();
        Destroy(gameObject);
    }
}

using UnityEngine;

public class playerBehaviour : MonoBehaviour
{
    int points = 0;
    int health = 100;
    coinehaviour currentCoin; 

    public void ModifyScore()
    {
        points += currentCoin.coinValue;
        Debug.Log("Score" + points);
    }

    
}
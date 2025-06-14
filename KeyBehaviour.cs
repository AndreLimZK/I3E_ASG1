using UnityEngine;

public class KeyBehaviour : MonoBehaviour
{
    public DoorBehaviour door;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (door != null)
            {
                door.DoorUnlock(); // Call the public method
            }
            Destroy(gameObject); // Destroy the key when the player collides with it
        }

    }
}
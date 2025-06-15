using System;
using UnityEngine;

public class KeyBehaviour : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DoorBehaviour[] allDoors = FindObjectsByType<DoorBehaviour>(FindObjectsSortMode.None);
            foreach (DoorBehaviour door in allDoors)
            {
                door.DoorUnlock();
            }
            Destroy(gameObject);
        }
    }

}
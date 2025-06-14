using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{
    private bool isOpen = false; //controls the state of the door
    public void Interact()
    {
        Vector3 doorRotation = transform.eulerAngles;
        if (!isOpen)
        {
            doorRotation.y -= 90f; // Rotate the door by 90 degrees
        }
        else
        {
            doorRotation.y += 90f; // Rotate the door back to closed position
        }
        transform.eulerAngles = doorRotation;
        isOpen = !isOpen;
    }
}
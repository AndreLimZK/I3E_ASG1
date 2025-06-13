using UnityEngine;
using UnityEngine.UI;

public class healthBar : MonoBehaviour
{
    public Slider healthSlider; // Reference to the UI Slider component

    public void SetSlider(float maxHealth)
    {
        healthSlider.maxValue = maxHealth; // Set the slider's max value
        healthSlider.value = maxHealth; // Set the slider value to the current health
    } 
}

using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Slider healthSlider;
    public PlayerHealth playerHealth;

    void Start()
    {
        InitializeHealthBar();
    }

    void InitializeHealthBar()
    {
        healthSlider.maxValue = playerHealth.maxHealth;
        healthSlider.value = playerHealth.maxHealth;
        UpdateHealthBar();
    }

    public void UpdateHealthBar()
    {
        Debug.Log("Current Health: " + playerHealth.CurrentHealth);
        healthSlider.value = playerHealth.CurrentHealth;
    }
}

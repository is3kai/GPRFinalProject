using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    public GameObject deathScreen;

    public UnityEvent OnHealthChanged = new UnityEvent();

    void Start()
    {
        currentHealth = maxHealth;
        UpdateUI();
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        UpdateUI();

        if (currentHealth <= 0)
        {
            Time.timeScale = 0f;
            deathScreen.SetActive(true);
        }
    }

    void UpdateUI()
    {
        OnHealthChanged.Invoke();
    }

    public int CurrentHealth
    {
        get { return currentHealth; }
    }
}
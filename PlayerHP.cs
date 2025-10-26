using UnityEngine;
using TMPro;

public class PlayerHP : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    public TextMeshProUGUI healthText;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthDisplay();
    }

    public void ApplyDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthDisplay();

        if (currentHealth <= 0)
        {
            HandleDeath();
        }
    }

    private void UpdateHealthDisplay()
    {
        healthText.text = currentHealth.ToString();
    }

    private void HandleDeath()
    {
        GameManager.instance.GameOver();
        // ปิดการควบคุมหรือแอนิเมชันตายได้ที่นี่
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Spike"))
        {
            ApplyDamage(20);
            Debug.Log("โดน spike แล้ว! HP เหลือ: " + currentHealth);
        }
    }
}

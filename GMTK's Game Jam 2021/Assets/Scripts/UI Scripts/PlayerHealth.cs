using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int startHealth;
    private int currentHealth;
    public string enemyTag;
    public Text healthText;

    private void Start()
    {
        currentHealth = startHealth;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag(enemyTag))
        {
            PlayerDamage(5);
        }
    }

    void PlayerDamage(int damage)
    {
        currentHealth -= damage;
    }

    private void Update()
    {
        healthText.text = currentHealth.ToString();

        if (currentHealth <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            currentHealth = startHealth;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int startHealth;
    private int currentHealth;
    public Slider healthCounter;
    public TextMeshProUGUI currentHealthText;

    private void Start()
    {
        currentHealth = startHealth;
    }

    // private void OnCollisionEnter(Collision other)
    // {
    //     if (other.collider.CompareTag("bullet"))
    //     {

    //     }

    // }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }

    private void Update()
    {
        healthCounter.value = currentHealth;
        currentHealthText.text = currentHealth.ToString();

        if (currentHealth <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            currentHealth = startHealth;
        }
    }
}

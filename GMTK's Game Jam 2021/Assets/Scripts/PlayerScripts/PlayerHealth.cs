using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int startHealth;
    private int currentHealth;
    public Text healthText;

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
        healthText.text = currentHealth.ToString();

        if (currentHealth <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            currentHealth = startHealth;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int startHealth;
    private int currentHealth;
    public Slider healthCounter;

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

        if (currentHealth <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            currentHealth = startHealth;
        }
    }
}

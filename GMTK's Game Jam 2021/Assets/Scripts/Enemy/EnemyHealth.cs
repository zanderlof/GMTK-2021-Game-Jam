using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int starthealth;
    private int currentHealth;
    EnemyManager manager;

    void Start()
    {
        currentHealth = starthealth;
        manager = GameObject.FindGameObjectWithTag("EnemyManager").GetComponent<EnemyManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            manager.IDied(GetComponent<BasicEnemyStateMachine>());
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("bullet"))
        {
            currentHealth -= other.gameObject.GetComponent<BulletController>().bulletDamage;
            manager.ISaw();
        }

    }
}

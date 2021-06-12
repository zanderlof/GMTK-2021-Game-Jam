using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private float deathTime = 3f;

    // Update is called once per frame
    void Update()
    {
        deathTime -= Time.deltaTime;
        if (deathTime <= 0)
        {
            Object.Destroy(gameObject);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        Object.Destroy(gameObject);
    }
}

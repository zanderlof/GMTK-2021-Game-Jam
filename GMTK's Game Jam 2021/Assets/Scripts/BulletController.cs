using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private float deathTime = 3f;

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, deathTime);
    }

    public void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private float deathTime = 3f;
    public int bulletDamage;

    private PowerGenerator.Elemental type;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, deathTime);
    }

    public void OnCollisionEnter(Collision collision)
    {
        // Destroy(gameObject);
    }

    public void SetType(PowerGenerator.Elemental element)
    {
        type = element;

        if (type == PowerGenerator.Elemental.electricity)
        {
            GetComponent<TrailRenderer>().material.color = Color.yellow;
        }
        else if(type == PowerGenerator.Elemental.fire)
        {
            GetComponent<TrailRenderer>().material.color = Color.red;
        }
        else if (type == PowerGenerator.Elemental.water)
        {
            GetComponent<TrailRenderer>().material.color = Color.blue;
        }
        
    }
}

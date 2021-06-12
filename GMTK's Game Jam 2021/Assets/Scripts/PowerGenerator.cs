using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerGenerator : MonoBehaviour
{
    public GunController gun;
    public float powerDistance;

    private float distance;
    bool powering;

    // Start is called before the first frame update
    void Start()
    {
        powering = false;
    }

    // Update is called once per frame
    void Update()
    {
        //get distance to player's gun
        distance = Vector3.Distance(gun.gameObject.transform.position, transform.position);

        //if distance is close enough
        if(distance <= powerDistance)
        {
            gun.powerOn();
            powering = true;
        }
        else if(distance > powerDistance && powering)
        {
            gun.powerOff();
            powering = false;
        }

    }

    
}

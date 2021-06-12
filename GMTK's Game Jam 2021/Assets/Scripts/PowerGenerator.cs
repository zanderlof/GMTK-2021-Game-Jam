using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerGenerator : MonoBehaviour
{
    //public variables
    public GunController gun;
    public float powerDistance;

    //sounds
    public AK.Wwise.Event powerUp;
    public AK.Wwise.Event powerDown;

    //private variables
    private float distance;
    bool powering;
    bool previous;

    // Start is called before the first frame update
    void Start()
    {
        powering = false;
        previous = false;
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

        if (powering != previous)
        {
            previous = powering;

            if (powering)
            {
                powerUp.Post(gameObject);
            }
            else
            {
                powerDown.Post(gameObject);
            }
        }
    }

    
}

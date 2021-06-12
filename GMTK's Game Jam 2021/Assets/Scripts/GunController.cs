using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    //public variables
    public PlayerController player;
    public GameObject bullet;
    public float bulletSpeed;

    //sounds
    public AK.Wwise.Event fireBullet;

    //private variables
    private bool powered;
    private bool previous;

    // Start is called before the first frame update
    void Start()
    {
        powered = false;
        previous = false;
    }

    // Update is called once per frame
    void Update()
    {
        //aim gun up and down
        transform.eulerAngles += player.lookSpeed * new Vector3(-Input.GetAxis("Mouse Y"), 0, 0);

        //attacking
        if (Input.GetKeyDown(KeyCode.Mouse0) && powered)
        {
            Shoot();
        }

        
    }

    public void Shoot()
    {
        fireBullet.Post(gameObject);
        GameObject holder = Instantiate(bullet, transform.position, transform.localRotation);
        holder.GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed;
    }

    public void powerOn()
    {
        powered = true;
    }

    public void powerOff()
    {
        powered = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    //public variables
    public PlayerController player;
    public GameObject bullet;
    public float bulletSpeed;
    public float bulletSpawn;
    //sounds
    public AK.Wwise.Event fireBullet;

    //private variables
    private bool powered;
    private bool previous;

    //editor variabled
    [SerializeField] bool cursorLock = true;

    // Start is called before the first frame update
    void Start()
    {
        powered = false;
        previous = false;
        if(cursorLock){Cursor.lockState = CursorLockMode.Locked;}
    }

    // Update is called once per frame
    void Update()
    {
        //aim gun up and down
        transform.eulerAngles += player.lookSpeed * new Vector3(-Input.GetAxis("Mouse Y"), 0, 0);
        if(Input.GetKeyDown(KeyCode.Escape)){Cursor.lockState = Cursor.lockState == CursorLockMode.Locked ? CursorLockMode.None : CursorLockMode.Locked;}

        //attacking
        if (Input.GetKeyDown(KeyCode.Mouse0) && powered)
        {
            Shoot();
        }

        
    }

    public void Shoot()
    {
        fireBullet.Post(gameObject);
        GameObject holder = Instantiate(bullet, transform.position + (transform.forward * bulletSpawn), transform.localRotation);
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

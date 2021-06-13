using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    //public variables
    // public PlayerController player;
    public GameObject bullet;
    public float bulletSpeed;
    public Transform bulletSpawn;
    public int damage = 10;
    public bool continuousFire = false;
    public float FireRate = 1;
    //sounds
    public AK.Wwise.Event fireBullet;

    //private variables
    private bool powered;
    private bool previous;
    private PowerGenerator.Elemental type;
    private float nextTimeToFire;

    // Start is called before the first frame update
    void Start()
    {
        powered = false;
        // previous = false;
    }

    private void OnEnable()
    {
        // Debug.Log("HI");
    }

    // Update is called once per frame
    void Update()
    {
        // //aim gun up and down
        // transform.eulerAngles += player.lookSpeed * new Vector3(-Input.GetAxis("Mouse Y"), 0, 0);

        //attacking
        if (Input.GetKeyDown(KeyCode.Mouse0) && powered && !continuousFire)
        {
            Shoot();
        }
        else if (Input.GetKey(KeyCode.Mouse0) && powered && continuousFire)
        {
            Shoot();
        }
    }


    public void Shoot()
    {
        if (Time.time >= nextTimeToFire || !continuousFire)
        {
            fireBullet.Post(gameObject);
            GameObject holder = Instantiate(bullet, bulletSpawn.position, transform.localRotation);
            holder.GetComponent<Rigidbody>().velocity = transform.right * bulletSpeed;
            holder.GetComponent<BulletController>().bulletDamage = damage;
            holder.GetComponent<BulletController>().SetType(type);

            nextTimeToFire = Time.time + 1 / FireRate;
        }
    }

    public void powerOn()
    {
        powered = true;
    }

    public void powerOff()
    {
        powered = false;
    }

    public void SetType(PowerGenerator.Elemental element)
    {
        type = element;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] Transform shootPos;

    GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

     private void Update()
     {
         Quaternion lookdir = Quaternion.LookRotation(player.transform.position - transform.position, Vector3.up);
         transform.rotation = Quaternion.RotateTowards(transform.rotation, lookdir, Time.deltaTime * 100);
         transform.rotation = Quaternion.Euler(transform.eulerAngles.x, 0, 0);
     }

    public void Shoot()
    {
        GameObject shotBullet = Instantiate(bullet, shootPos.position, shootPos.rotation);
        shotBullet.GetComponent<Rigidbody>().AddForce(shootPos.forward * 100);
    }
}

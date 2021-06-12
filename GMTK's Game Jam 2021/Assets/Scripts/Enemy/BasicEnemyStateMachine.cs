using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class BasicEnemyStateMachine : MonoBehaviour
{
    [Header("misc")]
    [SerializeField] bool drawGizmo;

    [Header("References")]
    [SerializeField] EnemyGun gun;
    [SerializeField] LayerMask playerMask;
    [SerializeField] LayerMask obstacleMask;

    [Header("Roaming")]
    [SerializeField] Transform[] patrolPoints;
    [SerializeField] bool isStill;
    [SerializeField] float viewRadius;
    [SerializeField] float viewAngle;

    [Header("Shooting")]
    [SerializeField] float shootStoppingDist = 10;
    [SerializeField] float fireRate = 2;
    [SerializeField] float minEngageDist = 10;
    public enum enemyState
    {
        Roam,
        Shoot,
        Search,
        SpecialSearch
    }
    public enemyState state = enemyState.Shoot;

    //private vars
    NavMeshAgent enemy;
    private Transform player;
    private float nextTimeToShoot;
    private bool LineOfSighCheck;
    int destPoint = 0;
    private bool inView;

    private void Start()
    {
        enemy = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        switch (state)
        {
            case enemyState.Shoot:
                Shoot();
                //agent Changes
                enemy.updateRotation = false;
                enemy.stoppingDistance = shootStoppingDist;
                //agent changes
                break;
            case enemyState.Roam:
                Roam();
                //agent Changes
                enemy.updateRotation = true;
                enemy.stoppingDistance = 0;
                //agent changes
                break;
        }

        RaycastHit hit;
        Debug.DrawRay(gun.transform.position, gun.transform.forward * 10, Color.green);
        if (Physics.Raycast(gun.transform.position, gun.transform.forward, out hit))
        {
            if (hit.collider.CompareTag("Player"))
            {
                LineOfSighCheck = true;
            }
            else
            {
                LineOfSighCheck = false;
            }
        }
        FindVisibleTargets();
    }

    void Roam()
    {
        if (!enemy.pathPending && enemy.remainingDistance <= 0.5f)   //If distance to next point is short
        {
            GotoNextPoint();
        }

        if (inView)
        {
            state = enemyState.Shoot;
        }
    }
    private void GotoNextPoint()
    {
        if (isStill)    //If is still quit current method
        {
            return;
        }
        if (patrolPoints.Length == 0)   //If no patrol points quit current method
        {
            return;
        }

        enemy.destination = patrolPoints[destPoint].position;   //Set patrol destination

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint = (destPoint + 1) % patrolPoints.Length;
    }

    void Shoot()
    {
        Quaternion lookdir = Quaternion.LookRotation(player.transform.position - gun.transform.position, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookdir, Time.deltaTime * 100);
        transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
        gun.transform.localRotation = Quaternion.RotateTowards(gun.transform.rotation, lookdir, Time.deltaTime * 100);
        gun.transform.localRotation = Quaternion.Euler(gun.transform.eulerAngles.x, 0, 0);

        enemy.SetDestination(player.position);

        if (Time.time >= nextTimeToShoot && LineOfSighCheck)
        {
            gun.Shoot();
            nextTimeToShoot = Time.time + 1 / fireRate;
        }

        if (Vector3.Distance(player.position, transform.position) >= minEngageDist && !LineOfSighCheck)
        {
            state = enemyState.Roam;
        }
    }

    void FindVisibleTargets()
    {
        inView = false;
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, playerMask);

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position);
                if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
                {
                    inView = true;
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (drawGizmo)
        {
            Gizmos.DrawWireSphere(transform.position, viewRadius);
        }
    }
}
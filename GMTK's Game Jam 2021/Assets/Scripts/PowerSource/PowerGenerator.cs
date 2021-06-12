using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerGenerator : MonoBehaviour
{
    //References
    EnemyManager manager;
    public GameObject elecIndicator, waterIndicator, fireIndicator;
    public LayerMask playerMask;
    public GameObject promptText;

    //misc
    [SerializeField] bool drawGizmo;

    //public variables
    public GunController gun;
    public float powerDistance;
    public float powerleft = 60f;
    public bool noEnemyAlert = false;

    public enum Elemental
    {
        electricity,
        water,
        fire
    }

    public Elemental elemCurrent = Elemental.electricity;
    public float interactDistance = 2;


    //sounds
    public AK.Wwise.Event powerUp;
    public AK.Wwise.Event powerDown;

    //private variables
    private float distance;
    bool powering;
    bool previous;
    bool elementalChosen = false;

    // Start is called before the first frame update
    void Start()
    {
        powering = false;
        previous = false;
        manager = GameObject.FindGameObjectWithTag("EnemyManager").GetComponent<EnemyManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //get distance to player's gun
        distance = Vector3.Distance(gun.gameObject.transform.position, transform.position);

        //if distance is close enough
        if (distance <= powerDistance && powerleft > 0 && elementalChosen)
        {
            gun.powerOn();
            if (!noEnemyAlert)
            { manager.ISaw(); }
            powering = true;
            powerleft -= Time.deltaTime;
        }
        else if (distance > powerDistance && powering)
        {
            gun.powerOff();
            powering = false;
        }
        else if (powerleft <= 0)
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
        if (!elementalChosen)
        { ElementalSwitch(); }
    }

    private void ElementalSwitch()
    {
        elecIndicator.SetActive(false);
        fireIndicator.SetActive(false);
        waterIndicator.SetActive(false);

        Collider[] player = Physics.OverlapSphere(transform.position, interactDistance, playerMask);
        if (player.Length >= 1)
        {
            promptText.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                elemCurrent = Elemental.electricity;
                elementalChosen = true;
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                elemCurrent = Elemental.fire;
                elementalChosen = true;
            }
            if (Input.GetKeyDown(KeyCode.G))
            {
                elemCurrent = Elemental.water;
                elementalChosen = true;
            }
        }
        else
        {
            promptText.SetActive(false);
        }

        if (!elementalChosen) return;
        switch (elemCurrent)
        {
            case Elemental.electricity:
                elecIndicator.SetActive(true);
                fireIndicator.SetActive(false);
                waterIndicator.SetActive(false);
                break;
            case Elemental.water:
                elecIndicator.SetActive(false);
                fireIndicator.SetActive(false);
                waterIndicator.SetActive(true);
                break;
            case Elemental.fire:
                elecIndicator.SetActive(false);
                fireIndicator.SetActive(true);
                waterIndicator.SetActive(false);
                break;
        }
    }

    private void OnDrawGizmos()
    {
        if (drawGizmo)
        {
            Gizmos.DrawWireSphere(transform.position, powerDistance);
        }
    }
}

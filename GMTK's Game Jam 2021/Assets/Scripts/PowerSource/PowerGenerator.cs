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
    public LineRenderer powerLine;
    public Transform powerStartPosition;
    public Material elecMat, fireMat, waterMat;

    //misc
    [SerializeField] bool drawGizmo;

    //public variables
    // public GunController gun;
    private PlayerController pc;
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
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        powering = false;
        previous = false;
        manager = GameObject.FindGameObjectWithTag("EnemyManager").GetComponent<EnemyManager>();
        elecIndicator.SetActive(false);
        fireIndicator.SetActive(false);
        waterIndicator.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //get distance to player's gun
        distance = Vector3.Distance(pc.transform.position, transform.position);

        //if distance is close enough
        if (distance <= powerDistance && powerleft > 0 && elementalChosen)
        {
            pc.powered = true;
            if (!noEnemyAlert)
            { manager.ISaw(); }
            powering = true;
            powerleft -= Time.deltaTime;
            powerLine.SetPosition(1, pc.transform.localPosition);
            powerLine.SetPosition(0, powerStartPosition.position);
        }
        else if (distance > powerDistance && powering)
        {
            pc.powered = false;
            powering = false;
            powerLine.SetPosition(1, powerStartPosition.position);
        }
        else if (powerleft <= 0)
        {
            pc.powered = false;
            powering = false;
            powerLine.SetPosition(1, powerStartPosition.position);
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
        ElementalSwitch();
    }

    private void ElementalSwitch()
    {
        Collider[] player = Physics.OverlapSphere(transform.position, interactDistance, playerMask);
        if (player.Length >= 1)
        {
            promptText.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                elemCurrent = Elemental.electricity;
                elementalChosen = true;
                elecIndicator.SetActive(true);
                fireIndicator.SetActive(false);
                waterIndicator.SetActive(false);
                powerLine.material = elecMat;
                pc.SetType(elemCurrent);

            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                elemCurrent = Elemental.fire;
                elementalChosen = true;
                elecIndicator.SetActive(false);
                fireIndicator.SetActive(true);
                waterIndicator.SetActive(false);
                powerLine.material = fireMat;
                pc.SetType(elemCurrent);
            }
            if (Input.GetKeyDown(KeyCode.G))
            {
                elemCurrent = Elemental.water;
                elementalChosen = true;
                elecIndicator.SetActive(false);
                fireIndicator.SetActive(false);
                waterIndicator.SetActive(true);
                powerLine.material = waterMat;
                pc.SetType(elemCurrent);
            }
        }
        else
        {
            promptText.SetActive(false);
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

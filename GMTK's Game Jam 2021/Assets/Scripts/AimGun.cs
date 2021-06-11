using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimGun : MonoBehaviour
{
    public PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles += player.lookSpeed * new Vector3(-Input.GetAxis("Mouse Y"), 0, 0);
    }
}

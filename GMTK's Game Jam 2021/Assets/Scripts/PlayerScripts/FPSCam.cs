using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCam : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float mouseSensitivity;

    float camXRot;
    Transform playerBody;

    private void Start()
    {
        playerBody = transform.parent;
    }
    private void Update()
    {
        //Get Mouse Pos
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        camXRot -= mouseY;
        camXRot = Mathf.Clamp(camXRot, -90f, 90f);      //Clamp Mouse Pos

        //Set rotation
        playerBody.Rotate(Vector3.up * mouseX);
        transform.localRotation = Quaternion.Euler(camXRot, 0, 0);
    }
}

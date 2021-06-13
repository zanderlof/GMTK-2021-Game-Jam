using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelEnder : MonoBehaviour
{
    public LayerMask playerMask;
    public GameObject eText;
    private void Update()
    {
        Collider[] player = Physics.OverlapSphere(transform.position, 2, playerMask);
        if (player.Length >= 1)
        {
            eText.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }
}

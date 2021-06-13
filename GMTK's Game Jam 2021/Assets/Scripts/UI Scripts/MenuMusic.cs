using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMusic : MonoBehaviour
{


    public AK.Wwise.Event playMenuMusic;


    // Start is called before the first frame update
    void Start()
    {

        playMenuMusic.Post(gameObject);
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

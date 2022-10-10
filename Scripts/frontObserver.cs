using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class frontObserver : MonoBehaviour
{

    public behaviour _player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "pared")
        {
            _player._frontColision = true;
        }
    }

    /*private void OnTriggerExit(Collider other)
    {
        _player._frontColision = false;
    }*/
}

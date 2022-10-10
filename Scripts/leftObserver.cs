using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leftObserver : MonoBehaviour
{
    public behaviour2 _player;

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
            _player._leftColision = true;
        }
    }
}
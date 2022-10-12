using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leftObserver : MonoBehaviour
{
    public behaviour _player;
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "pared")
        {
            _player._leftColision = true;
        }
    }
}
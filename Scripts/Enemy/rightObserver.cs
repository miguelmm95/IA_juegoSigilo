using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rightObserver : MonoBehaviour
{
    public behaviour _player;
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "pared")
        {
            _player._rightColision = true;
        }
    }
}
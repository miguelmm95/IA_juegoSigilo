using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lata : MonoBehaviour
{
    public CapsuleCollider capsule;
    //public AudioSource sonido;
    bool impacto = false;

    public Rigidbody can;
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody can = GetComponent<Rigidbody>();
        Destroy(gameObject, 2);
    }

    // Update is called once per frame
    void OnCollisionEnter(Collision other) 
    {
        Debug.Log(other.gameObject.tag);
        if(impacto == false)
        {
            impacto = true;
            capsule.radius = 40;
            Debug.Log(capsule.radius);
            //sonido.Play();
        }
        
        else
        {
            can.velocity.Set(0,0,0);
        }

    }
}

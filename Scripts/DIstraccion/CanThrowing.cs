using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanThrowing : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody can;
    public Transform canTransform;
    public float launchForce = 20f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ThrowCan()
    {
        Rigidbody canInstance = Instantiate(can, canTransform.position, canTransform.rotation) as Rigidbody;
        canInstance.velocity = launchForce * canTransform.forward;
    }
}

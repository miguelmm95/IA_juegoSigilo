using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lata : MonoBehaviour
{
    public CapsuleCollider capsule;
    //public AudioSource sonido;
    bool impacto = false;
    //public StateManager state;
    //public PatrolState patrol;
    public Vector3 posicion;

    public Rigidbody can;
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody can = GetComponent<Rigidbody>();
        Destroy(gameObject, 3);

    }

    // Update is called once per frame
    void OnCollisionEnter(Collision other) 
    {
        if(impacto == false)
        {
            //guardar posición de la lata
            posicion = this.transform.position;
            impacto = true;
            capsule.radius = 40;
            StartCoroutine(soundOff());
            //sonido.Play();
        }
        
        else
        {
            can.velocity.Set(0,0,0);
        }

    }

    IEnumerator soundOff(){
        yield return new WaitForSeconds(2);
        capsule.radius = 1;
    }
    void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == "enemy"){
            Unit enemy = other.GetComponent<Unit>();
            enemy.puntoInvestigacion = posicion;
            enemy.impactoSonido = true;
            StateManager state = other.GetComponent<StateManager>();
            Debug.Log(state.currentState);
            PatrolState patrol = other.GetComponentInChildren<PatrolState>();
            if(state.currentState is PatrolState){
                patrol.goToInvestigate = true;
            }
        }

    }
}

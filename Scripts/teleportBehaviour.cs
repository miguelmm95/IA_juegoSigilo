using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleportBehaviour : MonoBehaviour
{
    public float velocidad = 10f;
    //public Transform waypoint;
    
    private ArrayList waypointsArray = new ArrayList();
    private Vector3 waypointActual;
    private int waypointDestino;
    private bool andando = true;

    void Start(){
        GameObject[] waypointsLista = GameObject.FindGameObjectsWithTag("waypoint");
        foreach(GameObject waypoint in waypointsLista){
            waypointsArray.Add(waypoint.transform.position);
        }
        waypointActual = (Vector3) waypointsArray[0];
        //StartCoroutine(CorutinaMovimiento(waypoint));
    }

    void Update(){
        if(++waypointDestino == waypointsArray.Count){
            waypointDestino = 0;
            waypointActual = (Vector3) waypointsArray[waypointDestino];
            andando = false;
        }
        if(andando)
            andarFuncion(waypointActual);
        else
            StartCoroutine(esperarYPaAbajoFuncion());
    }

    void andarFuncion(Vector3 waypoint){
        Vector3 direction = waypoint - transform.position;
        Vector3 movement = direction.normalized * velocidad * Time.deltaTime;
        transform.LookAt(waypoint);
        transform.position = transform.position + movement;
            //yield return null;
        print("Ya he llegado");
        /*yield return new WaitForSeconds(3f);
        print("La corutina de movimiento ha finalizado");
        */
    }

    IEnumerator esperarYPaAbajoFuncion(){
        yield return new WaitForSeconds(2.0f);
        andando = true;
    }
}

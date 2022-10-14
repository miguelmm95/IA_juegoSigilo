using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleportBehaviour : MonoBehaviour
{
    public float velocidad = 5f;
    public float velocidadBajada = 10f;
    
    private ArrayList waypointsArray = new ArrayList();
    private Vector3 waypointActual;
    private Vector3 waypointAnterior;
    private Vector3 calculoBajada;
    private int waypointDestino;
    private bool andando = true;
    private bool agachado = false;

    void Start(){

        GameObject[] waypointsLista = GameObject.FindGameObjectsWithTag("waypoint");
        foreach(GameObject waypoint in waypointsLista)
            waypointsArray.Add(waypoint.transform.position);
        
        calculoBajada = waypointActual - new Vector3(0, 15, 0);
        waypointActual = (Vector3) waypointsArray[0];

    }

    void Update(){

        if(Vector3.Distance(transform.position, waypointActual) < 2){
            if(++waypointDestino == waypointsArray.Count)
                waypointDestino = 0;
            waypointAnterior = waypointActual;
            calculoBajada = waypointActual - new Vector3(0, 5, 0);
            waypointActual = (Vector3) waypointsArray[waypointDestino];
            andando = false;
        }


        if(andando)
            andarFuncion(waypointActual);
        else if(!andando && !agachado)
            StartCoroutine(agachadoFuncion());
        else if(!andando && agachado)
            StartCoroutine(subidaFuncion());
    }

    void andarFuncion(Vector3 waypoint){

        Vector3 direction = waypoint - transform.position;
        Vector3 movement = direction.normalized * velocidad * Time.deltaTime;
        transform.position = transform.position + movement;
    }

    IEnumerator agachadoFuncion(){
        
        while(Vector3.Distance(transform.position, calculoBajada) > 0.05f){
            transform.position = Vector3.Lerp(transform.position, calculoBajada, velocidadBajada * Time.deltaTime);
            yield return null;
        }
        
        yield return new WaitForSeconds(2.0f);
        agachado = true;
    }

    IEnumerator subidaFuncion(){
        while(Vector3.Distance(transform.position, waypointAnterior) > 0.05f){
            transform.position = Vector3.Lerp(transform.position, waypointAnterior, velocidadBajada * Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(2.0f);
        andando = true;
        agachado = false;
    }
}

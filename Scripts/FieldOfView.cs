using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class FieldOfView : MonoBehaviour
{
    public float radioInvestigación;
    public float radioDetección;
    [Range(0,360)]
    public float anguloInvestigacion;
    [Range(0, 360)]
    public float anguloDeteccion;

    public GameObject jugador;

    public LayerMask targetM;
    public LayerMask ObstacleM;

    public bool alerta;
    public bool deteccion;
    public bool volador;

    public int tiempoEsperaPersecucion;
    public int tiempoEsperaInvestigacion;

    private int contador;

    public GoBackToPatrolState _goBack;
    public PatrolState _patrol;
    public InvestigateState _invetigate;
    public PersecutionState _persecution;
    public StateManager _state;
    public Spawner spawner;
    public GameObject waypointSpawner;

    private void Start()
    {
        jugador = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(FOVRoutine());

        if (gameObject.tag == "volador") volador = true;
        else volador = false;

        _goBack = GetComponentInChildren<GoBackToPatrolState>();
        _patrol = GetComponentInChildren<PatrolState>();
        _invetigate = GetComponentInChildren<InvestigateState>();
        _persecution = GetComponentInChildren<PersecutionState>();
        _state = GetComponentInChildren<StateManager>();

    }

    void Update()
    {

    }

    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            Check();
        }
    }

    private void Check()
    {
        Collider[] investigacion = Physics.OverlapSphere(transform.position, radioInvestigación, targetM);
        Collider[] detectado = Physics.OverlapSphere(transform.position, radioDetección, targetM);

        if (investigacion.Length != 0)
        {
            Transform Dtarget = investigacion[0].transform;
            Vector3 direccion = (Dtarget.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, direccion) < anguloInvestigacion / 2)
            {
                float distancia = Vector3.Distance(transform.position, Dtarget.position);

                if (volador && transform.position.y > Dtarget.position.y)
                {
                    alerta = false;
                }
                else
                {
                    if (!Physics.Raycast(transform.position, direccion, distancia, ObstacleM))
                    {
                        alerta = true;
                        if (_state.currentState is PatrolState)
                        {
                            Unit enemy = GetComponent<Unit>();
                            enemy.puntoInvestigacion = jugador.transform.position;
                            _patrol.goToInvestigate = true;
                        }
                        /*else if (_state.currentState is GoBackToPatrolState)
                        {
                            _goBack.goToInvestigate = true;
                            _persecution.playerLost = false;
                            _invetigate.playerLost = false;
                        }*/
                    }
                    else alerta = false;
                }
            }
            else
                alerta = false;
        }
        else if (alerta)
            alerta = false;

        if (!alerta)
        {
            if (_state.currentState is InvestigateState || _state.currentState is PersecutionState)
            {
                contador += 1;
                //Debug.Log(contador);
                if (_state.currentState is InvestigateState)
                {
                    if (contador >= 5 * tiempoEsperaInvestigacion)
                    {
                        _invetigate.detectPlayer = false;
                        _patrol.goToInvestigate = false;
                        _invetigate.playerLost = true;
                        contador = 0;
                    }
                }
                else
                {
                    if (contador >= 5 * tiempoEsperaPersecucion)
                    {
                        Destroy(gameObject);
                        spawner.contador--;
                        contador = 0;
                    }
                }
                
            }
        }

        if (detectado.Length != 0)
        {
            Transform Dtarget = detectado[0].transform;
            Vector3 direccion = (Dtarget.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, direccion) < anguloDeteccion / 2)
            {
                float distancia = Vector3.Distance(transform.position, Dtarget.position);

                if (volador && transform.position.y > Dtarget.position.y)
                {
                    deteccion = false;
                }
                else
                {
                    if (!Physics.Raycast(transform.position, direccion, distancia, ObstacleM))
                    {
                        deteccion = true;
                        _invetigate.detectPlayer = true;
                    }
                    else deteccion = false;
                }
            }
            else
                deteccion = false;
        }
        else if (deteccion)
            deteccion = false;
    }

    void OnDrawGizmos()
    {
        UnityEditor.Handles.color = Color.white;
        UnityEditor.Handles.DrawWireArc(transform.position, Vector3.up, Vector3.forward, 360, radioInvestigación);
        UnityEditor.Handles.color = Color.grey;
        UnityEditor.Handles.DrawWireArc(transform.position, Vector3.up, Vector3.forward, 360, radioDetección);

        Vector3 AnguloI1 = DireccionAngulo(transform.eulerAngles.y, -anguloInvestigacion / 2);
        Vector3 AnguloI2 = DireccionAngulo(transform.eulerAngles.y, anguloInvestigacion / 2);

        Vector3 AnguloD1 = DireccionAngulo(transform.eulerAngles.y, -anguloDeteccion / 2);
        Vector3 AnguloD2 = DireccionAngulo(transform.eulerAngles.y, anguloDeteccion / 2);

        UnityEditor.Handles.color = Color.yellow;
        UnityEditor.Handles.DrawLine(transform.position, transform.position + AnguloI1 * radioInvestigación);
        UnityEditor.Handles.DrawLine(transform.position, transform.position + AnguloI2 * radioInvestigación);

        UnityEditor.Handles.color = Color.magenta;
        UnityEditor.Handles.DrawLine(transform.position, transform.position + AnguloD1 * radioDetección);
        UnityEditor.Handles.DrawLine(transform.position, transform.position + AnguloD2 * radioDetección);

        if (alerta)
        {
            UnityEditor.Handles.color = Color.green;
            UnityEditor.Handles.DrawLine(transform.position, jugador.transform.position);
        }
        if (deteccion)
        {
            UnityEditor.Handles.color = Color.red;
            UnityEditor.Handles.DrawLine(transform.position, jugador.transform.position);
        }
    }


    private Vector3 DireccionAngulo(float eulerY, float AnguloEnGrados)
    {
        AnguloEnGrados += eulerY;

        return new Vector3(Mathf.Sin(AnguloEnGrados * Mathf.Deg2Rad), 0, Mathf.Cos(AnguloEnGrados * Mathf.Deg2Rad));
    }
}
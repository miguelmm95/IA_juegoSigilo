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

    private void Start()
    {
        jugador = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(FOVRoutine());

        if (gameObject.tag == "volador") volador = true;
        else volador = false;
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
                    }
                    else alerta = false;
                }
            }
            else
                alerta = false;
        }
        else if (alerta)
            alerta = false;

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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject fantasmas;
    [HideInInspector] public int contador;
    public GameObject[] waypoints;

    // Start is called before the first frame update
    void Start()
    {
        contador = 4;
    }

    // Update is called once per frame
    void Update()
    {
        if(contador < 4){
            int i = UnityEngine.Random.Range(0, 4);
            Debug.Log(waypoints[i]);
            Instantiate(fantasmas, waypoints[i].transform.position, Quaternion.identity);
            contador++;
        }   
    }
}

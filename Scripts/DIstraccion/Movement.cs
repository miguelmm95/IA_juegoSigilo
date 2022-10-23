using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 20f;
    public Vector3 playerPosition;
    private CanThrowing canThrowing;
    float turnSmoothVelocity;
    public float turnSmoothTime = 0.2f;
    public int llaves = 0;
    public GameObject paredFinal;
    public GameObject[] Arrayllaves;
    public GameObject jugador;
    public GameObject spawn;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        canThrowing = GetComponent<CanThrowing>();

        jugador = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        bool hasHorizontalInput = !Mathf.Approximately (horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately (vertical, 0f);
        bool isMoving = hasHorizontalInput || hasVerticalInput;

        if(Input.GetMouseButtonDown(0)){
            canThrowing.ThrowCan();
        }

        if (direction.magnitude >= 0.1f) {

            //Movimiento
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            controller.Move(direction * speed * Time.deltaTime);

        }

        if (llaves == 4)
        {
            Destroy(paredFinal);
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "llave")
        {
            llaves++;
            collision.gameObject.SetActive(false);
        }

        else if (collision.gameObject.tag == "enemy") Death();

    }

    void Death()
    {
        foreach (GameObject llave in Arrayllaves)
        {
            llave.SetActive(true);
        }

        jugador.transform.position = spawn.transform.position;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class behaviour : MonoBehaviour
{
    [SerializeField] private GameObject _frontObject;
    [SerializeField] private GameObject _leftObject;
    [SerializeField] private GameObject _rightObject;
    private float _movementSpeed = 5f;

    RaycastHit hit;

    public Rigidbody _rb;
    public bool _isMoving;
    public Vector3 _forwardDiection;
    public Vector3 _leftDirection;
    public Vector3 _rightDirection;
    public List<Vector3> possibleDirections;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _isMoving = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _forwardDiection = (_frontObject.transform.position - transform.position).normalized;
        _leftDirection = (_leftObject.transform.position - transform.position).normalized;
        _rightDirection = (_rightObject.transform.position + transform.position).normalized;


        if (_isMoving)
        {
            Vector3 velocity = _forwardDiection * _movementSpeed * Time.deltaTime;
            transform.Translate(velocity);
        }

        wallDetection();
    }

    void wallDetection()
    {
        Ray ray = new Ray(transform.position, _forwardDiection);
        //Ray rightRay = new Ray(rightDetector.position, rightDetector.position);

        Debug.DrawRay(transform.position, _forwardDiection, Color.red);
        //Debug.DrawRay(rightDetector.position, transform.forward, Color.blue);

        if (Physics.Raycast(ray, out hit, 1.5f))
        {
            if (hit.collider.tag == "pared")
            {
                _isMoving = false;
                StartCoroutine(SearchWall());
            }
        }
    }

    IEnumerator SearchWall()
    {
        Ray leftRay = new Ray(_leftObject.transform.position, _leftDirection);
        Debug.DrawRay(_leftObject.transform.position, transform.forward, Color.yellow);

        Ray rightRay = new Ray(_rightObject.transform.position, _rightDirection);
        Debug.DrawRay(_rightObject.transform.position, -transform.forward, Color.yellow);


        if (Physics.Raycast(leftRay, out hit, 1.5f))
        {
            if (hit.collider.tag == "pared")
            {
                possibleDirections.Add(_leftDirection);
                Debug.Log("Detectada pared izquierda");
            }
        }
        if(Physics.Raycast(rightRay, out hit, 1.5f))
        {
            if(hit.collider.tag == "pared")
            {
                possibleDirections.Add(_rightDirection);
                Debug.Log("Detectada pared derecha");
            }
        }
        yield return new WaitForSecondsRealtime(10);
    }
}

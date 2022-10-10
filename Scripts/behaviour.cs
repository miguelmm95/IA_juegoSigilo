using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class behaviour : MonoBehaviour
{
    [SerializeField] private GameObject _frontObject;
    [SerializeField] private GameObject _leftObject;
    [SerializeField] private GameObject _rightObject;
    private float _movementSpeed = 5f;
    private float _rotationSpeed = 90f;
    private Vector3 _rotation;

    //private static System.Random rnd = new System.Random();

    public Rigidbody _rb;
    public int direction;
    public bool _isMoving;
    public bool _colliding;
    public bool _rotating;
    public bool _frontColision = false;
    public bool _rightColision = false;
    public bool _leftColision = false;
    public Vector3 _forwardDiection;
    public List<Transform> _observers;

    void Start()
    {
        _observers = GetChildren(transform);

        _rb = GetComponent<Rigidbody>();
        _isMoving = true;
        _colliding = false;
    }

    void FixedUpdate()
    {
        _forwardDiection = (_frontObject.transform.position - transform.position).normalized;

        if (_isMoving)
        {
            Vector3 velocity = _forwardDiection * _movementSpeed * Time.deltaTime;
            transform.Translate(velocity);
        }

        if (_frontColision)
        {
            wallDetection();
        }
        

        if (_colliding)
        {
            enaleObservers();
        }

        if (_rotating)
        {
            transform.Rotate(checkPossiblesDirections());
            _rotating = false;
        }
    }


    void wallDetection()
    {
        _isMoving = false;
        _colliding = true;
    }

    List<Transform> GetChildren(Transform parent)
    {
        List<Transform> children = new List<Transform>();

        foreach(Transform child in parent)
        {
            if(child.name != "FrontDetector")
            {
                children.Add(child);
            }
        }

        return children;
    }

    public Vector3 checkPossiblesDirections()
    {
        Debug.Log("izquiedo: " + _leftColision);
        Debug.Log("derecho: " + _rightColision);

        if (_leftColision && _rightColision)
        {
            Debug.Log("Tengo que gira 180º");

        }else if (!_leftColision && !_rightColision)
        {
            direction = -1 + 2 * UnityEngine.Random.Range(0, 2);
            _rotation = new Vector3(0f, 1f, 0f) * _rotationSpeed * direction * Time.deltaTime;

        }
        else if (!_leftColision)
        {
            _rotation = new Vector3(0f, 1f, 0f) * _rotationSpeed * -1;

            Debug.Log("izquiedo: " + _rotation);

        }
        else
        {
            _rotation = new Vector3(0f, 1f, 0f) * _rotationSpeed * 1 * Time.deltaTime;
            Debug.Log("derecho: " + _rotation);
        }
        return _rotation;
    }

    void enaleObservers()
    {
        foreach(Transform child in _observers)
        {
            child.gameObject.SetActive(true);
        }
        _frontObject.SetActive(false);
        _colliding = false;
        _rotating = true;
    }

    void disableObservers()
    {
        foreach (Transform child in _observers)
        {
            child.gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "pared")
        {
            _frontColision = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        _frontColision = false;
    }
}
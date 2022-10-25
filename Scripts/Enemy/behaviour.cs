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
    private int _direction = 0;
    private Vector3 _rotation;

    public InvestigateState _investigateState;
    public PersecutionState _persecutionState;

    public Rigidbody _rb;
    public GameController _gc;
    public bool _isMoving;
    public bool _isRotating;
    public bool _rightColision;
    public bool _leftColision;

    public StateManager _state;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _isMoving = true;
        _state = this.GetComponent<StateManager>();
        _investigateState = this.GetComponentInChildren<InvestigateState>();
        _persecutionState = this.GetComponentInChildren<PersecutionState>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_state.currentState is PatrolState)
        {
            _investigateState.playerLost = false;
            _persecutionState.playerLost = false;
            if (_isMoving)
            {
                Vector3 velocity = Vector3.forward * _movementSpeed * Time.deltaTime;
                transform.Translate(velocity);
            }

            if (_isRotating)
            {
                
                //StartCoroutine(waitingToRotate());

                if (_leftColision && _rightColision)
                {
                    _rotation = new Vector3(0f, 1f, 0f) * 180f * 1;

                }
                else if (!_leftColision && !_rightColision)
                {
                    if (_direction == 0)
                    {
                        _direction = -1 + 2 * UnityEngine.Random.Range(0, 2);
                    }
                    _rotation = new Vector3(0f, 1f, 0f) * _rotationSpeed * _direction;

                }
                else if (!_leftColision)
                {
                    _rotation = new Vector3(0f, 1f, 0f) * _rotationSpeed * -1;
                }
                else
                {
                    _rotation = new Vector3(0f, 1f, 0f) * _rotationSpeed * 1;
                }
                //return _rotation;
                disableObservers();
                transform.Rotate(_rotation);
                _isRotating = false;
                _leftColision = false;
                _rightColision = false;
                _isMoving = true;


            }
        }

        

    }

    IEnumerator waitingToRotate()
    {
        yield return new WaitForSeconds(1);

    }

    void enableObservers()
    {
        _rightObject.SetActive(true);
        _leftObject.SetActive(true);
        _frontObject.SetActive(false);
    }

    void disableObservers()
    {
        _rightObject.SetActive(false);
        _leftObject.SetActive(false);
        _frontObject.SetActive(true);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "pared")
        {
            _isMoving = false;
            _isRotating = true;
            enableObservers();
            StartCoroutine("waitingToRotate");
        }
    }
}

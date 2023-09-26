using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float _speed = 100;
    private PlayerActions _playerActions;
    private Rigidbody2D _rb;
    private Vector2 _moveInput;
    private float _rotSpeed;
    private GameObject _aimCursor;
    private float maxTurnSpeed = 120;
    private float smoothTime = 0.3f;
    private float angle;
    private float currentVelocity;
    private float _fire;
    private bool _firing;

    private void Awake()
    {
        _playerActions = new PlayerActions();
        _rb = GetComponent<Rigidbody2D>();

    }

    private void Start()
    {
        Cursor.visible = false;
    }

    void OnFire(InputValue value)
    {
        _firing = value.isPressed;
        Debug.Log("Kaboom!");
    }

    private void Update()
    {
        if(_firing == true)
        {
            Debug.Log("krrrra!");
            _firing = false;
        }


        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePosition - transform.position;

        float targetAngle = Vector2.SignedAngle(Vector2.up, direction);
        angle = Mathf.SmoothDampAngle(angle, targetAngle, ref currentVelocity, smoothTime, maxTurnSpeed);

        transform.eulerAngles = new Vector3(0, 0, angle);

        //transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(targetRotation), maxTurnSpeed * Time.deltaTime);
    }

    private void OnEnable()
    {
        _playerActions.Player_Map.Enable();
    }

    private void OnDisable()
    {
        _playerActions.Player_Map.Disable();
    }

    private void FixedUpdate()
    {
        _moveInput = _playerActions.Player_Map.Movement.ReadValue<Vector2>();
        _rb.velocity = _moveInput * _speed;
    }
}

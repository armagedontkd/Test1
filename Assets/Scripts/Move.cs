using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    
    [SerializeField] private float _rotateSpeed = 75;

    [SerializeField] private float _walkSpeed = 5;
    [SerializeField] private float _runSpeed = 8;
    [SerializeField] private float _jumpForce = 5;
    [SerializeField] private float _gravity = -9.81f;

    private CharacterController _characterController;

    private Camera _playerCamera;
    
    private Vector2 _rotation;
    private Vector3 _velocity;
    private Vector2 _direction;

    public List<Collider> helpers;
    
    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _playerCamera = GetComponentInChildren<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
        helpers = new List<Collider>();
    }


    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Helpers" && col.isTrigger)
        {
            int index = helpers.FindIndex(x => x.gameObject == col.gameObject);
            if (index == -1) helpers.Add(col);
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Helpers" && col.isTrigger)
        {
            int index = helpers.FindIndex(x => x.gameObject == col.gameObject);
            if (index != -1) helpers.RemoveAt(index);
        }
    }

    private void Update()
    {
        _characterController.Move(motion: _velocity * Time.deltaTime);

        _direction = new Vector2(x: Input.GetAxis("Horizontal"), y: Input.GetAxis("Vertical"));
        Vector2 mouseDelta = new Vector2(x: Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        if (_characterController.isGrounded) _velocity.y = Input.GetKeyDown(KeyCode.Space) ? _jumpForce : -0.1f;
        else _velocity.y += _gravity * Time.deltaTime;

        mouseDelta *= _rotateSpeed * Time.deltaTime;
        _rotation.y += mouseDelta.x;
        _rotation.x = Mathf.Clamp(value:_rotation.x - mouseDelta.y, min: -90, max: 90);
        _playerCamera.transform.localEulerAngles = (Vector3)_rotation;
    }

    private void FixedUpdate()
    {
        _direction *= Input.GetKey(KeyCode.LeftShift) ? _runSpeed : _walkSpeed;
        Vector3 move = Quaternion.Euler(x:0, _playerCamera.transform.eulerAngles.y, z:0) * new Vector3(_direction.x, y:0, z:_direction.y);
        _velocity = new Vector3(move.x, _velocity.y, move.z);
    }


}

using UnityEngine;
using System;

[RequireComponent(typeof(CharacterController))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _gravity;
    [SerializeField] private float _gravityWallSlidingMultiplier;
    [SerializeField] private float _yVelocity;

    private bool _isWallSliding;

    private Vector3 _moveDirection;

    private CharacterController _characterController;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (_characterController.isGrounded)
        {
            _yVelocity = -_gravity * Time.deltaTime;
            _isWallSliding = false;
            Jump();
        }
        else
        {
            var temp = _gravity * Time.deltaTime;
            _yVelocity -= _isWallSliding ? temp * _gravityWallSlidingMultiplier : temp;
        }

        _moveDirection = new Vector3(transform.forward.x, _yVelocity, 0);
        _characterController.Move(_moveDirection * _speed * Time.deltaTime);
    }

    private void Jump(Action action = null)
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            _yVelocity = _jumpForce;
            action?.Invoke();
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.gameObject.CompareTag("Wall"))
        {
            if(_yVelocity < 0) _isWallSliding = true;
            
            Jump(() => 
            { 
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, -transform.eulerAngles.y, transform.eulerAngles.z);
                _isWallSliding = false;
            });
        }
    }
}

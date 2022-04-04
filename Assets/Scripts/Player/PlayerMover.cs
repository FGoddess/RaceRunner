using System;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _gravity;
    [SerializeField] private float _gravityWallSlidingMultiplier;

    [SerializeField] private float _yVelocity;
    private float _startWallSlidingVelocity = 0.3f;

    private bool _isWallSliding;
    private bool _needToTurn;

    private Vector3 _moveDirection;

    private CharacterController _characterController;
    private Animator _animator;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();

        if (transform.GetChild(0).TryGetComponent(out Animator animator))
        {
            _animator = animator;
        }
        else
        {
            Debug.LogError("Animator is null!");
        }
    }

    private void Update()
    {
        if (_characterController.isGrounded)
        {
            _yVelocity = -_gravity * Time.deltaTime;
            _isWallSliding = false;
            Jump();

            if(_needToTurn)
            {
                ReflectTransform();
                _needToTurn = false;
            }
        }
        else
        {
            var temp = _gravity * Time.deltaTime;
            _yVelocity -= _isWallSliding ? temp * _gravityWallSlidingMultiplier : temp;
        }

        _animator.SetBool("IsWallSlide", _isWallSliding);
        _animator.SetBool("IsGrounded", _characterController.isGrounded);

        _moveDirection = new Vector3(transform.forward.x, _yVelocity, 0);
        _characterController.Move(_moveDirection * _speed * Time.deltaTime);
    }

    private void Jump(Action action = null)
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            _yVelocity = _jumpForce;

            _animator.SetTrigger("Jump");

            action?.Invoke();
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.TryGetComponent(out Obstacle obstacle))
        {
            switch(obstacle.ObstacleType)
            {
                case ObstacleType.Ground:
                    {
                        if (hit.collider.transform.up != transform.forward && !_needToTurn)
                        {
                            _needToTurn = true;
                        }

                        break;
                    }
                case ObstacleType.Wall:
                    {
                        if (_yVelocity < 0 && !_isWallSliding)
                        {
                            _isWallSliding = true;
                            _yVelocity = -_startWallSlidingVelocity;
                        }

                        Jump(() =>
                        {
                            ReflectTransform();
                            _isWallSliding = false;
                        });

                        break;
                    }
            }
        }
    }

    private void ReflectTransform()
    {
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, -transform.eulerAngles.y, transform.eulerAngles.z);
    }
}

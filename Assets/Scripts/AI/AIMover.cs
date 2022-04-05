using System;
using System.Collections;
using UnityEngine;

public class AIMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _gravity;
    [SerializeField] private float _gravityWallSlidingMultiplier;

    [SerializeField] private float _minJumpDelay = 0.2f;
    [SerializeField] private float _maxJumpDelay = 0.6f;

    private float _yVelocity;
    private float _startWallSlidingVelocity = 0.3f;
    private float _raycastDistance = 3f;

    private bool _isWallSliding;
    private bool _needToTurn;
    private bool _isJumping;

    private Vector3 _moveDirection;

    private CharacterController _characterController;
    private Animator _animator;

    private Coroutine _jumpCoroutine;

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
        if (_isWallSliding)
        {
            _isJumping = false;
        }

        if (_characterController.isGrounded)
        {
            _yVelocity = -_gravity * Time.deltaTime;
            _isWallSliding = false;
            _isJumping = false;

            Raycast();

            if (_needToTurn)
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
        _isJumping = true;
        _yVelocity = _jumpForce;

        _animator.SetTrigger("Jump");

        action?.Invoke();
    }

    private void Raycast()
    {
        Debug.DrawRay(transform.position + Vector3.up, transform.TransformDirection(Vector3.forward * _raycastDistance), Color.green);

        if (!_isJumping && Physics.Raycast(transform.position + Vector3.up, transform.forward, out RaycastHit hit, _raycastDistance))
        {
            if (hit.collider.TryGetComponent(out Obstacle obstacle))
            {
                if (obstacle.ObstacleType == ObstacleType.Wall)
                {
                    Jump();
                }
            }
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.TryGetComponent(out Obstacle obstacle))
        {
            switch (obstacle.ObstacleType)
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

                        if (_jumpCoroutine == null)
                        {
                            _jumpCoroutine = StartCoroutine(JumpDelay());
                        }

                        break;
                    }
            }
        }
    }

    private IEnumerator JumpDelay()
    {
        var delay = UnityEngine.Random.Range(_minJumpDelay, _maxJumpDelay);
        yield return new WaitForSeconds(delay);

        Jump(() =>
        {
            ReflectTransform();
            _isWallSliding = false;
        });

        _jumpCoroutine = null;
    }

    private void ReflectTransform()
    {
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, -transform.eulerAngles.y, transform.eulerAngles.z);
    }
}

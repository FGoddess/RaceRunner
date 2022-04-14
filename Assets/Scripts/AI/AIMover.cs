using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class AIMover : MonoBehaviour
{
    [SerializeField] private Countdown _countdown;

    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _gravity;
    [SerializeField] private float _gravityWallSlidingMultiplier;
    [SerializeField] private float _springJumpForceMultiplier = 1.5f;

    [SerializeField] private float _minJumpDelay = 0.2f;
    [SerializeField] private float _maxJumpDelay = 0.6f;

    private float _yVelocity;
    private float _startWallSlidingVelocity = 0.3f;
    private float _raycastDistance = 3f;
    private float _reflectionDelay = 0.3f;

    private bool _isJumping;
    private bool _isWallSliding;
    private bool _isCountdownEnded;
    private bool _needToTurn;
    private bool _needSpringJump;

    private Vector3 _moveDirection;

    private CharacterController _characterController;
    private Animator _animator;

    private Coroutine _jumpCoroutine;
    private Coroutine _reflectRoutine;

    private void OnEnable()
    {
        _countdown.CountdownEnded += OnCountDownEnded;
    }
    private void OnDisable()
    {
        _countdown.CountdownEnded -= OnCountDownEnded;
    }

    private void OnCountDownEnded()
    {
        _isCountdownEnded = true;
    }

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
        if (!_isCountdownEnded) return;

        if (_isWallSliding)
        {
            _isJumping = false;
        }

        if (_characterController.collisionFlags == CollisionFlags.None)
        {
            _isWallSliding = false;
        }

        if (_characterController.isGrounded)
        {
            _yVelocity = -_gravity * Time.deltaTime;
            _isWallSliding = false;
            _isJumping = false;

            Raycast();

            if (_needToTurn && _reflectRoutine == null)
            {
                if (_jumpCoroutine != null)
                {
                    ReflectTransform();
                    _needToTurn = false;
                }
                else
                {
                    _reflectRoutine = StartCoroutine(DelayedReflectTransform());
                }
            }
        }
        else
        {
            var temp = _gravity * Time.deltaTime;
            _yVelocity -= _isWallSliding ? temp * _gravityWallSlidingMultiplier : temp;
        }

        if (_needSpringJump)
        {
            _yVelocity = _jumpForce * _springJumpForceMultiplier;
            _animator.SetTrigger("Jump");
            _needSpringJump = false;
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
                        if (!_characterController.isGrounded) return;

                        if (hit.collider.transform.up != transform.forward && !_needToTurn && _reflectRoutine == null)
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
                case ObstacleType.SlideWall:
                    {
                        if (_yVelocity < 0 && !_isWallSliding)
                        {
                            _isWallSliding = true;
                            _yVelocity = -_startWallSlidingVelocity;
                        }

                        break;
                    }
                case ObstacleType.Spring:
                    {
                        Debug.Log("da");
                        _needSpringJump = true;
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

    private IEnumerator DelayedReflectTransform()
    {
        yield return new WaitForSeconds(_reflectionDelay);
        ReflectTransform();
        _needToTurn = false;
        _reflectRoutine = null;
    }
}

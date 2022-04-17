using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private Countdown _countdown;

    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _springJumpForceMultiplier = 1.5f;
    [SerializeField] private float _gravity;
    [SerializeField] private float _gravityWallSlidingMultiplier;

    private float _yVelocity;
    private float _startWallSlidingVelocity = 0.3f;
    private float _reflectionDelay = 0.3f;
    private float _danceXRotation = 180f;

    private bool _isWallSliding;
    private bool _isGameOver;
    private bool _isCountdownEnded;
    private bool _needToTurn;
    private bool _needSpringJump;

    private Vector3 _moveDirection;

    private CharacterController _characterController;
    private Animator _animator;

    private Coroutine _reflectRoutine;

    public event Action Jumped;

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
        if (_isGameOver || !_isCountdownEnded) return;

        if (_characterController.collisionFlags == CollisionFlags.None)
        {
            _isWallSliding = false;
        }

        if (_characterController.isGrounded)
        {
            _yVelocity = -_gravity * Time.deltaTime;
            _isWallSliding = false;

            Jump();

            if (_needToTurn && _reflectRoutine == null)
            {
                _reflectRoutine = StartCoroutine(DelayedReflectTransform());
            }
        }
        else
        {
            var temp = _gravity * Time.deltaTime;
            _yVelocity -= _isWallSliding ? temp * _gravityWallSlidingMultiplier : temp;

            if (_reflectRoutine != null)
            {
                StopCoroutine(_reflectRoutine);
                _reflectRoutine = null;
                _needToTurn = false;
            }
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
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            Jumped?.Invoke();

            _yVelocity = _jumpForce;

            _animator.SetTrigger("Jump");

            action?.Invoke();
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
                        WallBehaviour();
                        break;
                    }
                case ObstacleType.SlideWall:
                    {
                        WallBehaviour();
                        break;
                    }
                case ObstacleType.Spring:
                    {
                        _needSpringJump = true;
                        break;
                    }
            }
        }
    }

    private void WallBehaviour()
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

    public void GameOver()
    {
        _isGameOver = true;
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, _danceXRotation, transform.eulerAngles.z);
        _animator.SetTrigger("Dance");
    }
}

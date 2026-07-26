using UnityEngine;
using UnityEngine.InputSystem;

public class NejikoController : MonoBehaviour
{

    const int MinLane = -2;
    const int MaxLane = 2;
    const float Lanewidth = 1.0f;
    public float Gravity;
    public float SpeedZ;
    public float SpeedX;
    public float SpeedJump;
    public float AccelerationZ;

    CharacterController _controller;
    Animator _animator;
    Vector3 _moveDirection = Vector3.zero;
    int _targetLane;
    InputAction _jumpAction;
    InputAction _moveAction;

    void Start()
    {
        _jumpAction = InputSystem.actions.FindAction("Jump");
        _moveAction = InputSystem.actions.FindAction("Move");
        _jumpAction.Enable();
        _moveAction.Enable();

        _controller = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(Keyboard.current.leftArrowKey.wasPressedThisFrame) MoveToLeft();
        if(Keyboard.current.rightArrowKey.wasPressedThisFrame) MoveToRight();
        if(Keyboard.current.spaceKey.wasPressedThisFrame) Jump();

        float acceleratedZ = _moveDirection.z +(AccelerationZ * Time.deltaTime);
        _moveDirection.z = Mathf.Clamp(acceleratedZ, 0, SpeedZ);

        float ratioX = (_targetLane * Lanewidth - transform.position.x) / Lanewidth;
        _moveDirection.x = ratioX * SpeedX;

        _moveDirection.y -= Gravity * Time.deltaTime;

        Vector3 globalDirection = transform.TransformDirection(_moveDirection);
        _controller.Move(globalDirection*Time.deltaTime);

        if(_controller.isGrounded) _moveDirection.y = 0;

        _animator.SetBool("run",_moveDirection.z > 0.0f);
    }

    public void MoveToLeft()
    {
        if(_controller.isGrounded && _targetLane > MinLane) _targetLane--;
    }

    public void MoveToRight()
    {
        if(_controller.isGrounded && _targetLane < MaxLane) _targetLane++;
    }

    public void Jump()
    {
        if (_controller.isGrounded)
        {
            _moveDirection.y = SpeedJump;

            _animator.SetTrigger("jump");
        }
    }
}

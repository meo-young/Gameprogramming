using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("# Component")]
    public Rigidbody rigidBody;

    [Header("# Movement")]
    [Tooltip("플레이어 이동 속도")]
    public float moveSpeed;
    [Tooltip("플레이어 회전 속도")]
    public float rotationSpeed;
    [Tooltip("플레이어의 방향")]
    public Vector2 movement;

    [Header("# Bullet Info")]
    public List<GameObject> bulletPrefab;
    public List<Transform> firePosition;
    int bulletIndex;

    public static PlayerController Instance { get; private set; } // Singleton 인스턴스


    public IPlayerState CurrentState
    {
        get; set;
    }

    public Vector2 CurrentDirection
    {
        get; set;
    }

    public IPlayerState _idleState, _moveState;


    private void Start()
    {
        bulletIndex = 0;

        Cursor.lockState = CursorLockMode.Locked;

        _idleState = gameObject.AddComponent<PlayerIdleState>();
        _moveState = gameObject.AddComponent<PlayerMoveState>();

        ChangeState(_idleState);
    }

    private void Update()
    {
        UpdateState();
    }

    public void ChangeState(IPlayerState playerState)
    {
        if (CurrentState != null)
            CurrentState.OnStateExit();
        CurrentState = playerState;
        CurrentState.OnStateEnter(this);
    }

    public void UpdateState()
    {
        if (CurrentState != null)
        {
            CurrentState.OnStateUpdate();
        }
    }

    public void OnMove(InputValue value)
    {
        movement = value.Get<Vector2>();
    }
    public void RotateToMousePosition()
    {
        transform.Rotate(0f, Input.GetAxis("Mouse X") * rotationSpeed, 0f, Space.World);
    }

    public void OnLeftFire()
    {
        GameObject bullet = GameManager.instance.poolManager.GetWeapon(0);
    }

    public void OnRightFire()
    {
        GameObject bullet = GameManager.instance.poolManager.GetWeapon(1);
    }
}

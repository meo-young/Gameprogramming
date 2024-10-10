using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : MonoBehaviour, IPlayerState
{
    private PlayerController _playerController;
    private Rigidbody _rb;
    private float _moveSpeed;
    private float _rotationSpeed;
    private Vector3 _direction;

    public void OnStateEnter(PlayerController playerController)
    {
        if (!_playerController)
            _playerController = playerController;
        _rb = _playerController.rigidBody;
        _moveSpeed = _playerController.moveSpeed;
        _rotationSpeed = _playerController.rotationSpeed;
    }
    public void OnStateUpdate()
    {
        _direction = new(_playerController.movement.x, 0, _playerController.movement.y);

        if (_direction != Vector3.zero)
        {

            Vector3 moveDirection = transform.TransformDirection(_direction);

            _rb.MovePosition(_rb.position + _moveSpeed * Time.fixedDeltaTime * moveDirection);
            _playerController.RotateToMousePosition();
        }
        else
        {
            _playerController.ChangeState(_playerController._idleState);
        }
    }

    public void OnStateExit()
    {

    }
}

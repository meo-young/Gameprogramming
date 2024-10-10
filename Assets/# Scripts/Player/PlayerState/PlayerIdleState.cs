using UnityEngine;

public class PlayerIdleState : MonoBehaviour, IPlayerState
{
    private PlayerController _playerController;
    private Vector2 _direction;

    public void OnStateEnter(PlayerController npcController)
    {
        if (!_playerController)
            _playerController = npcController;
    }
    public void OnStateUpdate()
    {
        _direction = _playerController.movement;

        _playerController.RotateToMousePosition();

        if (_direction != Vector2.zero)
        {
            _playerController.ChangeState(_playerController._moveState);
        }
    }

    public void OnStateExit()
    {

    }
}

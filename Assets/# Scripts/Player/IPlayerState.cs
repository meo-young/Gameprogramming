public interface IPlayerState
{
    void OnStateEnter(PlayerController playerController);
    void OnStateUpdate();
    void OnStateExit();
}

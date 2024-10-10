public interface IEnemyState
{
    void OnStateEnter(EnemyController enemyController);
    void OnStateUpdate();
    void OnStateExit();
}

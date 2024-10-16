using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyIdleState : MonoBehaviour, IEnemyState
{
    private EnemyController _enemyController;

    NavMeshAgent _navMeshAget;
    float startTIme;
    float _idleTimer;

    public void OnStateEnter(EnemyController enemyController)
    {
        if (!_enemyController)
            _enemyController = enemyController;

        ResetDefaultValue();
    }
    public void OnStateUpdate()
    {
        startTIme += Time.deltaTime;
        if(startTIme > _idleTimer)
        {
            _navMeshAget.isStopped = false;
            _enemyController.ChangeState(_enemyController._moveState);
        }
    }

    public void OnStateExit()
    {

    }

    void ResetDefaultValue()
    {
        _navMeshAget = _enemyController.navMeshAgent;
        startTIme = 0;
        _idleTimer = _enemyController.idleTimer;
    }
}

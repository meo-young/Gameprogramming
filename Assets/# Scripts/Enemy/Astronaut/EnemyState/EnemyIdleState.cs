using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyIdleState : MonoBehaviour, IEnemyState
{
    private EnemyController _enemyController;


    public void OnStateEnter(EnemyController enemyController)
    {
        if (!_enemyController)
            _enemyController = enemyController;

    }
    public void OnStateUpdate()
    {
        /*startTIme += Time.deltaTime;
        if(startTIme > _idleTimer)
        {
            _navMeshAget.isStopped = false;
            _enemyController.ChangeState(_enemyController._moveState);
        }*/
    }

    public void OnStateExit()
    {

    }

}

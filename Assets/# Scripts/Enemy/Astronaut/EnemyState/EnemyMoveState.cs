using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMoveState : MonoBehaviour, IEnemyState
{
    private EnemyController _enemyController;

    Transform _target;
    NavMeshAgent _navMeshAgent;
    float startTimer;
    float _chasingTimer;
    float moveRadius = 100f; // 캐릭터가 이동할 수 있는 반경


    public void OnStateEnter(EnemyController enemyController)
    {
        if (!_enemyController)
            _enemyController = enemyController;

        ResetDefaultValue();

        if (_navMeshAgent.isOnNavMesh)
            MoveToRandomPoint();
    }
    public void OnStateUpdate()
    {
        startTimer += Time.deltaTime;
        
            //_navMeshAgent.SetDestination(_target.transform.position);

        if(startTimer > _chasingTimer)
        {
            _navMeshAgent.isStopped = true;
            _enemyController.ChangeState(_enemyController._idleState);
        }
    }

    public void OnStateExit()
    {

    }

    void ResetDefaultValue()
    {
        _target = _enemyController.target;
        _navMeshAgent = _enemyController.navMeshAgent;
        _navMeshAgent.speed = _enemyController.moveSpeed;
        startTimer = 0;
        _chasingTimer = _enemyController.chasingTimer;
    }

    void MoveToRandomPoint()
    {
        // 현재 위치를 기준으로 랜덤한 위치 계산
        Vector3 randomDirection = Random.insideUnitSphere * moveRadius;
        randomDirection += transform.position;

        NavMeshHit hit;
        // 랜덤 위치가 NavMesh 내에 있는지 확인
        if (NavMesh.SamplePosition(randomDirection, out hit, moveRadius, NavMesh.AllAreas))
        {
            _navMeshAgent.SetDestination(hit.position); // 목적지 설정
        }
    }
}

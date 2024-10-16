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
    float moveRadius = 100f; // ĳ���Ͱ� �̵��� �� �ִ� �ݰ�


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
        // ���� ��ġ�� �������� ������ ��ġ ���
        Vector3 randomDirection = Random.insideUnitSphere * moveRadius;
        randomDirection += transform.position;

        NavMeshHit hit;
        // ���� ��ġ�� NavMesh ���� �ִ��� Ȯ��
        if (NavMesh.SamplePosition(randomDirection, out hit, moveRadius, NavMesh.AllAreas))
        {
            _navMeshAgent.SetDestination(hit.position); // ������ ����
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DroneController : MonoBehaviour
{
    [Header("# Stat")]
    [Tooltip("드론 이동속도")]
    [SerializeField] float moveSpeed = 15f;
    [Tooltip("드론 회전속도")]
    [SerializeField] float rotationSpeed = 10f;

    [Header("# Component")]
    [Tooltip("드론 AI 구현을 위한 NavMeshAgent")]
    [SerializeField] NavMeshAgent navMeshAgent;

    [Header("# Target")]
    [Tooltip("드론이 추적할 Target")]
    [SerializeField] Transform target;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        target = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    private void Start()
    {
        navMeshAgent.speed = moveSpeed;
    }


    // Update is called once per frame
    void Update()
    {
        RotationToTarget();
        navMeshAgent.SetDestination(target.position);
    }

    void RotationToTarget()
    {
        //플레이어 방향으로 천천히 회전
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        targetRotation = Quaternion.Euler(0f, targetRotation.eulerAngles.y, 0f);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
    }
}

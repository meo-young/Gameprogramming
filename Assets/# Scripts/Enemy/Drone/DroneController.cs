using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SocialPlatforms.Impl;

public class DroneController : MonoBehaviour
{
    [Header("# Stat")]
    [Tooltip("드론 이동속도")]
    [SerializeField] float moveSpeed = 15f;
    [Tooltip("드론 회전속도")]
    [SerializeField] float rotationSpeed = 10f;

    [Header("# Component")]
    [Tooltip("드론 AI 구현을 위한 NavMeshAgent")]
    public NavMeshAgent navMeshAgent;

    [Header("# Target")]
    [Tooltip("드론이 추적할 Target")]
    [SerializeField] Transform target;

    [Header("# Score")]
    [Tooltip("잡을 시 받을 점수")]
    [SerializeField] int score;

    NavMeshHit hit;

    private void Awake()
    {
        if(navMeshAgent == null)
            navMeshAgent = GetComponent<NavMeshAgent>();
        target = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    private void Start()
    {
        navMeshAgent.speed = moveSpeed;
        AdjustPositionForNavmesh();
    }


    // Update is called once per frame
    void Update()
    {
        RotationToTarget();
        navMeshAgent.SetDestination(target.position);
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Red"))
        {
            if (other.gameObject.activeSelf)
                other.gameObject.SetActive(false);
            //0 : Red Bullet, 1 : Bule Bullet
            Dead(deadType: 0);
        }
        else if (other.CompareTag("Blue"))
        {
            if (other.gameObject.activeSelf)
                other.gameObject.SetActive(false);
            Dead(deadType: 1);
        }
    }

    void AdjustPositionForNavmesh()
    {
        if (NavMesh.SamplePosition(this.transform.position, out hit, 1.0f, NavMesh.AllAreas))
        {
            navMeshAgent.Warp(hit.position); // 에이전트를 NavMesh 위로 이동
        }
    }
    void RotationToTarget()
    {
        //플레이어 방향으로 천천히 회전
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        targetRotation = Quaternion.Euler(0f, targetRotation.eulerAngles.y, 0f);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
    }

    public void Dead(int deadType)
    {
        GameManager.instance.scoreCounter += score;


        switch (deadType)
        {
            case 0:
                Instantiate(GameManager.instance.redBulletEffect, transform.position, transform.rotation);
                break;
            case 1:
                Instantiate(GameManager.instance.blueBulletEffect, transform.position, transform.rotation);
                break;
        }

        if (this.gameObject.activeSelf)
        {
            this.gameObject.SetActive(false);
        }
    }
}

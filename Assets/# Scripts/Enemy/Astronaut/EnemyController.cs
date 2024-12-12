using INab.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    [Header("# Component")]
    [Tooltip("���� ������ ���� Rigidbody ������Ʈ")]
    public Rigidbody rigidBody;
    [Tooltip("AI ������ ���� NavMeshAgent ������Ʈ")]
    public NavMeshAgent navMeshAgent;
    public Animator anim;

    [Header("# Target")]
    [Tooltip("�߰��� ���")]
    public Transform target;
    [Tooltip("�Ѿư��� �ð� ����")]
    public float chasingTimer = 5f;
    [Tooltip("���� �ð� ����")]
    public float idleTimer = 2f;
    [SerializeField] public GameObject explosionEffect;
    private float explosionCounter = 2;

    [Header("# Movement")]
    [Tooltip("�÷��̾� �̵� �ӵ�")]
    public float moveSpeed;

    [Header("# Score")]
    [Tooltip("���� �� ���� ����")]
    public int score;

    int bulletIndex;
    NavMeshHit hit;

    public static EnemyController Instance { get; private set; } // Singleton �ν��Ͻ�


    public IEnemyState CurrentState
    {
        get; set;
    }

    public Vector2 CurrentDirection
    {
        get; set;
    }

    public IEnemyState _idleState, _moveState;

    private void OnEnable()
    {
        Init();
    }

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        rigidBody = GetComponent<Rigidbody>();
        navMeshAgent = GetComponent<NavMeshAgent>();


        if (_idleState == null)
            _idleState = gameObject.AddComponent<EnemyIdleState>();

        if (_moveState == null)
            _moveState = gameObject.AddComponent<EnemyMoveState>();
    }


    private void Start()
    {
        AdjustPositionForNavmesh();
        //Init();
    }

    private void Update()
    {
        UpdateState();


        navMeshAgent.SetDestination(target.position);

        if(navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance+2)
        {
            anim.SetBool("Walk", false);
            navMeshAgent.isStopped = true;
            explosionCounter -= Time.deltaTime;
            if(explosionCounter < 0)
            {
                explosionCounter = 2;
                Instantiate(explosionEffect, this.transform.position, Quaternion.identity);
                GameManager.instance.scoreCounter -= 10;
                this.gameObject.SetActive(false);
            }
        }
        else
        {
            anim.SetBool("Walk", true);
            navMeshAgent.isStopped=false;
            explosionCounter = 2;
        }


    }

    public void ChangeState(IEnemyState enemyState)
    {
        if (CurrentState != null)
            CurrentState.OnStateExit();
        CurrentState = enemyState;
        CurrentState.OnStateEnter(this);
    }

    public void UpdateState()
    {
        if (CurrentState != null)
        {
            CurrentState.OnStateUpdate();
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if(other.CompareTag("Red"))
        {
            if (other.gameObject.activeSelf)
                other.gameObject.SetActive(false);
            //0 : Red Bullet, 1 : Bule Bullet
            Dead(deadType: 0);
        }
    }



    void AdjustPositionForNavmesh()
    {
        if (NavMesh.SamplePosition(this.transform.position, out hit, 1.0f, NavMesh.AllAreas))
        {
            navMeshAgent.Warp(hit.position); // ������Ʈ�� NavMesh ���� �̵�
        }
    }

    void Init()
    {
        Debug.Log(gameObject.name);
        ChangeState(_idleState);
    }

    public void Dead(int deadType)
    {
        GameManager.instance.scoreCounter += score;

        if(this.gameObject.activeSelf)
        {
            this .gameObject.SetActive(false);
        }
    }
}

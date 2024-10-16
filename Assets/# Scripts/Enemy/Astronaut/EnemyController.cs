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

    [Header("# Target")]
    [Tooltip("�߰��� ���")]
    public Transform target;
    [Tooltip("�Ѿư��� �ð� ����")]
    public float chasingTimer = 5f;
    [Tooltip("���� �ð� ����")]
    public float idleTimer = 2f;

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
            ShowBulletEffect(deadType: 0);

        }
        else if(other.CompareTag("Blue"))
        {
            if (other.gameObject.activeSelf)
                other.gameObject.SetActive(false);
            //Dead(deadType: 1);
            ShowBulletEffect(deadType : 1);

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
        StartCoroutine(SetActiveFalseSelf());
    }
    IEnumerator SetActiveFalseSelf()
    {
        yield return new WaitForSeconds(5.0f);

        if(this.gameObject.activeSelf)
        {
            //yield return new WaitForSeconds(2.0f);
            this.gameObject.SetActive(false);
        }
    }

    public void Dead(int deadType)
    {
        GameManager.instance.scoreCounter += score;

        if(this.gameObject.activeSelf)
        {
            this .gameObject.SetActive(false);
        }
    }

    public void ShowBulletEffect(int deadType)
    {
        Vector3 effectPos = new(transform.position.x, transform.position.y + 5f, transform.position.z);
        switch (deadType)
        {
            case 0:
                Instantiate(GameManager.instance.redBulletEffect, effectPos, transform.rotation);
                break;
            case 1:
                Instantiate(GameManager.instance.blueBulletEffect, effectPos, transform.rotation);
                break;
        }
    }
}

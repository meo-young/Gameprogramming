using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnManager : MonoBehaviour
{
    [Tooltip("Enemy가 나올 영역")]
    [SerializeField] Transform[] spawnPoint;

    [Tooltip("Spawn할 Enemy에 대한 Type별 속성값 지정")]
    [SerializeField] SpawnData spawnData;

    private float timer;

    private void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>(); //자기자신을 포함해 들어가기때문에 유효 index는 1부터 시작
    }
    private void Update()
    {
        if (!GameManager.instance.gameStartFlag)
            return;

        timer += Time.deltaTime;
        if (timer > spawnData.spawnTime)
        {
            timer = 0;
            Spawn();

        }
    }

    void Spawn()
    {
        GameObject enemy = null;
        switch (spawnData.enemyType)
        {
            case EnemyType.Astronaut:
                enemy = GameManager.instance.poolManager.GetMonster(0);
                enemy.GetComponent<EnemyController>().navMeshAgent.Warp(spawnPoint[Random.Range(1, spawnPoint.Length)].position);
                break;
            case EnemyType.Drone:
                enemy = GameManager.instance.poolManager.GetMonster(1);
                enemy.GetComponent<DroneController>().navMeshAgent.Warp(spawnPoint[Random.Range(1, spawnPoint.Length)].position);
                break;
        }
    }
}


[System.Serializable] // Inspector 창에서 보이도록 적용
public class SpawnData
{
    public float spawnTime; //소환시간

    public EnemyType enemyType;
    public int health; //체력
    public float speed; //이동속도
}


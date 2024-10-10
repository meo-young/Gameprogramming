using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnManager : MonoBehaviour
{
    [Tooltip("Enemy가 나올 영역")]
    [SerializeField] Transform[] spawnPoint;

    [Tooltip("Spawn할 Enemy에 대한 Type별 속성값 지정")]
    [SerializeField] SpawnData[] spawnData;

    private float timer;
    private int level = 0;

    private void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>(); //자기자신을 포함해 들어가기때문에 유효 index는 1부터 시작
    }
    private void Update()
    {
        timer += Time.deltaTime;
        //level = Mathf.Min(Mathf.FloorToInt(GameManager.instance.gameTime / 10f), spawnData.Length - 1); //level이 spawnData의 index를 넘어가지 않도록 설정
        if (timer > spawnData[level].spawnTime)
        {
            timer = 0;
            Spawn();

        }
    }

    void Spawn()
    {
        GameObject enemy = GameManager.instance.poolManager.GetMonster(Random.Range(0, level + 1));
        //Debug.Log(enemy.name);
        enemy.GetComponent<EnemyController>().navMeshAgent.Warp(spawnPoint[Random.Range(1, spawnPoint.Length)].position);
        //enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
        //enemy.GetComponent<Enemy>().Init(spawnData[level]);
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


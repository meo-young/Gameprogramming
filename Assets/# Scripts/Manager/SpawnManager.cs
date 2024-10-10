using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnManager : MonoBehaviour
{
    [Tooltip("Enemy�� ���� ����")]
    [SerializeField] Transform[] spawnPoint;

    [Tooltip("Spawn�� Enemy�� ���� Type�� �Ӽ��� ����")]
    [SerializeField] SpawnData[] spawnData;

    private float timer;
    private int level = 0;

    private void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>(); //�ڱ��ڽ��� ������ ���⶧���� ��ȿ index�� 1���� ����
    }
    private void Update()
    {
        timer += Time.deltaTime;
        //level = Mathf.Min(Mathf.FloorToInt(GameManager.instance.gameTime / 10f), spawnData.Length - 1); //level�� spawnData�� index�� �Ѿ�� �ʵ��� ����
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


[System.Serializable] // Inspector â���� ���̵��� ����
public class SpawnData
{
    public float spawnTime; //��ȯ�ð�

    public EnemyType enemyType;
    public int health; //ü��
    public float speed; //�̵��ӵ�
}


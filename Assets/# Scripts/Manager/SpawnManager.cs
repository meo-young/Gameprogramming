using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnManager : MonoBehaviour
{
    [Tooltip("Enemy�� ���� ����")]
    [SerializeField] Transform[] spawnPoint;

    [Tooltip("Spawn�� Enemy�� ���� Type�� �Ӽ��� ����")]
    [SerializeField] SpawnData spawnData;

    private float timer;

    private void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>(); //�ڱ��ڽ��� ������ ���⶧���� ��ȿ index�� 1���� ����
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


[System.Serializable] // Inspector â���� ���̵��� ����
public class SpawnData
{
    public float spawnTime; //��ȯ�ð�

    public EnemyType enemyType;
    public int health; //ü��
    public float speed; //�̵��ӵ�
}


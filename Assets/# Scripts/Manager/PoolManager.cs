﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [Tooltip("무기별 프리팹을 보관할 변수")]
    [SerializeField] GameObject[] weaponArray;

    [Tooltip("오브젝트 풀을 구성하는 리스트")]
    [SerializeField] List<GameObject>[] weaponRedPools;
    [Tooltip("오브젝트 풀을 구성하는 리스트")]
    [SerializeField] List<GameObject>[] weaponBulePools;

    [Tooltip("레벨별 몬스터 프리팹을 보관할 변수")]
    [SerializeField] PrefabArray[] levelArray;

    [Tooltip("오브젝트 풀을 구성하는 리스트")]
    [SerializeField] List<GameObject>[] monsterPools;

    [Tooltip("Red Bullet Pool")]
    [SerializeField] Transform redPool;

    [Tooltip("Bule Bullet Pool")]
    [SerializeField] Transform bulePool;


    readonly WaitForSeconds delayForActiveFalse = new(2.0f); //오브젝트가 사라지는 데에 소요되는 시간

    private void Awake()
    {
        monsterPools = new List<GameObject>[levelArray.Length];

        for (int i = 0; i < monsterPools.Length; i++)
        {
            monsterPools[i] = new List<GameObject>();
        }

        weaponRedPools = new List<GameObject>[weaponArray.Length];

        for (int i = 0; i < weaponRedPools.Length; i++)
        {
            weaponRedPools[i] = new List<GameObject>();
        }

        weaponBulePools = new List<GameObject>[weaponArray.Length];

        for (int i = 0; i < weaponBulePools.Length; i++)
        {
            weaponBulePools[i] = new List<GameObject>();
        }
    }

    public GameObject GetMonster(int index)
    {
        //선택한 풀의 비활성화된 게임오브젝트 접근
        //발견시 selectObject에 할당
        //모두 활성화된 경우 새롭게 생성한 후 select 변수에 할당
        //Destroy, Instantiate로 발생하는 메모리 누수 방지

        GameObject selectObject = null;

        foreach (GameObject item in monsterPools[index])
        {
            if (!item.activeSelf)
            {
                selectObject = item;
                selectObject.SetActive(true);
                break;
            }
        }

        if (!selectObject)
        {
            selectObject = Instantiate(levelArray[index].monster[Random.Range(0, levelArray[index].monster.Length)], transform);
            monsterPools[index].Add(selectObject);
        }


        return selectObject;
    }

    public GameObject GetWeapon(int index)
    {
        //선택한 풀의 비활성화된 게임오브젝트 접근
        //발견시 selectObject에 할당
        //모두 활성화된 경우 새롭게 생성한 후 select 변수에 할당
        //Destroy, Instantiate로 발생하는 메모리 누수 방지
        GameObject selectObject = null;


        if (index == 0)
        {
            foreach (GameObject item in weaponRedPools[index])
            {
                if (!item.activeSelf)
                {
                    selectObject = item;
                    selectObject.SetActive(true);
                    break;
                }
            }

            if (!selectObject)
            {

                selectObject = Instantiate(weaponArray[index], redPool);
                weaponRedPools[index].Add(selectObject);
            }
        }

        else if (index == 1)
        {
            foreach (GameObject item in weaponBulePools[index])
            {
                if (!item.activeSelf)
                {
                    selectObject = item;
                    selectObject.SetActive(true);
                    break;
                }
            }

            if (!selectObject)
            {

                selectObject = Instantiate(weaponArray[index], bulePool);
                weaponBulePools[index].Add(selectObject);
            }
        }
        StartCoroutine(SetActiveFalseSelf(selectObject));

        return selectObject;
    }

    IEnumerator SetActiveFalseSelf(GameObject _object)
    {
        yield return delayForActiveFalse;

        if(_object.activeSelf)
            _object.SetActive(false);
    }
}


[System.Serializable]
public class PrefabArray
{
    public GameObject[] monster;
}
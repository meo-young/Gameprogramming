﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("# Manager")]
    public PoolManager poolManager;

    [Header("# Variable")]
    [SerializeField] int scoreForWin = 500; // 승리조건 점수
    public float gameTimer = 60f; //게임 종료 시간

    [Header("# Effect")]
    public GameObject redBulletEffect;
    public GameObject blueBulletEffect;

    private bool resultFlag;
    [HideInInspector] public int scoreCounter;
    [HideInInspector] public bool gameStartFlag;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        scoreCounter = 0;
        resultFlag = false;
        gameStartFlag = false;
    }

    private void Update()
    {
        if (resultFlag)
            return;

        if (!gameStartFlag)
            return;

        if(gameTimer <= 0)
        {   
            resultFlag = true;
            gameTimer = 0;
            if(scoreCounter >= scoreForWin)
            {
                Debug.Log("Win");
                SceneManager.LoadScene("Win");
            }
            else
            {
                Debug.Log("Lose");
                SceneManager.LoadScene("Lose");
            }
        }
        else
        {
            gameTimer -= Time.deltaTime;
        }
    }
}

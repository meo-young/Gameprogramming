using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIController : MonoBehaviour
{
    [SerializeField] Text gameTimer;
    [SerializeField] Text gameScore;

    private void Update()
    {
        gameTimer.text = GameManager.instance.gameTimer.ToString("F0");
        gameScore.text = "Score : " +GameManager.instance.scoreCounter.ToString();
    }
}

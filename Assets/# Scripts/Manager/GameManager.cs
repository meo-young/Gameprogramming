using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("# Manager")]
    public PoolManager poolManager;

    [Header("# Player")]
    public PlayerController player;

    private void Awake()
    {
        instance = this;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStarter : MonoBehaviour
{
    [SerializeField] Material dissolveMAT;
    [SerializeField] List<GameObject> wall;

    private float value = 1.0f;
    private bool startFlag = false;
    private bool finishFlag = false;

    private void Update()
    {
        if (finishFlag)
            return;

        if(startFlag)
        {
            value -= Time.deltaTime;
            dissolveMAT.SetFloat("_SplitValue", value);
            if(value < 0f)
            {
                finishFlag = true;
                this.gameObject.SetActive(false);
                for(int i =0; i< wall.Count; i++)
                {
                    Destroy(wall[i]);
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            GameManager.instance.gameStartFlag = true;
            startFlag = true;
        }
    }
}

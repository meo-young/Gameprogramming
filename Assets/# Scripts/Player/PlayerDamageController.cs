using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("EnemyAttack"))
        {
            GameManager.instance.scoreCounter -= 10;
        }
    }
}

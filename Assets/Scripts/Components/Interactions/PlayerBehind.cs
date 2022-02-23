using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehind : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        EnemyPathFind enemy = other.GetComponent<EnemyPathFind>();
        if (enemy != null)
        {
            if (enemy.isBigEnemy)
            {
                enemy.goneBehind = true;
            }
        }
    }
}

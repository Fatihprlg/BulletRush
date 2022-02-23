using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InGameUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI bigEnemyCounterTxt;
    [SerializeField] TextMeshProUGUI simpleEnemyCounterTxt;
    [SerializeField] TextMeshProUGUI enemySpawnCounterTxt;

    void Update()
    {
        bigEnemyCounterTxt.text = GameController.Instance.GetActiveEnemyCount(PoolType.BigEnemy).ToString();
        simpleEnemyCounterTxt.text = GameController.Instance.GetActiveEnemyCount(PoolType.SimpleEnemy).ToString();
        enemySpawnCounterTxt.text = GameController.Instance.activeSpawnCount.ToString();
    }
}

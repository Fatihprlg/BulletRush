using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private static GameController _instance;

    public static GameController Instance { get { return _instance; } }
    [SerializeField] int spawnCount = 3;
    public int activeSpawnCount;
    void Start()
    {
        activeSpawnCount = spawnCount;
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public int GetActiveEnemyCount(PoolType enemyType)
    {
        int enemyCount = 0;
        PoolInfo info = ObjectPool.Instance.GetPoolInfo(enemyType);
        foreach (var item in info.poolObjects)
        {
            if (item.activeInHierarchy) enemyCount++;
        }
        return enemyCount;
    }


    public void GameOver()
    {

    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] int spawnCount = 3;
    [SerializeField] GameObject levelFailedMenu;
    [SerializeField] GameObject levelPassedMenu;
    [SerializeField] GameObject inGameUI;

    private static GameController _instance;
    public static GameController Instance { get { return _instance; } }

    [HideInInspector] public int activeSpawnCount;
    [HideInInspector] public bool isSuccess;

    void Start()
    {
        activeSpawnCount = spawnCount;
        //Singleton
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    private void Update()
    {
        if (activeSpawnCount <= 0 && 
            GetActiveEnemyCount(PoolType.SimpleEnemy) <= 0 &&
            GetActiveEnemyCount(PoolType.BigEnemy) <= 0 &&
            !isSuccess)
        {
            isSuccess = true;
            Invoke("GameOver", 1.2f);
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
        inGameUI.SetActive(false);
        if (!isSuccess)
            levelFailedMenu.SetActive(true);
        else
            levelPassedMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResetLevel()
    {
        Time.timeScale = 1f;
        // if I want to add new scenes this can be useful. Because I want to reload active scene.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
    }
    
    public void NextLevel()
    {
        Time.timeScale = 1f;
        // I want to make a condition that if a next level exists go to next level, if there is no new level reload level
        if (SceneManager.sceneCountInBuildSettings > SceneManager.GetActiveScene().buildIndex + 1)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        else
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

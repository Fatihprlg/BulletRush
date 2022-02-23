using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] GameObject gun1, gun2;
    [SerializeField] float fireDistance = 10f;
    
    GameObject target1, target2;
    List<GameObject> enemies = new List<GameObject>();
    GameObject[] spawnPoints;
    PoolInfo bulletPool; 
    
    int bulletPoolCounter = 0;
    float fireElapsedTime = 0;
    
    public float fireDelay = 0.1f;

    private void Start()
    {
        PoolInfo simpleEnemies = ObjectPool.Instance.GetPoolInfo(PoolType.SimpleEnemy);
        PoolInfo bigEnemies = ObjectPool.Instance.GetPoolInfo(PoolType.BigEnemy);
        bulletPool = ObjectPool.Instance.GetPoolInfo(PoolType.Bullet);
        enemies.AddRange(simpleEnemies.poolObjects);
        enemies.AddRange(bigEnemies.poolObjects);
        spawnPoints = GameObject.FindGameObjectsWithTag("EnemySpawn");
    }

    private void FixedUpdate()
    {
        fireElapsedTime += Time.deltaTime;
        if (fireElapsedTime >= fireDelay)
        {
            fireElapsedTime = 0;
            Fire();
        }

    }

    bool FindNearestEnemy(GameObject gun, out GameObject target)
    {

        float enemyDistance = fireDistance * 2;
        float spawnDistance = enemyDistance;
        float survivingDistance = fireDistance / 2;
        GameObject nearestEnemy = null, nearestSpawn = null;
        foreach (GameObject enemy in enemies)
        {
            if (Vector3.Distance(gun.transform.position, enemy.transform.position) < enemyDistance)
            {
                nearestEnemy = enemy;
                enemyDistance = Vector3.Distance(gun.transform.position, enemy.transform.position);
            }
        }

        foreach (GameObject spawn in spawnPoints)
        {
            if (Vector3.Distance(gun.transform.position, spawn.transform.position) < spawnDistance)
            {
                nearestSpawn = spawn;
                enemyDistance = Vector3.Distance(gun.transform.position, spawn.transform.position);
            }
        }
        if (nearestEnemy != null && nearestSpawn != null)
        {
            //if spawnPoint near than enemies and nearest enemy further than surviving distance, fire spawnPoint
            if (Vector3.Distance(gun.transform.position, nearestEnemy.transform.position) > survivingDistance &&
            Vector3.Distance(gun.transform.position, nearestSpawn.transform.position) < Vector3.Distance(gun.transform.position, nearestEnemy.transform.position))
            {
                target = nearestSpawn;
            }
            else target = nearestEnemy;
        }
        else if (nearestEnemy != null && nearestSpawn == null) target = nearestEnemy;
        else if (nearestEnemy == null && nearestSpawn != null) target = nearestSpawn;
        else target = null;

        if (target == null) return false;
        else return true;
    }

    void Fire()
    {
        if (FindNearestEnemy(gun1, out target1) && target1.activeInHierarchy)
        {
            gun1.transform.LookAt(new Vector3(target1.transform.position.x, 1.5f, target1.transform.position.z));
            if (Vector3.Distance(gun1.transform.position, target1.transform.position) <= fireDistance)
            {
                bulletPool.poolObjects[bulletPoolCounter].SetActive(true);
                bulletPool.poolObjects[bulletPoolCounter].transform.position = gun1.transform.GetChild(0).position;
                bulletPool.poolObjects[bulletPoolCounter].GetComponent<Rigidbody>().velocity = Vector3.zero;
                bulletPool.poolObjects[bulletPoolCounter++].GetComponent<Rigidbody>().AddForce((target1.transform.position - gun1.transform.position).normalized * 1000);
                if (bulletPoolCounter >= bulletPool.poolSize) bulletPoolCounter = 0;

            }
        }
        if (FindNearestEnemy(gun2, out target2) && target2.activeInHierarchy)
        {
            gun2.transform.LookAt(new Vector3(target2.transform.position.x, 1.5f, target2.transform.position.z));
            if (Vector3.Distance(gun2.transform.position, target2.transform.position) <= fireDistance)
            {
                bulletPool.poolObjects[bulletPoolCounter].SetActive(true);
                bulletPool.poolObjects[bulletPoolCounter].transform.position = gun2.transform.GetChild(0).position;
                bulletPool.poolObjects[bulletPoolCounter].GetComponent<Rigidbody>().velocity = Vector3.zero;
                bulletPool.poolObjects[bulletPoolCounter++].GetComponent<Rigidbody>().AddForce((target2.transform.position - gun2.transform.position).normalized * 1000);
                if (bulletPoolCounter >= bulletPool.poolSize) bulletPoolCounter = 0;
            }
        }
    }

    public void DealDamage()
    {
        GameController.Instance.isSuccess = false;
        GameController.Instance.GameOver();
        gameObject.SetActive(false);
    }

}

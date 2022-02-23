using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] GameObject gun1, gun2;
    [SerializeField] float fireDistance = 10f;
    GameObject target1, target2;
    List<GameObject> enemies = new List<GameObject>();
    PoolInfo bulletPool;
    int bulletPoolCounter = 0;
    float fireElapsedTime = 0;
    public float fireDelay = 0.01f;

    private void Start()
    {
        PoolInfo simpleEnemies = ObjectPool.Instance.GetPoolInfo(PoolType.SimpleEnemy);
        PoolInfo bigEnemies = ObjectPool.Instance.GetPoolInfo(PoolType.BigEnemy);
        bulletPool = ObjectPool.Instance.GetPoolInfo(PoolType.Bullet);
        enemies.AddRange(simpleEnemies.poolObjects);
        enemies.AddRange(bigEnemies.poolObjects);
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

        float distance = fireDistance * 2;
        GameObject nearest = null;
        foreach (GameObject enemy in enemies)
        {
            if (Vector3.Distance(gun.transform.position, enemy.transform.position) < distance)
            {
                nearest = enemy;
                distance = Vector3.Distance(gun.transform.position, enemy.transform.position);
            }
        }
        target = nearest;
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
                Debug.Log("fire1");
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
            if (Vector3.Distance(gun2.transform.position, target2.transform.position) <= fireDistance )
            {
                Debug.Log("fire2");
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
        GameController.Instance.GameOver();
        gameObject.SetActive(false);
    }

}

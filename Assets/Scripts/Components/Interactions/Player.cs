using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] GameObject gun1, gun2;
    [SerializeField] float fireDistance = 10f;
    GameObject target1, target2;
    List<GameObject> enemies = new List<GameObject>();


    private void Start()
    {
        PoolInfo simpleEnemies = ObjectPool.Instance.GetPoolInfo(PoolType.SimpleEnemy);
        PoolInfo bigEnemies = ObjectPool.Instance.GetPoolInfo(PoolType.BigEnemy);
        enemies.AddRange(simpleEnemies.poolObjects);
        enemies.AddRange(bigEnemies.poolObjects);
    }

    private void Update()
    {

        if(FindNearestEnemy(gun1, out target1))
        {
            gun1.transform.LookAt(new Vector3(target1.transform.position.x, 1.5f, target1.transform.position.z));
            if(Vector3.Distance(gun1.transform.position, target1.transform.position) <= fireDistance)
            {
                Debug.Log("fire1");
            }
        }
        if (FindNearestEnemy(gun2, out target2))
        {
            gun2.transform.LookAt(new Vector3(target2.transform.position.x, 1.5f, target2.transform.position.z));
            if (Vector3.Distance(gun2.transform.position, target2.transform.position) <= fireDistance)
            {
                Debug.Log("fire2");
            }
        }
    }

    bool FindNearestEnemy(GameObject gun, out GameObject target)
    {
        
        float distance = fireDistance * 2;
        GameObject nearest = null;
        foreach (GameObject enemy in enemies)
        {
            if(Vector3.Distance(gun.transform.position, enemy.transform.position) < distance)
            {
                nearest = enemy;
                distance = Vector3.Distance(gun.transform.position, enemy.transform.position);
            }
        }
        target = nearest;
        if (target == null) return false;
        else return true;
    }

    public void DealDamage()
    {
        GameController.Instance.GameOver();
        gameObject.SetActive(false);
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ObjectPool : MonoBehaviour
{

    [SerializeField] List<PoolInfo> poolInfos;
    private static ObjectPool _instance;

    public static ObjectPool Instance { get { return _instance; } }

    private void Awake()
    {

        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        foreach (PoolInfo inf in poolInfos)
        {
            CreatePool(inf);
        }
    }

    void CreatePool(PoolInfo info)
    {
        Debug.Log(info.type);
        if (info.type != PoolType.Bullet)
        {
            NavMeshHit hit;
            if (NavMesh.SamplePosition(info.container.transform.position, out hit, 1.0f, NavMesh.AllAreas))
                for (int i = 0; i < info.poolSize; i++)
                {
                    GameObject obj = Instantiate(info.prefab, hit.position, Quaternion.identity, info.container.transform);
                    obj.SetActive(false);
                    info.poolObjects.Add(obj);
                }
        }
        else
        {
            for (int i = 0; i < info.poolSize; i++)
            {
                GameObject obj = Instantiate(info.prefab, info.container.transform);
                obj.SetActive(false);
                info.poolObjects.Add(obj);

            }
        }

    }

    public PoolInfo GetPoolInfo(PoolType type) => poolInfos.Find(i => i.type == type);
}
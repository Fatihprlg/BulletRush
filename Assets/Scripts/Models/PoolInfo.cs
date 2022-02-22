using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PoolInfo
{
    public PoolType type;
    public GameObject prefab;
    public GameObject container;
    public int poolSize;
    [HideInInspector]
    public List<GameObject> poolObjects;
}
[Serializable]
public enum PoolType
{
    SimpleEnemy,
    BigEnemy,
    Bullet
}

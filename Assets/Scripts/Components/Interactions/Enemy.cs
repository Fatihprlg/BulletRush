using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    //[SerializeField] bool isBigEnemy;
    [SerializeField] int maxHealth;
    int health;

    void Start()
    {
        health = maxHealth;
    }

    public void DealDamage(int value)
    {
        health -= value;
        if (health <= 0) ResetEnemy();
    }

    void ResetEnemy()
    {
        health = maxHealth;
        transform.localPosition = Vector3.zero;
        gameObject.SetActive(false);
    }

}

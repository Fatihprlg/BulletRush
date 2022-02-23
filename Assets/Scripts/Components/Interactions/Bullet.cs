using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 100;
    private void OnEnable()
    {
        StartCoroutine(ResetCounter());
    }

    IEnumerator ResetCounter()
    {
        yield return new WaitForSeconds(2.5f);
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        Debug.Log(other.gameObject.layer);
        if (enemy != null)
        {
            enemy.DealDamage(damage);
        }
        gameObject.SetActive(false);
    }
}
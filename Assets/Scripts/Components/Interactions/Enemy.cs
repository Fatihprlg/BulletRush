using UnityEngine;

public class Enemy : MonoBehaviour
{
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
    private void OnCollisionEnter(Collision collision)
    {
        Player player = collision.collider.GetComponent<Player>();
        if (player != null)
        {
            player.DealDamage();
        }
    }
}

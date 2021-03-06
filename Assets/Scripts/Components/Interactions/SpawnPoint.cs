using UnityEngine;

[RequireComponent(typeof(HealthBar))]
public class SpawnPoint : MonoBehaviour
{
    [SerializeField] int maxHealth = 1500;
    int health;
   
    HealthBar healthBar;

    void Start()
    {
        health = maxHealth;
        healthBar = GetComponent<HealthBar>();
    }

    public void DealDamage(int value)
    {
        health -= value;
        healthBar.UpdateBar(health, maxHealth);
        if (health <= 0)
        {
            gameObject.SetActive(false);
            GameController.Instance.activeSpawnCount--;
        }
    }
}

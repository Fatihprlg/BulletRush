
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Image healthBar;
    [SerializeField] Canvas canvas;
    public void UpdateBar(float health, float maxHealth)
    {
        healthBar.fillAmount = health / maxHealth;
    }


    // Update is called once per frame
    void Update()
    {
        canvas.gameObject.transform.LookAt(Camera.main.transform); 
    }
}

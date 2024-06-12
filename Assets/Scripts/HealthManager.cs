using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthManager : MonoBehaviour
{
    public Image healthBar;
    public float healthAmount = 100f;
    public Animator animator;

    private bool isDead = false;

    private void Update()
    {
        if (healthAmount <= 0 && !isDead)
        {
            Die();
        }
    }

    public void TakeDamage(float damage)
    {
        healthAmount -= damage;
        if (healthBar != null)
        {
            healthBar.fillAmount = healthAmount / 100f;
        }

        if(healthAmount <= 0 && !isDead)
        {
            Die();
        }
    }

    public void Heal(float heal)
    {
        healthAmount += heal;
        healthAmount = Mathf.Clamp(healthAmount, 0, 100);

        if (healthBar != null)
        {
            healthBar.fillAmount = healthAmount / 100f;
        }
    }

    private void Die()
    {      
        isDead = true;
        animator.SetTrigger("Death");
        StartCoroutine(RemoveAfterDelay(6f));
    }

    private IEnumerator RemoveAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}

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

    public bool isPlayer = false;
    public bool isDead = false;

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

        if (healthAmount <= 0 && !isDead)
        {
            Die();
        }
    }

    /*public void Heal(float heal)
    {
        healthAmount += heal;
        healthAmount = Mathf.Clamp(healthAmount, 0, 100);

        if (healthBar != null)
        {
            healthBar.fillAmount = healthAmount / 100f;
        }
    }*/

    private void Die()
    {      
        isDead = true;
        if(isPlayer == true)
        {
            StartCoroutine(ReloadLevelAfterDelay(0f));
        }
        else
        {
            if(animator != null)
            {
                animator.ResetTrigger("Walk");
                animator.ResetTrigger("Attack");
                animator.SetTrigger("Death");
            }
            StartCoroutine(RemoveAfterDelay(2.5f));
        }
    }

    private IEnumerator ReloadLevelAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private IEnumerator RemoveAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}

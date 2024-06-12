using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhoulAI : MonoBehaviour
{
    public Animator animator;
    private float ghoulSpeed = 2.5f;

    private float distance;
    private float attackDistance = 2f;

    private float delay = 1.5f;
    private float time = 0;

    private HealthManager healthManager;

    private void Start()
    {
        healthManager = GetComponent<HealthManager>();
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && healthManager.healthAmount > 0)
        {
            animator.ResetTrigger("Idle");
            animator.SetTrigger("Run");

            Vector3 direction = (other.transform.position - transform.position).normalized;
            distance = Vector3.Distance(transform.position, other.transform.position);

            if (distance <= attackDistance)
            {
                if (time <= 0)
                {
                    animator.ResetTrigger("Run");
                    animator.SetTrigger("Attack");

                    other.SendMessage("Damage", 20);
                    time = delay;
                }
                else
                {
                    time -= Time.deltaTime;
                }
            }
            else
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, ghoulSpeed * Time.deltaTime);

                transform.Translate(Vector3.forward * ghoulSpeed * Time.deltaTime);
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            animator.ResetTrigger("Run");
            animator.SetTrigger("Idle");
        }
    }
}

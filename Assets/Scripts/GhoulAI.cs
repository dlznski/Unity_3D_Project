using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhoulAI : MonoBehaviour
{
    public Animator animator;
    public AudioSource ghoulAudioSource;
    public AudioClip ghoulSound;

    private float ghoulSpeed = 4f;
    private float distance;
    private float attackDistance = 2f;
    private float delay = 1.5f;
    private float time = 0;

    private HealthManager healthManager;
    private Collider playerCollider;
    private InGameMenu inGameMenu;

    private bool isPlayerInRange = false;

    private void Start()
    {
        healthManager = GetComponent<HealthManager>();
        playerCollider = GameObject.FindWithTag("Player").GetComponent<Collider>();
        inGameMenu = FindObjectOfType<InGameMenu>();

        if (ghoulAudioSource != null)
        {
            ghoulAudioSource.clip = ghoulSound;
            ghoulAudioSource.loop = true;
        }
    }

    private void Update()
    {
        HandleAudio();
    }

    private void HandleAudio()
    {
        if (isPlayerInRange && !inGameMenu.paused && !ghoulAudioSource.isPlaying)
        {
            ghoulAudioSource.Play();
        }
        else if ((!isPlayerInRange || inGameMenu.paused) && ghoulAudioSource.isPlaying)
        {
            ghoulAudioSource.Stop();
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other == playerCollider && healthManager != null && healthManager.healthAmount > 0)
        {
            isPlayerInRange = true;

            if (healthManager.isDead)
            {
                return;
            }

            Vector3 direction = (other.transform.position - transform.position).normalized;
            distance = Vector3.Distance(transform.position, other.transform.position);

            if (distance <= attackDistance)
            {
                if (time <= 0)
                {
                    animator.SetBool("IsWalking", false);
                    animator.SetTrigger("Attack");

                    HealthManager playerHealth = other.GetComponent<HealthManager>();
                    if (playerHealth != null)
                    {
                        Debug.Log("Attacking player");
                        playerHealth.TakeDamage(20);
                    }

                    time = delay;
                }
                else
                {
                    time -= Time.deltaTime;
                }
            }
            else
            {
                animator.SetBool("IsWalking", true);
                animator.SetBool("IsIdle", false);

                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, ghoulSpeed * Time.deltaTime);

                transform.Translate(Vector3.forward * ghoulSpeed * Time.deltaTime);
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other == playerCollider)
        {
            isPlayerInRange = false;
            animator.SetBool("IsWalking", false);
            animator.SetBool("IsIdle", true);
        }
    }
}

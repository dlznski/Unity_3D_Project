using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
    private NavMeshAgent navMeshAgent;
    private bool isPlayerInRange = false;

    private void Start()
    {
        healthManager = GetComponent<HealthManager>();
        playerCollider = GameObject.FindWithTag("Player").GetComponent<Collider>();
        inGameMenu = FindObjectOfType<InGameMenu>();
        navMeshAgent = GetComponent<NavMeshAgent>();

        if (ghoulAudioSource != null)
        {
            ghoulAudioSource.clip = ghoulSound;
            ghoulAudioSource.loop = true;
        }
    }

    void Update()
    {
        if (healthManager.isDead)
        {
            StopAllActions();
        }
        else if (isPlayerInRange && !inGameMenu.paused)
        {
            FollowPlayer();
            PlayAudio();
            CheckAttack();
        }
        else
        {
            navMeshAgent.isStopped = true;
            if (ghoulAudioSource.isPlaying)
            {
                ghoulAudioSource.Stop();
            }
        }
    }

    private void PlayAudio()
    {
        if (!ghoulAudioSource.isPlaying)
        {
            ghoulAudioSource.Play();
        }
    }

    private void FollowPlayer()
    {
        if (playerCollider != null && !healthManager.isDead)
        {
            navMeshAgent.isStopped = false;
            navMeshAgent.SetDestination(playerCollider.transform.position);
        }
    }

    private void StopAllActions()
    {
        navMeshAgent.isStopped = true;
        navMeshAgent.ResetPath();
        if (ghoulAudioSource.isPlaying)
        {
            ghoulAudioSource.Stop();
        }
        animator.SetBool("IsWalking", false);
        animator.SetBool("IsIdle", true);
    }

    private void CheckAttack()
    {
        distance = Vector3.Distance(transform.position, playerCollider.transform.position);

        if (distance <= attackDistance)
        {
            if (time <= 0)
            {
                animator.SetBool("IsWalking", false);
                animator.SetTrigger("Attack");

                HealthManager playerHealth = playerCollider.GetComponent<HealthManager>();
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
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other == playerCollider)
        {
            isPlayerInRange = true;
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other == playerCollider && !healthManager.isDead && healthManager.healthAmount > 0)
        {
            isPlayerInRange = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other == playerCollider)
        {
            isPlayerInRange = false;
            animator.SetBool("IsWalking", false);
            animator.SetBool("IsIdle", true);
            if (ghoulAudioSource.isPlaying)
            {
                ghoulAudioSource.Stop();
            }
            navMeshAgent.isStopped = true;
        }
    }
}

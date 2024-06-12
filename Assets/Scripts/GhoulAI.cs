using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhoulAI : MonoBehaviour
{
    public Animator animator;

    private float ghoulSpeed = 2.5f;

    private Quaternion playerRotation;
    private Quaternion ghoulRotation;

    private float valueX;
    private float valueZ;

    private float healthAmount = 50f;

    private float distance;
    private float attackDistance = 2f;

    private float delay = 1.5f;
    private float time = 0;

    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && healthAmount > 0)
        {
            animator.ResetTrigger("Idle");
            animator.SetTrigger("Run");

            playerRotation = Quaternion.LookRotation(other.transform.position - transform.position);
            valueX = transform.rotation.x;
            valueZ = transform.rotation.z;

            ghoulRotation = Quaternion.Slerp(transform.rotation, playerRotation, ghoulSpeed * Time.deltaTime);
            ghoulRotation.x = valueX;
            ghoulRotation.z = valueZ;

            transform.rotation = ghoulRotation;

            distance = Vector3.Distance(transform.position, transform.position);

            if (distance <= attackDistance)
            {
                if (time <= 0)
                {
                    animator.ResetTrigger("Move");
                    animator.SetTrigger("Attack");

                    other.SendMessage("Damage", 20);
                    time -= delay;
                }
                else
                {
                    time -= Time.deltaTime;
                }
            }
            else
            {
                transform.Translate(Vector3.forward * ghoulSpeed * Time.deltaTime);
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            animator.ResetTrigger("Run");
            animator.SetTrigger("Idle");
        }
    }
}

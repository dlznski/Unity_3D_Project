using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorTrigger : MonoBehaviour
{
    public Animator animator;
    public bool isOpen = false;

    public Canvas canvasTips;
    public Text tip;

    private void Start()
    {
        canvasTips.enabled = false;
    }

    public void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player" && Input.GetKeyDown(KeyCode.E))
        {
            if(isOpen == true)
            {
                animator.SetTrigger("Close");
                isOpen = false;
            }
            else
            {
                animator.SetTrigger("Open");
                isOpen = true;
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            canvasTips.enabled = true;

            if (isOpen == true)
            {
                tip.text = "Press E to close the door";
            }
            else
            {
                tip.text = "Press E to open the door";
            }
        }
    }   

    public void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            canvasTips.enabled = false;
        }
    }
}

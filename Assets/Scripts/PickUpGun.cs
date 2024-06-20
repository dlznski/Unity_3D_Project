using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PickUpGun : MonoBehaviour
{
    public GameObject GunOnPlayer;
    public Canvas canvasTips;
    public Text tip;

    public static bool isArmed = false;

    void Start()
    {
        GunOnPlayer.SetActive(false);
    }

    public void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player" && Input.GetKeyDown(KeyCode.E))
        {
            canvasTips.enabled = false;
            Destroy(gameObject);
            isArmed = true;
            GunOnPlayer.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            canvasTips.enabled = true;
            tip.text = "Press E to pick up the gun";
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            canvasTips.enabled = false;
        }
    }
}

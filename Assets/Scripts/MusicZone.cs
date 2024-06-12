using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicZone : MonoBehaviour
{
    public AudioManager audioManager;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            audioManager.SetLocationAudio();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            audioManager.SetNormalAudio();
        }
    }
}

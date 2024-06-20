using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroduceManager : MonoBehaviour
{
    public Canvas introduce;
    private AudioManager audioManager;
    private InGameMenu inGameMenu;

    void Start()
    {
        introduce.enabled = false;
        audioManager = FindObjectOfType<AudioManager>();
        inGameMenu = FindObjectOfType<InGameMenu>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && introduce != null)
        {

            inGameMenu.paused = true;
            introduce.enabled = true;
            inGameMenu.hud.enabled = false;
            Time.timeScale = 0;
            audioManager.SetPausedAudio();
        }
    }

    public void Understood()
    {
        introduce.enabled = false;
        Destroy(gameObject.GetComponent<BoxCollider>());
        Time.timeScale = 1;
        audioManager.SetNormalAudio();
        inGameMenu.hud.enabled = true;
        inGameMenu.paused = false;
    }
}

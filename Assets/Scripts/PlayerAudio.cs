using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerAudio : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioMixerGroup soundsMixerGroup;

    public AudioClip move1;
    public AudioClip move2;
    public AudioClip jump;
    public AudioClip land;
    public AudioClip pickUpGunSound;

    private PlayerController playerController;
    private InGameMenu inGameMenu;
    private IntroduceManager introduceManager;

    public bool isSteped = false;
    private bool playerGrounded;
    private float timer = 0f;

    void Start()
    {
        playerController = GetComponent<PlayerController>();
        inGameMenu = FindObjectOfType<InGameMenu>();
        introduceManager = FindObjectOfType<IntroduceManager>();

        if (audioSource != null && soundsMixerGroup != null)
        {
            audioSource.outputAudioMixerGroup = soundsMixerGroup;
        }
    }

    void Update()
    {
        PlayerSounds();
    }

    private void PlayerSounds()
    {
        if ((playerController.horizontal != 0 || playerController.vertical != 0) && playerController.characterController.isGrounded && timer <= 0)
        {
            if (isSteped == false)
            {
                audioSource.PlayOneShot(move1);
                isSteped = true;
            }
            else
            {
                audioSource.PlayOneShot(move2);
                isSteped = false;
            }
            timer = 0.5f;
            playerGrounded = true;
        }

        if (timer > 0)
        {
            if (playerController.movementSpeed > 5)
            {
                timer -= Time.deltaTime * 1.8f;
            }
            else
            {
                timer -= Time.deltaTime;
            }
        }

        if (Input.GetButton("Jump") && !introduceManager.introduce.enabled && !inGameMenu.paused && playerGrounded)
        {
            audioSource.PlayOneShot(jump);
        }

        if (!playerGrounded && playerController.characterController.isGrounded)
        {
            audioSource.PlayOneShot(land);
        }

        playerGrounded = playerController.characterController.isGrounded;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("PickUpGun") && Input.GetKeyDown(KeyCode.E))
        {
            audioSource.PlayOneShot(pickUpGunSound);
        }
    }
}

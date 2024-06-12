using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip move1;
    public AudioClip move2;
    public AudioClip jump;
    public AudioClip land;

    private PlayerController playerController;

    public bool isSteped = false;
    private bool playerGrounded;
    private float timer = 0f;

    void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        PlayerSounds();
    }


    private void PlayerSounds()
    {
        if((playerController.horizontal != 0 || playerController.vertical != 0) && playerController.characterController.isGrounded && timer <= 0)
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
        }

        if (timer > 0)
        {
            if(playerController.movementSpeed > 5)
            {
                timer -= Time.deltaTime * 1.8f;
            }
            else
            {
                timer -= Time.deltaTime;
            }
        }

        if (Input.GetButton("Jump") && playerGrounded)
        {
            audioSource.PlayOneShot(jump);
        }

        if (!playerGrounded && playerController.characterController.isGrounded)
        {
            audioSource.PlayOneShot(land);
        }

        playerGrounded = playerController.characterController.isGrounded;
    }
}

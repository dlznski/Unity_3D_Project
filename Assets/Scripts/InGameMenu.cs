using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class InGameMenu : MonoBehaviour
{
    public bool paused = false;

    public Canvas inGameMenu;
    public Canvas options;
    public Canvas toMainMenu;
    public Canvas hud;
    public Canvas victory;
    public Canvas death;

    public AudioManager audioManager;
    private IntroduceManager introduceManager;

    private void Escape()
    {
        if (paused == true)
        {
            inGameMenu.enabled = false;
            options.enabled = false;
            toMainMenu.enabled = false;
            hud.enabled = true;
            Time.timeScale = 1;
            paused = false;
            audioManager.SetNormalAudio();
        }
        else
        {
            inGameMenu.enabled = true;
            hud.enabled = false;
            Time.timeScale = 0;
            paused = true;
            audioManager.SetPausedAudio();
        }
    }

    void Start()
    {
        paused = false;
        inGameMenu.enabled = false;
        options.enabled = false;
        toMainMenu.enabled = false;
        victory.enabled = false;
        death.enabled = false;

        introduceManager = FindObjectOfType<IntroduceManager>();

        Time.timeScale = 1;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !victory.enabled && !introduceManager.introduce.enabled)
        {
            Escape();
        }

        if (paused == true)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void SetDeath()
    {
        paused = true;
        death.enabled = true;
        hud.enabled = false;
        Time.timeScale = 0;
        audioManager.SetPausedAudio();
    }

    public void SetVictory()
    {
        paused = true;
        victory.enabled = true;
        hud.enabled = false;
        Time.timeScale = 0;
        audioManager.SetPausedAudio();
    }

    public void Resume()
    {
        Escape();
    }

    public void InGame_Options()
    {
        inGameMenu.enabled = false;
        options.enabled = true;
    }

    public void InGame_ToMainMenu()
    {
        inGameMenu.enabled = false;
        toMainMenu.enabled = true;
    }

    public void InGame_Back()
    {
        inGameMenu.enabled = true;
        options.enabled = false;
    }

    public void InGame_Quit()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    public void InGame_BackToPauseMenu()
    {
        inGameMenu.enabled = true;
        toMainMenu.enabled = false;
    }

    public void PlayButton()
    {
        StartCoroutine(ReloadLevelAfterDelay(0f));
    }

    private IEnumerator ReloadLevelAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

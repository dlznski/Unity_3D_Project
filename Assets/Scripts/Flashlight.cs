using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    Light light;
    private InGameMenu inGameMenu;
    void Start()
    {
        light = GetComponent<Light>();
        inGameMenu = FindObjectOfType<InGameMenu>();
        light.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && !inGameMenu.paused)
        {
            light.enabled = !light.enabled;
        }
    }
}

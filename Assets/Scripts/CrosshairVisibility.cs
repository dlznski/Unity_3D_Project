using System.Collections;
using System.Collections.Generic;
using UnityEditor.Profiling;
using UnityEngine;
using UnityEngine.UI;

public class CrosshairVisibility : MonoBehaviour
{
    public RawImage crosshair;

    void Start()
    {
       crosshair.enabled = false;
    }

    void Update()
    {
        if (PickUpGun.isArmed)
        {
            crosshair.enabled = true;
        }
        else
        {
            crosshair.enabled = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    public Button btn;

    public void DestroyCube()
    {
        btn.interactable = false;
        Destroy(gameObject);
    }
}
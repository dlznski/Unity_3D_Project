using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleScript : MonoBehaviour
{
    public Toggle mage;
    public Toggle warior;
    public Toggle archer;

    private string selected;

    public void NameOfToggle()
    {
        if (mage.isOn == true)
        {
            selected = "Mage";
        }
        else if (warior.isOn == true)
        {
            selected = "Warior";
        }
        else if (archer.isOn == true)
        {
            selected = "Archer";
        }
        Debug.Log("Wybrana profesja: " + selected);
    }
}

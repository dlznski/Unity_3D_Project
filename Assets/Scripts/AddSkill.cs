using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddSkill : MonoBehaviour
{
    public Dropdown list;
    public Text result;

    private void Start()
    {
        result.enabled = false;
    }

    public void OnClick_AddSkill()
    {
        result.enabled = true;

        switch(list.value)
        {
            case 0:
                result.text = "Strenght +1";
                break;
            case 1:
                result.text = "Armor +1";
                break;
            case 2:
                result.text = "Cleverness +1";
                break;
            case 3:
                result.text = "Alchemy +1";
                break;
            case 4:
                result.text = "Shooting +1";
                break;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointCounter : MonoBehaviour
{
    public Text counter;
    private InGameMenu inGameMenu;

    void Start()
    {
        inGameMenu = FindObjectOfType<InGameMenu>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            int pointsLeft = Points() - 1;

            Destroy(gameObject, 0.1f);

            if (pointsLeft == 0)
            {
                counter.enabled = false;
                inGameMenu.SetVictory();
            }
            else
            {
                int currentValue = Convert.ToInt32(counter.text);
                currentValue++;
                counter.text = Convert.ToString(currentValue);
            }
        }
    }

    public int Points()
    {
        return FindObjectsOfType<PointCounter>().Length;
    }
}

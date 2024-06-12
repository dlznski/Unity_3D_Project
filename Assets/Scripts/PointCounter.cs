using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointCounter : MonoBehaviour
{
    public Text counter;
    public Text time;

    private float timer = 0;
    private int minutes = 0;
    private int seconds = 0;

    public PointCounter[] objects;

    void Start()
    {
        
    }

    void Update()
    {
        timer += Time.deltaTime;
        seconds = (int)timer;

        if (seconds == 60)
        {
            minutes++;
            timer = 0;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if (Points() == 1)
            {
                Destroy(gameObject, 0.1f);
                counter.enabled = false;

                time.text = "Gratulacje! Uda³o Ci siê zebraæ wszystkie kryszta³y!" + "\n" + "Czas: " + minutes + " minut " + seconds + " sekund!";
            }
            else
            {
                Destroy(gameObject, 0.1f);

                int currentValue = Convert.ToInt32(counter.text);
                currentValue++;

                counter.text = Convert.ToString(currentValue);
            }
        }
    }

    public int Points()
    {
        objects = Component.FindObjectsOfType<PointCounter>();
        return objects.Length;
    }
}

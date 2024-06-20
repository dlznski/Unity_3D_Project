using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotionManager : MonoBehaviour
{
    public Text potionCounterText;
    private int potionCount = 5;
    private int maxPotionCount = 5;
    private float potionRegenTime = 60f;

    public HealthManager playerHealthManager;
    private InGameMenu inGameMenu;

    void Start()
    {
        inGameMenu = FindObjectOfType<InGameMenu>();
        potionCounterText.text = potionCount.ToString();
        StartCoroutine(RegenPotions());
    }

    void Update()
    {
        if (!inGameMenu.paused && Input.GetKeyDown(KeyCode.Q) && potionCount > 0 && playerHealthManager.healthAmount < 100)
        {
            UsePotion();
        }
    }

    public void UsePotion()
    {
        potionCount--;
        potionCounterText.text = potionCount.ToString();
        playerHealthManager.Heal(20f);
    }

    private IEnumerator RegenPotions()
    {
        while (true)
        {
            yield return new WaitForSeconds(potionRegenTime);
            if (potionCount < maxPotionCount)
            {
                potionCount++;
                potionCounterText.text = potionCount.ToString();
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerStats : MonoBehaviour
{
    public int health = 25;
    public int maxHealth = 25;
    public int damage = 1;
    public int currentEXP = 0;
    public int maxEXP = 25;
    public int playerLevel=1;
    public int playerPotions = 3;
    public int maxPlayerPotions = 5;
    
     [SerializeField] private Text levelText;
     [SerializeField] private Text healthText;
     [SerializeField] private Slider healthBar;
     [SerializeField] private Slider EXPBar;
    [SerializeField] private Text EXPText;


    [SerializeField] private Text potionCountText;
      

    // Start is called before the first frame update
    void Start()
    {
        UpdateUIText();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            PlayerDrinkPotion();
        }
    }

    public void PlayerTakeDamage(int enemyDamage)
    {
        int damageDealt = Random.Range(enemyDamage/2, enemyDamage + 1);

        print("Player has taken " + damageDealt + " damage.");

        health -= damageDealt;
        CheckForDeath();
        UpdateUIText();

    }

    public void PlayerHeal(int healAmount)
    {
        health += healAmount;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    private void CheckForDeath()
    {
        if (health <= 0)
        {
            health = 0;
            print("Player has died");
            UpdateUIText();

        }
    }
    public void PlayerDrinkPotion()
    {
        if (playerPotions > 0 && health<maxHealth)
        {
            playerPotions--;
            PlayerHeal(maxHealth / 3);
            UpdateUIText();
        }
        else
        {
            print("Either player has full health or no potions");
        }
    }

    public void PlayerGainEXP(int EXPGained)
    {
        print("You have gained " + EXPGained + " EXP");

        currentEXP += EXPGained;
        if (currentEXP >= maxEXP)
        {
            print("Level Up! You are now lvl."+ playerLevel);
            playerLevel++;
            currentEXP = 0;
            maxEXP = (int)(maxEXP*1.5);
        }
        UpdateUIText();
    }

    public void UpdateUIText()//Used for texts and sliders
    {
        
        // healthBar.value = health;
       //  healthBar.maxValue = maxHealth;
        healthBar.value = ((float)health / (float)maxHealth);
    

    healthText.text = health.ToString()+"/"+maxHealth.ToString();

        //EXPBar.minValue =currentEXP;
        //EXPBar.value = maxEXP;
        EXPBar.value = ((float)currentEXP / (float)maxEXP);
        EXPText.text = (((float)currentEXP / (float)maxEXP) * 100).ToString() + "%";

        levelText.text = playerLevel.ToString();

        potionCountText.text = playerPotions.ToString();
         
    }


}

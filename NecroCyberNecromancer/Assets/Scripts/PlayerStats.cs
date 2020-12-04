using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerStats : MonoBehaviour
{
    public int health = 25;
    public int maxHealth = 25;
    public int damage = 1;
    public int currentEXP = 0;
    public int maxEXP = 25;
    public int playerLevel=1;
    /*
     [SerializeField] private Text levelText;
     [SerializeField] private Text healthText;
     [SerializeField] private Slider healthBar;
     [SerializeField] private Slider EXPBar;
     [SerializeField] private Text playerLevelText;

     
      */

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerTakeDamage(int enemyDamage)
    {
        int damageDealt = Random.Range(0, enemyDamage + 1);

        print("Player has taken " + damageDealt + " damage.");

        health -= damageDealt;
        CheckForDeath();
        UpdateUIText();

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

    public void PlayerGainEXP(int EXPGained)
    {
        print("You have gained " + EXPGained + " EXP");

        currentEXP += EXPGained;
        if (currentEXP >= maxEXP)
        {
            print("Level Up! You are now lvl."+ playerLevel);
            playerLevel++;
        }
        UpdateUIText();
    }

    void UpdateUIText()//Used for texts and sliders
    {
        /*
         healthBar.minValue = health;
         healthBar.maxValue = maxHealth;

        healthText.text = "HP:"+health.toString()+"/"+maxHealth.toString();

        EXPBar.minValue =currentEXP;
        EXPBar.maxValue = maxEXP;
        
        playerLevelText.text = playerLevel.toString();
         */
    }

}

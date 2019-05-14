using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealthManager : MonoBehaviour
{
    //Lives
    public int maxLives;
    public int currentLives;

    //Health
    public Image healthBar;
    public Text healthText;
    public Text livesText;
    public float maxHealth;
    public float currentHealth;


    public GameObject spawnPoint;
    public float invulTime;
    private float invulCountdown;
    private bool invul;

    public GameObject deathBlossom;


    //Setup variables
    void Start()
    {
        currentHealth = maxHealth;
        invulCountdown = invulTime;
        updateHealthDisplay();
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
    }

    //Handles invulnerability
    void Update()
    {
        //Invulnerability
        if (invul == true)
        {
            if (invulCountdown > 0)
            {
                invulCountdown -= Time.deltaTime;
            }
            else
            {
                invul = false;
                invulCountdown = invulTime;
            }
        }
    }

    //Deals damage to the player and handles death
    public void DamagePlayer(int damageAmount)
    {
        //Damage if not invulnerable
        if (invul != true)
        {
            currentHealth -= damageAmount;
            invul = true;
            updateHealthDisplay();
        }
        //Check for death
        if (currentHealth <= 0)
        {
            //Subtract life, reset health, reset postion, begin invulnerability
            currentLives -= 1;
            livesText.text = "Lives: " + currentLives;
            Instantiate(deathBlossom, transform.position, transform.rotation);
            if (transform.position.x > 10)
            {
                transform.position -= (new Vector3(5, 0, 0));
            }
            invul = true;
            currentHealth = maxHealth;
            updateHealthDisplay();
        }
        //Check for game over
        if (currentLives <= 0)
        {
            Instantiate(deathBlossom, transform.position, transform.rotation);
            GameObject.Find("MenuSystem").GetComponent<GameController>().switchToLosing();
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            currentHealth = maxHealth;
            currentLives = maxLives;

            //Destroy Weapons
            foreach (Transform child in transform.GetChild(0))
            {
                Destroy(child.gameObject);
            }
            gameObject.GetComponent<EquipmentManager>().clearEquipment();
        }
    }

    //Heals the player
    public void HealPlayer(int healAmount)
    {
        if (healAmount + currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else
        {
            currentHealth += healAmount;
        }
        updateHealthDisplay();
    }

    public void updateHealthDisplay()
    {
        healthBar.rectTransform.localScale = new Vector3((currentHealth / maxHealth), 1, 1);
        healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();
        livesText.text = "Lives: " + currentLives;
    }
}

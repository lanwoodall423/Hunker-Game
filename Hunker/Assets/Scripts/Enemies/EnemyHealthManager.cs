using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour {
    public float enemyMaxHealth;
    public float enemyCurrentHealth;

    public GameObject deathBlossom;
    
    private float decomposeCountdown;

    // Use this for initialization
    void Start()
    {
        enemyCurrentHealth = enemyMaxHealth;
        decomposeCountdown = 0.4f;
    }

    // Update is called once per frame
    void Update()
    {
        //Check if dead. If so, change color and countdown to destroy
        if (enemyCurrentHealth <= 0)
        {
            gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.blue;
            if (decomposeCountdown <= 0)
            {
                Instantiate(deathBlossom, transform.position, transform.rotation);
                Destroy(gameObject);
            }
            else
            {
                decomposeCountdown -= Time.deltaTime;

            }
        }
    }

    public void DamageEnemy(float damageAmount)
    {
        enemyCurrentHealth -= damageAmount;
    }

    public void HealEnemy(float healAmount)
    {
        if (healAmount + enemyCurrentHealth > enemyMaxHealth)
        {
            enemyCurrentHealth = enemyMaxHealth;
        }
        else
        {
            enemyCurrentHealth += healAmount;
        }
    }
}

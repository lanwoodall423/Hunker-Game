using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEnemy : MonoBehaviour {

    public int baseDamage;
    private float adjustedDamage;
    public GameObject damageGraphic;
    //private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
        //rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        //adjustedDamage = baseDamage * rb.mass * Mathf.Pow(rb.velocity.magnitude, 2);
    }

    private void OnCollisionEnter2D(Collision2D entity)
    {
        if (entity.gameObject.tag == "Enemy")
        {
            entity.gameObject.GetComponent<EnemyHealthManager>().DamageEnemy(baseDamage);
            Instantiate(damageGraphic, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}

using UnityEngine;

public class BulletMovement : MonoBehaviour {
    public float speed;
    public float lifeTime;
    private float lifeCountdown;
    private Rigidbody2D bullet;

    void Start()
    {
        lifeCountdown = lifeTime;
        bullet = GetComponent<Rigidbody2D>();
        bullet.AddForce(transform.right * speed * 4);
    }

    void FixedUpdate () {
        if (lifeCountdown > 0)
        {
            lifeCountdown -= Time.deltaTime;
        }
        else if (lifeCountdown <= 0)
        {
            Destroy(gameObject);
        }
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(GetComponent<DamageEnemy>());
    }
}

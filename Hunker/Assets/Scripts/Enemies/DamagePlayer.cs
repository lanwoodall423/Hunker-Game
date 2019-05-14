using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour {

    public int damageToGive;
    public GameObject damageGraphic;
    public Transform hitPoint;

    public AudioClip damageSound;
    private AudioSource source;

    void Start () {
        source = GetComponent<AudioSource>();
    }


    private void OnCollisionEnter2D(Collision2D entity)
    {
        if (entity.gameObject.name == "Player")
        {
            entity.gameObject.GetComponent<PlayerHealthManager>().DamagePlayer(damageToGive);
            source.PlayOneShot(damageSound);
            Instantiate(damageGraphic, hitPoint.position, hitPoint.rotation);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour {
    public GameObject weaponHolder;
    private GameObject equipment;
    public bool equipped;

	// Use this for initialization
	void Start () {
        //inv = GameObject.Find("InventorySystem").GetComponent<Inventory>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (equipped == true && Input.GetKeyDown("q"))
        {
            equipment.transform.parent = null;
            equipment = null;
            equipped = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown("e") && collision.gameObject.tag == "Weapon" && equipped == false)
        {
            collision.gameObject.transform.parent = weaponHolder.transform;
            collision.gameObject.transform.position = weaponHolder.transform.position;
            collision.gameObject.transform.rotation = weaponHolder.transform.rotation;
            equipment = collision.gameObject;
            equipped = true;
        }
    }
}

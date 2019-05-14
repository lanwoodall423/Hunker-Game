using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour {
    
    //The Empty that acts as the parent of each equipment object
    public GameObject equipmentHolder;

    //Contains all held equipment
    public List<GameObject> heldEquipment;
    public int activeEquipmentIndex;
    public int maxEquipment;
    public GameObject activeEquipment;

    //Setup variables
    private void Start()
    {
        heldEquipment = new List<GameObject>();
        activeEquipmentIndex = 0;
        activeEquipment = null;
    }

    //Checks for dropping and switching weapons
    void Update()
    {
        if (Input.GetKeyDown("q") && activeEquipment != null)
        {
            dropEquipment();
        }
        if(Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            nextEquipment();
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            previousEquipment();
        }
    }

    void pickupEquipment(GameObject equipment)
    {
        //Add it to the equipment list
        heldEquipment.Add(equipment);

        //Deactivate current weapon if exists
        if (activeEquipment != null)
        {
            activeEquipment.SetActive(false);
        }

        //Set its index as the current index
        activeEquipmentIndex = heldEquipment.IndexOf(equipment);

        //Set it as the active equipment
        activeEquipment = equipment;
        equipment.SetActive(true);

        //Position it on the equipment holder
        equipment.transform.parent = equipmentHolder.transform;
        equipment.transform.position = equipmentHolder.transform.position;
        equipment.transform.rotation = equipmentHolder.transform.rotation;

        //if weapon, equip the weapon from its perspective
        if (equipment.GetComponent<GunWeapon>())
        {
            equipment.GetComponent<GunWeapon>().equipWeapon();
        }
    }

    void dropEquipment()
    {
        //If there is a held item
        if (activeEquipment != null)
        {
            //Remove the equipped item from the equipment holder
            activeEquipment.transform.parent = null;


            //If weapon, unequip the weapon from its perspective
            if (activeEquipment.GetComponent<GunWeapon>())
            {
                activeEquipment.GetComponent<GunWeapon>().unequipWeapon();
            }

            //If there's just one item, remove it from the list and set active to null
            if (heldEquipment.Count == 1)
            {
                heldEquipment.Remove(activeEquipment);
                activeEquipment = null;
            } else //Remove the item and go to the next item
            {
                GameObject oldEquipment = activeEquipment;
                nextEquipment();
                oldEquipment.SetActive(true);
                heldEquipment.Remove(oldEquipment);
                activeEquipmentIndex = heldEquipment.IndexOf(activeEquipment);
            }
        }
    }

    void nextEquipment()
    {
        //Set next item in list as active or if at the end return to the first
        activeEquipmentIndex = ++activeEquipmentIndex % heldEquipment.Count;
        activeEquipment.SetActive(false);
        heldEquipment[activeEquipmentIndex].SetActive(true);
        activeEquipment = heldEquipment[activeEquipmentIndex];
    }


    void previousEquipment()
    {
        //Set next item in list as active or if at the end return to the first
        if(activeEquipmentIndex != 0)
        {
            activeEquipmentIndex = --activeEquipmentIndex % heldEquipment.Count;
        }
        else
        {
            activeEquipmentIndex = heldEquipment.Count-1;
        }
        activeEquipment.SetActive(false);
        heldEquipment[activeEquipmentIndex].SetActive(true);
        activeEquipment = heldEquipment[activeEquipmentIndex];

    }

    public void clearEquipment()
    {
        heldEquipment = new List<GameObject>();
        activeEquipment = null;
        activeEquipmentIndex = 0;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("in" + collision);
        //Check if the object is equipment and whether there is space to hold it
        if (Input.GetKeyDown("e") && collision.gameObject.tag == "Equipment" && heldEquipment.Count <= maxEquipment && !heldEquipment.Contains(collision.gameObject))
        {
            pickupEquipment(collision.gameObject);
        }
    }
}

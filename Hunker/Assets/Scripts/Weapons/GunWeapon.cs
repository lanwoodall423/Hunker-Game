using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunWeapon : MonoBehaviour {

    public int itemID;

    //States of the gun and firing types
    public enum GunState { RELOADING, IDLE, UNEQUIPPED };
    public enum FireType { AUTO, SEMI, BOLTACTION };
    private GunState gunState;
    public FireType fireType;

    //AUTO
    public float fireRate;
    private float rateCountdown;

    //BOLTACTION
    public float boltActionDelay;
    private float boltActionCountdown;

    //End of gun barrel (Point of bullet instantiation)
    public GameObject projectile;
    private SpriteRenderer sprite;
    private GameObject shootPoint;
    private float shootOffset;

    //Ammo and reloading
    public int magSize;
    private int ammoInMag;
    public float reloadTime;
    private float reloadCountdown;
    private Text ammoText;

    //Audio
    public AudioClip fireSound;
    public AudioClip reloadSound1;
    public AudioClip reloadSound2;
    private AudioSource source;

    //Game State
    private GameState gameState;

    private void OnEnable()
    {
        GameController.changeGameState += updateGameState;
    }

    private void OnDisable()
    {
        GameController.changeGameState -= updateGameState;
    }

    void updateGameState(GameState gameState)
    {
        this.gameState = gameState;
    }

    void Start () {
        //Create the position for instantiating a bullet by taking half of the top border of the sprite to get the X value of the end of the gun
        sprite = GetComponent<SpriteRenderer>();
        shootOffset = sprite.bounds.size.x/2;
        shootPoint = new GameObject();
        shootPoint.transform.parent = gameObject.transform;
        shootPoint.transform.position = new Vector3(transform.position.x + shootOffset+0.2f, transform.position.y, transform.position.z);

        //Check if on the ground or equipped
        if (transform.parent == null)
        {
            gunState = GunState.UNEQUIPPED;
        }
        else
        {
            gunState = GunState.IDLE;

        }

        //Audio Source
        source = GetComponent<AudioSource>();

        //Variable setup
        ammoInMag = magSize;
        reloadCountdown = reloadTime;
        rateCountdown = 0;
        ammoText = GameObject.Find("CurrentAmmoNum").GetComponent<Text>();

        //Begin in the playing state
        gameState = GameState.PLAYING;
    }

    void Update()
    {
        //Check if on the ground or equipped
        switch (gameState)
        {
            case GameState.MENU:
                break;
            case GameState.PLAYING:
                switch (gunState)
                {
                    //Ready to shoot
                    case GunState.IDLE:
                        //Determines appropriate fire type
                        switch (fireType)
                        {
                            case FireType.SEMI:
                                if (Input.GetButtonDown("Fire1"))
                                {
                                    fireWeapon();
                                }
                                break;

                            case FireType.BOLTACTION:
                                if (Input.GetButtonDown("Fire1") && boltActionCountdown <= 0)
                                {
                                    boltActionCountdown = boltActionDelay;
                                    fireWeapon();
                                }
                                else
                                {
                                    boltActionCountdown -= Time.fixedDeltaTime;
                                }
                                break;

                            case FireType.AUTO:
                                if (Input.GetButton("Fire1") && rateCountdown <= 0)
                                {
                                    rateCountdown = 1 / fireRate;
                                    fireWeapon();
                                }
                                else
                                {
                                    rateCountdown -= Time.fixedDeltaTime;
                                }
                                break;
                        }
                        if (Input.GetKeyDown("r"))
                        {
                            source.PlayOneShot(reloadSound1);
                            gunState = GunState.RELOADING;
                        }
                        //Check for reload
                        if (ammoInMag == 0)
                        {
                            source.PlayOneShot(reloadSound1);
                            gunState = GunState.RELOADING;
                        }
                        break;


                    case GunState.RELOADING:
                        if (reloadCountdown >= 0)
                        {
                            reloadCountdown -= Time.fixedDeltaTime;
                        }
                        else if (reloadCountdown < 0)
                        {
                            source.PlayOneShot(reloadSound2);
                            reloadCountdown = reloadTime;
                            ammoInMag = magSize;
                            updateDisplay();
                            gunState = GunState.IDLE;
                        }
                        break;
                    case GunState.UNEQUIPPED:
                        break;
                }
                break;
        }
    }

    public void equipWeapon()
        {
        gunState = GunState.IDLE;
        updateDisplay();
        }

    public void unequipWeapon()
    {
        gunState = GunState.UNEQUIPPED;
        ammoText.text = null;
    }

    void fireWeapon()
    {
        Instantiate(projectile, shootPoint.transform.position, shootPoint.transform.rotation);
        source.PlayOneShot(fireSound);
        ammoInMag -= 1;
        updateDisplay();
        if (ammoInMag != 0)
        {
            gunState = GunState.IDLE;
        }
        else
        {
            gunState = GunState.RELOADING;
        }
    }

    void updateDisplay()
    {
        ammoText.text = "Ammo: " + ammoInMag + " / " + magSize;
    }
}

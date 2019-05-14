using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float moveSpeed;

    private Rigidbody2D playerRigidBody2D;
    private static bool playerExists;
    private GameState gameState = GameState.PLAYING;

    void updateGameState(GameState gameState)
    {
        this.gameState = gameState;
    }

    private void OnEnable()
    {
        GameController.changeGameState += updateGameState;
    }

    private void OnDisable()
    {
        GameController.changeGameState -= updateGameState;
    }


    void Start () {
        playerRigidBody2D = GetComponent<Rigidbody2D>();

        //Deletes old player on reload scene
        if (!playerExists)
        {
            playerExists = true;
            DontDestroyOnLoad(transform.gameObject);
        } else
        {
            Destroy(gameObject);
        }
        
    }


    void FixedUpdate()
    {
        switch (gameState)
        {
            case GameState.MENU:
                //Disable player movement
                GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                break;

            case GameState.MENU_TO_PLAYING:
                //Enable player Movement
                GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                break;

            case GameState.PLAYING:
                GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
               
                //Horizontal Movement
                if (Input.GetAxisRaw("Horizontal") > 0.5f || Input.GetAxisRaw("Horizontal") < -0.5f)
                {
                    //transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime, 0f, 0f));
                    playerRigidBody2D.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, playerRigidBody2D.velocity.y);
                }

                //Vertical Movement
                if (Input.GetAxisRaw("Vertical") > 0.5f || Input.GetAxisRaw("Vertical") < -0.5f)
                {
                    //transform.Translate(new Vector3(0f, Input.GetAxisRaw("Vertical") * moveSpeed * Time.deltaTime, 0f));
                    playerRigidBody2D.velocity = new Vector2(playerRigidBody2D.velocity.x, Input.GetAxisRaw("Vertical") * moveSpeed);
                }

                //Idling
                if (Input.GetAxisRaw("Horizontal") < 0.5f && Input.GetAxisRaw("Horizontal") > -0.5f)
                {
                    playerRigidBody2D.velocity = new Vector2(0f, playerRigidBody2D.velocity.y);
                }
                if (Input.GetAxisRaw("Vertical") < 0.5f && Input.GetAxisRaw("Vertical") > -0.5f)
                {
                    playerRigidBody2D.velocity = new Vector2(playerRigidBody2D.velocity.x, 0f);
                }
                break;
        }
    }

        /*
        //Animation
        anim.SetBool("IsWalking",playerMoving);
        */


}

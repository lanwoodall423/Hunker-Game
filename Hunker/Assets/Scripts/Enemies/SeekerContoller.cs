using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekerContoller : MonoBehaviour {
    public float speed;
    private Vector3 originalPosition;
    private Transform target;
    private int tryDirectionCount;

    private Ray2D ray;
    private RaycastHit2D hit;

    private Rigidbody2D rb;

    private GameState gameState = GameState.PLAYING;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        tryDirectionCount = 0;
        target = GameObject.FindWithTag("Player").transform;
    }

    void FixedUpdate()
    {
        switch (gameState)
        {
            case GameState.MENU:
                rb.constraints = RigidbodyConstraints2D.FreezeAll;
                break;
            case GameState.PLAYING:
                rb.constraints = RigidbodyConstraints2D.None;

                //rotate to look at the player
                transform.LookAt(target.position);
                transform.Rotate(new Vector3(0, -90, 0), Space.Self);//correcting the original rotation

                Debug.DrawRay(transform.position, new Vector3(1, 1, 0));
                hit = Physics2D.Raycast(transform.position, transform.rotation.eulerAngles);
                if (hit.distance < 7)
                {
                    rb.AddForce(-transform.forward * speed / 2);
                }

                //move towards the player
                if (Vector3.Distance(transform.position, target.position) > 1f)
                {
                        rb.AddForce((transform.right * speed) * Time.fixedDeltaTime);
                }

                break;
        }
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        //bounce back and right or left on collision
        if (tryDirectionCount < 5)
        {
            rb.AddForce(-transform.forward * speed/2);
            rb.AddForce(transform.up * speed / 5);
        }
        else if (tryDirectionCount < 10)
        {
            rb.AddForce(-transform.forward * speed/2);
            rb.AddForce(-transform.up * speed / 5);
        }
        else
        {
            tryDirectionCount = 0;
        }
        tryDirectionCount++;
    }



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
}

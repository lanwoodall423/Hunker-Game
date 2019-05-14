using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BatController : MonoBehaviour {
    public float moveSpeed;

    private Rigidbody2D batRigidBody;

    private bool moving;


    public float timeBetweenMove;
    private float timeBetweenMoveCounter;

    public float timeToMove;
    private float timeToMoveCounter;

    private Vector3 moveDirection;

    private GameObject player;

	// Use this for initialization
	void Start () {
        batRigidBody = GetComponent<Rigidbody2D>();

        //timeBetweenMoveCounter = timeBetweenMove;
        //timeToMoveCounter = timeToMove;

        timeBetweenMoveCounter = Random.Range(timeBetweenMove * 0.75f, timeBetweenMove * 1.25f);
        timeToMoveCounter = Random.Range(timeToMove * 0.75f, timeToMove * 1.25f);
    }
	
	// Update is called once per frame
	void Update () {

        //Movement
		if (moving)
        {
            timeToMoveCounter -= Time.deltaTime;
            batRigidBody.velocity = moveDirection;

            if (timeToMoveCounter < 0f)
            {
                moving = false;
                //timeBetweenMoveCounter = timeBetweenMove;
                timeBetweenMoveCounter = Random.Range(timeBetweenMove * 0.75f, timeBetweenMove * 1.25f);
            }
        } else
        {
            timeBetweenMoveCounter -= Time.deltaTime;
            batRigidBody.velocity = Vector2.zero;
            if (timeBetweenMoveCounter < 0)
            {
                moving = true;
                //timeToMoveCounter = timeToMove;
                timeToMoveCounter = Random.Range(timeToMove * 0.75f, timeToMove * 1.25f);

                moveDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f),0);
            }
        }
	}

    private void OnCollisionEnter2D(Collision2D entity)
    {
        /*
        if (entity.gameObject.name == "Player")
        {
            entity.gameObject.SetActive(false);
            reloading = true;
            player = entity.gameObject;
        }
        */
    }
}

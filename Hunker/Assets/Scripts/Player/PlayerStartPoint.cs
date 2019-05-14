using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStartPoint : MonoBehaviour {

    private PlayerController player;

    public Vector2 startDirection;

	// Use this for initialization
	void Start () {
        player = FindObjectOfType<PlayerController>();
        player.transform.position = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

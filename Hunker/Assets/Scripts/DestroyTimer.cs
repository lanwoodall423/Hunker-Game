using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTimer : MonoBehaviour {
    public float time;
    private float countdown;

	// Use this for initialization
	void Start () {
        countdown = time;
	}
	
	// Update is called once per frame
	void Update () {
        if (countdown <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            countdown -= Time.deltaTime;
        }
	}
}

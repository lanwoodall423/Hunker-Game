using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadArea : MonoBehaviour {

    public string areaToLoad;
    private GameController menuSystem;

    private void Start()
    {
        menuSystem = GameObject.Find("MenuSystem").GetComponent<GameController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            if(areaToLoad == "WINNER")
            {
                menuSystem.switchToWinning();
            } else
            {
                menuSystem.switchToMenu();
                SceneManager.LoadScene(areaToLoad);
            }
            
        }
    }
}

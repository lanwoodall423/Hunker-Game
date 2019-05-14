using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum GameState { MENU, MENU_TO_PLAYING, PLAYING, PLAYING_TO_MENU, WINLOSE };
public enum MenuState { NONE, MAIN, INVENTORY, LOSER, WINNER };


public class GameController : MonoBehaviour
{
    private GameState _gameState;
    private MenuState _menuState;
    public Text gameStateDisplay;
    private Vector3 originalPosition;
    public GameState gameState
    {
        set
        {
            _gameState = value;
            if (changeGameState != null)
            {
                changeGameState(gameState);
            }
        }
        get
        {
            return _gameState;
        }
    }
    public MenuState menuState
    {
        set
        {
            _menuState = value;
            if (changeMenu != null)
            {
                changeMenu(menuState);
            }
        }
        get
        {
            return _menuState;
        }
    }

    public static event Action<MenuState> changeMenu;
    public static event Action<GameState> changeGameState;

    private static bool menuExists;


    void Start()
    {
        if (!menuExists)
        {
            menuExists = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        switchToMenu();
    }


    void Update()
    {
        switch (gameState)
        {
            case GameState.MENU:
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    gameState = GameState.MENU_TO_PLAYING;
                }
                break;
            case GameState.MENU_TO_PLAYING:
                switchToPlaying();
                break;
            case GameState.PLAYING:
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    gameState = GameState.PLAYING_TO_MENU;
                }
                break;
            case GameState.PLAYING_TO_MENU:
                switchToMenu();
                break;
            case GameState.WINLOSE:
                break;
            default:
                break;

        }
    }

    public void switchToPlaying()
    {
        gameState = GameState.PLAYING;
        menuState = MenuState.NONE;
        gameStateDisplay.text = "Playing";
    }

    public void switchToMenu()
    {
        gameState = GameState.MENU;
        menuState = MenuState.MAIN;
        gameStateDisplay.text = "Menu";
    }

    public void switchToInventory()
    {
        menuState = MenuState.INVENTORY;
        gameStateDisplay.text = "Inventory";
    }

    public void switchToLosing()
    {
        gameState = GameState.WINLOSE;
        menuState = MenuState.LOSER;
        gameStateDisplay.text = "LOSER";
    }

    public void switchToWinning()
    {
        gameState = GameState.WINLOSE;
        menuState = MenuState.WINNER;
        gameStateDisplay.text = "WINNER";
    }

    public void switchToMenu(MenuState newMenu)
    {
        menuState = newMenu;
    }

    //Create notification for this
    public void restart()
    {
        PlayerHealthManager playerHealth = GameObject.Find("Player").GetComponent<PlayerHealthManager>();
        playerHealth.updateHealthDisplay();
        switchToMenu();
        SceneManager.LoadScene("Level1");
    }

    public void quitGame()
    {
        Application.Quit();
    }
}


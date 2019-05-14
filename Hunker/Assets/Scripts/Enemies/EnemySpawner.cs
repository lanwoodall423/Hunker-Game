using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public enum SpawnState {SPAWNING, WAITING, COUNTING};

    //Lets you embed a class with sub-properties in the inspector.
    [System.Serializable]

    public class Wave
    {
        public string name;
        public Transform enemy;
        public int count;
        public float rate;

    }
    //Stores waves and iterates through them
    public Wave[] waves;
    private int nextWaveIndex = 0;

    //Triggers spawner when player enters collider
    private bool triggered;

    //Delay between waves
    public float timeBetweenWaves;
    private float waveTimer;

    //States
    public SpawnState spawnState = SpawnState.COUNTING;
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


    //Setup variables
    void Start () {
        waveTimer = timeBetweenWaves;
        triggered = false;
	}



    void FixedUpdate () {
        switch (gameState)
        {
            case GameState.MENU:
                break;
            case GameState.PLAYING:
                if (waveTimer <= 0 && triggered)
                {
                    waveTimer = timeBetweenWaves;
                    if (spawnState != SpawnState.SPAWNING && nextWaveIndex < waves.Length)
                    {
                        StartCoroutine(SpawnWave(waves[nextWaveIndex++]));
                    }
                }
                else if (triggered)
                {
                    waveTimer -= Time.deltaTime;
                }
                break;
        }
	}

    //Spawns wave of enemies
    IEnumerator SpawnWave(Wave _wave)
    {
        spawnState = SpawnState.SPAWNING;

        //Spawn enemy waiting 1/waverate seconds between each spawn
        for (int i = 0; i < _wave.count; i++)
        {
            switch (gameState)
            {
                case GameState.MENU:
                    break;
                case GameState.PLAYING:
                    spawnEnemy(_wave.enemy);
                    yield return new WaitForSeconds(1f / _wave.rate);
                    break;
            }
        }
        spawnState = SpawnState.WAITING;
        yield break;
    }

    //Spawns a single enemy defined in the wave
    void spawnEnemy (Transform _enemy)
    {
        Instantiate(_enemy, transform.position, transform.rotation);
    }

    //When player enters collider, spawner is triggered
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            triggered = true;
        }
    }



}

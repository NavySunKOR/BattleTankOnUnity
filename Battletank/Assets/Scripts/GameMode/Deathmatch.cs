using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deathmatch : MonoBehaviour {
    
    public GameObject playerInstance;
    public GameObject enemySource;

    private GameObject[] spawnPoint;
    private Tank playerTank;
    private int enemyKillCount;
    private GameObject[] enemyInstances;
    private GameModeUI gameModeUI;
    private bool gameOver;


	// Use this for initialization
	void Start () {
        playerTank = playerInstance.GetComponent<Tank>();
        spawnPoint = GameObject.FindGameObjectsWithTag("SpawnPoint");

        enemyInstances = new GameObject[spawnPoint.Length];

        for(int i = 0; i < spawnPoint.Length;i++)
        {
            enemyInstances[i] = Instantiate(enemySource, spawnPoint[i].transform.position, Quaternion.identity) as GameObject;
        }

        gameModeUI = GetComponent<GameModeUI>();
        gameModeUI.StartCoroutine(gameModeUI.WelcomeMessage());

    }
	
	// Update is called once per frame
	void Update () {
        if(!gameOver)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                gameModeUI.PopUpPauseMenu();
                Debug.Log("Input!");
            }
        }

		if(!playerTank.IsDead() && !gameOver)
        {
            //check enemy counts and if they wiped out, then show win sign.
            if(enemyKillCount == spawnPoint.Length)
            {
                //Game win
                gameModeUI.GameWin();
                gameOver = true;
            }
        }
        else if(!gameOver)
        {
            //Show death sign.
            gameModeUI.GameLose();
            gameOver = true;
        }
	}

    void AddEnemyKillCount()
    {
        enemyKillCount++;
        Debug.Log("Kill count added");
    }
    
    
}

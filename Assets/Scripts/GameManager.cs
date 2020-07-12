using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [HideInInspector]
    public List<GameObject> Marbles;
    private List<KeyValuePair<string, int>> _ballNamesAndPoints;

    private bool _isGameOver = false;
    private int _winningIndex = -1;


    void Start()
	{
		SetupMarbles();
		SetupJerks();
	}

    public void GameOver()
    {
        _isGameOver = true;

        int index = 0;
        _ballNamesAndPoints = new List<KeyValuePair<string, int>>();
        foreach (var marble in Marbles)
        {
            var ballComponent = marble.GetComponent<Ball>();
            _ballNamesAndPoints.Add(new KeyValuePair<string, int>(ballComponent.Name, ballComponent.Points));
            
            if (marble.activeSelf)
            {
                _winningIndex = index;
            }
            index++;
        }
    }

    public List<KeyValuePair<string, int>> GetBallNamesAndPoints()
    {
        return _ballNamesAndPoints;
    }

    public int GetWinningBallIndex()
    {
        return _winningIndex;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))//&& _isGameOver
        {
            ReloadScene();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToMainMenu();
        }
    }

	private void InitiateGame()
	{
		foreach (var marble in Marbles)
		{
			marble.SetActive(true);
		}
	}

    private void ReloadScene()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
		InitiateGame();
    }

    private void ToMainMenu()
    {
        SceneManager.LoadSceneAsync(0);
		InitiateGame();
    }

    public bool IsGameOver()
    {
        return _isGameOver;
    }
	
	private void SetupMarbles()
	{
		Marbles = new List<GameObject>();

		GameObject[] list = GameObject.FindGameObjectsWithTag("Ball");

		foreach (var marble in list)
		{
			Marbles.Add(marble);
		}
	}

	private void SetupJerks()
	{		
		var jerkSim = this.GetComponent<JerkSimulator>();
		if (jerkSim != null)
		{
			JerkPreferences jerkData = DataSaver.loadData<JerkPreferences>("Jerks");
			jerkSim.JerkCount = jerkData.jerkCount;
			jerkSim.JerkWeight = jerkData.jerkWeight;
			jerkSim.JerkInterval = jerkData.jerkInterval;
		}
	}
}

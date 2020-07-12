using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private float _waitTimeForRestart = 5.0f;

    [HideInInspector]
	public List<GameObject> Marbles;

    private bool _isGameOver = false;

	void Start()
	{
		SetupMarbles();
		SetupJerks();
	}

    public void GameOver()
    {
        _isGameOver = true;

        StartCoroutine(RestartGame());
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

    private IEnumerator RestartGame()
    {
        while (true)
        {
            yield return new WaitForSeconds(_waitTimeForRestart);

            ReloadScene();
        }
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

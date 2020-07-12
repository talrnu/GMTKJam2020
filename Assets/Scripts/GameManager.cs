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
		Marbles = new List<GameObject>();

		GameObject[] list = GameObject.FindGameObjectsWithTag("Ball");

		foreach (var marble in list)
		{
			Marbles.Add(marble);
		}
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
        SceneManager.LoadSceneAsync(1); // this hard coded value needs to change
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
}

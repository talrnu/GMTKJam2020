using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private bool _isGameOver = false;

    [HideInInspector]
    public List<GameObject> Marbles;
    private List<KeyValuePair<string, int>> _ballNamesAndPoints;

    void Start()
    {
        Debug.Log("GameManager.Start() called");
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

        _ballNamesAndPoints = new List<KeyValuePair<string, int>>();
        foreach (var marble in Marbles)
        {
            var ballComponent = marble.GetComponent<Ball>();
            _ballNamesAndPoints.Add(new KeyValuePair<string, int>(ballComponent.Name, ballComponent.Points));
        }
    }

    public List<KeyValuePair<string, int>> GetBallNamesAndPoints()
    {
        return _ballNamesAndPoints;
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

    private void ReloadScene()
    {
        SceneManager.LoadSceneAsync(1); // this hard coded value needs to change
	    foreach (var marble in Marbles)
	    {
	        marble.SetActive(true);
	    }
    }

    private void ToMainMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }

    public bool IsGameOver()
    {
        return _isGameOver;
    }
}

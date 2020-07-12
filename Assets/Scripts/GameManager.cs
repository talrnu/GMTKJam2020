﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private float _waitTimeForRestart = 5.0f;

    [SerializeField]
    private bool _isGameOver = false;

    public float WaitTimeForRestart => _waitTimeForRestart;

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

    private void ReloadScene()
    {
        SceneManager.LoadSceneAsync(1); // this hard coded value needs to change
    }

    private void ToMainMenu()
    {
        SceneManager.LoadSceneAsync(0);
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

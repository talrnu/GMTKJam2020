using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LoadMainGame()
    {
        SceneManager.LoadSceneAsync("main");
    }

    public void LoadRaceGame()
    {
        SceneManager.LoadSceneAsync("Race_test");
    }
}

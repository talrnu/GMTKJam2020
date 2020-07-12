using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LoadMainGame()
    {
		SaveData();
        SceneManager.LoadSceneAsync("main");
    }

    public void LoadRaceGame()
    {
		SaveData();
        SceneManager.LoadSceneAsync("Race_test");
    }
	
	private void SaveData()
	{
		JerkPreferences jerkData = new JerkPreferences();
		jerkData.jerkCount = (int)GameObject.Find("JerkCountSlider").GetComponent<Slider>().value;
		jerkData.jerkWeight = GameObject.Find("JerkWeightSlider").GetComponent<Slider>().value;
		jerkData.jerkInterval = (int)GameObject.Find("JerkIntervalSlider").GetComponent<Slider>().value;
		
		DataSaver.saveData(jerkData, "Jerks");
	}
}

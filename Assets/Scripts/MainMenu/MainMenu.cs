using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	private GameObject _customPanel;
	private Dropdown _dropdown;

	// 0 - Game ends all balls removed
	// 1 - Game ends on specified ball
	// 2 - Game ends on one ball remaining
	private int _gameEndValue;

	void Start()
	{
		_customPanel = GameObject.FindGameObjectWithTag("custom_panel");
		if (_customPanel == null)
		{
			Debug.LogError("_customPanel is null");
		}
        _customPanel.SetActive(false);
        _dropdown = GameObject.FindGameObjectWithTag("win_conditions_dropdown").GetComponent<Dropdown>();
		if (_dropdown == null)
		{
			Debug.LogError("_dropdown is null");
		}
		else
		{
			_dropdown.onValueChanged.AddListener(delegate
			{
				DropdownValueChanged(_dropdown);
			});
		}

		
	}

    public void LoadMainGame()
    {
		//SaveData();
        SceneManager.LoadSceneAsync("main");
    }

    public void LoadRaceGame()
    {
		//SaveData();
        SceneManager.LoadSceneAsync("Race_test");
    }

	public void DisplayCustomPanel()
	{
		_customPanel.SetActive(true);
	}

	public void OnBackButton()
	{
        SaveData();
        _customPanel.SetActive(false);
	}

	public void OnExitButton()
	{
		Application.Quit();
	}
	
	private void SaveData()
	{
		JerkPreferences jerkData = new JerkPreferences();
		jerkData.jerkCount = (int)GameObject.Find("JerkCountSlider").GetComponent<Slider>().value;
		jerkData.jerkWeight = GameObject.Find("JerkWeightSlider").GetComponent<Slider>().value;
		jerkData.jerkInterval = GameObject.Find("JerkIntervalSlider").GetComponent<Slider>().value;
		
		DataSaver.saveData(jerkData, "Jerks");

		GameEndPreference gameEndData = new GameEndPreference();
		gameEndData.gameEndValue = _gameEndValue;

		DataSaver.saveData(gameEndData, "GameEnd");
	}

	private void DropdownValueChanged(Dropdown dropdown)
	{
		_gameEndValue = dropdown.value;
	}
}

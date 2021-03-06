﻿using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Meters : MonoBehaviour
{
	[SerializeField] private GameManager _gameManager;
	[SerializeField] private Text _firePoints;
	[SerializeField] private Text _waterPoints;
	[SerializeField] private Text _earthPoints;
	[SerializeField] private Text _airPoints;
	private Text _meterVotes;
	private string _stationaryColor = "orange";
	private string _warningColor = "red";
	private string _deadColor = "black";
	private Color _normalColor = Color.blue;
    private InputVoteCollector _inputVoteCollector;
	
	void Start()
	{
		Debug.Log("Meters.Start() called");

		// Fire
		_firePoints.color = _normalColor;
		_firePoints.text = "Roll out";

		// Water
		_waterPoints.color = _normalColor;
		_waterPoints.text = "Roll out";

		// Earth
		_earthPoints.color = _normalColor;
		_earthPoints.text = "Roll out";

		// Air
		_airPoints.color = _normalColor;
		_airPoints.text = "Roll out";

		_meterVotes = GameObject.Find("MeterVotes").GetComponent<Text>();
		_meterVotes.color = _normalColor;
		_meterVotes.text = "";
		
		_inputVoteCollector = GameObject.Find("game_manager").GetComponent<InputVoteCollector>();
	}
	
	void OnGUI ()
	{
		UpdateBallMeters();
		UpdateVoteMeters();
	}
	
	private void UpdateBallMeters()
	{
		_firePoints.text = "";
		_waterPoints.text = "";
		_earthPoints.text = "";
		_airPoints.text = "";

		int i = 0;
		foreach (var marble in _gameManager.Marbles)
		{
			var ballComponent = marble.GetComponent<Ball>();

			var stationary = ballComponent.TimeToSelfDestruct <= ballComponent.SelfDestructSeconds - 1;
			var warning = ballComponent.TimeToSelfDestruct <= 1;
			var dead = !marble.activeSelf;
			if (marble.name == "Player_earth")
			{
				_earthPoints.text += dead ? "<color=" + _deadColor + ">" : warning ? "<color=" + _warningColor + ">" : stationary ? "<color=" + _stationaryColor + ">" : "";
				_earthPoints.text += ballComponent.Name + " " + ballComponent.Points;
				_earthPoints.text += (dead || warning || stationary) ? "</color>" : "";
				_earthPoints.text += "\n";
			}
			else if (marble.name == "Player_fire")
			{
				_firePoints.text += dead ? "<color=" + _deadColor + ">" : warning ? "<color=" + _warningColor + ">" : stationary ? "<color=" + _stationaryColor + ">" : "";
				_firePoints.text += ballComponent.Name + " " + ballComponent.Points;
				_firePoints.text += (dead || warning || stationary) ? "</color>" : "";
				_firePoints.text += "\n";
			}
			else if (marble.name == "Player_water")
			{
				_waterPoints.text += dead ? "<color=" + _deadColor + ">" : warning ? "<color=" + _warningColor + ">" : stationary ? "<color=" + _stationaryColor + ">" : "";
				_waterPoints.text += ballComponent.Name + " " + ballComponent.Points;
				_waterPoints.text += (dead || warning || stationary) ? "</color>" : "";
				_waterPoints.text += "\n";
			}
			else if (marble.name == "Player_wind")
			{
				_airPoints.text += dead ? "<color=" + _deadColor + ">" : warning ? "<color=" + _warningColor + ">" : stationary ? "<color=" + _stationaryColor + ">" : "";
				_airPoints.text += ballComponent.Name + " " + ballComponent.Points;
				_airPoints.text += (dead || warning || stationary) ? "</color>" : "";
				_airPoints.text += "\n";
			}

			i++;
		}
	}

	private void UpdateVoteMeters()
    {
		_meterVotes.text = "";
		foreach(var direction in _inputVoteCollector.Choices.Keys)
        {
			_meterVotes.text += direction + " " + _inputVoteCollector.Choices[direction].tally + "\n";
		}
	}
}

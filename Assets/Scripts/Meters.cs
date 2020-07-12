using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Meters : MonoBehaviour
{
	[SerializeField] private GameManager _gameManager;
	private Text _meterPoints;
	private Text _meterVotes;
	private string _stationaryColor = "orange";
	private string _warningColor = "red";
	private string _deadColor = "black";
	private Color _normalColor = Color.blue;
    private InputVoteCollector _inputVoteCollector;
	
	void Start()
	{
		Debug.Log("Meters.Start() called");
		_meterPoints = GameObject.Find("MeterPoints").GetComponent<Text>();
		_meterPoints.color = _normalColor;
		_meterPoints.text = "Roll out";
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
//		var balls = GameObject.FindGameObjectsWithTag("Ball");
		
		_meterPoints.text = "";
		int i = 0;
		foreach (var marble in _gameManager.Marbles)
		{
			var ballComponent = marble.GetComponent<Ball>();

			var stationary = ballComponent.TimeToSelfDestruct <= ballComponent.SelfDestructSeconds - 1;
			var warning = ballComponent.TimeToSelfDestruct <= 1;
			var dead = !marble.activeSelf;
			_meterPoints.text += dead ? "<color=" + _deadColor + ">" : warning ? "<color=" + _warningColor + ">" : stationary ? "<color=" + _stationaryColor + ">" : "";
			_meterPoints.text += ballComponent.Name + " " + ballComponent.Points;
			_meterPoints.text += (dead || warning || stationary) ? "</color>" : "";
			_meterPoints.text += "\n";
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

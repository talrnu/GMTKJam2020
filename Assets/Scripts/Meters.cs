using System;
using UnityEngine;
using UnityEngine.UI;

public class Meters : MonoBehaviour
{
	private Text _meterPoints;
	private Text _meterVotes;
	private string _statonaryColor = "orange";
	private string _warningColor = "red";
	private Color _normalColor = Color.blue;
    private InputVoteCollector _inputVoteCollector;
	
	void Start()
	{
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
		var balls = GameObject.FindGameObjectsWithTag("Ball");
		
		_meterPoints.text = "";
		int i = 0;
		foreach (var ball in balls)
		{
			var ballComponent = ball.GetComponent<Ball>();
			var statonary = ballComponent.TimeToSelfDestruct <= ballComponent.SelfDestructSeconds - 1;
			var warning = ballComponent.TimeToSelfDestruct <= 1;
			_meterPoints.text += warning ? "<color=" + _warningColor + ">" : statonary ? "<color=" + _statonaryColor + ">" : "";
			_meterPoints.text += "Points[" + i + "]:" + ballComponent.Points;
			_meterPoints.text += (warning || statonary) ? "</color>" : "";
			i++;
		}
	}

	private void UpdateVoteMeters()
    {
		_meterVotes.text = "";
		foreach(var direction in _inputVoteCollector.Choices.Keys)
        {
//			_meterVotes.text += _inputVoteCollector.Choices[direction].display + " " + _inputVoteCollector.Choices[direction].tally;
			_meterVotes.text += direction + " " + _inputVoteCollector.Choices[direction].tally;
		}
	}
}

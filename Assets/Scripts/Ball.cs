using System;
using UnityEngine;

public class Ball : MonoBehaviour
{
	public bool OneSecondToSelfDestruct = false;
	private int selfDestructSeconds = 5;
	private int stationarySeconds = 0;
	private Vector3 _currentPosition;
	private Vector3 _previousPosition;
	bool timerStarted = false;
	
	void Start()
	{
		_currentPosition = this.GetComponent<Transform>().position;
		_previousPosition = _currentPosition;
		Debug.Log("-=-=- start");
		
		if (!timerStarted)
		{
			timerStarted = true;
			Invoke ( "_tick", 1f );
		}
	}

	private void Update()
	{
		_currentPosition = this.GetComponent<Transform>().position;

		if (_previousPosition != _currentPosition)
		{
			OneSecondToSelfDestruct = false;
			stationarySeconds = 0;
			Debug.Log("-=-=- .");
		}
		else if (stationarySeconds >= selfDestructSeconds - 1)
		{
			OneSecondToSelfDestruct = true;
			Debug.Log("-=-=- !!@");
		}
		else
		{
			Debug.Log("-=-=- !");
		}
		
		_previousPosition = _currentPosition;
		
		if (stationarySeconds >= selfDestructSeconds)
		{
			Debug.Log("-=-=- KABOOM!");
		}
	}
	
	private void _tick()
	{
		stationarySeconds++;
		Invoke ( "_tick", 1f );
	}
}

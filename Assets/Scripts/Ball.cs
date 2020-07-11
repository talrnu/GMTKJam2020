using System;
using UnityEngine;

public class Ball : MonoBehaviour
{
	[SerializeField] private GameManager _gameManager;

	public int Points = 0;
	public bool OneSecondToSelfDestruct = false;
	private int selfDestructSeconds = 5;
	private int stationarySeconds = 0;
	private float _epsilon = 0.1f;
	private Vector3 _currentPosition;
	private Vector3 _previousPosition;
	bool _timerStarted = false;

	
	void Start()
	{
		_currentPosition = this.GetComponent<Transform>().position;
		_previousPosition = _currentPosition;
		
		if (!_timerStarted)
		{
			_timerStarted = true;
			Invoke ( "_tick", 1f );
		}
	}

	private void Update()
	{
		_currentPosition = this.GetComponent<Transform>().position;
		float dist = Vector3.Distance(_previousPosition, _currentPosition);
		if (Vector3.Distance(_previousPosition, _currentPosition) > _epsilon)
		{
			OneSecondToSelfDestruct = false;
			stationarySeconds = 0;
		}
		else if (stationarySeconds >= selfDestructSeconds - 1)
		{
			OneSecondToSelfDestruct = true;
		}
		
		_previousPosition = _currentPosition;
		
		if (stationarySeconds >= selfDestructSeconds)
		{
			UnityEngine.Object.Destroy(this.gameObject);
			_gameManager.GameOver();
		}
	}
	
	private void _tick()
	{
		Points++;

		stationarySeconds++;
		Invoke ( "_tick", 1f );
	}
}

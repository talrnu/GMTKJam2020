using System;
using UnityEngine;

public class Ball : MonoBehaviour
{
	public float MovementEpsilon = 0.05f;
	public int SelfDestructSeconds = 5;
	
	[HideInInspector]
	public bool DestroyThisBall = false;
	[HideInInspector]
	public int Points = 0;
	[HideInInspector]
	public int TimeToSelfDestruct;

	private Vector3 _currentPosition;
	private Vector3 _previousPosition;
	private bool _timerStarted = false;

	void Start()
	{
		_currentPosition = this.GetComponent<Transform>().position;
		_previousPosition = _currentPosition;
		TimeToSelfDestruct = SelfDestructSeconds;
		
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
		if (Vector3.Distance(_previousPosition, _currentPosition) > MovementEpsilon)
		{
			TimeToSelfDestruct = SelfDestructSeconds;
		}
		
		_previousPosition = _currentPosition;
		
		if (TimeToSelfDestruct <= 0)
		{
			DestroyThisBall = true;
		}
	}
	
	private void _tick()
	{
		if (!DestroyThisBall)
		{
			Points++;
			TimeToSelfDestruct--;

			Invoke ( "_tick", 1f );
		}
	}
}

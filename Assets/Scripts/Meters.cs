using System;
using UnityEngine;
using UnityEngine.UI;

public class Meters : MonoBehaviour
{
	private int points = 0;
	bool timerStarted = false;
	private Text meterPoints;

	public int Points
	{ 
		get => points; 
		private set => points = value;
	}
	
	void Start()
	{
		meterPoints = GameObject.Find("MeterPoints").GetComponent<Text>();
		meterPoints.text = "Roll out";
		
		if (!timerStarted)
		{
			timerStarted = true;
			Invoke ( "_tick", 1f );
		}
	}

	void OnGUI ()
	{
		meterPoints.text = "Points: " + points;
	}
	
	private void _tick()
	{
		points++;
		Debug.Log("Points: " + points);
		Debug.Log("Points prop: " + Points);
		Invoke ( "_tick", 1f );
	}
}
